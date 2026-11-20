using MySql.Data.MySqlClient;
using System.Data;

namespace Limk.VolleyBall.Backend
{
    internal static class DadosUsuario
    {
        public static Task<DataTable> CarregarEquipesAsync(int usuarioId)
        {
            const string consulta = @"SELECT e.id, e.nome, e.escudo,
                    (SELECT COUNT(*) FROM equipe_atleta ea
                        WHERE ea.equipe_id = e.id AND ea.ativo = TRUE) AS atletas,
                    (SELECT COUNT(*) FROM tatica t WHERE t.equipe_id = e.id) AS taticas
                FROM equipe e
                INNER JOIN usuario_equipe ue ON ue.equipe_id = e.id
                WHERE ue.usuario_id = @usuarioId
                ORDER BY e.nome, e.id";
            return ConsultarAsync(consulta, usuarioId);
        }

        public static Task<DataTable> CarregarAtletasAsync(int usuarioId)
        {
            // O vínculo também é filtrado: um atleta pode atuar em duas equipes.
            const string consulta = @"SELECT a.id, ea.equipe_id, ea.numero_camisa,
                    a.nome, a.apelido, a.data_nascimento, e.nome AS equipe, ea.titular,
                    (SELECT GROUP_CONCAT(DISTINCT p.nome ORDER BY p.nome SEPARATOR ', ')
                        FROM atleta_posicao ap INNER JOIN posicao p ON p.id = ap.posicao_id
                        WHERE ap.atleta_id = a.id) AS posicoes
                FROM atleta a
                INNER JOIN equipe_atleta ea ON ea.atleta_id = a.id AND ea.ativo = TRUE
                INNER JOIN equipe e ON e.id = ea.equipe_id
                INNER JOIN usuario_equipe ue ON ue.equipe_id = e.id
                WHERE ue.usuario_id = @usuarioId
                ORDER BY a.nome, e.nome";
            return ConsultarAsync(consulta, usuarioId);
        }

        public static Task<DataTable> CarregarTaticasAsync(int usuarioId)
        {
            const string consulta = @"SELECT t.id, t.equipe_id, t.nome, t.descricao,
                    e.nome AS equipe, tt.nome AS tipo, sj.nome AS sistema,
                    (SELECT COUNT(*) FROM quadro q WHERE q.tatica_id = t.id) AS quadros
                FROM tatica t
                INNER JOIN equipe e ON e.id = t.equipe_id
                INNER JOIN usuario_equipe ue ON ue.equipe_id = t.equipe_id
                LEFT JOIN tipo_tatica tt ON tt.id = t.tipo_tatica_id
                LEFT JOIN sistema_jogo sj ON sj.id = t.sistema_jogo_id
                WHERE ue.usuario_id = @usuarioId
                ORDER BY t.id DESC";
            return ConsultarAsync(consulta, usuarioId);
        }

        private static async Task<DataTable> ConsultarAsync(string consulta, int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentOutOfRangeException(nameof(usuarioId));

            using MySqlConnection conexao = ConexaoBD.CriarConexao();
            await conexao.OpenAsync().ConfigureAwait(false);
            using MySqlCommand comando = new MySqlCommand(consulta, conexao);
            comando.CommandTimeout = 5;
            comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
            using var leitor = await comando.ExecuteReaderAsync().ConfigureAwait(false);
            DataTable tabela = new DataTable();
            tabela.Load(leitor);
            return tabela;
        }
    }
}
