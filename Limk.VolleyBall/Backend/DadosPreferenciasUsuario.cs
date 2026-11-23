using System.Globalization;
using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal static class DadosPreferenciasUsuario
    {
        public static PreferenciasUsuario Carregar(
            int usuarioId)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);

            const string consulta = """
                SELECT cor_atleta_principal, cor_atleta_adversario, cor_bola, duracao_quadro_padrao_ms
                FROM preferencia_usuario
                WHERE usuario_id = @usuarioId
                """;

            PreferenciasUsuario padrao = PreferenciasUsuario.CriarPadrao();
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                using MySqlCommand comando = new MySqlCommand(consulta, conexao) { CommandTimeout = 5 };
                comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                using MySqlDataReader leitor = comando.ExecuteReader();
                if (!leitor.Read())
                    return padrao;

                int duracao = leitor.IsDBNull(3) ? padrao.GetDuracaoQuadroPadraoMs() : leitor.GetInt32(3);
                return new PreferenciasUsuario(
                    LerCor(leitor.IsDBNull(0) ? null : leitor.GetString(0), padrao.GetCorAtletaPrincipal()),
                    LerCor(leitor.IsDBNull(1) ? null : leitor.GetString(1), padrao.GetCorAtletaAdversario()),
                    LerCor(leitor.IsDBNull(2) ? null : leitor.GetString(2), padrao.GetCorBola()),
                    duracao > 0 ? duracao : padrao.GetDuracaoQuadroPadraoMs());
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível acessar as preferências.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        public static void Salvar(
            int usuarioId, PreferenciasUsuario preferencias)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            ArgumentNullException.ThrowIfNull(preferencias);
            if (!CorValida(preferencias.GetCorAtletaPrincipal()) || !CorValida(preferencias.GetCorAtletaAdversario())
                || !CorValida(preferencias.GetCorBola()) || preferencias.GetDuracaoQuadroPadraoMs() <= 0)
            {
                throw new ValidacaoException("Informe cores no formato #RRGGBB e uma duração positiva.");
            }

            const string consulta = """
                INSERT INTO preferencia_usuario (usuario_id, cor_atleta_principal, cor_atleta_adversario, cor_bola,
                    duracao_quadro_padrao_ms)
                VALUES (@usuarioId, @corAtletaPrincipal, @corAtletaAdversario, @corBola, @duracao)
                ON DUPLICATE KEY UPDATE
                    cor_atleta_principal = @corAtletaPrincipal,
                    cor_atleta_adversario = @corAtletaAdversario,
                    cor_bola = @corBola,
                    duracao_quadro_padrao_ms = @duracao
                """;

            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                using MySqlTransaction transacao = conexao.BeginTransaction();
                using MySqlCommand comando = new MySqlCommand(consulta, conexao, transacao) { CommandTimeout = 5 };
                comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                comando.Parameters.Add("@corAtletaPrincipal", MySqlDbType.VarChar, 7).Value =
                    preferencias.GetCorAtletaPrincipal().ToUpperInvariant();
                comando.Parameters.Add("@corAtletaAdversario", MySqlDbType.VarChar, 7).Value =
                    preferencias.GetCorAtletaAdversario().ToUpperInvariant();
                comando.Parameters.Add("@corBola", MySqlDbType.VarChar, 7).Value = preferencias.GetCorBola().ToUpperInvariant();
                comando.Parameters.Add("@duracao", MySqlDbType.Int32).Value = preferencias.GetDuracaoQuadroPadraoMs();
                comando.ExecuteNonQuery();
                transacao.Commit();
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível acessar as preferências.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        private static string LerCor(string? cor, string padrao)
        {
            return cor != null && CorValida(cor) ? cor.ToUpperInvariant() : padrao;
        }

        private static bool CorValida(string? cor)
        {
            return cor != null && cor.Length == 7 && cor[0] == '#'
            && int.TryParse(cor.AsSpan(1), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out _);
        }
    }
}
