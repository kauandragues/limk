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
            if (usuarioId <= 0)
            {
                throw new ArgumentOutOfRangeException("usuarioId");
            }

            if (equipeId <= 0)
            {
                throw new ArgumentOutOfRangeException("equipeId");
            }

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
                MySqlCommand comando = Comando(conexao, null, consulta);
                try
                {
                    comando.Parameters.AddWithValue("@usuarioId", usuarioId);
                    comando.Parameters.AddWithValue("@equipeId", equipeId);
                    MySqlDataReader leitor = comando.ExecuteReader();
                    try
                    {
                        if (!leitor.Read())
                        {
                            throw new UnauthorizedAccessException("Você não tem acesso à equipe desta tática.");
                        }

                        List<AtletaDaEquipe> elenco = new List<AtletaDaEquipe>();
                        do
                        {
                            if (!leitor.IsDBNull(0))
                            {
                                AtletaDaEquipe registroAtletaDaEquipe = new AtletaDaEquipe(leitor.GetInt32(0), leitor.GetString(1), leitor.GetInt32(2));
                                elenco.Add(registroAtletaDaEquipe);
                            }
                        }
                        while (leitor.Read());
                        return elenco;
                    }
                    finally
                    {
                        leitor.Dispose();
                    }
                }
                finally
                {
                    comando.Dispose();
                }
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("carregar o elenco da equipe", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        public static Tatica Carregar(int usuarioId, int taticaId)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentOutOfRangeException("usuarioId");
            }

            if (taticaId <= 0)
            {
                throw new ArgumentOutOfRangeException("taticaId");
            }

            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                // As consultas leem o mesmo retrato, mesmo que outra pessoa salve a tática.
                MySqlTransaction transacao = conexao.BeginTransaction(IsolationLevel.RepeatableRead);
                try
                {
                    const string raiz = @"SELECT tatica.id, tatica.equipe_id, tatica.nome, tatica.descricao,
                        tatica.tipo_tatica_id, tatica.sistema_jogo_id
                    FROM tatica
                    INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                    WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId";
                    Tatica tatica;
                    {
                        MySqlCommand comando = ConsultaTatica(conexao, transacao, raiz, usuarioId, taticaId);
                        try
                        {
                            MySqlDataReader leitor = comando.ExecuteReader();
                            try
                            {
                                if (!leitor.Read())
                                {
                                    throw new UnauthorizedAccessException("Tática não encontrada nas suas equipes.");
                                }

                                string descricao = "";
                                if (!leitor.IsDBNull(3))
                                {
                                    descricao = leitor.GetString(3);
                                }

                                int? tipoTaticaId = null;
                                if (!leitor.IsDBNull(4))
                                {
                                    tipoTaticaId = leitor.GetInt32(4);
                                }

                                int? sistemaJogoId = null;
                                if (!leitor.IsDBNull(5))
                                {
                                    sistemaJogoId = leitor.GetInt32(5);
                                }

                                tatica = new Tatica(leitor.GetInt32(0), leitor.GetInt32(1), leitor.GetString(2), descricao, tipoTaticaId, sistemaJogoId);
                            }
                            finally
                            {
                                leitor.Dispose();
                            }
                        }
                        finally
                        {
                            comando.Dispose();
                        }
                    }

                    // O dicionário permite encontrar cada fase pelo código que ela tem no banco.
                    Dictionary<int, QuadroTatica> quadros = new Dictionary<int, QuadroTatica>();
                    const string fases = @"SELECT quadro.id, quadro.nome, quadro.duracao_ms, quadro.rotacao
                    FROM quadro
                    INNER JOIN tatica ON tatica.id = quadro.tatica_id
                    INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                    WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId
                    ORDER BY quadro.ordem";
                    {
                        MySqlCommand comando = ConsultaTatica(conexao, transacao, fases, usuarioId, taticaId);
                        try
                        {
                            MySqlDataReader leitor = comando.ExecuteReader();
                            try
                            {
                                while (leitor.Read())
                                {
                                    int? rotacao = null;
                                    if (!leitor.IsDBNull(3))
                                    {
                                        rotacao = leitor.GetInt32(3);
                                    }

                                    QuadroTatica quadro = new QuadroTatica(leitor.GetString(1), leitor.GetInt32(2), rotacao);
                                    quadros.Add(leitor.GetInt32(0), quadro);
                                    tatica.GetQuadros().Add(quadro);
                                }
                            }
                            finally
                            {
                                leitor.Dispose();
                            }
                        }
                        finally
                        {
                            comando.Dispose();
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
                    {
                        MySqlCommand comando = ConsultaTatica(conexao, transacao, atletas, usuarioId, taticaId);
                        try
                        {
                            MySqlDataReader leitor = comando.ExecuteReader();
                            try
                            {
                                while (leitor.Read())
                                {
                                    int numero = leitor.GetInt32(3);
                                    int? equipeAtletaId = null;
                                    if (!leitor.IsDBNull(1))
                                    {
                                        equipeAtletaId = leitor.GetInt32(1);
                                    }

                                    string nome = $"Atleta {numero}";
                                    if (!leitor.IsDBNull(2))
                                    {
                                        nome = leitor.GetString(2);
                                    }

                                    LadoAtleta lado = LadoAtleta.Principal;
                                    if (leitor.GetString(4) == "ADVERSARIO")
                                    {
                                        lado = LadoAtleta.Adversario;
                                    }

                                    AtletaQuadro atletaDaFase = new AtletaQuadro(equipeAtletaId, nome, numero, lado, (float)leitor.GetDecimal(5), (float)leitor.GetDecimal(6));
                                    quadros[leitor.GetInt32(0)].GetAtletas().Add(atletaDaFase);
                                }
                            }
                            finally
                            {
                                leitor.Dispose();
                            }
                        }
                        finally
                        {
                            comando.Dispose();
                        }
                    }

                    const string bolas = @"SELECT bola_quadro.quadro_id, bola_quadro.posicao_x, bola_quadro.posicao_y
                    FROM bola_quadro
                    INNER JOIN quadro ON quadro.id = bola_quadro.quadro_id
                    INNER JOIN tatica ON tatica.id = quadro.tatica_id
                    INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                    WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId";
                    {
                        MySqlCommand comando = ConsultaTatica(conexao, transacao, bolas, usuarioId, taticaId);
                        try
                        {
                            MySqlDataReader leitor = comando.ExecuteReader();
                            try
                            {
                                while (leitor.Read())
                                {
                                    PosicaoBola bolaDaFase = new PosicaoBola((float)leitor.GetDecimal(1), (float)leitor.GetDecimal(2));
                                    quadros[leitor.GetInt32(0)].SetBola(bolaDaFase);
                                }
                            }
                            finally
                            {
                                leitor.Dispose();
                            }
                        }
                        finally
                        {
                            comando.Dispose();
                        }
                    }

                    transacao.Commit();
                    return tatica;
                }
                finally
                {
                    transacao.Dispose();
                }
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("carregar a tática", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        public static int Salvar(int usuarioId, Tatica tatica)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentOutOfRangeException("usuarioId");
            }

            if (tatica == null)
            {
                throw new ArgumentNullException("tatica");
            }

            tatica.Validar();
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                MySqlTransaction transacao = conexao.BeginTransaction();
                try
                {
                    VerificarEquipeEVinculos(conexao, transacao, usuarioId, tatica);
                    int taticaId = GravarCabecalho(conexao, transacao, tatica);
                    GravarFases(conexao, transacao, taticaId, tatica.GetQuadros());
                    transacao.Commit();
                    return taticaId;
                }
                finally
                {
                    transacao.Dispose();
                }
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("salvar a tática", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        private static void VerificarEquipeEVinculos(MySqlConnection conexao, MySqlTransaction transacao, int usuarioId, Tatica tatica)
        {
            if (!DadosEquipe.TemAcessoEquipe(conexao, transacao, usuarioId, tatica.GetEquipeId()))
            {
                throw new UnauthorizedAccessException("Você não tem acesso à equipe desta tática.");
            }

            if (tatica.GetId() > 0)
            {
                const string consulta = @"SELECT tatica.equipe_id FROM tatica
                    INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                    WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId";
                MySqlCommand comando = ConsultaTatica(conexao, transacao, consulta, usuarioId, tatica.GetId());
                try
                {
                    MySqlDataReader leitor = comando.ExecuteReader();
                    try
                    {
                        if (!leitor.Read())
                        {
                            throw new UnauthorizedAccessException("Tática não encontrada nas suas equipes.");
                        }

                        int equipeId = leitor.GetInt32(0);
                        if (equipeId != tatica.GetEquipeId())
                        {
                            throw new ValidacaoException("A equipe de uma tática salva não pode ser alterada.");
                        }
                    }
                    finally
                    {
                        leitor.Dispose();
                    }
                }
                finally
                {
                    comando.Dispose();
                }
            }

            List<int> vinculos = new List<int>();
            {
                MySqlCommand comando = Comando(conexao, transacao, "SELECT id FROM equipe_atleta WHERE equipe_id = @equipeId");
                try
                {
                    comando.Parameters.AddWithValue("@equipeId", tatica.GetEquipeId());
                    MySqlDataReader leitor = comando.ExecuteReader();
                    try
                    {
                        while (leitor.Read())
                        {
                            vinculos.Add(leitor.GetInt32(0));
                        }
                    }
                    finally
                    {
                        leitor.Dispose();
                    }
                }
                finally
                {
                    comando.Dispose();
                }
            }

            foreach (QuadroTatica quadro in tatica.GetQuadros())
            {
                foreach (AtletaQuadro atleta in quadro.GetAtletas())
                {
                    int? vinculo = atleta.GetEquipeAtletaId();
                    if (vinculo.HasValue && !vinculos.Contains(vinculo.Value))
                    {
                        throw new ValidacaoException("Um atleta vinculado não pertence à equipe da tática.");
                    }
                }
            }
        }

        private static int GravarCabecalho(MySqlConnection conexao, MySqlTransaction transacao, Tatica tatica)
        {
            string sql;
            if (tatica.GetId() == 0)
            {
                sql = """
                    INSERT INTO tatica (equipe_id, nome, descricao, tipo_tatica_id, sistema_jogo_id)
                    VALUES (@equipeId, @nome, @descricao, @tipo, @sistema)
                    """;
            }
            else
            {
                sql = """
                    UPDATE tatica
                    SET nome = @nome, descricao = @descricao, tipo_tatica_id = @tipo, sistema_jogo_id = @sistema
                    WHERE id = @id AND equipe_id = @equipeId
                    """;
            }

            {
                MySqlCommand comando = Comando(conexao, transacao, sql);
                try
                {
                    comando.Parameters.AddWithValue("@id", tatica.GetId());
                    comando.Parameters.AddWithValue("@equipeId", tatica.GetEquipeId());
                    comando.Parameters.AddWithValue("@nome", tatica.GetNome().Trim());
                    comando.Parameters.AddWithValue("@descricao", tatica.GetDescricao());
                    AdicionarNumeroOpcional(comando, "@tipo", tatica.GetTipoTaticaId());
                    AdicionarNumeroOpcional(comando, "@sistema", tatica.GetSistemaJogoId());
                    comando.ExecuteNonQuery();
                    if (tatica.GetId() == 0)
                    {
                        return Convert.ToInt32(comando.LastInsertedId);
                    }
                }
                finally
                {
                    comando.Dispose();
                }
            }

            return tatica.GetId();
        }

        private static void GravarFases(MySqlConnection conexao, MySqlTransaction transacao, int taticaId, List<QuadroTatica> fases)
        {
            // Substitui as fases como uma unidade. Uma falha desfaz também esta exclusão.
            {
                MySqlCommand comando = Comando(conexao, transacao, "DELETE FROM quadro WHERE tatica_id = @id");
                try
                {
                    comando.Parameters.AddWithValue("@id", taticaId);
                    comando.ExecuteNonQuery();
                }
                finally
                {
                    comando.Dispose();
                }
            }

            for (int i = 0; i < fases.Count; i++)
            {
                QuadroTatica quadro = fases[i];
                int quadroId;
                {
                    MySqlCommand comando = Comando(conexao, transacao, """
                        INSERT INTO quadro (tatica_id, nome, ordem, duracao_ms, rotacao)
                        VALUES (@taticaId, @nome, @ordem, @duracao, @rotacao)
                        """);
                    try
                    {
                        comando.Parameters.AddWithValue("@taticaId", taticaId);
                        comando.Parameters.AddWithValue("@nome", quadro.GetNome().Trim());
                        comando.Parameters.AddWithValue("@ordem", i + 1);
                        comando.Parameters.AddWithValue("@duracao", quadro.GetDuracaoMs());
                        AdicionarNumeroOpcional(comando, "@rotacao", quadro.GetRotacao());
                        comando.ExecuteNonQuery();
                        quadroId = Convert.ToInt32(comando.LastInsertedId);
                    }
                    finally
                    {
                        comando.Dispose();
                    }
                }

                foreach (AtletaQuadro atleta in quadro.GetAtletas())
                {
                    MySqlCommand comando = Comando(conexao, transacao, """
                            INSERT INTO atleta_quadro (quadro_id, equipe_atleta_id, numero, lado, posicao_x, posicao_y)
                            VALUES (@quadroId, @vinculo, @numero, @lado, @x, @y)
                            """);
                    try
                    {
                        comando.Parameters.AddWithValue("@quadroId", quadroId);
                        AdicionarNumeroOpcional(comando, "@vinculo", atleta.GetEquipeAtletaId());
                        comando.Parameters.AddWithValue("@numero", atleta.GetNumeroCamisa());
                        string lado = "PRINCIPAL";
                        if (atleta.GetLado() == LadoAtleta.Adversario)
                        {
                            lado = "ADVERSARIO";
                        }

                        comando.Parameters.AddWithValue("@lado", lado);
                        AdicionarCoordenadas(comando, atleta.GetX(), atleta.GetY());
                        comando.ExecuteNonQuery();
                    }
                    finally
                    {
                        comando.Dispose();
                    }
                }

                PosicaoBola? bola = quadro.GetBola();
                if (bola != null)
                {
                    MySqlCommand comando = Comando(conexao, transacao, "INSERT INTO bola_quadro (quadro_id, posicao_x, posicao_y) VALUES (@quadroId, @x, @y)");
                    try
                    {
                        comando.Parameters.AddWithValue("@quadroId", quadroId);
                        AdicionarCoordenadas(comando, bola.GetX(), bola.GetY());
                        comando.ExecuteNonQuery();
                    }
                    finally
                    {
                        comando.Dispose();
                    }
                }
            }
        }

        public static bool Excluir(int usuarioId, int taticaId)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentOutOfRangeException("usuarioId");
            }

            if (taticaId <= 0)
            {
                throw new ArgumentOutOfRangeException("taticaId");
            }

            const string sql = """
                DELETE tatica FROM tatica
                INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                WHERE tatica.id = @taticaId AND usuario_equipe.usuario_id = @usuarioId
                """;
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                MySqlCommand comando = ConsultaTatica(conexao, null, sql, usuarioId, taticaId);
                try
                {
                    return comando.ExecuteNonQuery() == 1;
                }
                finally
                {
                    comando.Dispose();
                }
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("excluir a tática", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        private static void AdicionarNumeroOpcional(MySqlCommand comando, string nome, int? valor)
        {
            // HasValue indica se o número foi informado. Value acessa o número quando ele existe.
            if (valor.HasValue)
            {
                comando.Parameters.AddWithValue(nome, valor.Value);
            }
            else
            {
                comando.Parameters.AddWithValue(nome, DBNull.Value);
            }
        }

        private static MySqlCommand Comando(MySqlConnection conexao, MySqlTransaction? transacao, string sql)
        {
            MySqlCommand comando = new MySqlCommand(sql, conexao, transacao);
            comando.CommandTimeout = 5;
            return comando;
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
