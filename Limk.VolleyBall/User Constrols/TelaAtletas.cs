using Limk.VolleyBall.Backend;
using System.ComponentModel;
using System.Data;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaAtletas : UserControl, ITelaUsuario
    {
        private readonly int _equipeInicialId;
        private UsuarioAutenticado? _usuario;
        private DataTable _atletas = new DataTable();
        private Button? _filtroAtivo;
        private string _posicaoFiltro = string.Empty;
        private (int AtletaId, int EquipeId)? _edicao;
        private DateTime? _nascimentoForaDaFaixa;
        private bool _podeEditarDados = true;
        private bool _ocupado;
        private bool _atualizandoGrid;
        private bool _dadosCarregados;
        private EstadoFicha? _estadoOriginal;

        // Guarda os valores que estavam no formulário quando a edição começou.
        private class EstadoFicha
        {
            public readonly string Nome;
            private readonly string apelido;
            private readonly string camisa;
            private readonly int equipeId;
            private readonly DateTime? nascimento;
            private readonly bool titular;
            private readonly string posicoes;

            public EstadoFicha(string nome, string apelido, string camisa, int equipeId,
                DateTime? nascimento, bool titular, string posicoes)
            {
                Nome = nome;
                this.apelido = apelido;
                this.camisa = camisa;
                this.equipeId = equipeId;
                this.nascimento = nascimento;
                this.titular = titular;
                this.posicoes = posicoes;
            }

            public bool TemMesmosValores(EstadoFicha outra)
            {
                return Nome == outra.Nome && apelido == outra.apelido && camisa == outra.camisa
                    && equipeId == outra.equipeId && nascimento == outra.nascimento
                    && titular == outra.titular && posicoes == outra.posicoes;
            }
        }

        private bool TemAlteracoesPendentes()
        {
            return _estadoOriginal != null && !_estadoOriginal.TemMesmosValores(CapturarFicha());
        }

        public TelaAtletas()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            ConfigurarGrid();
            txtbNomeCompleto.MaxLength = 100;
            txtbNomeCamisa.MaxLength = 50;
            txtbNumCamisa.MaxLength = 2;

            Load += TelaAtletas_Load;
            cmbEquipe.SelectedIndexChanged += FiltroAlterado;
            txtbBuscarAtleta.TextChanged += FiltroAlterado;
            // Ao soltar o mouse ou a tecla, o grid já terminou de trocar a célula atual.
            dataGridViewAtletas.MouseUp += AtletaSelecionado;
            dataGridViewAtletas.KeyUp += AtletaSelecionado;
            dateTimePicker1.ValueChanged += NascimentoAlterado;
            btnLimparAtleta.Click += NovoCadastro;
            btnCadastrarAtletas.Click += NovoCadastro;
            btnSalvarAtleta.Click += btnSalvarAtleta_Click;
            btnExcluirAtleta.Click += btnExcluirAtleta_Click;
            btnFiltroTodosAtletas.Click += FiltroDePosicaoClicado;
            btnFiltroLevAtletas.Click += FiltroDePosicaoClicado;
            btnFiltroPontAtleta.Click += FiltroDePosicaoClicado;
            btnFiltroCentAtletas.Click += FiltroDePosicaoClicado;
            btnFiltroLibeAtletas.Click += FiltroDePosicaoClicado;
            btnFiltroOpostoAtletas.Click += FiltroDePosicaoClicado;

            SelecionarFiltro(btnFiltroTodosAtletas, string.Empty);
            LimparFicha();
        }

        private void NascimentoAlterado(object? sender, EventArgs e)
        {
            if (_nascimentoForaDaFaixa == null)
                return;

            _nascimentoForaDaFaixa = null;
            bool marcada = dateTimePicker1.Checked;
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Checked = marcada;
        }

        private void FiltroDePosicaoClicado(object? sender, EventArgs e)
        {
            if (sender is Button botao)
            {
                string posicao = botao.Text;
                if (botao == btnFiltroTodosAtletas)
                    posicao = string.Empty;
                SelecionarFiltro(botao, posicao);
            }
        }

        public TelaAtletas(int equipeId) : this()
        {
            _equipeInicialId = equipeId;
        }

        public void DefinirUsuario(UsuarioAutenticado usuario)
        {
            _usuario = usuario;
        }

        internal bool ConfirmarSaida()
        {
            if (_ocupado && _dadosCarregados)
            {
                ExibirStatus("Aguarde a conclusão da operação no banco.");
                return false;
            }

            return !TemAlteracoesPendentes() || MessageBox.Show(this,
                "Você tem alterações sem salvar. Deseja descartá-las?", "Alterações sem salvar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        private void ConfigurarGrid()
        {
            dataGridViewAtletas.AutoGenerateColumns = false;

            AdicionarColuna(new DataGridViewTextBoxColumn(), "numero_camisa", "Camisa", 55, 60);
            AdicionarColuna(new DataGridViewTextBoxColumn(), "nome", "Nome", 105, 170);
            AdicionarColuna(new DataGridViewTextBoxColumn(), "apelido", "Apelido", 65, 95);
            AdicionarColuna(new DataGridViewTextBoxColumn(), "data_nascimento", "Nascimento", 90, 105);
            dataGridViewAtletas.Columns["data_nascimento"]!.DefaultCellStyle.Format = "dd/MM/yyyy";
            AdicionarColuna(new DataGridViewTextBoxColumn(), "equipe", "Equipe", 95, 135);
            AdicionarColuna(new DataGridViewCheckBoxColumn(), "titular", "Titular", 52, 58);
            AdicionarColuna(new DataGridViewTextBoxColumn(), "posicoes", "Posições", 88, 145);
            AjustarLayoutAtletas();
        }

        private void AdicionarColuna(DataGridViewColumn coluna, string campo, string titulo, int larguraMinima, float peso)
        {
            coluna.Name = campo;
            coluna.DataPropertyName = campo;
            coluna.HeaderText = titulo;
            coluna.MinimumWidth = LogicalToDeviceUnits(larguraMinima);
            coluna.FillWeight = peso;
            coluna.SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewAtletas.Columns.Add(coluna);
        }

        private void TelaAtletas_Load(object? sender, EventArgs e)
        {
            if (_usuario == null || _ocupado || _dadosCarregados)
                return;

            DefinirOcupado(true);
            ExibirStatus("Carregando equipes, posições e atletas…");
            try
            {
                DataTable equipes = DadosEquipe.Listar(_usuario.GetId());
                DataTable atletas = DadosAtletas.Listar(_usuario.GetId());
                List<PosicaoCadastro> posicoes = DadosAtletas.CarregarPosicoes();

                CarregarOpcoes(equipes, posicoes);
                _atletas = atletas;
                _dadosCarregados = true;
                AplicarFiltros();
                LimparFicha();
                ExibirStatus(cmbEquipe.Items.Count == 1 ? "Sua conta ainda não tem uma equipe vinculada." : string.Empty);
            }
            catch (Exception)
            {
                ExibirStatus("Não foi possível carregar os atletas. Verifique a conexão e reabra a tela.", erro: true);
            }
            finally
            {
                DefinirOcupado(false);
            }
        }

        private void CarregarOpcoes(DataTable equipes, List<PosicaoCadastro> posicoes)
        {
            List<KeyValuePair<int, string>> opcoes = new List<KeyValuePair<int, string>>();
            foreach (DataRow equipe in equipes.Rows)
            {
                int id = equipe.Field<int>("id");
                string nome = Convert.ToString(equipe["nome"]) ?? "";
                opcoes.Add(new KeyValuePair<int, string>(id, nome));
            }

            cmbEquipe.DisplayMember = cmbEquipeAtleta.DisplayMember = "Value";
            cmbEquipe.ValueMember = cmbEquipeAtleta.ValueMember = "Key";
            List<KeyValuePair<int, string>> filtros = new List<KeyValuePair<int, string>>();
            filtros.Add(new KeyValuePair<int, string>(0, "Todas as minhas equipes"));
            filtros.AddRange(opcoes);
            List<KeyValuePair<int, string>> equipesDaFicha = new List<KeyValuePair<int, string>>();
            equipesDaFicha.Add(new KeyValuePair<int, string>(0, "Selecione uma equipe"));
            equipesDaFicha.AddRange(opcoes);
            cmbEquipe.DataSource = filtros;
            cmbEquipeAtleta.DataSource = equipesDaFicha;
            foreach (KeyValuePair<int, string> equipe in opcoes)
            {
                if (equipe.Key == _equipeInicialId)
                {
                    cmbEquipe.SelectedValue = _equipeInicialId;
                    break;
                }
            }

            listaPosicoes.Items.Clear();
            foreach (PosicaoCadastro posicao in posicoes)
                listaPosicoes.Items.Add(posicao);
        }

        private void FiltroAlterado(object? sender, EventArgs e)
        {
            if (_ocupado || !_dadosCarregados)
                return;

            AplicarFiltros();
            if (_edicao == null && ReferenceEquals(sender, cmbEquipe) && !TemAlteracoesPendentes())
            {
                SelecionarEquipeDoCadastro();
                _estadoOriginal = CapturarFicha();
            }
        }

        private void SelecionarFiltro(Button botao, string posicao)
        {
            if (_ocupado)
                return;

            if (_filtroAtivo != null)
                _filtroAtivo.BackColor = Color.FromArgb(101, 117, 140);
            _filtroAtivo = botao;
            botao.BackColor = Color.FromArgb(123, 57, 237);
            _posicaoFiltro = posicao;
            AplicarFiltros();
        }

        private void AplicarFiltros(bool atualizarFicha = false)
        {
            if (!_dadosCarregados)
                return;

            int equipeId = cmbEquipe.SelectedValue is int id ? id : 0;
            string busca = txtbBuscarAtleta.Text.Trim();
            List<DataRow> linhas = new List<DataRow>();
            HashSet<int> atletasVisiveis = new HashSet<int>();
            HashSet<int> posicoesVisiveis = new HashSet<int>();
            foreach (DataRow linha in _atletas.Rows)
            {
                if (equipeId != 0 && linha.Field<int>("equipe_id") != equipeId)
                    continue;
                string nome = linha.Field<string>("nome") ?? "";
                string apelido = linha.Field<string>("apelido") ?? "";
                if (!nome.Contains(busca, StringComparison.OrdinalIgnoreCase) &&
                    !apelido.Contains(busca, StringComparison.OrdinalIgnoreCase))
                    continue;
                string nomesDasPosicoes = linha.Field<string>("posicoes") ?? "";
                if (_posicaoFiltro.Length > 0 && !nomesDasPosicoes.Split(", ").Contains(_posicaoFiltro, StringComparer.OrdinalIgnoreCase))
                    continue;
                linhas.Add(linha);
                atletasVisiveis.Add(linha.Field<int>("id"));
                posicoesVisiveis.UnionWith(LerPosicaoIds(linha));
            }

            _atualizandoGrid = true;
            DataRowView? selecao = null;
            try
            {
                dataGridViewAtletas.DataSource = linhas.Count == 0 ? _atletas.Clone() : linhas.CopyToDataTable();
                selecao = SelecionarFichaNoGrid();
            }
            finally
            {
                _atualizandoGrid = false;
            }
            if (selecao != null && atualizarFicha)
                PreencherFicha(selecao);
            else if (selecao == null && _edicao != null && !TemAlteracoesPendentes())
                LimparFicha();

            lblQtdAtletas.Text = atletasVisiveis.Count.ToString();
            label13.Text = linhas.Count == 0 ? "Nenhum atleta para os filtros selecionados." : cmbEquipe.Text;
            int posicoes = posicoesVisiveis.Count;
            label14.Text = $"{posicoes} posições no elenco";
            lblPorcentagem.Text = $"{posicoes}/{listaPosicoes.Items.Count}";
        }

        private static IEnumerable<int> LerPosicaoIds(DataRow linha)
        {
            string texto = linha.Field<string>("posicao_ids") ?? "";
            List<int> identificadores = new List<int>();
            foreach (string numero in texto.Split(',', StringSplitOptions.RemoveEmptyEntries))
                identificadores.Add(int.Parse(numero));
            return identificadores;
        }

        private static (int AtletaId, int EquipeId) IdentificarVinculo(DataRow linha)
        {
            return (linha.Field<int>("id"), linha.Field<int>("equipe_id"));
        }

        private DataRowView? SelecionarFichaNoGrid()
        {
            dataGridViewAtletas.CurrentCell = null;
            dataGridViewAtletas.ClearSelection();
            if (!_edicao.HasValue)
                return null;
            (int AtletaId, int EquipeId) edicao = _edicao.Value;

            foreach (DataGridViewRow linha in dataGridViewAtletas.Rows)
            {
                if (linha.DataBoundItem is not DataRowView registro || IdentificarVinculo(registro.Row) != edicao)
                    continue;

                dataGridViewAtletas.CurrentCell = linha.Cells["nome"];
                linha.Selected = true;
                return registro;
            }

            return null;
        }

        private void AtletaSelecionado(object? sender, EventArgs e)
        {
            if (_atualizandoGrid || _ocupado || dataGridViewAtletas.SelectedRows.Count != 1 ||
                dataGridViewAtletas.SelectedRows[0].DataBoundItem is not DataRowView registro ||
                _edicao == IdentificarVinculo(registro.Row))
                return;

            if (!ConfirmarSaida())
            {
                SelecionarFichaNoGrid();
                return;
            }

            PreencherFicha(registro);
            ExibirStatus(_podeEditarDados ? string.Empty : "Atleta compartilhado. Você pode editar a camisa e a titularidade desta equipe.");
        }

        private void PreencherFicha(DataRowView registro)
        {
            _edicao = IdentificarVinculo(registro.Row);
            _podeEditarDados = Convert.ToBoolean(registro["pode_editar_dados"]);
            txtbNomeCompleto.Text = registro["nome"].ToString();
            txtbNomeCamisa.Text = registro["apelido"].ToString();
            txtbNumCamisa.Text = registro["numero_camisa"].ToString();
            cmbEquipeAtleta.SelectedValue = _edicao.Value.EquipeId;
            checkBoxAtletaTitular.Checked = Convert.ToBoolean(registro["titular"]);

            DateTime? nascimento = registro.Row.Field<DateTime?>("data_nascimento");
            DateTime exibido = nascimento ?? DateTime.Today;
            if (exibido < dateTimePicker1.MinDate)
                exibido = dateTimePicker1.MinDate;
            if (exibido > dateTimePicker1.MaxDate)
                exibido = dateTimePicker1.MaxDate;
            DateTime? dataLegada = nascimento.HasValue && nascimento.Value != exibido ? nascimento : null;
            _nascimentoForaDaFaixa = null;
            if (dataLegada.HasValue)
            {
                // O calendário do Windows tem uma faixa menor que a coluna DATE do MySQL.
                dateTimePicker1.CustomFormat = $"'{dataLegada.Value:dd/MM/yyyy}'";
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
            }
            else
            {
                dateTimePicker1.Format = DateTimePickerFormat.Short;
            }
            dateTimePicker1.Value = exibido;
            dateTimePicker1.Checked = nascimento.HasValue;
            _nascimentoForaDaFaixa = dataLegada;

            HashSet<int> posicoes = LerPosicaoIds(registro.Row).ToHashSet();
            for (int i = 0; i < listaPosicoes.Items.Count; i++)
                listaPosicoes.SetItemChecked(i, posicoes.Contains(((PosicaoCadastro)listaPosicoes.Items[i]).GetId()));

            label2.Text = "Editar atleta";
            _estadoOriginal = CapturarFicha();
            AtualizarDisponibilidade();
        }

        private void NovoCadastro(object? sender, EventArgs e)
        {
            if (!ConfirmarSaida())
                return;

            dataGridViewAtletas.ClearSelection();
            dataGridViewAtletas.CurrentCell = null;
            LimparFicha();
            ExibirStatus(string.Empty);
            if (ReferenceEquals(sender, btnCadastrarAtletas))
                panel1.ScrollControlIntoView(panelFichaAtleta);
            txtbNomeCompleto.Focus();
        }

        private void LimparFicha()
        {
            _edicao = null;
            _podeEditarDados = true;
            _nascimentoForaDaFaixa = null;
            txtbNomeCompleto.Clear();
            txtbNomeCamisa.Clear();
            txtbNumCamisa.Clear();
            SelecionarEquipeDoCadastro();
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.Checked = false;
            checkBoxAtletaTitular.Checked = false;
            for (int i = 0; i < listaPosicoes.Items.Count; i++)
                listaPosicoes.SetItemChecked(i, false);
            label2.Text = "Novo atleta";
            _estadoOriginal = CapturarFicha();
            AtualizarDisponibilidade();
        }

        private List<int> ObterPosicoesDaFicha()
        {
            List<int> posicoes = new List<int>();
            foreach (PosicaoCadastro posicao in listaPosicoes.CheckedItems)
                posicoes.Add(posicao.GetId());
            return posicoes;
        }

        private DateTime? ObterNascimentoDaFicha()
        {
            return dateTimePicker1.Checked ? _nascimentoForaDaFaixa ?? dateTimePicker1.Value.Date : null;
        }

        private EstadoFicha CapturarFicha()
        {
            return new EstadoFicha(txtbNomeCompleto.Text, txtbNomeCamisa.Text, txtbNumCamisa.Text,
            cmbEquipeAtleta.SelectedValue is int equipeId ? equipeId : 0, ObterNascimentoDaFicha(),
            checkBoxAtletaTitular.Checked, string.Join(",", ObterPosicoesDaFicha()));
        }

        private void SelecionarEquipeDoCadastro()
        {
            if (cmbEquipeAtleta.Items.Count == 0)
                return;
            cmbEquipeAtleta.SelectedValue = cmbEquipe.SelectedValue is int id && id > 0 ? id : 0;
            if (cmbEquipeAtleta.SelectedIndex == 0 && cmbEquipeAtleta.Items.Count == 2)
                cmbEquipeAtleta.SelectedIndex = 1;
        }

        private void btnSalvarAtleta_Click(object? sender, EventArgs e)
        {
            if (_usuario == null || _ocupado || !_dadosCarregados)
                return;

            string nome = txtbNomeCompleto.Text.Trim();
            if (nome.Length == 0)
            {
                ExibirStatus("Informe o nome do atleta.", erro: true);
                txtbNomeCompleto.Focus();
                return;
            }
            if (!int.TryParse(txtbNumCamisa.Text, out int camisa) || camisa < 1 || camisa > 99)
            {
                ExibirStatus("Informe um número de camisa de 1 a 99.", erro: true);
                txtbNumCamisa.Focus();
                return;
            }
            if (cmbEquipeAtleta.SelectedValue is not int equipeId || equipeId <= 0)
            {
                ExibirStatus("Selecione a equipe do atleta na ficha.", erro: true);
                cmbEquipeAtleta.Focus();
                return;
            }
            int[] posicoes = ObterPosicoesDaFicha().ToArray();
            if (_edicao == null && posicoes.Length == 0)
            {
                ExibirStatus("Selecione ao menos uma posição.", erro: true);
                listaPosicoes.Focus();
                return;
            }

            Atleta atleta = new Atleta();
            atleta.SetId(_edicao?.AtletaId ?? 0);
            atleta.SetNome(nome);
            atleta.SetApelido(txtbNomeCamisa.Text.Trim());
            atleta.SetNumeroCamisa(camisa);
            atleta.SetDataNascimento(ObterNascimentoDaFicha());
            atleta.SetIdEquipe(equipeId);
            atleta.SetTitular(checkBoxAtletaTitular.Checked);

            bool salvo = false;
            DefinirOcupado(true);
            ExibirStatus("Salvando atleta…");
            try
            {
                int atletaId = DadosAtletas.Salvar(_usuario.GetId(), atleta, posicoes);
                salvo = true;
                _edicao = (atletaId, equipeId);
                _estadoOriginal = CapturarFicha();
                DataTable atletas = DadosAtletas.Listar(_usuario.GetId());
                _atletas = atletas;
                AplicarFiltros(atualizarFicha: true);
                ExibirStatus("Atleta salvo.", sucesso: true);
            }
            catch (ValidacaoException erro)
            {
                ExibirStatus(erro.Message, erro: true);
            }
            catch (Exception)
            {
                ExibirStatus(salvo ? "Atleta salvo. Reabra a tela para atualizar a lista." :
                    "Não foi possível salvar o atleta. Verifique a conexão e tente novamente.", erro: true);
            }
            finally
            {
                DefinirOcupado(false);
            }
        }

        private void btnExcluirAtleta_Click(object? sender, EventArgs e)
        {
            if (_usuario == null || _ocupado || !_edicao.HasValue || _estadoOriginal == null)
                return;
            (int AtletaId, int EquipeId) edicao = _edicao.Value;

            string mensagem = $"Tem certeza que quer excluir {_estadoOriginal.Nome}?\n\n" +
                $"A exclusão vale para o elenco de {cmbEquipeAtleta.Text}. " +
                "As táticas salvas manterão o vínculo e o número da camisa ficará reservado nesta equipe.";
            if (TemAlteracoesPendentes())
                mensagem += "\n\nAs alterações pendentes nesta ficha serão descartadas.";

            if (MessageBox.Show(this, mensagem, "Excluir atleta do elenco", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            DefinirOcupado(true);
            ExibirStatus("Excluindo atleta do elenco…");
            try
            {
                DadosAtletas.Excluir(_usuario.GetId(), edicao.AtletaId, edicao.EquipeId);

                DataRow? removida = null;
                foreach (DataRow linha in _atletas.Rows)
                {
                    if (IdentificarVinculo(linha) == edicao)
                    {
                        removida = linha;
                        break;
                    }
                }
                if (removida != null)
                    _atletas.Rows.Remove(removida);
                LimparFicha();
                AplicarFiltros();
                ExibirStatus("Atleta excluído do elenco.", sucesso: true);
            }
            catch (ValidacaoException erro)
            {
                ExibirStatus(erro.Message, erro: true);
            }
            catch (Exception)
            {
                ExibirStatus("Não foi possível concluir a exclusão. Verifique a conexão e atualize a tela.", erro: true);
            }
            finally
            {
                DefinirOcupado(false);
            }
        }

        private void DefinirOcupado(bool ocupado)
        {
            _ocupado = ocupado;
            UseWaitCursor = ocupado;
            AtualizarDisponibilidade();
        }

        private void AtualizarDisponibilidade()
        {
            bool disponivel = _dadosCarregados && !_ocupado;
            bool temEquipe = cmbEquipeAtleta.Items.Count > 1;
            dataGridViewAtletas.Enabled = disponivel;
            cmbEquipe.Enabled = disponivel;
            txtbBuscarAtleta.Enabled = disponivel;
            btnCadastrarAtletas.Enabled = disponivel && temEquipe;
            btnLimparAtleta.Enabled = disponivel && temEquipe;
            btnSalvarAtleta.Enabled = disponivel && temEquipe;
            btnExcluirAtleta.Enabled = disponivel && _edicao != null;
            cmbEquipeAtleta.Enabled = disponivel && temEquipe && _edicao == null;
            txtbNumCamisa.Enabled = disponivel && temEquipe;
            checkBoxAtletaTitular.Enabled = disponivel && temEquipe;

            bool editarDados = disponivel && temEquipe && _podeEditarDados;
            txtbNomeCompleto.ReadOnly = !editarDados;
            txtbNomeCamisa.ReadOnly = !editarDados;
            dateTimePicker1.Enabled = editarDados;
            listaPosicoes.Enabled = editarDados;
        }

        private void ExibirStatus(string texto, bool erro = false, bool sucesso = false)
        {
            lblStatusAtleta.ForeColor = erro ? Color.FromArgb(155, 39, 64) :
                sucesso ? Color.FromArgb(26, 103, 67) : Color.FromArgb(71, 85, 105);
            lblStatusAtleta.Text = texto;
            lblStatusAtleta.Visible = texto.Length > 0;
        }
    }
}
