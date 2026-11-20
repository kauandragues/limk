using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal static class DadosInicio
    {
        public static async Task<ResumoInicio> CarregarAsync(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentOutOfRangeException(nameof(usuarioId));

            using MySqlConnection conexao = ConexaoBD.CriarConexao();
            await conexao.OpenAsync();

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
            using (MySqlCommand comando = new MySqlCommand(consulta, conexao))
            {
                comando.CommandTimeout = 5;
                comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                using var leitor = await comando.ExecuteReaderAsync();
                await leitor.ReadAsync();
                resumo.Equipes = leitor.GetInt64(0);
                resumo.Atletas = leitor.GetInt64(1);
                resumo.Taticas = leitor.GetInt64(2);
                resumo.Quadros = leitor.GetInt64(3);
            }

            // Sem histórico de uso, as últimas cadastradas são ordenadas pelo ID.
            const string consultaTaticas = @"SELECT t.nome, t.descricao, e.nome AS equipe,
                    (SELECT COUNT(*) FROM quadro q WHERE q.tatica_id = t.id) AS quadros
                FROM tatica t
                INNER JOIN equipe e ON e.id = t.equipe_id
                INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
                WHERE ue.usuario_id = @usuarioId
                ORDER BY t.id DESC
                LIMIT 3";

            using (MySqlCommand comando = new MySqlCommand(consultaTaticas, conexao))
            {
                comando.CommandTimeout = 5;
                comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                using var leitor = await comando.ExecuteReaderAsync();
                while (await leitor.ReadAsync())
                {
                    resumo.UltimasTaticas.Add(new TaticaInicio
                    {
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

            using (MySqlCommand comando = new MySqlCommand(consultaEquipes, conexao))
            {
                comando.CommandTimeout = 5;
                comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                using var leitor = await comando.ExecuteReaderAsync();
                while (await leitor.ReadAsync())
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
        }
    }
}
