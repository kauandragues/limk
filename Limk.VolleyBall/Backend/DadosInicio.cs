using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal static class DadosInicio
    {
        public static ResumoInicio Carregar(int usuarioId)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(usuarioId));
            }

            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                return ConsultarResumo(conexao, usuarioId);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        private static ResumoInicio ConsultarResumo(MySqlConnection conexao, int usuarioId)
        {
            // Conta os atletas pelo cadastro, sem repetir quem pertence a duas equipes.
            const string consulta = @"SELECT
                COUNT(DISTINCT usuario_equipe.equipe_id) AS equipes,
                COUNT(DISTINCT equipe_atleta.atleta_id) AS atletas,
                COUNT(DISTINCT tatica.id) AS taticas,
                COUNT(DISTINCT quadro.id) AS quadros
                FROM usuario_equipe
                LEFT JOIN equipe_atleta ON equipe_atleta.equipe_id = usuario_equipe.equipe_id AND equipe_atleta.ativo = TRUE
                LEFT JOIN tatica ON tatica.equipe_id = usuario_equipe.equipe_id
                LEFT JOIN quadro ON quadro.tatica_id = tatica.id
                WHERE usuario_equipe.usuario_id = @usuarioId";
            ResumoInicio resumo;
            {
                MySqlCommand comando = CriarComando(conexao, usuarioId, consulta);
                try
                {
                    MySqlDataReader leitor = comando.ExecuteReader();
                    try
                    {
                        leitor.Read();
                        resumo = new ResumoInicio(leitor.GetInt64(0), leitor.GetInt64(1), leitor.GetInt64(2), leitor.GetInt64(3));
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

            // Sem histórico de uso, as últimas cadastradas são ordenadas pelo ID.
            const string consultaTaticas = @"SELECT tatica.nome, tatica.descricao, equipe.nome AS equipe,
                    COUNT(quadro.id) AS quadros, tatica.id
                FROM tatica
                INNER JOIN equipe ON equipe.id = tatica.equipe_id
                INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                LEFT JOIN quadro ON quadro.tatica_id = tatica.id
                WHERE usuario_equipe.usuario_id = @usuarioId
                GROUP BY tatica.id, tatica.nome, tatica.descricao, equipe.nome
                ORDER BY tatica.id DESC
                LIMIT 3";
            {
                MySqlCommand comando = CriarComando(conexao, usuarioId, consultaTaticas);
                try
                {
                    MySqlDataReader leitor = comando.ExecuteReader();
                    try
                    {
                        while (leitor.Read())
                        {
                            string descricao = "";
                            if (!leitor.IsDBNull(1))
                            {
                                descricao = leitor.GetString(1);
                            }

                            TaticaInicio registroTaticaInicio = new TaticaInicio(leitor.GetInt32(4), leitor.GetString(0), descricao, leitor.GetString(2), leitor.GetInt64(3));
                            resumo.GetUltimasTaticas().Add(registroTaticaInicio);
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

            const string consultaEquipes = @"SELECT equipe.nome,
                    COUNT(DISTINCT equipe_atleta.id) AS atletas,
                    COUNT(DISTINCT tatica.id) AS taticas
                FROM equipe
                INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = equipe.id
                LEFT JOIN equipe_atleta ON equipe_atleta.equipe_id = equipe.id AND equipe_atleta.ativo = TRUE
                LEFT JOIN tatica ON tatica.equipe_id = equipe.id
                WHERE usuario_equipe.usuario_id = @usuarioId
                GROUP BY equipe.id, equipe.nome
                ORDER BY equipe.nome, equipe.id
                LIMIT 3";
            {
                MySqlCommand comando = CriarComando(conexao, usuarioId, consultaEquipes);
                try
                {
                    MySqlDataReader leitor = comando.ExecuteReader();
                    try
                    {
                        while (leitor.Read())
                        {
                            EquipeInicio registroEquipeInicio = new EquipeInicio(leitor.GetString(0), leitor.GetInt64(1), leitor.GetInt64(2));
                            resumo.GetEquipesCadastradas().Add(registroEquipeInicio);
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

            return resumo;
        }

        private static MySqlCommand CriarComando(MySqlConnection conexao, int usuarioId, string sql)
        {
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.CommandTimeout = 5;
            comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
            return comando;
        }
    }
}
