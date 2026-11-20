using MySql.Data.MySqlClient;

namespace Limk.VolleyBall
{
    internal class ConexaoBD
    {
        // Retorna a conexão fechada para quem precisa abrir de forma assíncrona.
        public static MySqlConnection CriarConexao()
        {
            string porta = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
            MySqlConnectionStringBuilder configuracao = new MySqlConnectionStringBuilder
            {
                Server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost",
                Port = uint.Parse(porta),
                Database = Environment.GetEnvironmentVariable("DB_NAME") ?? "",
                UserID = Environment.GetEnvironmentVariable("DB_USER") ?? "",
                Password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "",
                ConnectionTimeout = 5
            };

            return new MySqlConnection(configuracao.ConnectionString);
        }

        public static MySqlConnection ObterConexao()
        {
            MySqlConnection conexao = CriarConexao();
            try
            {
                conexao.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar ao MySQL: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conexao;
        }
    }
}
