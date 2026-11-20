using MySql.Data.MySqlClient;

namespace Limk.VolleyBall.Backend
{
    public class Atleta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public int NumeroCamisa { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int IdEquipe { get; set; }
        public string NomeEquipe { get; set; }
        public List<string> Posicoes { get; set; }
        public bool Titular { get; set; }

        public string PosicoesResumo => Posicoes != null && Posicoes.Count > 0
            ? string.Join(", ", Posicoes)
            : string.Empty;

        public Atleta()
        {
            Nome = string.Empty;
            Apelido = string.Empty;
            NomeEquipe = string.Empty;
            Posicoes = new List<string>();
        }

        public bool CadastrarAtleta(string nomePosicao, out string mensagemErro)
        {
            mensagemErro = string.Empty;

            using (MySqlConnection conexao = ConexaoBD.ObterConexao())
            {
                if (conexao == null)
                {
                    mensagemErro = "Não foi possível conectar ao banco de dados.";
                    return false;
                }

                MySqlTransaction transacao = null;

                try
                {
                    transacao = conexao.BeginTransaction();

                    string sqlAtleta = @"
                        INSERT INTO atleta (nome, apelido, numero_camisa, data_nascimento, idEquipe, titular)
                        VALUES (@nome, @apelido, @numero_camisa, @data_nascimento, @idEquipe, @titular);
                        SELECT LAST_INSERT_ID();";

                    using (MySqlCommand cmd = new MySqlCommand(sqlAtleta, conexao, transacao))
                    {
                        cmd.Parameters.AddWithValue("@nome", Nome.Trim());
                        cmd.Parameters.AddWithValue("@apelido", string.IsNullOrWhiteSpace(Apelido) ? (object)DBNull.Value : Apelido.Trim());
                        cmd.Parameters.AddWithValue("@numero_camisa", NumeroCamisa);
                        cmd.Parameters.AddWithValue("@data_nascimento", DataNascimento.HasValue ? (object)DataNascimento.Value.Date : DBNull.Value);
                        cmd.Parameters.AddWithValue("@idEquipe", IdEquipe);

                        Id = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    if (!string.IsNullOrWhiteSpace(nomePosicao))
                    {
                        string sqlPos = "SELECT id FROM posicao WHERE nome = @nomePosicao LIMIT 1;";
                        object posIdObj;

                        using (MySqlCommand cmdPos = new MySqlCommand(sqlPos, conexao, transacao))
                        {
                            cmdPos.Parameters.AddWithValue("@nomePosicao", nomePosicao.Trim());
                            posIdObj = cmdPos.ExecuteScalar();
                        }

                        if (posIdObj != null && posIdObj != DBNull.Value)
                        {
                            string sqlVinc = "INSERT INTO atleta_posicao (atleta_id, posicao_id) VALUES (@atletaId, @posicaoId);";
                            using (MySqlCommand cmdVinc = new MySqlCommand(sqlVinc, conexao, transacao))
                            {
                                cmdVinc.Parameters.AddWithValue("@atletaId", Id);
                                cmdVinc.Parameters.AddWithValue("@posicaoId", Convert.ToInt32(posIdObj));
                                cmdVinc.ExecuteNonQuery();
                            }
                        }
                    }

                    string sqlEqAtleta = @"INSERT INTO equipe_atleta (equipe_id, atleta_id, numero_camisa, ativo) VALUES (@equipeId, @atletaId, @numeroCamisa, TRUE);";
                    using (MySqlCommand cmdEq = new MySqlCommand(sqlEqAtleta, conexao, transacao))
                    {
                        cmdEq.Parameters.AddWithValue("@equipeId", IdEquipe);
                        cmdEq.Parameters.AddWithValue("@atletaId", Id);
                        cmdEq.Parameters.AddWithValue("@numeroCamisa", NumeroCamisa);
                        cmdEq.ExecuteNonQuery();
                    }

                    transacao.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    try { transacao?.Rollback(); } catch { }
                    mensagemErro = "Erro ao cadastrar atleta: " + ex.Message;
                    return false;
                }
            }
        }

        public static List<Atleta> ListarPorEquipe(int idEquipe)
        {
            var lista = new List<Atleta>();

            string sql = @"
                SELECT 
                    a.id, a.nome, a.apelido, a.numero_camisa, a.data_nascimento, a.idEquipe, a.eh_titular,
                    e.nome AS nome_equipe,
                    GROUP_CONCAT(DISTINCT p.nome ORDER BY p.id SEPARATOR ', ') AS posicoes
                FROM atleta a
                INNER JOIN equipe e ON a.idEquipe = e.id
                LEFT JOIN atleta_posicao ap ON a.id = ap.atleta_id
                LEFT JOIN posicao p ON ap.posicao_id = p.id
                WHERE a.idEquipe = @idEquipe
                GROUP BY a.id, a.nome, a.apelido, a.numero_camisa, a.data_nascimento, a.idEquipe, a.eh_titular, e.nome
                ORDER BY a.numero_camisa, a.nome;";

            using (MySqlConnection conexao = ConexaoBD.ObterConexao())
            {
                if (conexao == null) return lista;

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@idEquipe", idEquipe);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var atleta = new Atleta
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"]?.ToString() ?? string.Empty,
                                Apelido = reader["apelido"]?.ToString() ?? string.Empty,
                                NumeroCamisa = Convert.ToInt32(reader["numero_camisa"]),
                                DataNascimento = reader["data_nascimento"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["data_nascimento"])
                                    : (DateTime?)null,
                                IdEquipe = Convert.ToInt32(reader["idEquipe"]),
                                NomeEquipe = reader["nome_equipe"]?.ToString() ?? string.Empty,
                                Titular = reader["titular"] != DBNull.Value && Convert.ToBoolean(reader["eh_titular"])
                            };

                            string posicoesStr = reader["posicoes"]?.ToString();
                            if (!string.IsNullOrWhiteSpace(posicoesStr))
                            {
                                atleta.Posicoes.AddRange(posicoesStr.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries));
                            }

                            lista.Add(atleta);
                        }
                    }
                }
            }

            return lista;
        }
    }
}