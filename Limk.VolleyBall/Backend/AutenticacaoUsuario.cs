using System.Text;
using System.Text.RegularExpressions;
using BCrypt.Net;
using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    public sealed record UsuarioAutenticado(int Id, string Nome, string Email);

    internal sealed class FalhaAutenticacaoException(Exception causa)
        : Exception("Não foi possível consultar o banco de dados para entrar.", causa)
    {
    }

    internal static class AutenticacaoUsuario
    {
        private const int LimiteEmail = 254;
        private const int LimiteSenhaEmBytes = 72;

        // Mantém o custo da verificação também quando o e-mail não está cadastrado.
        private const string HashDeExemplo = "$2a$12$cUje9FWL5jmZceN2x1X.GuhtK39/IKvktskYQs/dYtNwyeULurrqS";
        private static readonly Regex FormatoBcrypt = new(
            @"\A\$2[aby]\$(?:0[4-9]|[12][0-9]|3[01])\$[./A-Za-z0-9]{53}\z",
            RegexOptions.CultureInvariant);

        public static async Task<UsuarioAutenticado?> AutenticarAsync(
            string email, string senha, CancellationToken cancellationToken = default)
        {
            email = email.Trim();
            if (email.Length is 0 or > LimiteEmail || string.IsNullOrEmpty(senha) ||
                Encoding.UTF8.GetByteCount(senha) > LimiteSenhaEmBytes)
            {
                return null;
            }

            (UsuarioAutenticado? usuario, string? hash) =
                await ConsultarUsuarioAsync(email, cancellationToken).ConfigureAwait(false);

            bool formatoValido = hash is not null && FormatoBcrypt.IsMatch(hash);
            string hashParaVerificar = formatoValido ? hash! : HashDeExemplo;
            bool senhaValida = await Task.Run(() => VerificarSenha(senha, hashParaVerificar), cancellationToken)
                .ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            return formatoValido && senhaValida ? usuario : null;
        }

        private static async Task<(UsuarioAutenticado? Usuario, string? Hash)> ConsultarUsuarioAsync(
            string email, CancellationToken cancellationToken)
        {
            const string consulta = """
                SELECT id, nome, email, senha_hash
                FROM usuario
                WHERE email = @email
                LIMIT 1
                """;

            try
            {
                using MySqlConnection conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(cancellationToken).ConfigureAwait(false);
                using MySqlCommand comando = new(consulta, conexao) { CommandTimeout = 5 };
                comando.Parameters.Add("@email", MySqlDbType.VarChar, LimiteEmail).Value = email;
                using var leitor = await comando.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

                if (!await leitor.ReadAsync(cancellationToken).ConfigureAwait(false))
                {
                    return (null, null);
                }

                return (new UsuarioAutenticado(leitor.GetInt32(0), leitor.GetString(1), leitor.GetString(2)),
                    leitor.GetString(3));
            }
            catch (Exception ex) when (ex is MySqlException or InvalidOperationException or ArgumentException
                or FormatException or OverflowException or TimeoutException)
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw new FalhaAutenticacaoException(ex);
            }
        }

        private static bool VerificarSenha(string senha, string hash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(senha, hash);
            }
            catch (Exception ex) when (ex is SaltParseException or ArgumentException)
            {
                return false;
            }
        }
    }
}
