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

            using MySqlCommand comando = new MySqlCommand(consulta, conexao);
            comando.CommandTimeout = 5;
            using var leitor = await comando.ExecuteReaderAsync();
            await leitor.ReadAsync();

            return new ResumoInicio
            {
                Equipes = leitor.GetInt64(0),
                Atletas = leitor.GetInt64(1),
                Taticas = leitor.GetInt64(2),
                Quadros = leitor.GetInt64(3)
            };
        }
    }
}
