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
                        string query = "SELECT numero_camisa, nome, apelido, data_nascimento FROM atleta";

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
