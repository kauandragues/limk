using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal static class DadosPreferenciasUsuario
    {
        public static PreferenciasUsuario Carregar(int usuarioId)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentOutOfRangeException("usuarioId");
            }

            const string consulta = """
                SELECT cor_atleta_principal, cor_atleta_adversario, cor_bola, duracao_quadro_padrao_ms
                FROM preferencia_usuario
                WHERE usuario_id = @usuarioId
                """;
            PreferenciasUsuario padrao = PreferenciasUsuario.CriarPadrao();
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                MySqlCommand comando = new MySqlCommand(consulta, conexao);
                comando.CommandTimeout = 5;
                try
                {
                    comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                    MySqlDataReader leitor = comando.ExecuteReader();
                    try
                    {
                        if (!leitor.Read())
                        {
                            return padrao;
                        }

                        string corPrincipal = padrao.GetCorAtletaPrincipal();
                        if (!leitor.IsDBNull(0))
                        {
                            corPrincipal = LerCor(leitor.GetString(0), corPrincipal);
                        }

                        string corAdversario = padrao.GetCorAtletaAdversario();
                        if (!leitor.IsDBNull(1))
                        {
                            corAdversario = LerCor(leitor.GetString(1), corAdversario);
                        }

                        string corBola = padrao.GetCorBola();
                        if (!leitor.IsDBNull(2))
                        {
                            corBola = LerCor(leitor.GetString(2), corBola);
                        }

                        int duracao = padrao.GetDuracaoQuadroPadraoMs();
                        if (!leitor.IsDBNull(3) && leitor.GetInt32(3) > 0)
                        {
                            duracao = leitor.GetInt32(3);
                        }

                        return new PreferenciasUsuario(corPrincipal, corAdversario, corBola, duracao);
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
                throw new BancoDeDadosException("Não foi possível acessar as preferências.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        public static void Salvar(int usuarioId, PreferenciasUsuario preferencias)
        {
            if (usuarioId <= 0)
            {
                throw new ArgumentOutOfRangeException("usuarioId");
            }

            if (preferencias == null)
            {
                throw new ArgumentNullException("preferencias");
            }

            if (!CorValida(preferencias.GetCorAtletaPrincipal()) || !CorValida(preferencias.GetCorAtletaAdversario()) || !CorValida(preferencias.GetCorBola()) || preferencias.GetDuracaoQuadroPadraoMs() <= 0)
            {
                throw new ValidacaoException("Informe cores no formato #RRGGBB e uma duração positiva.");
            }

            const string consulta = """
                INSERT INTO preferencia_usuario (usuario_id, cor_atleta_principal, cor_atleta_adversario, cor_bola,
                    duracao_quadro_padrao_ms)
                VALUES (@usuarioId, @corAtletaPrincipal, @corAtletaAdversario, @corBola, @duracao)
                ON DUPLICATE KEY UPDATE
                    cor_atleta_principal = @corAtletaPrincipal,
                    cor_atleta_adversario = @corAtletaAdversario,
                    cor_bola = @corBola,
                    duracao_quadro_padrao_ms = @duracao
                """;
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                MySqlTransaction transacao = conexao.BeginTransaction();
                try
                {
                    MySqlCommand comando = new MySqlCommand(consulta, conexao, transacao);
                    comando.CommandTimeout = 5;
                    try
                    {
                        comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                        comando.Parameters.Add("@corAtletaPrincipal", MySqlDbType.VarChar, 7).Value = preferencias.GetCorAtletaPrincipal().ToUpperInvariant();
                        comando.Parameters.Add("@corAtletaAdversario", MySqlDbType.VarChar, 7).Value = preferencias.GetCorAtletaAdversario().ToUpperInvariant();
                        comando.Parameters.Add("@corBola", MySqlDbType.VarChar, 7).Value = preferencias.GetCorBola().ToUpperInvariant();
                        comando.Parameters.Add("@duracao", MySqlDbType.Int32).Value = preferencias.GetDuracaoQuadroPadraoMs();
                        comando.ExecuteNonQuery();
                        transacao.Commit();
                    }
                    finally
                    {
                        comando.Dispose();
                    }
                }
                finally
                {
                    transacao.Dispose();
                }
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível acessar as preferências.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        private static string LerCor(string cor, string padrao)
        {
            if (CorValida(cor))
            {
                return cor.ToUpperInvariant();
            }
            else
            {
                return padrao;
            }
        }

        private static bool CorValida(string cor)
        {
            if (string.IsNullOrEmpty(cor) || cor.Length != 7 || cor[0] != '#')
            {
                return false;
            }

            // Depois do #, cada caractere precisa ser um número de 0 a 9 ou uma letra de A a F.
            for (int indice = 1; indice < cor.Length; indice++)
            {
                if (!"0123456789ABCDEFabcdef".Contains(cor[indice]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
