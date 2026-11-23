using MySql.Data.MySqlClient;
using System.Data;

namespace Limk.VolleyBall.Backend
{
    internal static class DadosPrancheta
    {
        public static DataTable Listar(int usuarioId)
        {
            const string consulta = @"SELECT tatica.id, tatica.equipe_id, tatica.nome, tatica.descricao,
                    equipe.nome AS equipe, tipo_tatica.nome AS tipo, sistema_jogo.nome AS sistema,
                    COUNT(quadro.id) AS quadros
                FROM tatica
                INNER JOIN equipe ON equipe.id = tatica.equipe_id
                INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                LEFT JOIN tipo_tatica ON tipo_tatica.id = tatica.tipo_tatica_id
                LEFT JOIN sistema_jogo ON sistema_jogo.id = tatica.sistema_jogo_id
                LEFT JOIN quadro ON quadro.tatica_id = tatica.id
                WHERE usuario_equipe.usuario_id = @usuarioId
                GROUP BY tatica.id, tatica.equipe_id, tatica.nome, tatica.descricao,
                    equipe.nome, tipo_tatica.nome, sistema_jogo.nome
                ORDER BY tatica.id DESC";
            return ConexaoBD.ConsultarTabela(consulta, usuarioId);
        }

        public static DataTable CarregarTipos()
        {
            return ConexaoBD.ConsultarTabela("SELECT id, nome FROM tipo_tatica ORDER BY nome");
        }

        public static DataTable CarregarSistemas()
        {
            return ConexaoBD.ConsultarTabela("SELECT id, nome FROM sistema_jogo ORDER BY nome");
        }

        public static List<AtletaDaEquipe> CarregarElenco(int usuarioId, int equipeId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(equipeId);
            const string consulta = @"SELECT equipe_atleta.id, atleta.nome, equipe_atleta.numero_camisa
                FROM usuario_equipe
                LEFT JOIN equipe_atleta ON equipe_atleta.equipe_id = usuario_equipe.equipe_id AND equipe_atleta.ativo = TRUE
                LEFT JOIN atleta ON atleta.id = equipe_atleta.atleta_id
                WHERE usuario_equipe.usuario_id = @usuarioId AND usuario_equipe.equipe_id = @equipeId
                ORDER BY equipe_atleta.numero_camisa, atleta.nome";
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                using MySqlCommand comando = Comando(conexao, null, consulta);
                comando.Parameters.AddWithValue("@usuarioId", usuarioId);
                comando.Parameters.AddWithValue("@equipeId", equipeId);
                using MySqlDataReader leitor = comando.ExecuteReader();
                if (!leitor.Read())
                    throw new UnauthorizedAccessException("Você não tem acesso à equipe desta tática.");

                List<AtletaDaEquipe> elenco = new List<AtletaDaEquipe>();
                do
                {
                    if (!leitor.IsDBNull(0))
                    {
                        AtletaDaEquipe registroAtletaDaEquipe = new AtletaDaEquipe();
                        registroAtletaDaEquipe.SetEquipeAtletaId(leitor.GetInt32(0));
                        registroAtletaDaEquipe.SetNome(leitor.GetString(1));
                        registroAtletaDaEquipe.SetNumeroCamisa(leitor.GetInt32(2));
                        elenco.Add(registroAtletaDaEquipe);
                    }
                } while (leitor.Read());
                return elenco;
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível acessar os dados da tática.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        public static Tatica Carregar(int usuarioId, int taticaId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(taticaId);
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                // As consultas leem o mesmo retrato, mesmo que outra pessoa salve a tática.
                using MySqlTransaction transacao = conexao.BeginTransaction(IsolationLevel.RepeatableRead);
                const string raiz = @"SELECT tatica.id, tatica.equipe_id, tatica.nome, tatica.descricao,
                        tatica.tipo_tatica_id, tatica.sistema_jogo_id
                    FROM tatica
                    INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                    WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId";
                Tatica tatica;
                using (MySqlCommand comando = ConsultaTatica(conexao, transacao, raiz, usuarioId, taticaId))
                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    if (!leitor.Read())
                        throw new UnauthorizedAccessException("Tática não encontrada nas suas equipes.");
                    tatica = new Tatica();
                    tatica.SetId(leitor.GetInt32(0));
                    tatica.SetEquipeId(leitor.GetInt32(1));
                    tatica.SetNome(leitor.GetString(2));
                    tatica.SetDescricao(leitor.IsDBNull(3) ? "" : leitor.GetString(3));
                    tatica.SetTipoTaticaId(leitor.IsDBNull(4) ? null : leitor.GetInt32(4));
                    tatica.SetSistemaJogoId(leitor.IsDBNull(5) ? null : leitor.GetInt32(5));
                }

                Dictionary<int, QuadroTatica> quadros = new Dictionary<int, QuadroTatica>();
                const string fases = @"SELECT quadro.id, quadro.nome, quadro.duracao_ms, quadro.rotacao
                    FROM quadro
                    INNER JOIN tatica ON tatica.id = quadro.tatica_id
                    INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                    WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId
                    ORDER BY quadro.ordem";
                using (MySqlCommand comando = ConsultaTatica(conexao, transacao, fases, usuarioId, taticaId))
                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        QuadroTatica quadro = new QuadroTatica();
                        quadro.SetNome(leitor.GetString(1));
                        quadro.SetDuracaoMs(leitor.GetInt32(2));
                        quadro.SetRotacao(leitor.IsDBNull(3) ? null : leitor.GetInt32(3));
                        quadros.Add(leitor.GetInt32(0), quadro);
                        tatica.GetQuadros().Add(quadro);
                    }
                }

