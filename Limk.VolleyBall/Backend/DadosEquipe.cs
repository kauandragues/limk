using System.Data;
using MySql.Data.MySqlClient;
using System.Text;

namespace Limk.VolleyBall.Backend
{
    internal static class DadosEquipe
    {
        public static DataTable Listar(int usuarioId)
        {
            const string consulta = @"SELECT equipe.id, equipe.nome, equipe.escudo,
                    COUNT(DISTINCT equipe_atleta.id) AS atletas,
                    COUNT(DISTINCT tatica.id) AS taticas
                FROM equipe
                INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = equipe.id
                LEFT JOIN equipe_atleta ON equipe_atleta.equipe_id = equipe.id AND equipe_atleta.ativo = TRUE
                LEFT JOIN tatica ON tatica.equipe_id = equipe.id
                WHERE usuario_equipe.usuario_id = @usuarioId
                GROUP BY equipe.id, equipe.nome, equipe.escudo
                ORDER BY equipe.nome, equipe.id";
            return ConexaoBD.ConsultarTabela(consulta, usuarioId);
        }

        public static int Salvar(int usuarioId, int equipeId, string nome, byte[]? escudo)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            ArgumentOutOfRangeException.ThrowIfNegative(equipeId);
            ArgumentNullException.ThrowIfNull(nome);
            nome = nome.Trim();
            if (nome.Length == 0 || nome.EnumerateRunes().Count() > 100)
                throw new ValidacaoException("Informe um nome de equipe com até 100 caracteres.");
            if (escudo != null)
            {
                escudo = escudo.ToArray();
                using Bitmap? imagem = ImagemEscudo.CriarImagem(escudo);
                if (imagem == null)
                    throw new ValidacaoException("O escudo precisa ser uma imagem válida de até 4 MB e 4096 pixels por lado.");
            }

            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                using MySqlTransaction transacao = conexao.BeginTransaction();
                if (equipeId > 0)
                    VerificarAcesso(conexao, transacao, usuarioId, equipeId);

                string sql = equipeId == 0
                    ? "INSERT INTO equipe (nome, escudo) VALUES (@nome, @escudo)"
                    : "UPDATE equipe SET nome = @nome, escudo = @escudo WHERE id = @equipeId";
                using (MySqlCommand comando = Comando(conexao, transacao, sql))
                {
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@equipeId", equipeId);
                    comando.Parameters.Add("@escudo", MySqlDbType.MediumBlob).Value = (object?)escudo ?? DBNull.Value;
                    comando.ExecuteNonQuery();
                    if (equipeId == 0)
                    {
                        equipeId = checked((int)comando.LastInsertedId);
                        using MySqlCommand vinculo = Comando(conexao, transacao,
                            "INSERT INTO usuario_equipe (usuario_id, equipe_id) VALUES (@usuarioId, @equipeId)");
                        vinculo.Parameters.AddWithValue("@usuarioId", usuarioId);
                        vinculo.Parameters.AddWithValue("@equipeId", equipeId);
                        vinculo.ExecuteNonQuery();
                    }
                }
                transacao.Commit();
                return equipeId;
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível gravar a equipe no banco.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        public static void Excluir(int usuarioId, int equipeId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(equipeId);
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                using MySqlTransaction transacao = conexao.BeginTransaction();
                VerificarAcesso(conexao, transacao, usuarioId, equipeId);

                List<int> atletas = new List<int>();
                using (MySqlCommand comando = Comando(conexao, transacao, @"SELECT atleta.id FROM atleta
                    INNER JOIN equipe_atleta ON equipe_atleta.atleta_id = atleta.id
                    WHERE equipe_atleta.equipe_id = @equipeId
                    ORDER BY atleta.id"))
                {
                    comando.Parameters.AddWithValue("@equipeId", equipeId);
                    using MySqlDataReader leitor = comando.ExecuteReader();
                    while (leitor.Read())
                        atletas.Add(leitor.GetInt32(0));
                }
                // As chaves estrangeiras removem os vínculos, táticas, quadros e seus elementos.
                using (MySqlCommand comando = Comando(conexao, transacao, "DELETE FROM equipe WHERE id = @equipeId"))
                {
                    comando.Parameters.AddWithValue("@equipeId", equipeId);
                    comando.ExecuteNonQuery();
                }
                foreach (int atletaId in atletas)
                {
                    // Um cadastro compartilhado continua disponível para as outras equipes.
                    using MySqlCommand comando = Comando(conexao, transacao, @"DELETE atleta FROM atleta
                        LEFT JOIN equipe_atleta ON equipe_atleta.atleta_id = atleta.id
                        WHERE atleta.id = @atletaId AND equipe_atleta.id IS NULL");
                    comando.Parameters.AddWithValue("@atletaId", atletaId);
                    comando.ExecuteNonQuery();
                }
                transacao.Commit();
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível gravar a equipe no banco.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        private static void VerificarAcesso(MySqlConnection conexao, MySqlTransaction transacao,
            int usuarioId, int equipeId)
        {
            if (!TemAcessoEquipe(conexao, transacao, usuarioId, equipeId))
                throw new UnauthorizedAccessException("Equipe não encontrada nas suas equipes.");
        }

        internal static bool TemAcessoEquipe(MySqlConnection conexao, MySqlTransaction transacao,
            int usuarioId, int equipeId)
        {
            using MySqlCommand acesso = Comando(conexao, transacao,
                "SELECT equipe_id FROM usuario_equipe WHERE usuario_id = @usuarioId AND equipe_id = @equipeId");
            acesso.Parameters.Add("@equipeId", MySqlDbType.Int32).Value = equipeId;
            acesso.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
            return acesso.ExecuteScalar() != null;
        }

        private static MySqlCommand Comando(MySqlConnection conexao, MySqlTransaction transacao, string sql)
        {
            return new MySqlCommand(sql, conexao, transacao) { CommandTimeout = 5 };
        }
    }
}
