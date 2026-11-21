using System.Net.Mail;
using System.Text;
using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal sealed class FalhaCadastroUsuarioException(Exception causa)
        : Exception("Não foi possível criar a conta no banco de dados.", causa)
    {
    }

    internal static class CadastroUsuario
    {
        public static async Task<UsuarioAutenticado> CadastrarAsync(
            string nome, string email, string senha, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            nome = nome.Trim();
            email = email.Trim();
            if (nome.Length == 0 || nome.EnumerateRunes().Count() > 100)
                throw new ArgumentException("Informe seu nome com até 100 caracteres.");
            if (email.Length > 254 || !MailAddress.TryCreate(email, out MailAddress? endereco) || endereco.Address != email)
                throw new ArgumentException("Informe um e-mail válido.");
            if (string.IsNullOrWhiteSpace(senha) || senha.EnumerateRunes().Count() < 8)
                throw new ArgumentException("Use uma senha com pelo menos 8 caracteres.");
            // O bcrypt usado no login aceita no máximo 72 bytes, inclusive em senhas com acentos.
            if (Encoding.UTF8.GetByteCount(senha) > 72)
                throw new ArgumentException("A senha é muito longa. Use uma senha menor.");

            string hash = await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(senha, workFactor: 12), cancellationToken)
                .ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                using MySqlConnection conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(cancellationToken).ConfigureAwait(false);
                using MySqlTransaction transacao = await conexao.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

                const string sql = "INSERT INTO usuario (nome, email, senha_hash) VALUES (@nome, @email, @hash)";
                using MySqlCommand comando = new(sql, conexao, transacao) { CommandTimeout = 5 };
                comando.Parameters.Add("@nome", MySqlDbType.VarChar, 100).Value = nome;
                comando.Parameters.Add("@email", MySqlDbType.VarChar, 254).Value = email;
                comando.Parameters.Add("@hash", MySqlDbType.VarChar, 255).Value = hash;
                await comando.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                int usuarioId = checked((int)comando.LastInsertedId);

                // A conta e suas preferências são gravadas juntas. As cores e a duração usam os padrões do banco.
                using MySqlCommand preferencias = new(
                    "INSERT INTO preferencia_usuario (usuario_id) VALUES (@usuarioId)", conexao, transacao)
                    { CommandTimeout = 5 };
                preferencias.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                await preferencias.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                await transacao.CommitAsync(CancellationToken.None).ConfigureAwait(false);
                return new UsuarioAutenticado(usuarioId, nome, email);
            }
            catch (MySqlException erro) when (erro.Number == 1062)
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw new ArgumentException("Já existe uma conta com este e-mail. Entre com sua senha ou use outro e-mail.");
            }
            catch (Exception erro) when (erro is MySqlException or InvalidOperationException or ArgumentException
                or FormatException or OverflowException or TimeoutException)
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw new FalhaCadastroUsuarioException(erro);
            }
        }
    }
}
