using System.Globalization;
using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal sealed record PreferenciasUsuario(
        string CorAtletaPrincipal, string CorAtletaAdversario, string CorBola, int DuracaoQuadroPadraoMs)
    {
        public static PreferenciasUsuario Padrao { get; } = new("#6935D3", "#E53935", "#FFD700", 1000);
    }

    internal sealed class FalhaPreferenciasUsuarioException(Exception causa)
        : Exception("Não foi possível consultar ou salvar as preferências no banco de dados.", causa)
    {
    }

    internal static class DadosPreferenciasUsuario
    {
        public static async Task<PreferenciasUsuario> CarregarAsync(
            int usuarioId, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);

            const string consulta = """
                SELECT cor_atleta_principal, cor_atleta_adversario, cor_bola, duracao_quadro_padrao_ms
                FROM preferencia_usuario
                WHERE usuario_id = @usuarioId
                """;

            PreferenciasUsuario padrao = PreferenciasUsuario.Padrao;
            try
            {
                using MySqlConnection conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(cancellationToken).ConfigureAwait(false);
                using MySqlCommand comando = new(consulta, conexao) { CommandTimeout = 5 };
                comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                using var leitor = await comando.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
                if (!await leitor.ReadAsync(cancellationToken).ConfigureAwait(false))
                    return padrao;

                int duracao = leitor.IsDBNull(3) ? padrao.DuracaoQuadroPadraoMs : leitor.GetInt32(3);
                return new PreferenciasUsuario(
                    LerCor(leitor.IsDBNull(0) ? null : leitor.GetString(0), padrao.CorAtletaPrincipal),
                    LerCor(leitor.IsDBNull(1) ? null : leitor.GetString(1), padrao.CorAtletaAdversario),
                    LerCor(leitor.IsDBNull(2) ? null : leitor.GetString(2), padrao.CorBola),
                    duracao > 0 ? duracao : padrao.DuracaoQuadroPadraoMs);
            }
            catch (Exception erro) when (ErroDeBanco(erro))
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw new FalhaPreferenciasUsuarioException(erro);
            }
        }

        public static async Task SalvarAsync(
            int usuarioId, PreferenciasUsuario preferencias, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(usuarioId);
            ArgumentNullException.ThrowIfNull(preferencias);
            if (!CorValida(preferencias.CorAtletaPrincipal) || !CorValida(preferencias.CorAtletaAdversario)
                || !CorValida(preferencias.CorBola) || preferencias.DuracaoQuadroPadraoMs <= 0)
            {
                throw new ArgumentException("Informe cores no formato #RRGGBB e uma duração positiva.", nameof(preferencias));
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

            try
            {
                using MySqlConnection conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(cancellationToken).ConfigureAwait(false);
                using MySqlTransaction transacao = await conexao.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
                using MySqlCommand comando = new(consulta, conexao, transacao) { CommandTimeout = 5 };
                comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                comando.Parameters.Add("@corAtletaPrincipal", MySqlDbType.VarChar, 7).Value =
                    preferencias.CorAtletaPrincipal.ToUpperInvariant();
                comando.Parameters.Add("@corAtletaAdversario", MySqlDbType.VarChar, 7).Value =
                    preferencias.CorAtletaAdversario.ToUpperInvariant();
                comando.Parameters.Add("@corBola", MySqlDbType.VarChar, 7).Value = preferencias.CorBola.ToUpperInvariant();
                comando.Parameters.Add("@duracao", MySqlDbType.Int32).Value = preferencias.DuracaoQuadroPadraoMs;
                await comando.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                await transacao.CommitAsync(CancellationToken.None).ConfigureAwait(false);
            }
            catch (Exception erro) when (ErroDeBanco(erro))
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw new FalhaPreferenciasUsuarioException(erro);
            }
        }

        private static string LerCor(string? cor, string padrao) =>
            cor is not null && CorValida(cor) ? cor.ToUpperInvariant() : padrao;

        private static bool CorValida(string? cor) => cor is { Length: 7 } && cor[0] == '#'
            && int.TryParse(cor.AsSpan(1), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out _);

        private static bool ErroDeBanco(Exception erro) => erro is MySqlException or InvalidOperationException
            or ArgumentException or FormatException or OverflowException or TimeoutException;
    }
}
