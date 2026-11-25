using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using BCrypt.Net;
using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal static class DadosUsuario
    {
        private const int LimiteEmail = 254;
        private const int LimiteSenhaEmBytes = 72;
        // Mantém o custo da verificação também quando o e-mail não está cadastrado.
        private const string HashDeExemplo = "$2a$12$cUje9FWL5jmZceN2x1X.GuhtK39/IKvktskYQs/dYtNwyeULurrqS";
        // Regex verifica um formato de texto. Este padrão reconhece os hashes aceitos pelo bcrypt.
        private static readonly Regex FormatoBcrypt = new Regex(@"\A\$2[aby]\$(?:0[4-9]|[12][0-9]|3[01])\$[./A-Za-z0-9]{53}\z", RegexOptions.CultureInvariant);
        public static UsuarioAutenticado Cadastrar(string nome, string email, string senha)
        {
            nome = nome.Trim();
            email = email.Trim();
            if (nome.Length == 0 || Texto.ContarCaracteres(nome) > 100)
            {
                throw new ValidacaoException("Informe seu nome com até 100 caracteres.", campo: "nome");
            }

            if (email.Length > LimiteEmail || !MailAddress.TryCreate(email, out MailAddress? endereco) || endereco.Address != email)
            {
                throw new ValidacaoException("Informe um e-mail válido.", campo: "email");
            }

            if (string.IsNullOrWhiteSpace(senha) || Texto.ContarCaracteres(senha) < 8)
            {
                throw new ValidacaoException("Use uma senha com pelo menos 8 caracteres.", campo: "senha");
            }

            // O bcrypt usado no login aceita no máximo 72 bytes, inclusive em senhas com acentos.
            if (Encoding.UTF8.GetByteCount(senha) > LimiteSenhaEmBytes)
            {
                throw new ValidacaoException("A senha é muito longa. Use uma senha menor.", campo: "senha");
            }

            // O banco recebe um hash da senha. BCrypt permite conferir a senha sem guardar o texto original.
            string hash = BCrypt.Net.BCrypt.HashPassword(senha, workFactor: 12);
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                MySqlTransaction transacao = conexao.BeginTransaction();
                try
                {
                    const string sql = "INSERT INTO usuario (nome, email, senha_hash) VALUES (@nome, @email, @hash)";
                    MySqlCommand comando = new MySqlCommand(sql, conexao, transacao);
                    comando.CommandTimeout = 5;
                    try
                    {
                        comando.Parameters.Add("@nome", MySqlDbType.VarChar, 100).Value = nome;
                        comando.Parameters.Add("@email", MySqlDbType.VarChar, LimiteEmail).Value = email;
                        comando.Parameters.Add("@hash", MySqlDbType.VarChar, 255).Value = hash;
                        comando.ExecuteNonQuery();
                        int usuarioId = Convert.ToInt32(comando.LastInsertedId);
                        // A conta e suas preferências são gravadas juntas. As cores e a duração usam os padrões do banco.
                        MySqlCommand preferencias = new MySqlCommand("INSERT INTO preferencia_usuario (usuario_id) VALUES (@usuarioId)", conexao, transacao);
                        preferencias.CommandTimeout = 5;
                        try
                        {
                            preferencias.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                            preferencias.ExecuteNonQuery();
                            transacao.Commit();
                            return new UsuarioAutenticado(usuarioId, nome, email);
                        }
                        finally
                        {
                            preferencias.Dispose();
                        }
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
            catch (MySqlException erro) when (erro.Number == 1062)
            {
                throw new ValidacaoException("Já existe uma conta com este e-mail. Entre com sua senha ou use outro e-mail.", erro, campo: "email");
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("criar a conta", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        public static UsuarioAutenticado? Autenticar(string email, string senha)
        {
            email = email.Trim();
            if (email.Length == 0 || email.Length > LimiteEmail || string.IsNullOrEmpty(senha) || Encoding.UTF8.GetByteCount(senha) > LimiteSenhaEmBytes)
            {
                return null;
            }

            string hash;
            UsuarioAutenticado? usuario = ConsultarUsuario(email, out hash);
            bool formatoValido = FormatoBcrypt.IsMatch(hash);
            string hashParaVerificar;
            if (formatoValido)
            {
                hashParaVerificar = hash;
            }
            else
            {
                hashParaVerificar = HashDeExemplo;
            }

            bool senhaValida = VerificarSenha(senha, hashParaVerificar);
            if (formatoValido && senhaValida)
            {
                return usuario;
            }
            else
            {
                return null;
            }
        }

        private static UsuarioAutenticado? ConsultarUsuario(string email, out string hash)
        {
            // As aspas triplas permitem escrever o comando SQL em várias linhas.
            const string consulta = """
                SELECT id, nome, email, senha_hash
                FROM usuario
                WHERE email = @email
                LIMIT 1
                """;
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                MySqlCommand comando = new MySqlCommand(consulta, conexao);
                comando.CommandTimeout = 5;
                try
                {
                    comando.Parameters.Add("@email", MySqlDbType.VarChar, LimiteEmail).Value = email;
                    MySqlDataReader leitor = comando.ExecuteReader();
                    try
                    {
                        if (!leitor.Read())
                        {
                            hash = "";
                            return null;
                        }

                        hash = leitor.GetString(3);
                        return new UsuarioAutenticado(leitor.GetInt32(0), leitor.GetString(1), leitor.GetString(2));
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
            catch (Exception ex) when (ConexaoBD.EhFalhaDeBanco(ex))
            {
                throw new BancoDeDadosException("consultar o usuário para entrar na conta", ex);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        private static bool VerificarSenha(string senha, string hash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(senha, hash);
            }
            catch (Exception ex) when (ex is SaltParseException || ex is ArgumentException)
            {
                return false;
            }
        }
    }
}
