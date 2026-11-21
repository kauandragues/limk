using Limk.VolleyBall.Backend;
using System.ComponentModel;
using System.Data;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaAtletas : UserControl, ITelaUsuario
    {
        private readonly CancellationTokenSource _encerramento = new();
        private readonly int _equipeInicialId;
        private UsuarioAutenticado? _usuario;
        private DataTable _atletas = new();
        private Button? _filtroAtivo;
        private string _posicaoFiltro = string.Empty;
        private (int AtletaId, int EquipeId)? _edicao;
        private DateTime? _nascimentoForaDaFaixa;
        private bool _podeEditarDados = true;
        private bool _ocupado;
        private bool _atualizandoGrid;
        private bool _dadosCarregados;
        private bool _selecaoAgendada;
        private EstadoFicha? _estadoOriginal;

        private sealed record EstadoFicha(string Nome, string Apelido, string Camisa, int EquipeId,
            DateTime? Nascimento, bool Titular, string Posicoes);

        private bool TemAlteracoesPendentes => _estadoOriginal is not null && _estadoOriginal != CapturarFicha();

        public TelaAtletas()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            ConfigurarGrid();
            txtbNomeCompleto.MaxLength = 100;
            txtbNomeCamisa.MaxLength = 50;
            txtbNumCamisa.MaxLength = 2;
            listaPosicoes.DisplayMember = nameof(PosicaoCadastro.Nome);

            Load += TelaAtletas_Load;
            Disposed += EncerrarOperacoes;
            cmbEquipe.SelectedIndexChanged += FiltroAlterado;
            txtbBuscarAtleta.TextChanged += FiltroAlterado;
            dataGridViewAtletas.SelectionChanged += AtletaSelecionado;
            dateTimePicker1.ValueChanged += (_, _) =>
            {
                if (_nascimentoForaDaFaixa is not null)
                {
                    _nascimentoForaDaFaixa = null;
                    bool marcada = dateTimePicker1.Checked;
                    dateTimePicker1.Format = DateTimePickerFormat.Short;
                    dateTimePicker1.Checked = marcada;
                }
            };
            btnLimparAtleta.Click += NovoCadastro;
            btnCadastrarAtletas.Click += NovoCadastro;
            btnSalvarAtleta.Click += btnSalvarAtleta_Click;
            btnExcluirAtleta.Click += btnExcluirAtleta_Click;
            btnFiltroTodosAtletas.Click += (_, _) => SelecionarFiltro(btnFiltroTodosAtletas, string.Empty);
            btnFiltroLevAtletas.Click += (_, _) => SelecionarFiltro(btnFiltroLevAtletas, "Levantador");
            btnFiltroPontAtleta.Click += (_, _) => SelecionarFiltro(btnFiltroPontAtleta, "Ponteiro");
            btnFiltroCentAtletas.Click += (_, _) => SelecionarFiltro(btnFiltroCentAtletas, "Central");
            btnFiltroLibeAtletas.Click += (_, _) => SelecionarFiltro(btnFiltroLibeAtletas, "Líbero");
            btnFiltroOpostoAtletas.Click += (_, _) => SelecionarFiltro(btnFiltroOpostoAtletas, "Oposto");

            SelecionarFiltro(btnFiltroTodosAtletas, string.Empty);
            LimparFicha();
        }

        public TelaAtletas(int equipeId) : this()
        {
            _equipeInicialId = equipeId;
        }

        private void EncerrarOperacoes(object? sender, EventArgs e)
        {
            Disposed -= EncerrarOperacoes;
            _encerramento.Cancel();
            _encerramento.Dispose();
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

            return !TemAlteracoesPendentes || MessageBox.Show(this,
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

        private async void TelaAtletas_Load(object? sender, EventArgs e)
        {
            if (_usuario is null || _ocupado || _dadosCarregados)
                return;

            CancellationToken token = _encerramento.Token;
            DefinirOcupado(true);
            ExibirStatus("Carregando equipes, posições e atletas…");
            try
            {
                Task<DataTable> equipes = DadosUsuario.CarregarEquipesAsync(_usuario.Id, token);
                Task<DataTable> atletas = DadosUsuario.CarregarAtletasAsync(_usuario.Id, token);
                Task<List<PosicaoCadastro>> posicoes = DadosAtletas.CarregarPosicoesAsync(token);
                await Task.WhenAll(equipes, atletas, posicoes);
                if (token.IsCancellationRequested)
                    return;

                CarregarOpcoes(await equipes, await posicoes);
                _atletas = await atletas;
                _dadosCarregados = true;
                AplicarFiltros();
                LimparFicha();
                ExibirStatus(cmbEquipe.Items.Count == 1 ? "Sua conta ainda não tem uma equipe vinculada." : string.Empty);
            }
            catch (Exception)
            {
                if (!token.IsCancellationRequested)
                    ExibirStatus("Não foi possível carregar os atletas. Verifique a conexão e reabra a tela.", erro: true);
            }
            finally
            {
                if (!token.IsCancellationRequested)
                    DefinirOcupado(false);
            }
        }

        private void CarregarOpcoes(DataTable equipes, List<PosicaoCadastro> posicoes)
        {
            var opcoes = equipes.AsEnumerable().Select(equipe =>
                new KeyValuePair<int, string>(equipe.Field<int>("id"), equipe.Field<string>("nome")!)).ToList();

            cmbEquipe.DisplayMember = cmbEquipeAtleta.DisplayMember = "Value";
            cmbEquipe.ValueMember = cmbEquipeAtleta.ValueMember = "Key";
            List<KeyValuePair<int, string>> filtros = [new(0, "Todas as minhas equipes"), .. opcoes];
            List<KeyValuePair<int, string>> equipesDaFicha = [new(0, "Selecione uma equipe"), .. opcoes];
            cmbEquipe.DataSource = filtros;
            cmbEquipeAtleta.DataSource = equipesDaFicha;
            if (_equipeInicialId > 0 && opcoes.Any(opcao => opcao.Key == _equipeInicialId))
                cmbEquipe.SelectedValue = _equipeInicialId;

            listaPosicoes.Items.Clear();
            listaPosicoes.Items.AddRange(posicoes.Cast<object>().ToArray());
        }

        private void FiltroAlterado(object? sender, EventArgs e)
        {
            if (_ocupado || !_dadosCarregados)
                return;

            AplicarFiltros();
            if (_edicao is null && ReferenceEquals(sender, cmbEquipe) && !TemAlteracoesPendentes)
            {
                SelecionarEquipeDoCadastro();
                _estadoOriginal = CapturarFicha();
            }
        }

        private void SelecionarFiltro(Button botao, string posicao)
        {
            if (_ocupado)
                return;

            if (_filtroAtivo is not null)
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
            var linhas = _atletas.AsEnumerable().Where(linha =>
                (equipeId == 0 || linha.Field<int>("equipe_id") == equipeId) &&
                ((linha.Field<string>("nome") ?? "").Contains(busca, StringComparison.OrdinalIgnoreCase) ||
                 (linha.Field<string>("apelido") ?? "").Contains(busca, StringComparison.OrdinalIgnoreCase)) &&
                (_posicaoFiltro.Length == 0 || (linha.Field<string>("posicoes") ?? "")
                    .Split(", ").Contains(_posicaoFiltro, StringComparer.OrdinalIgnoreCase))).ToList();

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
            if (selecao is not null && atualizarFicha)
                PreencherFicha(selecao);
            else if (selecao is null && _edicao is not null && !TemAlteracoesPendentes)
                LimparFicha();

            lblQtdAtletas.Text = linhas.Select(linha => linha.Field<int>("id")).Distinct().Count().ToString();
            label13.Text = linhas.Count == 0 ? "Nenhum atleta para os filtros selecionados." : cmbEquipe.Text;
            int posicoes = linhas.SelectMany(LerPosicaoIds).Distinct().Count();
            label14.Text = $"{posicoes} posições no elenco";
            lblPorcentagem.Text = $"{posicoes}/{listaPosicoes.Items.Count}";
        }

        private static IEnumerable<int> LerPosicaoIds(DataRow linha) =>
            (linha.Field<string>("posicao_ids") ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

        private static (int AtletaId, int EquipeId) IdentificarVinculo(DataRow linha) =>
            (linha.Field<int>("id"), linha.Field<int>("equipe_id"));

        private DataRowView? SelecionarFichaNoGrid()
        {
            dataGridViewAtletas.CurrentCell = null;
            dataGridViewAtletas.ClearSelection();
            if (_edicao is not { } edicao)
                return null;

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
            if (_ocupado || _atualizandoGrid || _selecaoAgendada || IsDisposed || !IsHandleCreated)
                return;

            // A alteração de CurrentCell precisa terminar antes de restaurar a seleção recusada.
            _selecaoAgendada = true;
            BeginInvoke(() =>
            {
                try
                {
                    ProcessarSelecaoAtleta();
                }
                finally
                {
                    _selecaoAgendada = false;
                }
            });
        }

        private void ProcessarSelecaoAtleta()
        {
            if (IsDisposed || _ocupado || dataGridViewAtletas.SelectedRows.Count != 1 ||
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
            if (dataLegada is { } dataOriginal)
            {
                // O calendário do Windows tem uma faixa menor que a coluna DATE do MySQL.
                dateTimePicker1.CustomFormat = $"'{dataOriginal:dd/MM/yyyy}'";
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
                listaPosicoes.SetItemChecked(i, posicoes.Contains(((PosicaoCadastro)listaPosicoes.Items[i]).Id));

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

        private IEnumerable<int> ObterPosicoesDaFicha() =>
            listaPosicoes.CheckedItems.Cast<PosicaoCadastro>().Select(posicao => posicao.Id);

        private DateTime? ObterNascimentoDaFicha() =>
            dateTimePicker1.Checked ? _nascimentoForaDaFaixa ?? dateTimePicker1.Value.Date : null;

        private EstadoFicha CapturarFicha() => new(txtbNomeCompleto.Text, txtbNomeCamisa.Text, txtbNumCamisa.Text,
            cmbEquipeAtleta.SelectedValue is int equipeId ? equipeId : 0, ObterNascimentoDaFicha(),
            checkBoxAtletaTitular.Checked, string.Join(",", ObterPosicoesDaFicha()));

        private void SelecionarEquipeDoCadastro()
        {
            if (cmbEquipeAtleta.Items.Count == 0)
                return;
            cmbEquipeAtleta.SelectedValue = cmbEquipe.SelectedValue is int id && id > 0 ? id : 0;
            if (cmbEquipeAtleta.SelectedIndex == 0 && cmbEquipeAtleta.Items.Count == 2)
                cmbEquipeAtleta.SelectedIndex = 1;
        }

        private async void btnSalvarAtleta_Click(object? sender, EventArgs e)
        {
            if (_usuario is null || _ocupado || !_dadosCarregados)
                return;

            string nome = txtbNomeCompleto.Text.Trim();
            if (nome.Length == 0)
            {
                ExibirStatus("Informe o nome do atleta.", erro: true);
                txtbNomeCompleto.Focus();
                return;
            }
            if (!int.TryParse(txtbNumCamisa.Text, out int camisa) || camisa is < 1 or > 99)
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
            if (_edicao is null && posicoes.Length == 0)
            {
                ExibirStatus("Selecione ao menos uma posição.", erro: true);
                listaPosicoes.Focus();
                return;
            }

            var atleta = new Atleta
            {
                Id = _edicao?.AtletaId ?? 0,
                Nome = nome,
                Apelido = txtbNomeCamisa.Text.Trim(),
                NumeroCamisa = camisa,
                DataNascimento = ObterNascimentoDaFicha(),
                IdEquipe = equipeId,
                Titular = checkBoxAtletaTitular.Checked
            };

            CancellationToken token = _encerramento.Token;
            bool salvo = false;
            DefinirOcupado(true);
            ExibirStatus("Salvando atleta…");
            try
            {
                int atletaId = await DadosAtletas.SalvarAsync(_usuario.Id, atleta, posicoes, token);
                if (token.IsCancellationRequested)
                    return;
                salvo = true;
                _edicao = (atletaId, equipeId);
                _estadoOriginal = CapturarFicha();
                DataTable atletas = await DadosUsuario.CarregarAtletasAsync(_usuario.Id, token);
                if (token.IsCancellationRequested)
                    return;
                _atletas = atletas;
                AplicarFiltros(atualizarFicha: true);
                ExibirStatus("Atleta salvo.", sucesso: true);
            }
            catch (FalhaAtletaException erro)
            {
                if (!token.IsCancellationRequested)
                    ExibirStatus(erro.Message, erro: true);
            }
            catch (Exception)
            {
                if (!token.IsCancellationRequested)
                    ExibirStatus(salvo ? "Atleta salvo. Reabra a tela para atualizar a lista." :
                        "Não foi possível salvar o atleta. Verifique a conexão e tente novamente.", erro: true);
            }
            finally
            {
                if (!token.IsCancellationRequested)
                    DefinirOcupado(false);
            }
        }

        private async void btnExcluirAtleta_Click(object? sender, EventArgs e)
        {
            if (_usuario is null || _ocupado || _edicao is not { } edicao || _estadoOriginal is not { } original)
                return;

            string mensagem = $"Tem certeza que quer excluir {original.Nome}?\n\n" +
                $"A exclusão vale para o elenco de {cmbEquipeAtleta.Text}. " +
                "As táticas salvas manterão o vínculo e o número da camisa ficará reservado nesta equipe.";
            if (TemAlteracoesPendentes)
                mensagem += "\n\nAs alterações pendentes nesta ficha serão descartadas.";

            if (MessageBox.Show(this, mensagem, "Excluir atleta do elenco", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            CancellationToken token = _encerramento.Token;
            DefinirOcupado(true);
            ExibirStatus("Excluindo atleta do elenco…");
            try
            {
                await DadosAtletas.ExcluirAsync(_usuario.Id, edicao.AtletaId, edicao.EquipeId, token);
                if (token.IsCancellationRequested)
                    return;

                DataRow? removida = _atletas.AsEnumerable().FirstOrDefault(linha => IdentificarVinculo(linha) == edicao);
                if (removida is not null)
                    _atletas.Rows.Remove(removida);
                LimparFicha();
                AplicarFiltros();
                ExibirStatus("Atleta excluído do elenco.", sucesso: true);
            }
            catch (FalhaAtletaException erro)
            {
                if (!token.IsCancellationRequested)
                    ExibirStatus(erro.Message, erro: true);
            }
            catch (Exception)
            {
                if (!token.IsCancellationRequested)
                    ExibirStatus("Não foi possível concluir a exclusão. Verifique a conexão e atualize a tela.", erro: true);
            }
            finally
            {
                if (!token.IsCancellationRequested)
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
            btnExcluirAtleta.Enabled = disponivel && _edicao is not null;
            cmbEquipeAtleta.Enabled = disponivel && temEquipe && _edicao is null;
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
