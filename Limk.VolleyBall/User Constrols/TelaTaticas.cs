using Limk.VolleyBall.Backend;
using System.ComponentModel;
using System.Data;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaTaticas : UserControl, ITelaUsuario
    {
        private UsuarioAutenticado? _usuario;
        private DataTable _taticas = new DataTable();
        private bool _carregando;
        public event EventHandler<UserControl>? TelaSolicitada;
        public TelaTaticas()
        {
            InitializeComponent();
            Load += TelaTaticas_Load;
            textBox1.TextChanged += FiltroAlterado;
            comboBox1.SelectedIndexChanged += FiltroAlterado;
            comboBox2.SelectedIndexChanged += FiltroAlterado;
            button1.Click += SolicitarNovaTatica;
        }

        public void DefinirUsuario(UsuarioAutenticado usuario)
        {
            _usuario = usuario;
        }

        private void TelaTaticas_Load(object? sender, EventArgs e)
        {
            if (_usuario == null || _carregando || _taticas.Columns.Count > 0 || IsDisposed || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            _carregando = true;
            label2.Text = "Carregando as táticas das suas equipes...";
            try
            {
                DataTable equipes = DadosEquipe.Listar(_usuario.GetId());
                DataTable taticas = DadosPrancheta.Listar(_usuario.GetId());
                // Cada opção guarda o código da equipe (Key) e o nome mostrado na lista (Value).
                List<KeyValuePair<int, string>> opcoes = new List<KeyValuePair<int, string>>();
                opcoes.Add(new KeyValuePair<int, string>(0, "Todas as minhas equipes"));
                foreach (DataRow equipe in equipes.Rows)
                {
                    string? nome = equipe["nome"].ToString();
                    if (nome == null)
                    {
                        nome = "";
                    }

                    opcoes.Add(new KeyValuePair<int, string>(Convert.ToInt32(equipe["id"]), nome));
                }

                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
                comboBox1.DataSource = opcoes;
                comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Todos os tipos");
                foreach (DataRow registro in taticas.Rows)
                {
                    string? tipo = registro["tipo"].ToString();
                    if (tipo == null)
                    {
                        tipo = "";
                    }

                    if (tipo.Length > 0 && !comboBox2.Items.Contains(tipo))
                    {
                        comboBox2.Items.Add(tipo);
                    }
                }

                comboBox2.SelectedIndex = 0;
                _taticas = taticas;
            }
            catch (Exception)
            {
                label2.Text = "Não foi possível carregar suas táticas.";
            }
            finally
            {
                _carregando = false;
            }

            if (!IsDisposed && _taticas.Columns.Count > 0)
            {
                ExibirTaticas();
            }
        }

        private void FiltroAlterado(object? sender, EventArgs e)
        {
            if (!_carregando)
            {
                ExibirTaticas();
            }
        }

        private void LimparFiltros_Click(object? sender, EventArgs e)
        {
            if (_carregando)
            {
                return;
            }

            // Evita recriar os cartões a cada filtro removido.
            _carregando = true;
            textBox1.Clear();
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }

            _carregando = false;
            ExibirTaticas();
        }

        private void ExibirTaticas()
        {
            if (_taticas.Columns.Count == 0)
            {
                return;
            }

            tableLayoutPanel1.SuspendLayout();
            while (tableLayoutPanel1.Controls.Count > 0)
            {
                tableLayoutPanel1.Controls[0].Dispose();
            }

            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowCount = 0;
            int equipeId = 0;
            if (comboBox1.SelectedValue is int)
            {
                equipeId = (int)comboBox1.SelectedValue;
            }

            string tipo;
            if (comboBox2.SelectedIndex > 0)
            {
                tipo = comboBox2.Text;
            }
            else
            {
                tipo = "";
            }

            string busca = textBox1.Text.Trim();
            int indice = 0;
            foreach (DataRow tatica in _taticas.Rows)
            {
                string? nomeTatica = tatica["nome"].ToString();
                if (nomeTatica == null)
                {
                    nomeTatica = "";
                }

                if (equipeId > 0 && Convert.ToInt32(tatica["equipe_id"]) != equipeId)
                {
                    continue;
                }

                if (tipo.Length > 0 && tatica["tipo"].ToString() != tipo)
                {
                    continue;
                }

                if (!nomeTatica.Contains(busca, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                CardTática cartao = new CardTática();
                cartao.ExibirTatica(tatica);
                cartao.AbrirTatica += SolicitarAbertura;
                cartao.ApresentarTatica += SolicitarApresentacao;
                cartao.ExcluirTatica += ExcluirTatica;
                cartao.Dock = DockStyle.Top;
                int linha = indice / 2;
                // A divisão encontra a linha. O resto da divisão (%) alterna a coluna entre 0 e 1.
                if (linha >= tableLayoutPanel1.RowCount)
                {
                    tableLayoutPanel1.RowCount++;
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                }

                tableLayoutPanel1.Controls.Add(cartao, indice % 2, linha);
                indice++;
            }

            tableLayoutPanel1.ResumeLayout(true);
            if (indice == 0)
            {
                label2.Text = "Nenhuma tática nas equipes selecionadas.";
            }
            else
            {
                label2.Text = $"{indice} táticas das suas equipes";
            }
        }

        private void ExcluirTatica(object? sender, DataRow tatica)
        {
            if (_usuario == null || !tableLayoutPanel1.Enabled)
            {
                return;
            }

            string mensagem = $"Excluir a tática “{tatica["nome"]}” da equipe “{tatica["equipe"]}”?\n\n" + "Todos os seus quadros e as posições de atletas e bola serão apagados.";
            if (MessageBox.Show(this, mensagem, "Excluir tática", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            tableLayoutPanel1.Enabled = false;
            try
            {
                bool excluida = DadosPrancheta.Excluir(_usuario.GetId(), Convert.ToInt32(tatica["id"]));
                _taticas.Rows.Remove(tatica);
                ExibirTaticas();
                if (!excluida)
                {
                    label2.Text = "Esta tática já foi excluída ou você perdeu o acesso à equipe.";
                }
            }
            catch (BancoDeDadosException)
            {
                label2.Text = "Não foi possível confirmar a exclusão. Reabra Táticas para atualizar a lista.";
            }
            finally
            {
                tableLayoutPanel1.Enabled = true;
            }
        }

        private void SolicitarNovaTatica(object? sender, EventArgs e)
        {
            if (TelaSolicitada != null)
            {
                TelaSolicitada(this, new TelaPranchetaTatica());
            }
        }

        private void SolicitarAbertura(object? sender, int id)
        {
            if (TelaSolicitada != null)
            {
                TelaSolicitada(this, new TelaPranchetaTatica(id));
            }
        }

        private void SolicitarApresentacao(object? sender, int id)
        {
            if (TelaSolicitada != null)
            {
                TelaSolicitada(this, new TelaPranchetaTatica(id, apresentar: true));
            }
        }
    }
}
