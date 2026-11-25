using Limk.VolleyBall.Backend;
using MySql.Data.MySqlClient;
using System.Data;

namespace Limk.VolleyBall
{
    internal static class ConexaoBD
    {
        public static void Abrir(MySqlConnection conexao)
        {
            string portaConfigurada = LerConfiguracao("DB_PORT");
            int porta = 3306;
            if (!string.IsNullOrWhiteSpace(portaConfigurada))
            {
                // TryParse tenta transformar o texto em número e informa se a conversão funcionou.
                bool portaValida = int.TryParse(portaConfigurada, out porta);
                if (!portaValida || porta < 1 || porta > 65535)
                {
                    throw new FormatException("DB_PORT deve conter uma porta entre 1 e 65535.");
                }
            }

            string servidor = LerConfiguracao("DB_SERVER");
            if (string.IsNullOrWhiteSpace(servidor))
            {
                servidor = "localhost";
            }
            else
            {
                servidor = servidor.Trim();
            }

            MySqlConnectionStringBuilder configuracao = new MySqlConnectionStringBuilder();
            configuracao.Server = servidor;
            configuracao.Port = (uint)porta;
            configuracao.Database = LerConfiguracao("DB_NAME");
            configuracao.UserID = LerConfiguracao("DB_USER");
            configuracao.Password = LerConfiguracao("DB_PASSWORD");
            configuracao.ConnectionTimeout = 5;
            conexao.ConnectionString = configuracao.ConnectionString;
            conexao.Open();
        }

        private static string LerConfiguracao(string nome)
        {
            // As variáveis de ambiente guardam as configurações carregadas do arquivo .env.
            string? valor = Environment.GetEnvironmentVariable(nome);
            if (valor == null)
            {
                return "";
            }

            return valor;
        }

        public static void Fechar(MySqlConnection conexao)
        {
            // Dispose fecha a conexão e libera os recursos usados por ela.
            conexao.Dispose();
        }

        public static DataTable ConsultarTabela(string sql, int? usuarioId = null)
        {
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                Abrir(conexao);
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.CommandTimeout = 5;
                try
                {
                    if (usuarioId.HasValue)
                    {
                        // O parâmetro envia o valor separadamente, sem misturá-lo ao texto do SQL.
                        comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId.Value;
                    }

                    MySqlDataReader leitor = comando.ExecuteReader();
                    try
                    {
                        DataTable tabela = new DataTable();
                        tabela.Load(leitor);
                        return tabela;
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
            // when limita este catch aos tipos de erro relacionados ao acesso ao banco.
            catch (Exception erro) when (EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("consultar os dados", erro);
            }
            finally
            {
                // finally executa mesmo quando ocorre um erro ou um return dentro do try.
                Fechar(conexao);
            }
        }

        public static bool EhFalhaDeBanco(Exception erro)
        {
            return erro is MySqlException || erro is InvalidOperationException || erro is ArgumentException || erro is FormatException || erro is OverflowException || erro is TimeoutException;
        }
    }
}
