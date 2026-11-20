using Limk.VolleyBall.Backend;
using System.ComponentModel;
using System.Data;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaEquipes : UserControl, ITelaUsuario
    {
        private UsuarioAutenticado? _usuario;
        private DataTable _equipes = new DataTable();
        public event EventHandler<UserControl>? TelaSolicitada;

        public TelaEquipes()
        {
            InitializeComponent();
            Load += TelaEquipes_Load;
            textBox1.TextChanged += (sender, e) => ExibirEquipes();
        }

        public void DefinirUsuario(UsuarioAutenticado usuario)
        {
            _usuario = usuario;
        }

        private async void TelaEquipes_Load(object? sender, EventArgs e)
        {
            if (_usuario == null || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            label3.Text = "Carregando suas equipes...";
            try
            {
                DataTable equipes = await DadosUsuario.CarregarEquipesAsync(_usuario.Id);
                if (IsDisposed)
                    return;
                _equipes = equipes;
                ExibirEquipes();
            }
            catch (Exception)
            {
                if (!IsDisposed)
                    label3.Text = "Não foi possível carregar suas equipes.";
            }
        }

        private void ExibirEquipes()
        {
            if (_equipes.Columns.Count == 0)
                return;

            tableLayoutPanel1.SuspendLayout();
            while (tableLayoutPanel1.Controls.Count > 0)
                tableLayoutPanel1.Controls[0].Dispose();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowCount = 0;
            string busca = textBox1.Text.Trim();
            foreach (DataRow equipe in _equipes.Rows)
            {
                if (!equipe["nome"].ToString()!.Contains(busca, StringComparison.OrdinalIgnoreCase))
                    continue;
                CardEquipes cartao = new CardEquipes();
                cartao.ExibirEquipe(equipe);
                cartao.Dock = DockStyle.Top;
                cartao.Margin = new Padding(0, 0, 0, 8);
                int equipeId = Convert.ToInt32(equipe["id"]);
                cartao.VerElenco += (sender, e) => TelaSolicitada?.Invoke(this, new TelaAtletas(equipeId));
                int linha = tableLayoutPanel1.RowCount;
                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tableLayoutPanel1.Controls.Add(cartao, 0, linha);
            }
            tableLayoutPanel1.ResumeLayout(true);
            label3.Text = tableLayoutPanel1.Controls.Count == 0
                ? "Nenhuma equipe vinculada encontrada." : $"Suas equipes · {tableLayoutPanel1.Controls.Count}";
        }
    }
}
