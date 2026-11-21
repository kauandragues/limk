using MySql.Data.MySqlClient;

using System.Globalization;

namespace Limk.VolleyBall
{
    internal static class ConexaoBD
    {
        // Retorna a conexão fechada para quem precisa abrir de forma assíncrona.
        public static MySqlConnection CriarConexao()
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

            return new MySqlConnection(configuracao.ConnectionString);
        }
    }
}
