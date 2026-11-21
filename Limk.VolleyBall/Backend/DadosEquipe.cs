using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal sealed class FalhaEquipeException(Exception causa)
        : Exception("Não foi possível gravar as alterações da equipe no banco de dados.", causa) { }

    internal static class DadosEquipe
    {
        public static async Task<int> SalvarAsync(int usuarioId, int equipeId, string nome, byte[]? escudo,
            CancellationToken token = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            ArgumentOutOfRangeException.ThrowIfNegative(equipeId);
            nome = nome.Trim();
            if (nome.Length == 0 || nome.Length > 100)
                throw new ArgumentException("Informe um nome de equipe com até 100 caracteres.");
            if (escudo != null)
            {
                escudo = escudo.ToArray();
                using Bitmap? imagem = ImagemEscudo.CriarImagem(escudo);
                if (imagem == null)
                    throw new ArgumentException("O escudo precisa ser uma imagem válida de até 4 MB e 4096 pixels por lado.");
            }

            try
            {
                using var conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(token).ConfigureAwait(false);
                using var transacao = conexao.BeginTransaction();
                if (equipeId > 0)
                    await VerificarAcessoAsync(conexao, transacao, usuarioId, equipeId, token).ConfigureAwait(false);

                string sql = equipeId == 0
                    ? "INSERT INTO equipe (nome,escudo) VALUES (@nome,@escudo)"
                    : "UPDATE equipe SET nome=@nome,escudo=@escudo WHERE id=@equipeId";
                using (var comando = Comando(conexao, transacao, sql))
                {
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@equipeId", equipeId);
                    comando.Parameters.Add("@escudo", MySqlDbType.MediumBlob).Value = (object?)escudo ?? DBNull.Value;
                    await comando.ExecuteNonQueryAsync(token).ConfigureAwait(false);
                    if (equipeId == 0)
                    {
                        equipeId = checked((int)comando.LastInsertedId);
                        using var vinculo = Comando(conexao, transacao,
                            "INSERT INTO usuario_equipe (usuario_id,equipe_id) VALUES (@usuarioId,@equipeId)");
                        vinculo.Parameters.AddWithValue("@usuarioId", usuarioId);
                        vinculo.Parameters.AddWithValue("@equipeId", equipeId);
                        await vinculo.ExecuteNonQueryAsync(token).ConfigureAwait(false);
                    }
                }
                token.ThrowIfCancellationRequested();
                transacao.Commit();
                return equipeId;
            }
            catch (MySqlException erro) { throw new FalhaEquipeException(erro); }
        }

        public static async Task ExcluirAsync(int usuarioId, int equipeId, CancellationToken token = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(equipeId);
            try
            {
                using var conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(token).ConfigureAwait(false);
                using var transacao = conexao.BeginTransaction();
                await VerificarAcessoAsync(conexao, transacao, usuarioId, equipeId, token).ConfigureAwait(false);

                var atletas = new List<int>();
                using (var comando = Comando(conexao, transacao, @"SELECT a.id FROM atleta a
                    INNER JOIN equipe_atleta ea ON ea.atleta_id=a.id
                    WHERE ea.equipe_id=@equipeId ORDER BY a.id FOR UPDATE"))
                {
                    comando.Parameters.AddWithValue("@equipeId", equipeId);
                    using var leitor = await comando.ExecuteReaderAsync(token).ConfigureAwait(false);
                    while (await leitor.ReadAsync(token).ConfigureAwait(false))
                        atletas.Add(leitor.GetInt32(0));
                }
                // As chaves estrangeiras removem os vínculos, táticas, quadros e seus elementos.
                using (var comando = Comando(conexao, transacao, "DELETE FROM equipe WHERE id=@equipeId"))
                {
                    comando.Parameters.AddWithValue("@equipeId", equipeId);
                    await comando.ExecuteNonQueryAsync(token).ConfigureAwait(false);
                }
                foreach (int atletaId in atletas)
                {
                    // Um cadastro compartilhado continua disponível para as outras equipes.
                    using var comando = Comando(conexao, transacao, @"DELETE FROM atleta WHERE id=@atletaId
                        AND NOT EXISTS (SELECT 1 FROM equipe_atleta WHERE atleta_id=@atletaId)");
                    comando.Parameters.AddWithValue("@atletaId", atletaId);
                    await comando.ExecuteNonQueryAsync(token).ConfigureAwait(false);
                }
                token.ThrowIfCancellationRequested();
                transacao.Commit();
            }
            catch (MySqlException erro) { throw new FalhaEquipeException(erro); }
        }

        private static async Task VerificarAcessoAsync(MySqlConnection conexao, MySqlTransaction transacao,
            int usuarioId, int equipeId, CancellationToken token)
        {
            using var comando = Comando(conexao, transacao, @"SELECT e.id FROM equipe e
                INNER JOIN usuario_equipe ue ON ue.equipe_id=e.id
                WHERE e.id=@equipeId AND ue.usuario_id=@usuarioId FOR UPDATE");
            comando.Parameters.AddWithValue("@equipeId", equipeId);
            comando.Parameters.AddWithValue("@usuarioId", usuarioId);
            if (await comando.ExecuteScalarAsync(token).ConfigureAwait(false) == null)
                throw new UnauthorizedAccessException("Equipe não encontrada nas suas equipes.");
        }

        private static MySqlCommand Comando(MySqlConnection conexao, MySqlTransaction transacao, string sql) =>
            new MySqlCommand(sql, conexao, transacao) { CommandTimeout = 5 };
    }
}
