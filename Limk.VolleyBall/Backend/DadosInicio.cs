using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal static class DadosInicio
    {
        public static async Task<ResumoInicio> CarregarAsync(int usuarioId, CancellationToken token = default)
        {
            if (usuarioId <= 0)
                throw new ArgumentOutOfRangeException(nameof(usuarioId));

            using MySqlConnection conexao = ConexaoBD.CriarConexao();
            await conexao.OpenAsync(token).ConfigureAwait(false);

            // Conta os atletas pelo cadastro, sem repetir quem pertence a duas equipes.
            const string consulta = @"SELECT
                (SELECT COUNT(*) FROM usuario_equipe ue
                    WHERE ue.usuario_id = @usuarioId) AS equipes,
                (SELECT COUNT(DISTINCT ea.atleta_id) FROM equipe_atleta ea
                    INNER JOIN usuario_equipe ue ON ue.equipe_id = ea.equipe_id
                    WHERE ue.usuario_id = @usuarioId AND ea.ativo = TRUE) AS atletas,
                (SELECT COUNT(*) FROM tatica t
                    INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
                    WHERE ue.usuario_id = @usuarioId) AS taticas,
                (SELECT COUNT(*) FROM quadro q
                    INNER JOIN tatica t ON t.id = q.tatica_id
                    INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
                    WHERE ue.usuario_id = @usuarioId) AS quadros";

            ResumoInicio resumo = new ResumoInicio();
            using (MySqlCommand comando = CriarComando(consulta))
            {
                using var leitor = await comando.ExecuteReaderAsync(token).ConfigureAwait(false);
                await leitor.ReadAsync(token).ConfigureAwait(false);
                resumo.Equipes = leitor.GetInt64(0);
                resumo.Atletas = leitor.GetInt64(1);
                resumo.Taticas = leitor.GetInt64(2);
                resumo.Quadros = leitor.GetInt64(3);
            }

            // Sem histórico de uso, as últimas cadastradas são ordenadas pelo ID.
            const string consultaTaticas = @"SELECT t.nome, t.descricao, e.nome AS equipe,
                    (SELECT COUNT(*) FROM quadro q WHERE q.tatica_id = t.id) AS quadros, t.id
                FROM tatica t
                INNER JOIN equipe e ON e.id = t.equipe_id
                INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
                WHERE ue.usuario_id = @usuarioId
                ORDER BY t.id DESC
                LIMIT 3";

            using (MySqlCommand comando = CriarComando(consultaTaticas))
            {
                using var leitor = await comando.ExecuteReaderAsync(token).ConfigureAwait(false);
                while (await leitor.ReadAsync(token).ConfigureAwait(false))
                {
                    resumo.UltimasTaticas.Add(new TaticaInicio
                    {
                        Id = leitor.GetInt32(4),
                        Nome = leitor.GetString(0),
                        Descricao = leitor.IsDBNull(1) ? "" : leitor.GetString(1),
                        Equipe = leitor.GetString(2),
                        Quadros = leitor.GetInt64(3)
                    });
                }
            }

            const string consultaEquipes = @"SELECT e.nome,
                    (SELECT COUNT(*) FROM equipe_atleta ea
                        WHERE ea.equipe_id = e.id AND ea.ativo = TRUE) AS atletas,
                    (SELECT COUNT(*) FROM tatica t WHERE t.equipe_id = e.id) AS taticas
                FROM equipe e
                INNER JOIN usuario_equipe ue ON ue.equipe_id = e.id
                WHERE ue.usuario_id = @usuarioId
                ORDER BY e.nome, e.id
                LIMIT 3";

            using (MySqlCommand comando = CriarComando(consultaEquipes))
            {
                using var leitor = await comando.ExecuteReaderAsync(token).ConfigureAwait(false);
                while (await leitor.ReadAsync(token).ConfigureAwait(false))
                {
                    resumo.EquipesCadastradas.Add(new EquipeInicio
                    {
                        Nome = leitor.GetString(0),
                        Atletas = leitor.GetInt64(1),
                        Taticas = leitor.GetInt64(2)
                    });
                }
            }

            return resumo;

            MySqlCommand CriarComando(string sql)
            {
                var comando = new MySqlCommand(sql, conexao) { CommandTimeout = 5 };
                comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                return comando;
            }
        }
    }
}
