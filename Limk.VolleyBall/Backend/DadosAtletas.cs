using System.Data;
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
                    posicao.nome AS posicoes, posicao.id AS posicao_ids,
                    COUNT(DISTINCT vinculo.equipe_id) = COUNT(DISTINCT acesso.equipe_id) AS pode_editar_dados
                FROM atleta
                INNER JOIN equipe_atleta ON equipe_atleta.atleta_id = atleta.id AND equipe_atleta.ativo = TRUE
                INNER JOIN equipe ON equipe.id = equipe_atleta.equipe_id
                INNER JOIN usuario_equipe ON usuario_equipe.equipe_id = equipe.id
                LEFT JOIN atleta_posicao ON atleta_posicao.atleta_id = atleta.id
                LEFT JOIN posicao ON posicao.id = atleta_posicao.posicao_id
                LEFT JOIN equipe_atleta AS vinculo ON vinculo.atleta_id = atleta.id
                LEFT JOIN usuario_equipe AS acesso ON acesso.equipe_id = vinculo.equipe_id AND acesso.usuario_id = @usuarioId
                WHERE usuario_equipe.usuario_id = @usuarioId
                GROUP BY atleta.id, equipe_atleta.equipe_id, equipe_atleta.numero_camisa,
                    atleta.nome, atleta.apelido, atleta.data_nascimento, equipe.nome, equipe_atleta.titular,
                    posicao.id, posicao.nome
                ORDER BY atleta.nome, equipe.nome, atleta.id, equipe.id, posicao.nome, posicao.id";
            DataTable registros = ConexaoBD.ConsultarTabela(consulta, usuarioId);
            return AgruparPosicoes(registros);
        }

        private static DataTable AgruparPosicoes(DataTable registros)
        {
            // Clone copia as colunas da tabela, mas deixa a nova tabela sem linhas.
            DataTable atletas = registros.Clone();
            DataColumn? colunaPosicoes = atletas.Columns["posicoes"];
            DataColumn? colunaIds = atletas.Columns["posicao_ids"];
            if (colunaPosicoes == null || colunaIds == null)
            {
                throw new InvalidOperationException("A consulta de atletas precisa retornar as posições e seus códigos.");
            }

            colunaPosicoes.MaxLength = -1;
            colunaIds.DataType = typeof(string);
            // O ORDER BY da consulta mantém juntas as posições do mesmo atleta na mesma equipe.
            // Percorremos essas linhas para exibir todas as posições em uma única linha do grid.
            int indice = 0;
            while (indice < registros.Rows.Count)
            {
                DataRow primeiraLinha = registros.Rows[indice];
                int atletaId = primeiraLinha.Field<int>("id");
                int equipeId = primeiraLinha.Field<int>("equipe_id");
                DataRow atleta = atletas.NewRow();
                atleta.ItemArray = primeiraLinha.ItemArray;
                List<string> nomesPosicoes = new List<string>();
                List<int> idsPosicoes = new List<int>();
                while (indice < registros.Rows.Count)
                {
                    DataRow linha = registros.Rows[indice];
                    if (linha.Field<int>("id") != atletaId || linha.Field<int>("equipe_id") != equipeId)
                    {
                        break;
                    }

                    if (!linha.IsNull("posicao_ids"))
                    {
                        string? nomePosicao = linha.Field<string>("posicoes");
                        if (nomePosicao == null)
                        {
                            nomePosicao = "";
                        }

                        nomesPosicoes.Add(nomePosicao);
                        idsPosicoes.Add(linha.Field<int>("posicao_ids"));
                    }

                    indice++;
                }

                if (idsPosicoes.Count > 0)
                {
                    idsPosicoes.Sort();
                    // Join monta um texto separando cada item pela vírgula informada.
                    atleta["posicoes"] = string.Join(", ", nomesPosicoes);
                    atleta["posicao_ids"] = string.Join(",", idsPosicoes);
                }

                atletas.Rows.Add(atleta);
            }

            return atletas;
        }

        public static List<PosicaoCadastro> CarregarPosicoes()
        {
            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                MySqlCommand comando = CriarComando(conexao, "SELECT id, nome FROM posicao ORDER BY nome, id");
                try
                {
                    MySqlDataReader leitor = comando.ExecuteReader();
                    try
                    {
                        List<PosicaoCadastro> posicoes = new List<PosicaoCadastro>();
                        while (leitor.Read())
                        {
                            posicoes.Add(new PosicaoCadastro(leitor.GetInt32(0), leitor.GetString(1)));
                        }

                        return posicoes;
                    }
                    finally
                    {
                        leitor.Dispose();
                    }
                }
                finally
                {
                    comando.Dispose();
                }
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

        public static int Salvar(int usuarioId, Atleta atleta, List<int> posicaoIds)
        {
            if (atleta == null)
            {
                throw new ArgumentNullException("atleta");
            }

            if (posicaoIds == null)
            {
                throw new ArgumentNullException("posicaoIds");
            }

            int atletaId = atleta.GetId();
            int equipeId = atleta.GetIdEquipe();
            int numeroCamisa = atleta.GetNumeroCamisa();
            bool titular = atleta.GetTitular();
            string nome = atleta.GetNome();
            if (nome == null)
            {
                nome = "";
            }

            nome = nome.Trim();
            string apelido = atleta.GetApelido();
            if (apelido == null)
            {
                apelido = "";
            }

            apelido = apelido.Trim();
            DateTime? nascimento = atleta.GetDataNascimento();
            if (nascimento.HasValue)
            {
                nascimento = nascimento.Value.Date;
            }

            bool novo = atletaId == 0;
            List<int> posicoes = new List<int>();
            foreach (int posicaoId in posicaoIds)
            {
                if (!posicoes.Contains(posicaoId))
                {
                    posicoes.Add(posicaoId);
                }
            }

            if (usuarioId <= 0)
            {
                throw new ValidacaoException("Entre novamente para salvar um atleta.");
            }

            if (atletaId < 0 || equipeId <= 0)
            {
                throw new ValidacaoException("Selecione uma equipe válida para o atleta.");
            }

            if (numeroCamisa < 1 || numeroCamisa > 99)
            {
                throw new ValidacaoException("Informe um número de camisa de 1 a 99.");
            }

            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                // A transação só confirma as alterações quando todas as gravações dão certo.
                MySqlTransaction transacao = conexao.BeginTransaction();
                try
                {
                    GarantirAcessoEquipe(conexao, transacao, usuarioId, equipeId);
                    // Os dados pessoais e as posições pertencem ao cadastro compartilhado entre equipes.
                    bool podeEditarCadastro = novo || VerificarVinculos(conexao, transacao, usuarioId, atletaId, equipeId);
                    if (podeEditarCadastro)
                    {
                        ValidarCadastro(nome, apelido, nascimento);
                        if (novo && posicoes.Count == 0)
                        {
                            throw new ValidacaoException("Selecione pelo menos uma posição para o novo atleta.");
                        }

                        atletaId = GravarCadastro(conexao, transacao, atletaId, nome, apelido, nascimento);
                        GravarPosicoes(conexao, transacao, atletaId, posicoes, novo);
                    }

                    string consultaDoVinculo;
                    if (novo)
                    {
                        consultaDoVinculo = """
                        INSERT INTO equipe_atleta (equipe_id, atleta_id, numero_camisa, titular)
                        VALUES (@equipeId, @atletaId, @numeroCamisa, @titular)
                        """;
                    }
                    else
                    {
                        consultaDoVinculo = """
                        UPDATE equipe_atleta SET numero_camisa = @numeroCamisa, titular = @titular
                        WHERE equipe_id = @equipeId AND atleta_id = @atletaId AND ativo = TRUE
                        """;
                    }

                    {
                        MySqlCommand vinculo = CriarComando(conexao, consultaDoVinculo, transacao);
                        try
                        {
                            vinculo.Parameters.Add("@equipeId", MySqlDbType.Int32).Value = equipeId;
                            vinculo.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                            vinculo.Parameters.Add("@numeroCamisa", MySqlDbType.UByte).Value = (byte)numeroCamisa;
                            if (titular)
                            {
                                vinculo.Parameters.Add("@titular", MySqlDbType.Byte).Value = 1;
                            }
                            else
                            {
                                vinculo.Parameters.Add("@titular", MySqlDbType.Byte).Value = 0;
                            }

                            vinculo.ExecuteNonQuery();
                        }
                        finally
                        {
                            vinculo.Dispose();
                        }
                    }

                    transacao.Commit();
                    return atletaId;
                }
                finally
                {
                    transacao.Dispose();
                }
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
            {
                throw new ValidacaoException("Entre novamente para excluir um atleta.");
            }

            if (atletaId <= 0 || equipeId <= 0)
            {
                throw new ValidacaoException("Selecione um atleta do elenco para excluir.");
            }

            MySqlConnection conexao = new MySqlConnection();
            try
            {
                ConexaoBD.Abrir(conexao);
                MySqlTransaction transacao = conexao.BeginTransaction();
                try
                {
                    GarantirAcessoEquipe(conexao, transacao, usuarioId, equipeId);
                    // A inativação preserva as referências das táticas salvas a este vínculo.
                    MySqlCommand comando = CriarComando(conexao, """
                    UPDATE equipe_atleta
                    SET ativo = FALSE
                    WHERE equipe_id = @equipeId AND atleta_id = @atletaId AND ativo = TRUE
                    """, transacao);
                    try
                    {
                        comando.Parameters.Add("@equipeId", MySqlDbType.Int32).Value = equipeId;
                        comando.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                        if (comando.ExecuteNonQuery() != 1)
                        {
                            throw new ValidacaoException("O atleta já saiu do elenco ativo. Reabra a tela para atualizar a lista.");
                        }

                        transacao.Commit();
                    }
                    finally
                    {
                        comando.Dispose();
                    }
                }
                finally
                {
                    transacao.Dispose();
                }
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

        private static void GarantirAcessoEquipe(MySqlConnection conexao, MySqlTransaction transacao, int usuarioId, int equipeId)
        {
            if (!DadosEquipe.TemAcessoEquipe(conexao, transacao, usuarioId, equipeId))
            {
                throw new ValidacaoException("Você não tem acesso à equipe selecionada.");
            }
        }

        private static bool VerificarVinculos(MySqlConnection conexao, MySqlTransaction transacao, int usuarioId, int atletaId, int equipeId)
        {
            {
                MySqlCommand cadastro = CriarComando(conexao, "SELECT id FROM atleta WHERE id = @atletaId", transacao);
                try
                {
                    cadastro.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                    if (cadastro.ExecuteScalar() == null)
                    {
                        throw new ValidacaoException("O atleta não está mais no elenco ativo da equipe selecionada.");
                    }
                }
                finally
                {
                    cadastro.Dispose();
                }
            }

            MySqlCommand comando = CriarComando(conexao, """
                SELECT equipe_atleta.equipe_id, equipe_atleta.ativo, usuario_equipe.usuario_id
                FROM equipe_atleta
                LEFT JOIN usuario_equipe ON usuario_equipe.equipe_id = equipe_atleta.equipe_id AND usuario_equipe.usuario_id = @usuarioId
                WHERE equipe_atleta.atleta_id = @atletaId
                """, transacao);
            try
            {
                comando.Parameters.Add("@usuarioId", MySqlDbType.Int32).Value = usuarioId;
                comando.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                MySqlDataReader leitor = comando.ExecuteReader();
                try
                {
                    bool vinculoAtivo = false;
                    bool podeEditarCadastro = true;
                    while (leitor.Read())
                    {
                        if (leitor.GetInt32(0) == equipeId && leitor.GetBoolean(1))
                        {
                            vinculoAtivo = true;
                        }

                        if (leitor.IsDBNull(2))
                        {
                            podeEditarCadastro = false;
                        }
                    }

                    if (!vinculoAtivo)
                    {
                        throw new ValidacaoException("O atleta não está mais no elenco ativo da equipe selecionada.");
                    }

                    return podeEditarCadastro;
                }
                finally
                {
                    leitor.Dispose();
                }
            }
            finally
            {
                comando.Dispose();
            }
        }

        private static void ValidarCadastro(string nome, string apelido, DateTime? nascimento)
        {
            if (nome.Length == 0 || Texto.ContarCaracteres(nome) > 100)
            {
                throw new ValidacaoException("Informe um nome de até 100 caracteres.");
            }

            if (Texto.ContarCaracteres(apelido) > 50)
            {
                throw new ValidacaoException("Informe um apelido de até 50 caracteres.");
            }

            if (nascimento.HasValue && nascimento.Value.Year < 1000)
            {
                throw new ValidacaoException("Informe uma data de nascimento válida.");
            }
        }

        private static int GravarCadastro(MySqlConnection conexao, MySqlTransaction transacao, int atletaId, string nome, string apelido, DateTime? nascimento)
        {
            string consulta;
            if (atletaId == 0)
            {
                consulta = "INSERT INTO atleta (nome, apelido, data_nascimento) VALUES (@nome, @apelido, @nascimento)";
            }
            else
            {
                consulta = "UPDATE atleta SET nome = @nome, apelido = @apelido, data_nascimento = @nascimento WHERE id = @atletaId";
            }

            MySqlCommand comando = CriarComando(conexao, consulta, transacao);
            try
            {
                comando.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                comando.Parameters.Add("@nome", MySqlDbType.VarChar, 100).Value = nome;
                MySqlParameter parametroApelido = comando.Parameters.Add("@apelido", MySqlDbType.VarChar, 50);
                if (apelido.Length == 0)
                {
                    parametroApelido.Value = DBNull.Value;
                }
                else
                {
                    parametroApelido.Value = apelido;
                }

                // No banco, usamos DBNull.Value para representar um campo sem valor.
                MySqlParameter parametroNascimento = comando.Parameters.Add("@nascimento", MySqlDbType.Date);
                if (nascimento.HasValue)
                {
                    parametroNascimento.Value = nascimento.Value;
                }
                else
                {
                    parametroNascimento.Value = DBNull.Value;
                }

                comando.ExecuteNonQuery();
                if (atletaId == 0)
                {
                    return Convert.ToInt32(comando.LastInsertedId);
                }
                else
                {
                    return atletaId;
                }
            }
            finally
            {
                comando.Dispose();
            }
        }

        private static void GravarPosicoes(MySqlConnection conexao, MySqlTransaction transacao, int atletaId, List<int> posicoes, bool novo)
        {
            if (!novo)
            {
                MySqlCommand remover = CriarComando(conexao, "DELETE FROM atleta_posicao WHERE atleta_id = @atletaId", transacao);
                try
                {
                    remover.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                    remover.ExecuteNonQuery();
                }
                finally
                {
                    remover.Dispose();
                }
            }

            MySqlCommand gravar = CriarComando(conexao, """
                INSERT INTO atleta_posicao (atleta_id, posicao_id)
                SELECT @atletaId, id FROM posicao WHERE id = @posicaoId
                """, transacao);
            try
            {
                gravar.Parameters.Add("@atletaId", MySqlDbType.Int32).Value = atletaId;
                MySqlParameter parametroPosicao = gravar.Parameters.Add("@posicaoId", MySqlDbType.Int32);
                foreach (int posicaoId in posicoes)
                {
                    parametroPosicao.Value = posicaoId;
                    if (gravar.ExecuteNonQuery() != 1)
                    {
                        throw new ValidacaoException("Uma das posições selecionadas não está mais disponível.");
                    }
                }
            }
            finally
            {
                gravar.Dispose();
            }
        }

        private static MySqlCommand CriarComando(MySqlConnection conexao, string consulta, MySqlTransaction? transacao = null)
        {
            MySqlCommand comando = new MySqlCommand(consulta, conexao, transacao);
            comando.CommandTimeout = 5;
            return comando;
        }
    }
}