                // Um vínculo de outra equipe não revela o cadastro de um atleta alheio.
                const string atletas = @"SELECT atleta_quadro.quadro_id, equipe_atleta.id, atleta.nome, atleta_quadro.numero,
                        atleta_quadro.lado, atleta_quadro.posicao_x, atleta_quadro.posicao_y
                    FROM atleta_quadro
                    INNER JOIN quadro ON quadro.id = atleta_quadro.quadro_id
                    INNER JOIN tatica ON tatica.id = quadro.tatica_id
                    INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                    LEFT JOIN equipe_atleta ON equipe_atleta.id = atleta_quadro.equipe_atleta_id AND equipe_atleta.equipe_id = tatica.equipe_id
                    LEFT JOIN atleta ON atleta.id = equipe_atleta.atleta_id
                    WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId
                    ORDER BY quadro.ordem, atleta_quadro.lado, atleta_quadro.numero";
                using (MySqlCommand comando = ConsultaTatica(conexao, transacao, atletas, usuarioId, taticaId))
                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        int numero = leitor.GetInt32(3);
                        AtletaQuadro atletaDaFase = new AtletaQuadro();
                        atletaDaFase.SetEquipeAtletaId(leitor.IsDBNull(1) ? null : leitor.GetInt32(1));
                        atletaDaFase.SetNome(leitor.IsDBNull(2) ? $"Atleta {numero}" : leitor.GetString(2));
                        atletaDaFase.SetNumeroCamisa(numero);
                        atletaDaFase.SetLado(leitor.GetString(4) == "ADVERSARIO" ? LadoAtleta.Adversario : LadoAtleta.Principal);
                        atletaDaFase.SetX((float)leitor.GetDecimal(5));
                        atletaDaFase.SetY((float)leitor.GetDecimal(6));
                        quadros[leitor.GetInt32(0)].GetAtletas().Add(atletaDaFase);
                    }
                }

                const string bolas = @"SELECT bola_quadro.quadro_id, bola_quadro.posicao_x, bola_quadro.posicao_y
                    FROM bola_quadro
                    INNER JOIN quadro ON quadro.id = bola_quadro.quadro_id
                    INNER JOIN tatica ON tatica.id = quadro.tatica_id
                    INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                    WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId";
                using (MySqlCommand comando = ConsultaTatica(conexao, transacao, bolas, usuarioId, taticaId))
                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        PosicaoBola bolaDaFase = new PosicaoBola();
                        bolaDaFase.SetX((float)leitor.GetDecimal(1));
                        bolaDaFase.SetY((float)leitor.GetDecimal(2));
                        quadros[leitor.GetInt32(0)].SetBola(bolaDaFase);
                    }
                }
                transacao.Commit();
                return tatica;
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível acessar os dados da tática.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        public static int Salvar(int usuarioId, Tatica tatica)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            ArgumentNullException.ThrowIfNull(tatica);
            tatica.Validar();
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                using MySqlTransaction transacao = conexao.BeginTransaction();
                VerificarEquipeEVinculos(conexao, transacao, usuarioId, tatica);
                int taticaId = GravarCabecalho(conexao, transacao, tatica);
                GravarFases(conexao, transacao, taticaId, tatica.GetQuadros());
                transacao.Commit();
                return taticaId;
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível acessar os dados da tática.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        private static void VerificarEquipeEVinculos(MySqlConnection conexao, MySqlTransaction transacao,
            int usuarioId, Tatica tatica)
        {
            if (!DadosEquipe.TemAcessoEquipe(conexao, transacao, usuarioId, tatica.GetEquipeId()))
                throw new UnauthorizedAccessException("Você não tem acesso à equipe desta tática.");
            if (tatica.GetId() > 0)
            {
                const string consulta = @"SELECT tatica.equipe_id FROM tatica
                    INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                    WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId";
                using MySqlCommand comando = ConsultaTatica(conexao, transacao, consulta, usuarioId, tatica.GetId());
                object? equipe = comando.ExecuteScalar();
                if (equipe == null)
                    throw new UnauthorizedAccessException("Tática não encontrada nas suas equipes.");
                if (Convert.ToInt32(equipe) != tatica.GetEquipeId())
                    throw new ValidacaoException("A equipe de uma tática salva não pode ser alterada.");
            }

            HashSet<int> vinculos = new HashSet<int>();
            using (MySqlCommand comando = Comando(conexao, transacao, "SELECT id FROM equipe_atleta WHERE equipe_id = @equipeId"))
            {
                comando.Parameters.AddWithValue("@equipeId", tatica.GetEquipeId());
                using MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                    vinculos.Add(leitor.GetInt32(0));
            }
            foreach (QuadroTatica quadro in tatica.GetQuadros())
            {
                foreach (AtletaQuadro atleta in quadro.GetAtletas())
                {
                    int? vinculo = atleta.GetEquipeAtletaId();
                    if (vinculo.HasValue && !vinculos.Contains(vinculo.Value))
                        throw new ValidacaoException("Um atleta vinculado não pertence à equipe da tática.");
                }
            }
        }

        private static int GravarCabecalho(MySqlConnection conexao, MySqlTransaction transacao, Tatica tatica)
        {
            string sql = tatica.GetId() == 0
                ? """
                    INSERT INTO tatica (equipe_id, nome, descricao, tipo_tatica_id, sistema_jogo_id)
                    VALUES (@equipeId, @nome, @descricao, @tipo, @sistema)
                    """
                : """
                    UPDATE tatica
                    SET nome = @nome, descricao = @descricao, tipo_tatica_id = @tipo, sistema_jogo_id = @sistema
                    WHERE id = @id AND equipe_id = @equipeId
                    """;
            using (MySqlCommand comando = Comando(conexao, transacao, sql))
            {
                comando.Parameters.AddWithValue("@id", tatica.GetId());
                comando.Parameters.AddWithValue("@equipeId", tatica.GetEquipeId());
                comando.Parameters.AddWithValue("@nome", tatica.GetNome().Trim());
                comando.Parameters.AddWithValue("@descricao", tatica.GetDescricao());
                comando.Parameters.AddWithValue("@tipo", (object?)tatica.GetTipoTaticaId() ?? DBNull.Value);
                comando.Parameters.AddWithValue("@sistema", (object?)tatica.GetSistemaJogoId() ?? DBNull.Value);
                comando.ExecuteNonQuery();
                if (tatica.GetId() == 0)
                    return checked((int)comando.LastInsertedId);
            }
            return tatica.GetId();
        }

        private static void GravarFases(MySqlConnection conexao, MySqlTransaction transacao,
            int taticaId, List<QuadroTatica> fases)
        {
            // Substitui as fases como uma unidade. Uma falha desfaz também esta exclusão.
            using (MySqlCommand comando = Comando(conexao, transacao, "DELETE FROM quadro WHERE tatica_id = @id"))
            {
                comando.Parameters.AddWithValue("@id", taticaId);
                comando.ExecuteNonQuery();
            }
            for (int i = 0; i < fases.Count; i++)
            {
                QuadroTatica quadro = fases[i];
                int quadroId;
                using (MySqlCommand comando = Comando(conexao, transacao,
                    """
                        INSERT INTO quadro (tatica_id, nome, ordem, duracao_ms, rotacao)
                        VALUES (@taticaId, @nome, @ordem, @duracao, @rotacao)
                        """))
                {
                    comando.Parameters.AddWithValue("@taticaId", taticaId);
                    comando.Parameters.AddWithValue("@nome", quadro.GetNome().Trim());
                    comando.Parameters.AddWithValue("@ordem", i + 1);
                    comando.Parameters.AddWithValue("@duracao", quadro.GetDuracaoMs());
                    comando.Parameters.AddWithValue("@rotacao", (object?)quadro.GetRotacao() ?? DBNull.Value);
                    comando.ExecuteNonQuery();
                    quadroId = checked((int)comando.LastInsertedId);
                }
                foreach (AtletaQuadro atleta in quadro.GetAtletas())
                {
                    using MySqlCommand comando = Comando(conexao, transacao,
                        """
                            INSERT INTO atleta_quadro (quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y)
                            VALUES (@quadroId, @vinculo, @numero, @lado, @x, @y)
                            """);
                    comando.Parameters.AddWithValue("@quadroId", quadroId);
                    comando.Parameters.AddWithValue("@vinculo", (object?)atleta.GetEquipeAtletaId() ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@numero", atleta.GetNumeroCamisa());
                    comando.Parameters.AddWithValue("@lado", atleta.GetLado() == LadoAtleta.Adversario ? "ADVERSARIO" : "PRINCIPAL");
                    AdicionarCoordenadas(comando, atleta.GetX(), atleta.GetY());
                    comando.ExecuteNonQuery();
                }
                PosicaoBola? bola = quadro.GetBola();
                if (bola != null)
                {
                    using MySqlCommand comando = Comando(conexao, transacao, "INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (@quadroId, @x, @y)");
                    comando.Parameters.AddWithValue("@quadroId", quadroId);
                    AdicionarCoordenadas(comando, bola.GetX(), bola.GetY());
                    comando.ExecuteNonQuery();
                }
            }
        }

        public static bool Excluir(int usuarioId, int taticaId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(taticaId);
            const string sql = """
                DELETE tatica FROM tatica
                INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId
                """;
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                using MySqlCommand comando = ConsultaTatica(conexao, null, sql, usuarioId, taticaId);
                return comando.ExecuteNonQuery() == 1;
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível acessar os dados da tática.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        private static MySqlCommand Comando(MySqlConnection conexao, MySqlTransaction? transacao, string sql)
        {
            return new MySqlCommand(sql, conexao, transacao) { CommandTimeout = 5 };
        }

        private static MySqlCommand ConsultaTatica(MySqlConnection conexao, MySqlTransaction? transacao, string sql, int usuarioId, int taticaId)
        {
            MySqlCommand comando = Comando(conexao, transacao, sql);
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
