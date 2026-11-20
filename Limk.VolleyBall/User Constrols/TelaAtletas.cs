using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaAtletas : UserControl
    {
        public TelaAtletas()
        {
            InitializeComponent();
            CarregarAtletasGrid();
        }
        private Button btnAtualFiltro = null;

        private Color corBtnPadraoFiltro = Color.FromArgb(101, 117, 140);
        private Color corBtnAtualFiltro = Color.FromArgb(123, 57, 237);

        private void MudarCorBotao(Button botao)
        {
            if (btnAtualFiltro != null)
            {
                btnAtualFiltro.BackColor = corBtnPadraoFiltro;
            }

            botao.BackColor = corBtnAtualFiltro;
            btnAtualFiltro = botao;
        }

        private void btnFiltroTodosAtletas_Click(object sender, EventArgs e)
        {
            MudarCorBotao((Button)sender);
        }

        private void btnFiltroLevAtletas_Click(object sender, EventArgs e)
        {
            MudarCorBotao((Button)sender);
        }

        private void btnFiltroPontAtleta_Click(object sender, EventArgs e)
        {
            MudarCorBotao((Button)sender);
        }

        private void btnFiltroCentAtletas_Click(object sender, EventArgs e)
        {
            MudarCorBotao((Button)sender);
        }

        private void CarregarAtletasGrid()
        {
            using (MySqlConnection conexao = ConexaoBD.ObterConexao())
            {
                if (conexao != null)
                {
                    try
                    {
                        // Dados pessoais ficam no atleta; camisa, equipe e titular ficam no vínculo.
                        string query = @"SELECT ea.numero_camisa, a.nome, a.apelido,
                                                a.data_nascimento, e.nome AS equipe, ea.titular
                                         FROM atleta AS a
                                         LEFT JOIN equipe_atleta AS ea
                                             ON ea.atleta_id = a.id AND ea.ativo = TRUE
                                         LEFT JOIN equipe AS e ON e.id = ea.equipe_id
                                         ORDER BY a.nome, e.nome";

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexao))
                        {
                            DataTable tabelaAtletas = new DataTable();
                            adapter.Fill(tabelaAtletas);

                            dataGridViewAtletas.DataSource = tabelaAtletas;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar os atletas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
