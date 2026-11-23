using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    internal static class DadosAtletas
    {
        public static DataTable Listar(int usuarioId)
        {
            // O vínculo também é filtrado: um atleta pode atuar em duas equipes.
            const string consulta = @"SELECT atleta.id, equipe_atleta.equipe_id, equipe_atleta.numero_camisa,
                    atleta.nome, atleta.apelido, atleta.data_nascimento, equipe.nome AS equipe, equipe_atleta.titular,
                    (SELECT GROUP_CONCAT(DISTINCT posicao.nome ORDER BY posicao.nome SEPARATOR ', ')
                        FROM atleta_posicao
                        INNER JOIN posicao ON posicao.id = atleta_posicao.posicao_id
                        WHERE atleta_posicao.atleta_id = atleta.id) AS posicoes,
                    (SELECT GROUP_CONCAT(atleta_posicao.posicao_id ORDER BY atleta_posicao.posicao_id)
                        FROM atleta_posicao WHERE atleta_posicao.atleta_id = atleta.id) AS posicao_ids,
                    NOT EXISTS (SELECT 1 FROM equipe_atleta
                        LEFT JOIN usuario_equipe ON usuario_equipe.equipe_id = equipe_atleta.equipe_id
                            AND usuario_equipe.usuario_id = @usuarioId
                        WHERE equipe_atleta.atleta_id = atleta.id AND usuario_equipe.usuario_id IS NULL) AS pode_editar_dados
                FROM atleta
                INNER JOIN equipe_atleta ON equipe_atleta.atleta_id = atleta.id AND equipe_atleta.ativo = TRUE
                INNER JOIN equipe ON equipe.id = equipe_atleta.equipe_id
                INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = equipe.id
                WHERE usuario_equipe.usuario_id = @usuarioId
                ORDER BY atleta.nome, equipe.nome, atleta.id, equipe.id";
            return ConexaoBD.ConsultarTabela(consulta, usuarioId);
        }

        public static List<PosicaoCadastro> CarregarPosicoes()
        {
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                using MySqlCommand comando = CriarComando(conexao, "SELECT id, nome FROM posicao ORDER BY nome, id");
                using MySqlDataReader leitor = comando.ExecuteReader();

                List<PosicaoCadastro> posicoes = new List<PosicaoCadastro>();
                while (leitor.Read())
                    posicoes.Add(new PosicaoCadastro(leitor.GetInt32(0), leitor.GetString(1)));

                return posicoes;
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível carregar as posições. Verifique a conexão e tente novamente.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        public static int Salvar(int usuarioId, Atleta atleta, IReadOnlyCollection<int> posicaoIds)
        {
            ArgumentNullException.ThrowIfNull(atleta);
            ArgumentNullException.ThrowIfNull(posicaoIds);
            int atletaId = atleta.GetId();
            int equipeId = atleta.GetIdEquipe();
            int numeroCamisa = atleta.GetNumeroCamisa();
            bool titular = atleta.GetTitular();
            string nome = atleta.GetNome()?.Trim() ?? string.Empty;
            string apelido = atleta.GetApelido()?.Trim() ?? string.Empty;
            DateTime? nascimento = atleta.GetDataNascimento()?.Date;
            bool novo = atletaId == 0;
            int[] posicoes = posicaoIds.Distinct().ToArray();

            if (usuarioId <= 0)
                throw new ValidacaoException("Entre novamente para salvar um atleta.");
            if (atletaId < 0 || equipeId <= 0)
                throw new ValidacaoException("Selecione uma equipe válida para o atleta.");
            if (numeroCamisa < 1 || numeroCamisa > 99)
                throw new ValidacaoException("Informe um número de camisa de 1 a 99.");

            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                using MySqlTransaction transacao = conexao.BeginTransaction();

                GarantirAcessoEquipe(conexao, transacao, usuarioId, equipeId);

                // Os dados pessoais e as posições pertencem ao cadastro compartilhado entre equipes.
                bool podeEditarCadastro = novo || VerificarVinculos(conexao, transacao,
                    usuarioId, atletaId, equipeId);
                if (podeEditarCadastro)
                {
                    ValidarCadastro(nome, apelido, nascimento);
                    if (novo && posicoes.Length == 0)
                        throw new ValidacaoException("Selecione pelo menos uma posição para o novo atleta.");

                    atletaId = GravarCadastro(conexao, transacao, atletaId,
                        nome, apelido, nascimento);
                    GravarPosicoes(conexao, transacao, atletaId, posicoes, novo);
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
                    vinculo.ExecuteNonQuery();
                }

                transacao.Commit();
                return atletaId;
            }
            catch (MySqlException erro) when (erro.Number == 1062)
            {
                throw new ValidacaoException("Esse número de camisa já está em uso ou reservado na equipe.", erro);
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível salvar o atleta. Verifique a conexão e tente novamente.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        public static void Excluir(int usuarioId, int atletaId, int equipeId)
        {
            if (usuarioId <= 0)
                throw new ValidacaoException("Entre novamente para excluir um atleta.");
            if (atletaId <= 0 || equipeId <= 0)
                throw new ValidacaoException("Selecione um atleta do elenco para excluir.");

            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                using MySqlTransaction transacao = conexao.BeginTransaction();
                GarantirAcessoEquipe(conexao, transacao, usuarioId, equipeId);

                // A inativação preserva as referências das táticas salvas a este vínculo.
                using MySqlCommand comando = CriarComando(conexao, """
                    UPDATE equipe_atleta
                    SET ativo = FALSE
                    WHERE equipe_id = @equipeId AND atleta_id = @atletaId AND ativo = TRUE
                    """, transacao);
                comando.Parameters.Add("@equipeId", MySqlDbType.Int32).Value = equipeId;
                comando.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                if (comando.ExecuteNonQuery() != 1)
                    throw new ValidacaoException("O atleta já saiu do elenco ativo. Reabra a tela para atualizar a lista.");

                transacao.Commit();
            }
            catch (Exception erro) when (ConexaoBD.EhFalhaDeBanco(erro))
            {
                throw new BancoDeDadosException("Não foi possível excluir o atleta do elenco. Verifique a conexão e tente novamente.", erro);
            }
            finally
            {
                ConexaoBD.Fechar(conexao);
            }
        }

        private static void GarantirAcessoEquipe(MySqlConnection conexao, MySqlTransaction transacao,
            int usuarioId, int equipeId)
        {
            if (!DadosEquipe.BloquearComAcesso(conexao, transacao, usuarioId, equipeId,
                exclusivo: false))
                throw new ValidacaoException("Você não tem acesso à equipe selecionada.");
        }

        private static bool VerificarVinculos(MySqlConnection conexao, MySqlTransaction transacao,
            int usuarioId, int atletaId, int equipeId)
        {
            // Serializa alterações neste cadastro, inclusive a entrada de novos vínculos pela chave estrangeira.
            using (MySqlCommand cadastro = CriarComando(conexao, "SELECT id FROM atleta WHERE id = @atletaId FOR UPDATE", transacao))
            {
                cadastro.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                if (cadastro.ExecuteScalar() == null)
                    throw new ValidacaoException("O atleta não está mais no elenco ativo da equipe selecionada.");
            }

            using MySqlCommand comando = CriarComando(conexao, """
                SELECT equipe_atleta.equipe_id, equipe_atleta.ativo, usuario_equipe.usuario_id
                FROM equipe_atleta
                LEFT JOIN usuario_equipe ON usuario_equipe.equipe_id = equipe_atleta.equipe_id AND usuario_equipe.usuario_id = @usuarioId
                WHERE equipe_atleta.atleta_id = @atletaId
                FOR UPDATE OF equipe_atleta FOR SHARE OF usuario_equipe
                """, transacao);
            comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
            comando.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
            using MySqlDataReader leitor = comando.ExecuteReader();

            bool vinculoAtivo = false;
            bool podeEditarCadastro = true;
            while (leitor.Read())
            {
                if (leitor.GetInt32(0) == equipeId && leitor.GetBoolean(1))
                    vinculoAtivo = true;
                if (leitor.IsDBNull(2))
                    podeEditarCadastro = false;
            }

            if (!vinculoAtivo)
                throw new ValidacaoException("O atleta não está mais no elenco ativo da equipe selecionada.");

            return podeEditarCadastro;
        }

        private static void ValidarCadastro(string nome, string apelido, DateTime? nascimento)
        {
            if (nome.Length == 0 || nome.EnumerateRunes().Count() > 100)
                throw new ValidacaoException("Informe um nome de até 100 caracteres.");
            if (apelido.EnumerateRunes().Count() > 50)
                throw new ValidacaoException("Informe um apelido de até 50 caracteres.");
            if (nascimento is { Year: < 1000 })
                throw new ValidacaoException("Informe uma data de nascimento válida.");
        }

        private static int GravarCadastro(MySqlConnection conexao, MySqlTransaction transacao, int atletaId,
            string nome, string apelido, DateTime? nascimento)
        {
            string consulta = atletaId == 0
                ? "INSERT INTO atleta (nome, apelido, data_nascimento) VALUES (@nome, @apelido, @nascimento)"
                : "UPDATE atleta SET nome = @nome, apelido = @apelido, data_nascimento = @nascimento WHERE id = @atletaId";
            using MySqlCommand comando = CriarComando(conexao, consulta, transacao);
            comando.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
            comando.Parameters.Add("@nome", MySqlDbType.VarChar, 100).Value = nome;
            comando.Parameters.Add("@apelido", MySqlDbType.VarChar, 50).Value = apelido.Length == 0 ? DBNull.Value : apelido;
            comando.Parameters.Add("@nascimento", MySqlDbType.Date).Value = (object?)nascimento ?? DBNull.Value;
            comando.ExecuteNonQuery();

            return atletaId == 0 ? checked((int)comando.LastInsertedId) : atletaId;
        }

        private static void GravarPosicoes(MySqlConnection conexao, MySqlTransaction transacao, int atletaId,
            int[] posicoes, bool novo)
        {
            if (!novo)
            {
                using MySqlCommand remover = CriarComando(conexao, "DELETE FROM atleta_posicao WHERE atleta_id = @atletaId", transacao);
                remover.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                remover.ExecuteNonQuery();
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
                if (gravar.ExecuteNonQuery() != 1)
                    throw new ValidacaoException("Uma das posições selecionadas não está mais disponível.");
            }
        }

        private static MySqlCommand CriarComando(MySqlConnection conexao, string consulta, MySqlTransaction? transacao = null)
        {
            return new MySqlCommand(consulta, conexao, transacao) { CommandTimeout = 5 };
        }
    }
}
