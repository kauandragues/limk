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
            if (usuarioId <= 0)
            {
                throw new ArgumentOutOfRangeException("usuarioId");
            }

            ArgumentOutOfRangeException.ThrowIfNegative(equipeId);
            if (nome == null)
            {
                throw new ArgumentNullException("nome");
            }

            nome = nome.Trim();
            if (nome.Length == 0 || Texto.ContarCaracteres(nome) > 100)
            {
                throw new ValidacaoException("Informe um nome de equipe com até 100 caracteres.");
            }

            if (escudo != null)
            {
                escudo = escudo.ToArray();
                Bitmap? imagem = ImagemEscudo.CriarImagem(escudo);
                try
                {
                    if (imagem == null)
                    {
                        throw new ValidacaoException("O escudo precisa ser uma imagem válida de até 4 MB e 4096 pixels por lado.");
                    }
                }
                finally
                {
                    if (imagem != null)
                    {
                        imagem.Dispose();
                    }
                }
            }

            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                MySqlTransaction transacao = conexao.BeginTransaction();
                try
                {
                    if (equipeId > 0)
                    {
                        VerificarAcesso(conexao, transacao, usuarioId, equipeId);
                    }

                    string sql;
                    if (equipeId == 0)
                    {
                        sql = "INSERT INTO equipe (nome, escudo) VALUES (@nome, @escudo)";
                    }
                    else
                    {
                        sql = "UPDATE equipe SET nome = @nome, escudo = @escudo WHERE id = @equipeId";
                    }

                    {
                        MySqlCommand comando = Comando(conexao, transacao, sql);
                        try
                        {
                            comando.Parameters.AddWithValue("@nome", nome);
                            comando.Parameters.AddWithValue("@equipeId", equipeId);
                            MySqlParameter parametroEscudo = comando.Parameters.Add("@escudo", MySqlDbType.MediumBlob);
                            if (escudo == null)
                            {
                                parametroEscudo.Value = DBNull.Value;
                            }
                            else
                            {
                                parametroEscudo.Value = escudo;
                            }

                            comando.ExecuteNonQuery();
                            if (equipeId == 0)
                            {
                                equipeId = Convert.ToInt32(comando.LastInsertedId);
                                MySqlCommand vinculo = Comando(conexao, transacao, "INSERT INTO usuario_equipe (usuario_id, equipe_id) VALUES (@usuarioId, @equipeId)");
                                try
                                {
                                    vinculo.Parameters.AddWithValue("@usuarioId", usuarioId);
                                    vinculo.Parameters.AddWithValue("@equipeId", equipeId);
                                    vinculo.ExecuteNonQuery();
                                }
                                finally
                                {
                                    vinculo.Dispose();
                                }
                            }
                        }
                        finally
                        {
                            comando.Dispose();
                        }
                    }

                    transacao.Commit();
                    return equipeId;
                }
                finally
                {
                    transacao.Dispose();
                }
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
            if (usuarioId <= 0)
            {
                throw new ArgumentOutOfRangeException("usuarioId");
            }

            if (equipeId <= 0)
            {
                throw new ArgumentOutOfRangeException("equipeId");
            }

            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                MySqlTransaction transacao = conexao.BeginTransaction();
                try
                {
                    VerificarAcesso(conexao, transacao, usuarioId, equipeId);
                    List<int> atletas = new List<int>();
                    {
                        MySqlCommand comando = Comando(conexao, transacao, @"SELECT atleta.id FROM atleta
                    INNER JOIN equipe_atleta ON equipe_atleta.atleta_id = atleta.id
                    WHERE equipe_atleta.equipe_id = @equipeId
                    ORDER BY atleta.id");
                        try
                        {
                            comando.Parameters.AddWithValue("@equipeId", equipeId);
                            MySqlDataReader leitor = comando.ExecuteReader();
                            try
                            {
                                while (leitor.Read())
                                {
                                    atletas.Add(leitor.GetInt32(0));
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

                    // As chaves estrangeiras removem os vínculos, táticas, quadros e seus elementos.
                    {
                        MySqlCommand comando = Comando(conexao, transacao, "DELETE FROM equipe WHERE id = @equipeId");
                        try
                        {
                            comando.Parameters.AddWithValue("@equipeId", equipeId);
                            comando.ExecuteNonQuery();
                        }
                        finally
                        {
                            comando.Dispose();
                        }
                    }

                    foreach (int atletaId in atletas)
                    {
                        // Um cadastro compartilhado continua disponível para as outras equipes.
                        MySqlCommand comando = Comando(conexao, transacao, @"DELETE atleta FROM atleta
                        LEFT JOIN equipe_atleta ON equipe_atleta.atleta_id = atleta.id
                        WHERE atleta.id = @atletaId AND equipe_atleta.id IS NULL");
                        try
                        {
                            comando.Parameters.AddWithValue("@atletaId", atletaId);
                            comando.ExecuteNonQuery();
                        }
                        finally
                        {
                            comando.Dispose();
                        }
                    }

                    transacao.Commit();
                }
                finally
                {
                    transacao.Dispose();
                }
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

        private static void VerificarAcesso(MySqlConnection conexao, MySqlTransaction transacao, int usuarioId, int equipeId)
        {
            if (!TemAcessoEquipe(conexao, transacao, usuarioId, equipeId))
            {
                throw new UnauthorizedAccessException("Equipe não encontrada nas suas equipes.");
            }
        }

        internal static bool TemAcessoEquipe(MySqlConnection conexao, MySqlTransaction transacao, int usuarioId, int equipeId)
        {
            MySqlCommand acesso = Comando(conexao, transacao, "SELECT equipe_id FROM usuario_equipe WHERE usuario_id = @usuarioId AND equipe_id = @equipeId");
            try
            {
                acesso.Parameters.Add("@equipeId", MySqlDbType.Int32).Value = equipeId;
                acesso.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                return acesso.ExecuteScalar() != null;
            }
            finally
            {
                acesso.Dispose();
            }
        }

        private static MySqlCommand Comando(MySqlConnection conexao, MySqlTransaction transacao, string sql)
        {
            MySqlCommand comando = new MySqlCommand(sql, conexao, transacao);
            comando.CommandTimeout = 5;
            return comando;
        }
    }
}
