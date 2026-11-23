using Limk.VolleyBall.Backend;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace Limk.VolleyBall
{
    internal static class ConexaoBD
    {
        public static void Abrir(MySqlConnection conexao)
        {
            string? portaConfigurada = Environment.GetEnvironmentVariable("DB_PORT");
            ushort porta = 3306;
            if (!string.IsNullOrWhiteSpace(portaConfigurada) &&
                (!ushort.TryParse(portaConfigurada, NumberStyles.Integer, CultureInfo.InvariantCulture, out porta) || porta == 0))
            {
                throw new FormatException("DB_PORT deve conter uma porta entre 1 e 65535.");
            }

            string? servidor = Environment.GetEnvironmentVariable("DB_SERVER");
            MySqlConnectionStringBuilder configuracao = new MySqlConnectionStringBuilder
            {
                Server = string.IsNullOrWhiteSpace(servidor) ? "localhost" : servidor.Trim(),
                Port = porta,
                Database = Environment.GetEnvironmentVariable("DB_NAME") ?? "",
                UserID = Environment.GetEnvironmentVariable("DB_USER") ?? "",
                Password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "",
                ConnectionTimeout = 5
            };

            conexao.ConnectionString = configuracao.ConnectionString;
            conexao.Open();
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
                using MySqlCommand comando = new MySqlCommand(sql, conexao) { CommandTimeout = 5 };
                if (usuarioId.HasValue)
                    comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId.Value;
                using MySqlDataReader leitor = comando.ExecuteReader();
                DataTable tabela = new DataTable();
                tabela.Load(leitor);
                return tabela;
            }
            catch (Exception erro) when (EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível consultar os dados. Verifique a conexão.", erro);
            }
            finally
            {
                Fechar(conexao);
            }
        }

        public static bool EhFalhaDeBanco(Exception erro)
        {
            return erro is MySqlException || erro is InvalidOperationException || erro is ArgumentException
                || erro is FormatException || erro is OverflowException || erro is TimeoutException;
        }
    }
}
