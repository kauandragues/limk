using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace Limk.VolleyBall.Backend
{
    internal sealed class FalhaPranchetaException(Exception causa)
        : Exception("Não foi possível carregar ou salvar a tática no banco de dados.", causa) { }

    internal static class DadosPrancheta
    {
        public static Task<DataTable> CarregarTiposAsync(CancellationToken token = default) =>
            CarregarReferenciaAsync("SELECT id, nome FROM tipo_tatica ORDER BY nome", token);

        public static Task<DataTable> CarregarSistemasAsync(CancellationToken token = default) =>
            CarregarReferenciaAsync("SELECT id, nome FROM sistema_jogo ORDER BY nome", token);

        private static async Task<DataTable> CarregarReferenciaAsync(string consulta, CancellationToken token)
        {
            try
            {
                using var conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(token).ConfigureAwait(false);
                using var comando = Comando(conexao, null, consulta);
                using var leitor = await comando.ExecuteReaderAsync(token).ConfigureAwait(false);
                DataTable tabela = new DataTable();
                tabela.Load(leitor);
                return tabela;
            }
            catch (MySqlException erro) { throw new FalhaPranchetaException(erro); }
        }

        public static async Task<List<AtletaDaEquipe>> CarregarElencoAsync(int usuarioId, int equipeId, CancellationToken token = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(equipeId);
            const string consulta = @"SELECT ea.id, a.nome, ea.numero_camisa
                FROM equipe_atleta ea
                INNER JOIN atleta a ON a.id = ea.atleta_id
                INNER JOIN usuario_equipe ue ON ue.equipe_id = ea.equipe_id
                WHERE ue.usuario_id = @usuarioId AND ea.equipe_id = @equipeId AND ea.ativo = TRUE
                ORDER BY ea.numero_camisa, a.nome";
            try
            {
                using var conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(token).ConfigureAwait(false);
                using var comando = Comando(conexao, null, consulta);
                comando.Parameters.AddWithValue("@usuarioId", usuarioId);
                comando.Parameters.AddWithValue("@equipeId", equipeId);
                using var leitor = await comando.ExecuteReaderAsync(token).ConfigureAwait(false);
                var elenco = new List<AtletaDaEquipe>();
                while (await leitor.ReadAsync(token).ConfigureAwait(false))
                    elenco.Add(new AtletaDaEquipe { EquipeAtletaId = leitor.GetInt32(0), Nome = leitor.GetString(1), NumeroCamisa = leitor.GetInt32(2) });
                return elenco;
            }
            catch (MySqlException erro) { throw new FalhaPranchetaException(erro); }
        }

        public static async Task<Tatica> CarregarAsync(int usuarioId, int taticaId, CancellationToken token = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(taticaId);
            try
            {
                using var conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(token).ConfigureAwait(false);
                // As consultas leem o mesmo retrato, mesmo que outra pessoa salve a tática.
                using var transacao = conexao.BeginTransaction(IsolationLevel.RepeatableRead);
                const string raiz = @"SELECT t.id, t.equipe_id, t.nome, t.descricao, t.tipo_tatica_id, t.sistema_jogo_id
                    FROM tatica t INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
                    WHERE t.id = @taticaId AND ue.usuario_id = @usuarioId";
                Tatica tatica;
                using (var comando = ConsultaTatica(conexao, transacao, raiz, usuarioId, taticaId))
                using (var leitor = await comando.ExecuteReaderAsync(token).ConfigureAwait(false))
                {
                    if (!await leitor.ReadAsync(token).ConfigureAwait(false))
                        throw new UnauthorizedAccessException("Tática não encontrada nas suas equipes.");
                    tatica = new Tatica
                    {
                        Id = leitor.GetInt32(0), EquipeId = leitor.GetInt32(1), Nome = leitor.GetString(2),
                        Descricao = leitor.IsDBNull(3) ? "" : leitor.GetString(3),
                        TipoTaticaId = leitor.IsDBNull(4) ? null : leitor.GetInt32(4),
                        SistemaJogoId = leitor.IsDBNull(5) ? null : leitor.GetInt32(5)
                    };
                }

                var quadros = new Dictionary<int, QuadroTatica>();
                const string fases = @"SELECT q.id, q.nome, q.duracao_ms, q.rotacao
                    FROM quadro q INNER JOIN tatica t ON t.id = q.tatica_id
                    INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
                    WHERE t.id = @taticaId AND ue.usuario_id = @usuarioId ORDER BY q.ordem";
                using (var comando = ConsultaTatica(conexao, transacao, fases, usuarioId, taticaId))
                using (var leitor = await comando.ExecuteReaderAsync(token).ConfigureAwait(false))
                {
                    while (await leitor.ReadAsync(token).ConfigureAwait(false))
                    {
                        var quadro = new QuadroTatica
                        {
                            Nome = leitor.GetString(1), DuracaoMs = leitor.GetInt32(2),
                            Rotacao = leitor.IsDBNull(3) ? null : leitor.GetInt32(3)
                        };
                        quadros.Add(leitor.GetInt32(0), quadro);
                        tatica.Quadros.Add(quadro);
                    }
                }

                // Um vínculo de outra equipe não revela o cadastro de um atleta alheio.
                const string atletas = @"SELECT aq.quadro_id, ea.id, a.nome, aq.numero, aq.lado, aq.posicao_x, aq.posicao_y
                    FROM atleta_quadro aq INNER JOIN quadro q ON q.id = aq.quadro_id
                    INNER JOIN tatica t ON t.id = q.tatica_id
                    INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
                    LEFT JOIN equipe_atleta ea ON ea.id = aq.equipe_atleta_id AND ea.equipe_id = t.equipe_id
                    LEFT JOIN atleta a ON a.id = ea.atleta_id
                    WHERE t.id = @taticaId AND ue.usuario_id = @usuarioId ORDER BY q.ordem, aq.lado, aq.numero";
                using (var comando = ConsultaTatica(conexao, transacao, atletas, usuarioId, taticaId))
                using (var leitor = await comando.ExecuteReaderAsync(token).ConfigureAwait(false))
                {
                    while (await leitor.ReadAsync(token).ConfigureAwait(false))
                    {
                        int numero = leitor.GetInt32(3);
                        quadros[leitor.GetInt32(0)].Atletas.Add(new AtletaQuadro
                        {
                            EquipeAtletaId = leitor.IsDBNull(1) ? null : leitor.GetInt32(1),
                            Nome = leitor.IsDBNull(2) ? $"Atleta {numero}" : leitor.GetString(2),
                            NumeroCamisa = numero,
                            Lado = leitor.GetString(4) == "ADVERSARIO" ? LadoAtleta.Adversario : LadoAtleta.Principal,
                            X = (float)leitor.GetDecimal(5), Y = (float)leitor.GetDecimal(6)
                        });
                    }
                }

                const string bolas = @"SELECT b.quadro_id, b.posicao_x, b.posicao_y
                    FROM bola_quadro b INNER JOIN quadro q ON q.id = b.quadro_id
                    INNER JOIN tatica t ON t.id = q.tatica_id
                    INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
                    WHERE t.id = @taticaId AND ue.usuario_id = @usuarioId";
                using (var comando = ConsultaTatica(conexao, transacao, bolas, usuarioId, taticaId))
                using (var leitor = await comando.ExecuteReaderAsync(token).ConfigureAwait(false))
                {
                    while (await leitor.ReadAsync(token).ConfigureAwait(false))
                        quadros[leitor.GetInt32(0)].Bola = new PosicaoBola { X = (float)leitor.GetDecimal(1), Y = (float)leitor.GetDecimal(2) };
                }
                transacao.Commit();
                return tatica;
            }
            catch (MySqlException erro) { throw new FalhaPranchetaException(erro); }
        }

        public static async Task<int> SalvarAsync(int usuarioId, Tatica original, CancellationToken token = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            Tatica tatica = original.Copiar();
            Validar(tatica);
            try
            {
                using var conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(token).ConfigureAwait(false);
                using var transacao = conexao.BeginTransaction();
                // Bloqueia o vínculo enquanto grava: a autorização faz parte da transação.
                using (var comando = Comando(conexao, transacao,
                    "SELECT equipe_id FROM usuario_equipe WHERE usuario_id=@usuarioId AND equipe_id=@equipeId FOR UPDATE"))
                {
                    comando.Parameters.AddWithValue("@usuarioId", usuarioId);
                    comando.Parameters.AddWithValue("@equipeId", tatica.EquipeId);
                    if (await comando.ExecuteScalarAsync(token).ConfigureAwait(false) == null)
                        throw new UnauthorizedAccessException("Você não tem acesso à equipe desta tática.");
                }
                if (tatica.Id > 0)
                {
                    const string consulta = @"SELECT t.equipe_id FROM tatica t
                        INNER JOIN usuario_equipe ue ON ue.equipe_id=t.equipe_id
                        WHERE t.id=@taticaId AND ue.usuario_id=@usuarioId FOR UPDATE";
                    using var comando = ConsultaTatica(conexao, transacao, consulta, usuarioId, tatica.Id);
                    object? equipe = await comando.ExecuteScalarAsync(token).ConfigureAwait(false);
                    if (equipe == null)
                        throw new UnauthorizedAccessException("Tática não encontrada nas suas equipes.");
                    if (Convert.ToInt32(equipe) != tatica.EquipeId)
                        throw new ArgumentException("A equipe de uma tática salva não pode ser alterada.");
                }

                var vinculos = new HashSet<int>();
                using (var comando = Comando(conexao, transacao, "SELECT id FROM equipe_atleta WHERE equipe_id=@equipeId FOR UPDATE"))
                {
                    comando.Parameters.AddWithValue("@equipeId", tatica.EquipeId);
                    using var leitor = await comando.ExecuteReaderAsync(token).ConfigureAwait(false);
                    while (await leitor.ReadAsync(token).ConfigureAwait(false))
                        vinculos.Add(leitor.GetInt32(0));
                }
                foreach (var quadro in tatica.Quadros)
                    foreach (var atleta in quadro.Atletas)
                        if (atleta.EquipeAtletaId.HasValue && !vinculos.Contains(atleta.EquipeAtletaId.Value))
                            throw new ArgumentException("Um atleta vinculado não pertence à equipe da tática.");

                string sql = tatica.Id == 0
                    ? "INSERT INTO tatica (equipe_id,nome,descricao,tipo_tatica_id,sistema_jogo_id) VALUES (@equipeId,@nome,@descricao,@tipo,@sistema)"
                    : "UPDATE tatica SET nome=@nome,descricao=@descricao,tipo_tatica_id=@tipo,sistema_jogo_id=@sistema WHERE id=@id AND equipe_id=@equipeId";
                using (var comando = Comando(conexao, transacao, sql))
                {
                    comando.Parameters.AddWithValue("@id", tatica.Id);
                    comando.Parameters.AddWithValue("@equipeId", tatica.EquipeId);
                    comando.Parameters.AddWithValue("@nome", tatica.Nome.Trim());
                    comando.Parameters.AddWithValue("@descricao", tatica.Descricao);
                    comando.Parameters.AddWithValue("@tipo", (object?)tatica.TipoTaticaId ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@sistema", (object?)tatica.SistemaJogoId ?? DBNull.Value);
                    await comando.ExecuteNonQueryAsync(token).ConfigureAwait(false);
                    if (tatica.Id == 0)
                        tatica.Id = checked((int)comando.LastInsertedId);
                }

                // Substitui as fases como uma unidade. Uma falha desfaz também esta exclusão.
                using (var comando = Comando(conexao, transacao, "DELETE FROM quadro WHERE tatica_id=@id"))
                {
                    comando.Parameters.AddWithValue("@id", tatica.Id);
                    await comando.ExecuteNonQueryAsync(token).ConfigureAwait(false);
                }
                for (int i = 0; i < tatica.Quadros.Count; i++)
                {
                    QuadroTatica quadro = tatica.Quadros[i];
                    int quadroId;
                    using (var comando = Comando(conexao, transacao,
                        "INSERT INTO quadro (tatica_id,nome,ordem,duracao_ms,rotacao) VALUES (@taticaId,@nome,@ordem,@duracao,@rotacao)"))
                    {
                        comando.Parameters.AddWithValue("@taticaId", tatica.Id);
                        comando.Parameters.AddWithValue("@nome", quadro.Nome.Trim());
                        comando.Parameters.AddWithValue("@ordem", i + 1);
                        comando.Parameters.AddWithValue("@duracao", quadro.DuracaoMs);
                        comando.Parameters.AddWithValue("@rotacao", (object?)quadro.Rotacao ?? DBNull.Value);
                        await comando.ExecuteNonQueryAsync(token).ConfigureAwait(false);
                        quadroId = checked((int)comando.LastInsertedId);
                    }
                    foreach (AtletaQuadro atleta in quadro.Atletas)
                    {
                        using var comando = Comando(conexao, transacao,
                            "INSERT INTO atleta_quadro (quadro_id,equipe_atleta_id,numero,lado,posicao_x,posicao_y) VALUES (@quadroId,@vinculo,@numero,@lado,@x,@y)");
                        comando.Parameters.AddWithValue("@quadroId", quadroId);
                        comando.Parameters.AddWithValue("@vinculo", (object?)atleta.EquipeAtletaId ?? DBNull.Value);
                        comando.Parameters.AddWithValue("@numero", atleta.NumeroCamisa);
                        comando.Parameters.AddWithValue("@lado", atleta.Lado == LadoAtleta.Adversario ? "ADVERSARIO" : "PRINCIPAL");
                        AdicionarCoordenadas(comando, atleta.X, atleta.Y);
                        await comando.ExecuteNonQueryAsync(token).ConfigureAwait(false);
                    }
                    if (quadro.Bola != null)
                    {
                        using var comando = Comando(conexao, transacao, "INSERT INTO bola_quadro (quadro_id,posicao_x,posicao_y) VALUES (@quadroId,@x,@y)");
                        comando.Parameters.AddWithValue("@quadroId", quadroId);
                        AdicionarCoordenadas(comando, quadro.Bola.X, quadro.Bola.Y);
                        await comando.ExecuteNonQueryAsync(token).ConfigureAwait(false);
                    }
                }
                token.ThrowIfCancellationRequested();
                transacao.Commit();
                return tatica.Id;
            }
            catch (MySqlException erro) { throw new FalhaPranchetaException(erro); }
        }

        internal static void Validar(Tatica tatica)
        {
            if (tatica.Id < 0 || tatica.EquipeId <= 0)
                throw new ArgumentException("Selecione uma equipe para a tática.");
            if (string.IsNullOrWhiteSpace(tatica.Nome) || tatica.Nome.Trim().Length > 100)
                throw new ArgumentException("Informe um nome de tática com até 100 caracteres.");
            if (Encoding.UTF8.GetByteCount(tatica.Descricao) > 65535)
                throw new ArgumentException("A descrição da tática está muito longa.");
            if (tatica.TipoTaticaId <= 0 || tatica.SistemaJogoId <= 0)
                throw new ArgumentException("Selecione um tipo e um sistema válidos, ou deixe-os sem definição.");
            if (tatica.Quadros.Count == 0)
                throw new ArgumentException("A tática precisa ter pelo menos uma fase.");
            foreach (QuadroTatica quadro in tatica.Quadros)
            {
                if (string.IsNullOrWhiteSpace(quadro.Nome) || quadro.Nome.Trim().Length > 100)
                    throw new ArgumentException("Cada fase precisa de um nome com até 100 caracteres.");
                if (quadro.DuracaoMs <= 0 || (quadro.Rotacao.HasValue && (quadro.Rotacao < 1 || quadro.Rotacao > 6)))
                    throw new ArgumentException("Informe uma duração positiva e uma rotação entre P1 e P6, ou sem rotação.");
                var numeros = new HashSet<(LadoAtleta, int)>();
                var vinculos = new HashSet<int>();
                foreach (AtletaQuadro atleta in quadro.Atletas)
                {
                    if (atleta.NumeroCamisa < 1 || atleta.NumeroCamisa > 99 ||
                        (atleta.Lado != LadoAtleta.Principal && atleta.Lado != LadoAtleta.Adversario))
                        throw new ArgumentException("Os atletas precisam de números entre 1 e 99 e um lado válido.");
                    if (!numeros.Add((atleta.Lado, atleta.NumeroCamisa)))
                        throw new ArgumentException("Dois atletas do mesmo lado não podem ter o mesmo número na mesma fase.");
                    if (atleta.EquipeAtletaId.HasValue && !vinculos.Add(atleta.EquipeAtletaId.Value))
                        throw new ArgumentException("Um atleta cadastrado só pode aparecer uma vez em cada fase.");
                    ValidarCoordenadas(atleta.X, atleta.Y);
                }
                if (quadro.Bola != null)
                    ValidarCoordenadas(quadro.Bola.X, quadro.Bola.Y);
            }
        }

        private static void ValidarCoordenadas(float x, float y)
        {
            if (!float.IsFinite(x) || !float.IsFinite(y) || x < 0 || x > 1 || y < 0 || y > 1)
                throw new ArgumentException("As posições dos elementos precisam permanecer dentro da quadra.");
        }

        private static MySqlCommand Comando(MySqlConnection conexao, MySqlTransaction? transacao, string sql) =>
            new MySqlCommand(sql, conexao, transacao) { CommandTimeout = 5 };

        private static MySqlCommand ConsultaTatica(MySqlConnection conexao, MySqlTransaction transacao, string sql, int usuarioId, int taticaId)
        {
            var comando = Comando(conexao, transacao, sql);
            comando.Parameters.AddWithValue("@usuarioId", usuarioId);
            comando.Parameters.AddWithValue("@taticaId", taticaId);
            return comando;
        }

        private static void AdicionarCoordenadas(MySqlCommand comando, float x, float y)
        {
            comando.Parameters.Add("@x", MySqlDbType.Decimal).Value = (decimal)x;
            comando.Parameters.Add("@y", MySqlDbType.Decimal).Value = (decimal)y;
        }
    }
}
