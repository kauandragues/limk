using Limk.VolleyBall.Backend;
using System.ComponentModel;
using System.Data;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaAtletas : UserControl, ITelaUsuario
    {
        private UsuarioAutenticado? _usuario;
        private DataTable _atletas = new DataTable();
        private Button? btnAtualFiltro;
        private string _posicao = "";
        private readonly int _equipeInicialId;
        private bool _carregando;

        public TelaAtletas()
        {
            InitializeComponent();
            Load += TelaAtletas_Load;
            cmbEquipe.SelectedIndexChanged += FiltroAlterado;
            txtbBuscarAtleta.TextChanged += FiltroAlterado;
            btnFiltroLibeAtletas.Click += btnFiltroLibeAtletas_Click;
            dataGridViewAtletas.SelectionChanged += AtletaSelecionado;
            dataGridViewAtletas.ReadOnly = true;
            dataGridViewAtletas.AllowUserToAddRows = false;
            dataGridViewAtletas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewAtletas.MultiSelect = false;
        }

        public TelaAtletas(int equipeId) : this()
        {
            _equipeInicialId = equipeId;
        }

        public void DefinirUsuario(UsuarioAutenticado usuario)
        {
            _usuario = usuario;
        }

        private async void TelaAtletas_Load(object? sender, EventArgs e)
        {
            if (_usuario == null || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _carregando = true;
            label13.Text = "Carregando suas equipes...";
            try
            {
                DataTable equipes = await DadosUsuario.CarregarEquipesAsync(_usuario.Id);
                DataTable atletas = await DadosUsuario.CarregarAtletasAsync(_usuario.Id);
                if (IsDisposed)
                    return;

                _atletas = atletas;
                var opcoes = new List<KeyValuePair<int, string>> { new(0, "Todas as minhas equipes") };
                foreach (DataRow equipe in equipes.Rows)
                    opcoes.Add(new(Convert.ToInt32(equipe["id"]), equipe["nome"].ToString()!));
                cmbEquipe.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbEquipe.DisplayMember = "Value";
                cmbEquipe.ValueMember = "Key";
                cmbEquipe.DataSource = opcoes;
                if (opcoes.Any(opcao => opcao.Key == _equipeInicialId))
                    cmbEquipe.SelectedValue = _equipeInicialId;
            }
            catch (Exception)
            {
                if (!IsDisposed)
                {
                    _atletas = new DataTable();
                    label13.Text = "Não foi possível carregar seus atletas.";
                    dataGridViewAtletas.DataSource = null;
                    LimparFicha();
                }
            }
            finally
            {
                _carregando = false;
            }
            if (!IsDisposed && _atletas.Columns.Count > 0)
                AplicarFiltros();
        }

        private void FiltroAlterado(object? sender, EventArgs e)
        {
            if (!_carregando)
                AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            if (_atletas.Columns.Count == 0)
                return;

            int equipeId = cmbEquipe.SelectedValue is int id ? id : 0;
            string busca = txtbBuscarAtleta.Text.Trim();
            var linhas = _atletas.AsEnumerable().Where(linha =>
                (equipeId == 0 || Convert.ToInt32(linha["equipe_id"]) == equipeId) &&
                (linha["nome"].ToString()!.Contains(busca, StringComparison.OrdinalIgnoreCase) ||
                 linha["apelido"].ToString()!.Contains(busca, StringComparison.OrdinalIgnoreCase)) &&
                (_posicao.Length == 0 || linha["posicoes"].ToString()!.Split(", ").Contains(_posicao))).ToList();

            dataGridViewAtletas.DataSource = linhas.Count == 0 ? _atletas.Clone() : linhas.CopyToDataTable();
            dataGridViewAtletas.Columns["id"]!.Visible = false;
            dataGridViewAtletas.Columns["equipe_id"]!.Visible = false;
            dataGridViewAtletas.Columns["numero_camisa"]!.HeaderText = "Camisa";
            dataGridViewAtletas.Columns["nome"]!.HeaderText = "Nome";
            dataGridViewAtletas.Columns["apelido"]!.HeaderText = "Apelido";
            dataGridViewAtletas.Columns["data_nascimento"]!.HeaderText = "Nascimento";
            dataGridViewAtletas.Columns["equipe"]!.HeaderText = "Equipe";
            dataGridViewAtletas.Columns["titular"]!.HeaderText = "Titular";
            dataGridViewAtletas.Columns["posicoes"]!.HeaderText = "Posições";
            lblQtdAtletas.Text = linhas.Select(linha => Convert.ToInt32(linha["id"])).Distinct().Count().ToString();
            label13.Text = linhas.Count == 0 ? "Nenhum atleta nas equipes selecionadas." : cmbEquipe.Text;
            var posicoes = linhas.SelectMany(linha => linha["posicoes"].ToString()!.Split(", ", StringSplitOptions.RemoveEmptyEntries)).Distinct().Count();
            label14.Text = $"{posicoes} posições no elenco";
            lblPorcentagem.Text = $"{posicoes}/5";
            label10.Text = "ATLETAS VISÍVEIS";
            label11.Text = "POSIÇÕES COBERTAS";
            if (linhas.Count == 0)
                LimparFicha();
        }

        private void AtletaSelecionado(object? sender, EventArgs e)
        {
            if (dataGridViewAtletas.CurrentRow?.DataBoundItem is not DataRowView registro)
                return;
            txtbNomeCompleto.Text = registro["nome"].ToString();
            txtbNomeCamisa.Text = registro["apelido"].ToString();
            txtbNumCamisa.Text = registro["numero_camisa"].ToString();
            txtbEquipeAtleta.Text = registro["equipe"].ToString();
            cmbPosicao.Text = registro["posicoes"].ToString();
            checkBoxAtletaTitular.Checked = Convert.ToBoolean(registro["titular"]);
            dateTimePicker1.Checked = registro["data_nascimento"] != DBNull.Value;
            if (registro["data_nascimento"] is DateTime nascimento)
                dateTimePicker1.Value = nascimento;
        }

        private void LimparFicha()
        {
            txtbNomeCompleto.Clear();
            txtbNomeCamisa.Clear();
            txtbNumCamisa.Clear();
            txtbEquipeAtleta.Clear();
            cmbPosicao.Text = "";
            checkBoxAtletaTitular.Checked = false;
            dateTimePicker1.Checked = false;
        }

        private void SelecionarFiltro(Button botao, string posicao)
        {
            if (btnAtualFiltro != null)
                btnAtualFiltro.BackColor = Color.FromArgb(101, 117, 140);
            btnAtualFiltro = botao;
            botao.BackColor = Color.FromArgb(123, 57, 237);
            _posicao = posicao;
            AplicarFiltros();
        }

        private void btnFiltroTodosAtletas_Click(object sender, EventArgs e) => SelecionarFiltro(btnFiltroTodosAtletas, "");
        private void btnFiltroLevAtletas_Click(object sender, EventArgs e) => SelecionarFiltro(btnFiltroLevAtletas, "Levantador");
        private void btnFiltroPontAtleta_Click(object sender, EventArgs e) => SelecionarFiltro(btnFiltroPontAtleta, "Ponteiro");
        private void btnFiltroCentAtletas_Click(object sender, EventArgs e) => SelecionarFiltro(btnFiltroCentAtletas, "Central");
        private void btnFiltroLibeAtletas_Click(object? sender, EventArgs e) => SelecionarFiltro(btnFiltroLibeAtletas, "Líbero");
    }
}
