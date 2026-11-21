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
            btnLimparAtleta.Click += btnLimparAtleta_Click;
            btnSalvarAtleta.Click += btnSalvarAtleta_Click;

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

        private async Task RecarregarAtletasAsync()
        {
            if (_usuario == null || IsDisposed)
                return;

            try
            {
                _atletas = await DadosUsuario.CarregarAtletasAsync(_usuario.Id);
                if (!IsDisposed && _atletas.Columns.Count > 0)
                    AplicarFiltros();
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                {
                    MessageBox.Show("Não foi possível atualizar a lista de atletas: " + ex.Message, "Erro ao atualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimparAtleta_Click(object sender, EventArgs e)
        {
            dataGridViewAtletas.ClearSelection();
            dataGridViewAtletas.CurrentCell = null;
            LimparFicha();
            txtbNomeCompleto.Focus();
        }

        private async void btnSalvarAtleta_Click(object sender, EventArgs e)
        {
            if (_usuario == null)
                return;

            string nome = txtbNomeCompleto.Text.Trim();
            string apelido = txtbNomeCamisa.Text.Trim();
            string posicao = cmbPosicao.Text.Trim();

            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Informe o nome do atleta.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtbNomeCompleto.Focus();
                return;
            }

            if (!int.TryParse(txtbNumCamisa.Text.Trim(), out int numeroCamisa) || numeroCamisa < 1 || numeroCamisa > 99)
            {
                MessageBox.Show("Informe um número de camisa válido (1 a 99).", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtbNomeCamisa.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(posicao))
            {
                MessageBox.Show("Selecione ou informe uma posição.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPosicao.Focus();
                return;
            }

            int equipeId = ObterEquipeIdFicha();
            if (equipeId <= 0)
            {
                MessageBox.Show("Selecione uma equipe válida no topo ou informe o nome da equipe na ficha.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime? dataNascimento = dateTimePicker1.Checked ? dateTimePicker1.Value.Date : null;
            bool titular = checkBoxAtletaTitular.Checked;

            var atleta = new Atleta
            {
                Nome = nome,
                Apelido = apelido,
                NumeroCamisa = numeroCamisa,
                DataNascimento = dataNascimento,
                IdEquipe = equipeId,
                Titular = titular
            };

            if (!atleta.CadastrarAtleta(posicao, out string erro))
            {
                MessageBox.Show(erro, "Erro ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Atleta salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimparFicha();
            await RecarregarAtletasAsync();
        }

        private int ObterEquipeIdFicha()
        {
            if (cmbEquipe.SelectedValue is int idFiltro && idFiltro > 0)
                return idFiltro;

            string nomeEquipeDigitado = txtbEquipeAtleta.Text.Trim();
            if (string.IsNullOrWhiteSpace(nomeEquipeDigitado) || cmbEquipe.DataSource is not IEnumerable<KeyValuePair<int, string>> itens)
                return 0;

            var encontrada = itens.FirstOrDefault(item => item.Key > 0 && item.Value.Equals(nomeEquipeDigitado, StringComparison.OrdinalIgnoreCase));

            return encontrada.Key;
        }
    }
}
