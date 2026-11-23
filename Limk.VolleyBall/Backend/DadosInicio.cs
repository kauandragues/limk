using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal static class DadosInicio
    {
        public static ResumoInicio Carregar(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentOutOfRangeException(nameof(usuarioId));

            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                return ConsultarResumo(conexao, usuarioId);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        private static ResumoInicio ConsultarResumo(MySqlConnection conexao, int usuarioId)
        {
            // Conta os atletas pelo cadastro, sem repetir quem pertence a duas equipes.
            const string consulta = @"SELECT
                COUNT(DISTINCT usuario_equipe.equipe_id) AS equipes,
                COUNT(DISTINCT equipe_atleta.atleta_id) AS atletas,
                COUNT(DISTINCT tatica.id) AS taticas,
                COUNT(DISTINCT quadro.id) AS quadros
                FROM usuario_equipe
                LEFT JOIN equipe_atleta ON equipe_atleta.equipe_id = usuario_equipe.equipe_id AND equipe_atleta.ativo = TRUE
                LEFT JOIN tatica ON tatica.equipe_id = usuario_equipe.equipe_id
                LEFT JOIN quadro ON quadro.tatica_id = tatica.id
                WHERE usuario_equipe.usuario_id = @usuarioId";

            ResumoInicio resumo = new ResumoInicio();
            using (MySqlCommand comando = CriarComando(conexao, usuarioId, consulta))
            {
                using MySqlDataReader leitor = comando.ExecuteReader();
                leitor.Read();
                resumo.SetEquipes(leitor.GetInt64(0));
                resumo.SetAtletas(leitor.GetInt64(1));
                resumo.SetTaticas(leitor.GetInt64(2));
                resumo.SetQuadros(leitor.GetInt64(3));
            }

            // Sem histórico de uso, as últimas cadastradas são ordenadas pelo ID.
            const string consultaTaticas = @"SELECT tatica.nome, tatica.descricao, equipe.nome AS equipe,
                    COUNT(quadro.id) AS quadros, tatica.id
                FROM tatica
                INNER JOIN equipe ON equipe.id = tatica.equipe_id
                INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = tatica.equipe_id
                LEFT JOIN quadro ON quadro.tatica_id = tatica.id
                WHERE usuario_equipe.usuario_id = @usuarioId
                GROUP BY tatica.id, tatica.nome, tatica.descricao, equipe.nome
                ORDER BY tatica.id DESC
                LIMIT 3";

            using (MySqlCommand comando = CriarComando(conexao, usuarioId, consultaTaticas))
            {
                using MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    TaticaInicio registroTaticaInicio = new TaticaInicio();
                    registroTaticaInicio.SetId(leitor.GetInt32(4));
                    registroTaticaInicio.SetNome(leitor.GetString(0));
                    registroTaticaInicio.SetDescricao(leitor.IsDBNull(1) ? "" : leitor.GetString(1));
                    registroTaticaInicio.SetEquipe(leitor.GetString(2));
                    registroTaticaInicio.SetQuadros(leitor.GetInt64(3));
                    resumo.GetUltimasTaticas().Add(registroTaticaInicio);
                }
            }

            const string consultaEquipes = @"SELECT equipe.nome,
                    COUNT(DISTINCT equipe_atleta.id) AS atletas,
                    COUNT(DISTINCT tatica.id) AS taticas
                FROM equipe
                INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = equipe.id
                LEFT JOIN equipe_atleta ON equipe_atleta.equipe_id = equipe.id AND equipe_atleta.ativo = TRUE
                LEFT JOIN tatica ON tatica.equipe_id = equipe.id
                WHERE usuario_equipe.usuario_id = @usuarioId
                GROUP BY equipe.id, equipe.nome
                ORDER BY equipe.nome, equipe.id
                LIMIT 3";

            using (MySqlCommand comando = CriarComando(conexao, usuarioId, consultaEquipes))
            {
                using MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    EquipeInicio registroEquipeInicio = new EquipeInicio();
                    registroEquipeInicio.SetNome(leitor.GetString(0));
                    registroEquipeInicio.SetAtletas(leitor.GetInt64(1));
                    registroEquipeInicio.SetTaticas(leitor.GetInt64(2));
                    resumo.GetEquipesCadastradas().Add(registroEquipeInicio);
                }
            }

            return resumo;
        }

        private static MySqlCommand CriarComando(MySqlConnection conexao, int usuarioId, string sql)
        {
            MySqlCommand comando = new MySqlCommand(sql, conexao) { CommandTimeout = 5 };
            comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
            return comando;
        }
    }
}
