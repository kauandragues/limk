using System.Text;
using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal sealed record PosicaoCadastro(int Id, string Nome);

    internal sealed class FalhaAtletaException(string mensagem, Exception? erro = null) : Exception(mensagem, erro);

    internal static class DadosAtletas
    {
        public static async Task<List<PosicaoCadastro>> CarregarPosicoesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                using MySqlConnection conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(cancellationToken).ConfigureAwait(false);
                using MySqlCommand comando = CriarComando(conexao, "SELECT id, nome FROM posicao ORDER BY nome, id");
                using var leitor = await comando.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

                var posicoes = new List<PosicaoCadastro>();
                while (await leitor.ReadAsync(cancellationToken).ConfigureAwait(false))
                    posicoes.Add(new PosicaoCadastro(leitor.GetInt32(0), leitor.GetString(1)));

                return posicoes;
            }
            catch (Exception erro) when (ErroDeBanco(erro))
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw new FalhaAtletaException("Não foi possível carregar as posições. Verifique a conexão e tente novamente.", erro);
            }
        }

        public static async Task<int> SalvarAsync(int usuarioId, Atleta atleta, IReadOnlyCollection<int> posicaoIds,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(atleta);
            ArgumentNullException.ThrowIfNull(posicaoIds);
            int atletaId = atleta.Id;
            int equipeId = atleta.IdEquipe;
            int numeroCamisa = atleta.NumeroCamisa;
            bool titular = atleta.Titular;
            string nome = atleta.Nome?.Trim() ?? string.Empty;
            string apelido = atleta.Apelido?.Trim() ?? string.Empty;
            DateTime? nascimento = atleta.DataNascimento?.Date;
            bool novo = atletaId == 0;
            int[] posicoes = posicaoIds.Distinct().ToArray();

            if (usuarioId <= 0)
                throw new FalhaAtletaException("Entre novamente para salvar um atleta.");
            if (atletaId < 0 || equipeId <= 0)
                throw new FalhaAtletaException("Selecione uma equipe válida para o atleta.");
            if (numeroCamisa is < 1 or > 99)
                throw new FalhaAtletaException("Informe um número de camisa de 1 a 99.");

            try
            {
                using MySqlConnection conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(cancellationToken).ConfigureAwait(false);
                using MySqlTransaction transacao = await conexao.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

                await GarantirAcessoEquipeAsync(conexao, transacao, usuarioId, equipeId, cancellationToken)
                    .ConfigureAwait(false);

                // Os dados pessoais e as posições pertencem ao cadastro compartilhado entre equipes.
                bool podeEditarCadastro = novo || await VerificarVinculosAsync(conexao, transacao,
                    usuarioId, atletaId, equipeId, cancellationToken).ConfigureAwait(false);
                if (podeEditarCadastro)
                {
                    ValidarCadastro(nome, apelido, nascimento);
                    if (novo && posicoes.Length == 0)
                        throw new FalhaAtletaException("Selecione pelo menos uma posição para o novo atleta.");

                    atletaId = await GravarCadastroAsync(conexao, transacao, atletaId,
                        nome, apelido, nascimento, cancellationToken).ConfigureAwait(false);
                    await GravarPosicoesAsync(conexao, transacao, atletaId, posicoes, novo, cancellationToken)
                        .ConfigureAwait(false);
                }

                string consultaDoVinculo = novo
                    ? """
                        INSERT INTO equipe_atleta (equipe_id, atleta_id, numero_camisa, titular)
                        VALUES (@equipeId, @atletaId, @numeroCamisa, @titular)
                        """
                    : """
                        UPDATE equipe_atleta SET numero_camisa = @numeroCamisa, titular = @titular
                        WHERE equipe_id = @equipeId AND atleta_id = @atletaId AND ativo = TRUE
                        """;
                using (MySqlCommand vinculo = CriarComando(conexao, consultaDoVinculo, transacao))
                {
                    vinculo.Parameters.Add("@equipeId", MySqlDbType.Int32).Value = equipeId;
                    vinculo.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                    vinculo.Parameters.Add("@numeroCamisa", MySqlDbType.UByte).Value = (byte)numeroCamisa;
                    vinculo.Parameters.Add("@titular", MySqlDbType.Byte).Value = titular ? 1 : 0;
                    await vinculo.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                }

                cancellationToken.ThrowIfCancellationRequested();
                await transacao.CommitAsync(CancellationToken.None).ConfigureAwait(false);
                return atletaId;
            }
            catch (MySqlException erro) when (erro.Number == 1062)
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw new FalhaAtletaException("Esse número de camisa já está em uso ou reservado na equipe.", erro);
            }
            catch (Exception erro) when (ErroDeBanco(erro))
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw new FalhaAtletaException("Não foi possível salvar o atleta. Verifique a conexão e tente novamente.", erro);
            }
        }

        public static async Task ExcluirAsync(int usuarioId, int atletaId, int equipeId,
            CancellationToken cancellationToken = default)
        {
            if (usuarioId <= 0)
                throw new FalhaAtletaException("Entre novamente para excluir um atleta.");
            if (atletaId <= 0 || equipeId <= 0)
                throw new FalhaAtletaException("Selecione um atleta do elenco para excluir.");

            try
            {
                using MySqlConnection conexao = ConexaoBD.CriarConexao();
                await conexao.OpenAsync(cancellationToken).ConfigureAwait(false);
                using MySqlTransaction transacao = await conexao.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
                await GarantirAcessoEquipeAsync(conexao, transacao, usuarioId, equipeId, cancellationToken)
                    .ConfigureAwait(false);

                // A inativação preserva as referências das táticas salvas a este vínculo.
                using MySqlCommand comando = CriarComando(conexao, """
                    UPDATE equipe_atleta
                    SET ativo = FALSE
                    WHERE equipe_id = @equipeId AND atleta_id = @atletaId AND ativo = TRUE
                    """, transacao);
                comando.Parameters.Add("@equipeId", MySqlDbType.Int32).Value = equipeId;
                comando.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                if (await comando.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false) != 1)
                    throw new FalhaAtletaException("O atleta já saiu do elenco ativo. Reabra a tela para atualizar a lista.");

                cancellationToken.ThrowIfCancellationRequested();
                await transacao.CommitAsync(CancellationToken.None).ConfigureAwait(false);
            }
            catch (Exception erro) when (ErroDeBanco(erro))
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw new FalhaAtletaException("Não foi possível excluir o atleta do elenco. Verifique a conexão e tente novamente.", erro);
            }
        }

        private static async Task GarantirAcessoEquipeAsync(MySqlConnection conexao, MySqlTransaction transacao,
            int usuarioId, int equipeId, CancellationToken cancellationToken)
        {
            if (!await DadosEquipe.BloquearComAcessoAsync(conexao, transacao, usuarioId, equipeId,
                exclusivo: false, cancellationToken).ConfigureAwait(false))
                throw new FalhaAtletaException("Você não tem acesso à equipe selecionada.");
        }

        private static async Task<bool> VerificarVinculosAsync(MySqlConnection conexao, MySqlTransaction transacao,
            int usuarioId, int atletaId, int equipeId, CancellationToken cancellationToken)
        {
            // Serializa alterações neste cadastro, inclusive a entrada de novos vínculos pela chave estrangeira.
            using (MySqlCommand cadastro = CriarComando(conexao, "SELECT id FROM atleta WHERE id = @atletaId FOR UPDATE", transacao))
            {
                cadastro.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                if (await cadastro.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false) is null)
                    throw new FalhaAtletaException("O atleta não está mais no elenco ativo da equipe selecionada.");
            }

            using MySqlCommand comando = CriarComando(conexao, """
                SELECT ea.equipe_id, ea.ativo, ue.usuario_id
                FROM equipe_atleta ea
                LEFT JOIN usuario_equipe ue ON ue.equipe_id = ea.equipe_id AND ue.usuario_id = @usuarioId
                WHERE ea.atleta_id = @atletaId FOR UPDATE OF ea FOR SHARE OF ue
                """, transacao);
            comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
            comando.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
            using var leitor = await comando.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

            bool vinculoAtivo = false;
            bool podeEditarCadastro = true;
            while (await leitor.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                if (leitor.GetInt32(0) == equipeId && leitor.GetBoolean(1))
                    vinculoAtivo = true;
                if (leitor.IsDBNull(2))
                    podeEditarCadastro = false;
            }

            if (!vinculoAtivo)
                throw new FalhaAtletaException("O atleta não está mais no elenco ativo da equipe selecionada.");

            return podeEditarCadastro;
        }

        private static void ValidarCadastro(string nome, string apelido, DateTime? nascimento)
        {
            if (nome.Length == 0 || nome.EnumerateRunes().Count() > 100)
                throw new FalhaAtletaException("Informe um nome de até 100 caracteres.");
            if (apelido.EnumerateRunes().Count() > 50)
                throw new FalhaAtletaException("Informe um apelido de até 50 caracteres.");
            if (nascimento is { Year: < 1000 })
                throw new FalhaAtletaException("Informe uma data de nascimento válida.");
        }

        private static async Task<int> GravarCadastroAsync(MySqlConnection conexao, MySqlTransaction transacao, int atletaId,
            string nome, string apelido, DateTime? nascimento, CancellationToken cancellationToken)
        {
            string consulta = atletaId == 0
                ? "INSERT INTO atleta (nome, apelido, data_nascimento) VALUES (@nome, @apelido, @nascimento)"
                : "UPDATE atleta SET nome = @nome, apelido = @apelido, data_nascimento = @nascimento WHERE id = @atletaId";
            using MySqlCommand comando = CriarComando(conexao, consulta, transacao);
            comando.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
            comando.Parameters.Add("@nome", MySqlDbType.VarChar, 100).Value = nome;
            comando.Parameters.Add("@apelido", MySqlDbType.VarChar, 50).Value = apelido.Length == 0 ? DBNull.Value : apelido;
            comando.Parameters.Add("@nascimento", MySqlDbType.Date).Value = (object?)nascimento ?? DBNull.Value;
            await comando.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);

            return atletaId == 0 ? checked((int)comando.LastInsertedId) : atletaId;
        }

        private static async Task GravarPosicoesAsync(MySqlConnection conexao, MySqlTransaction transacao, int atletaId,
            int[] posicoes, bool novo, CancellationToken cancellationToken)
        {
            if (!novo)
            {
                using MySqlCommand remover = CriarComando(conexao, "DELETE FROM atleta_posicao WHERE atleta_id = @atletaId", transacao);
                remover.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                await remover.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }

            using MySqlCommand gravar = CriarComando(conexao, """
                INSERT INTO atleta_posicao (atleta_id, posicao_id)
                SELECT @atletaId, id FROM posicao WHERE id = @posicaoId
                """, transacao);
            gravar.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
            MySqlParameter parametroPosicao = gravar.Parameters.Add("@posicaoId", MySqlDbType.Int32);
            foreach (int posicaoId in posicoes)
            {
                parametroPosicao.Value = posicaoId;
                if (await gravar.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false) != 1)
                    throw new FalhaAtletaException("Uma das posições selecionadas não está mais disponível.");
            }
        }

        private static MySqlCommand CriarComando(MySqlConnection conexao, string consulta, MySqlTransaction? transacao = null) =>
            new MySqlCommand(consulta, conexao, transacao) { CommandTimeout = 5 };

        private static bool ErroDeBanco(Exception erro) => erro is MySqlException or InvalidOperationException
            or ArgumentException or FormatException or OverflowException or TimeoutException;
    }
}
