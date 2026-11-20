using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal static class DadosInicio
    {
        public static async Task<ResumoInicio> CarregarAsync()
        {
            using MySqlConnection conexao = ConexaoBD.CriarConexao();
            await conexao.OpenAsync();

            // Conta os atletas pelo cadastro, sem repetir quem pertence a duas equipes.
            const string consulta = @"SELECT
                (SELECT COUNT(*) FROM equipe) AS equipes,
                (SELECT COUNT(*) FROM atleta) AS atletas,
                (SELECT COUNT(*) FROM tatica) AS taticas,
                (SELECT COUNT(*) FROM quadro) AS quadros";

            ResumoInicio resumo = new ResumoInicio();
            using (MySqlCommand comando = new MySqlCommand(consulta, conexao))
            {
                comando.CommandTimeout = 5;
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
                ORDER BY t.id DESC
                LIMIT 3";

            using (MySqlCommand comando = new MySqlCommand(consultaTaticas, conexao))
            {
                comando.CommandTimeout = 5;
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
                ORDER BY e.nome, e.id
                LIMIT 3";

            using (MySqlCommand comando = new MySqlCommand(consultaEquipes, conexao))
            {
                comando.CommandTimeout = 5;
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
