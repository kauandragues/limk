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
            button1.Click += (sender, e) => TelaSolicitada?.Invoke(this, new TelaPranchetaTatica());
        }

        public void DefinirUsuario(UsuarioAutenticado usuario)
        {
            _usuario = usuario;
        }

        private async void TelaTaticas_Load(object? sender, EventArgs e)
        {
            if (_usuario == null || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _carregando = true;
            label2.Text = "Carregando as táticas das suas equipes...";
            try
            {
                DataTable equipes = await DadosUsuario.CarregarEquipesAsync(_usuario.Id);
                DataTable taticas = await DadosUsuario.CarregarTaticasAsync(_usuario.Id);
                if (IsDisposed)
                    return;
                _taticas = taticas;
                var opcoes = new List<KeyValuePair<int, string>> { new(0, "Todas as minhas equipes") };
                foreach (DataRow equipe in equipes.Rows)
                    opcoes.Add(new(Convert.ToInt32(equipe["id"]), equipe["nome"].ToString()!));
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
                comboBox1.DataSource = opcoes;
                comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Todos os tipos");
                foreach (string tipo in taticas.AsEnumerable().Select(linha => linha["tipo"].ToString()!).Where(tipo => tipo.Length > 0).Distinct())
                    comboBox2.Items.Add(tipo);
                comboBox2.SelectedIndex = 0;
            }
            catch (Exception)
            {
                if (!IsDisposed)
                    label2.Text = "Não foi possível carregar suas táticas.";
            }
            finally
            {
                _carregando = false;
            }
            if (!IsDisposed && _taticas.Columns.Count > 0)
                ExibirTaticas();
        }

        private void FiltroAlterado(object? sender, EventArgs e)
        {
            if (!_carregando)
                ExibirTaticas();
        }

        private void LimparFiltros_Click(object? sender, EventArgs e)
        {
            if (_carregando)
                return;

            // Evita recriar os cartões a cada filtro removido.
            _carregando = true;
            textBox1.Clear();
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
            _carregando = false;
            ExibirTaticas();
        }

        private void ExibirTaticas()
        {
            if (_taticas.Columns.Count == 0)
                return;
            tableLayoutPanel1.SuspendLayout();
            while (tableLayoutPanel1.Controls.Count > 0)
                tableLayoutPanel1.Controls[0].Dispose();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowCount = 0;

            int equipeId = comboBox1.SelectedValue is int id ? id : 0;
            string tipo = comboBox2.SelectedIndex > 0 ? comboBox2.Text : "";
            string busca = textBox1.Text.Trim();
            int indice = 0;
            foreach (DataRow tatica in _taticas.Rows)
            {
                if ((equipeId > 0 && Convert.ToInt32(tatica["equipe_id"]) != equipeId) ||
                    (tipo.Length > 0 && tatica["tipo"].ToString() != tipo) ||
                    !tatica["nome"].ToString()!.Contains(busca, StringComparison.OrdinalIgnoreCase))
                    continue;
                CardTática cartao = new CardTática();
                cartao.ExibirTatica(tatica);
                cartao.AbrirTatica += (sender, id) => TelaSolicitada?.Invoke(this, new TelaPranchetaTatica(id));
                cartao.ApresentarTatica += (sender, id) => TelaSolicitada?.Invoke(this, new TelaPranchetaTatica(id, apresentar: true));
                cartao.Dock = DockStyle.Top;
                int linha = indice / 2;
                if (linha >= tableLayoutPanel1.RowCount)
                {
                    tableLayoutPanel1.RowCount++;
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                }
                tableLayoutPanel1.Controls.Add(cartao, indice % 2, linha);
                indice++;
            }
            tableLayoutPanel1.ResumeLayout(true);
            label2.Text = indice == 0 ? "Nenhuma tática nas equipes selecionadas." : $"{indice} táticas das suas equipes";
        }
    }
}
