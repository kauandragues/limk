using Limk.VolleyBall.Backend;
using System.ComponentModel;
using System.Data;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaAtletas : UserControl
    {
        private readonly int _equipeInicialId;
        private UsuarioAutenticado? _usuario;
        private DataTable _atletas = new DataTable();
        private Button? _filtroAtivo;
        private string _posicaoFiltro = "";
        // Um atleta pode estar em várias equipes. Precisamos guardar os dois códigos da edição.
        private bool _editandoAtleta;
        private int _atletaEmEdicaoId;
        private int _equipeEmEdicaoId;
        private DateTime? _nascimentoForaDaFaixa;
        private bool _podeEditarDados = true;
        private bool _ocupado;
        private bool _atualizandoGrid;
        private bool _dadosCarregados;
        private EstadoFicha? _estadoOriginal;
        private bool TemAlteracoesPendentes()
        {
            return _estadoOriginal != null && !_estadoOriginal.TemMesmosValores(CapturarFicha());
        }

        public TelaAtletas()
        {
            PrepararTela();
        }

        public TelaAtletas(int equipeId)
        {
            PrepararTela();
            _equipeInicialId = equipeId;
        }

        private void PrepararTela()
        {
            InitializeComponent();
            // O Visual Studio também cria esta tela ao abrir o editor visual.
            // Nesse modo, o return encerra a preparação antes de configurar os dados e eventos da tela.
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            ConfigurarGrid();
            txtbNomeCompleto.MaxLength = 100;
            txtbNomeCamisa.MaxLength = 50;
            txtbNumCamisa.MaxLength = 2;
            // += liga um evento ao método que deve executar quando ele acontecer.
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
            SelecionarFiltro(btnFiltroTodosAtletas, "");
            LimparFicha();
        }

        private void NascimentoAlterado(object? sender, EventArgs e)
        {
            if (_nascimentoForaDaFaixa == null)
            {
                return;
            }

            _nascimentoForaDaFaixa = null;
            bool marcada = dateTimePicker1.Checked;
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Checked = marcada;
        }

        private void FiltroDePosicaoClicado(object? sender, EventArgs e)
        {
            Button? botao = sender as Button;
            if (botao != null)
            {
                string posicao = botao.Text;
                if (botao == btnFiltroTodosAtletas)
                {
                    posicao = "";
                }

                SelecionarFiltro(botao, posicao);
            }
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

            if (!TemAlteracoesPendentes())
            {
                return true;
            }

            DialogResult resposta = MessageBox.Show(this, "Você tem alterações sem salvar. Deseja descartá-las?", "Alterações sem salvar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            return resposta == DialogResult.Yes;
        }

        private void ConfigurarGrid()
        {
            dataGridViewAtletas.AutoGenerateColumns = false;
            AdicionarColuna(new DataGridViewTextBoxColumn(), "numero_camisa", "Camisa", 55, 60);
            AdicionarColuna(new DataGridViewTextBoxColumn(), "nome", "Nome", 105, 170);
            AdicionarColuna(new DataGridViewTextBoxColumn(), "apelido", "Apelido", 65, 95);
            DataGridViewColumn colunaNascimento = AdicionarColuna(
                new DataGridViewTextBoxColumn(), "data_nascimento", "Nascimento", 90, 105);
            colunaNascimento.DefaultCellStyle.Format = "dd/MM/yyyy";
            AdicionarColuna(new DataGridViewTextBoxColumn(), "equipe", "Equipe", 95, 135);
            AdicionarColuna(new DataGridViewCheckBoxColumn(), "titular", "Titular", 52, 58);
            AdicionarColuna(new DataGridViewTextBoxColumn(), "posicoes", "Posições", 88, 145);
            AjustarLayoutAtletas();
        }

        private DataGridViewColumn AdicionarColuna(DataGridViewColumn coluna, string campo, string titulo, int larguraMinima, float peso)
        {
            coluna.Name = campo;
            coluna.DataPropertyName = campo;
            coluna.HeaderText = titulo;
            coluna.MinimumWidth = LogicalToDeviceUnits(larguraMinima);
            // No modo Fill, distribui a largura proporcionalmente: quanto maior o peso, mais espaço a coluna recebe.
            coluna.FillWeight = peso;
            coluna.SortMode = DataGridViewColumnSortMode.Automatic;
            dataGridViewAtletas.Columns.Add(coluna);
            return coluna;
        }

        private void TelaAtletas_Load(object? sender, EventArgs e)
        {
            if (_usuario == null || _ocupado || _dadosCarregados)
            {
                return;
            }

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
                if (cmbEquipe.Items.Count == 1)
                {
                    ExibirStatus("Sua conta ainda não tem uma equipe vinculada.");
                }
                else
                {
                    ExibirStatus("");
                }
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
                string? nome = Convert.ToString(equipe["nome"]);
                if (nome == null)
                {
                    nome = "";
                }

                opcoes.Add(new KeyValuePair<int, string>(id, nome));
            }

            // DisplayMember escolhe o texto mostrado; ValueMember escolhe o código da opção.
            cmbEquipe.DisplayMember = "Value";
            cmbEquipeAtleta.DisplayMember = "Value";
            cmbEquipe.ValueMember = "Key";
            cmbEquipeAtleta.ValueMember = "Key";
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
            {
                listaPosicoes.Items.Add(posicao);
            }
        }

        private void FiltroAlterado(object? sender, EventArgs e)
        {
            if (_ocupado || !_dadosCarregados)
            {
                return;
            }

            AplicarFiltros();
            if (!_editandoAtleta && sender == cmbEquipe && !TemAlteracoesPendentes())
            {
                SelecionarEquipeDoCadastro();
                _estadoOriginal = CapturarFicha();
            }
        }

        private void SelecionarFiltro(Button botao, string posicao)
        {
            if (_ocupado)
            {
                return;
            }

            if (_filtroAtivo != null)
            {
                _filtroAtivo.BackColor = Color.FromArgb(101, 117, 140);
            }

            _filtroAtivo = botao;
            botao.BackColor = Color.FromArgb(123, 57, 237);
            _posicaoFiltro = posicao;
            AplicarFiltros();
        }

        private void AplicarFiltros(bool atualizarFicha = false)
        {
            if (!_dadosCarregados)
            {
                return;
            }

            int equipeId = 0;
            if (cmbEquipe.SelectedValue is int)
            {
                equipeId = (int)cmbEquipe.SelectedValue;
            }

            string busca = txtbBuscarAtleta.Text.Trim();
            List<DataRow> linhas = new List<DataRow>();
            List<int> atletasVisiveis = new List<int>();
            List<int> posicoesVisiveis = new List<int>();
            foreach (DataRow linha in _atletas.Rows)
            {
                if (equipeId != 0 && linha.Field<int>("equipe_id") != equipeId)
                {
                    continue;
                }

                string? nome = linha.Field<string>("nome");
                if (nome == null)
                {
                    nome = "";
                }

                string? apelido = linha.Field<string>("apelido");
                if (apelido == null)
                {
                    apelido = "";
                }

                if (!nome.Contains(busca, StringComparison.OrdinalIgnoreCase) && !apelido.Contains(busca, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (!PassaPeloFiltroDePosicao(linha))
                {
                    continue;
                }

                linhas.Add(linha);
                int atletaId = linha.Field<int>("id");
                if (!atletasVisiveis.Contains(atletaId))
                {
                    atletasVisiveis.Add(atletaId);
                }

                foreach (int posicaoId in LerPosicaoIds(linha))
                {
                    if (!posicoesVisiveis.Contains(posicaoId))
                    {
                        posicoesVisiveis.Add(posicaoId);
                    }
                }
            }

            _atualizandoGrid = true;
            DataRowView? selecao = null;
            try
            {
                DataTable tabelaFiltrada = _atletas.Clone();
                foreach (DataRow linha in linhas)
                {
                    tabelaFiltrada.ImportRow(linha);
                }

                dataGridViewAtletas.DataSource = tabelaFiltrada;
                selecao = SelecionarFichaNoGrid();
            }
            finally
            {
                _atualizandoGrid = false;
            }

            if (selecao != null && atualizarFicha)
            {
                PreencherFicha(selecao);
            }
            else if (selecao == null && _editandoAtleta && !TemAlteracoesPendentes())
            {
                LimparFicha();
            }

            lblQtdAtletas.Text = atletasVisiveis.Count.ToString();
            if (linhas.Count == 0)
            {
                label13.Text = "Nenhum atleta para os filtros selecionados.";
            }
            else
            {
                label13.Text = cmbEquipe.Text;
            }

            int posicoes = posicoesVisiveis.Count;
            label14.Text = $"{posicoes} posições no elenco";
            lblPorcentagem.Text = $"{posicoes}/{listaPosicoes.Items.Count}";
        }

        private bool PassaPeloFiltroDePosicao(DataRow linha)
        {
            if (_posicaoFiltro.Length == 0)
            {
                return true;
            }

            string? nomesDasPosicoes = linha.Field<string>("posicoes");
            if (nomesDasPosicoes == null)
            {
                return false;
            }

            foreach (string posicao in nomesDasPosicoes.Split(", "))
            {
                // OrdinalIgnoreCase compara os textos sem diferenciar maiúsculas de minúsculas.
                if (string.Equals(posicao, _posicaoFiltro, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static List<int> LerPosicaoIds(DataRow linha)
        {
            string? texto = linha.Field<string>("posicao_ids");
            if (texto == null)
            {
                texto = "";
            }

            List<int> identificadores = new List<int>();
            foreach (string numero in texto.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                identificadores.Add(int.Parse(numero));
            }

            return identificadores;
        }

        private bool EhAtletaEmEdicao(DataRow linha)
        {
            return _editandoAtleta && linha.Field<int>("id") == _atletaEmEdicaoId && linha.Field<int>("equipe_id") == _equipeEmEdicaoId;
        }

        private DataRowView? SelecionarFichaNoGrid()
        {
            dataGridViewAtletas.CurrentCell = null;
            dataGridViewAtletas.ClearSelection();
            if (!_editandoAtleta)
            {
                return null;
            }

            foreach (DataGridViewRow linha in dataGridViewAtletas.Rows)
            {
                // as tenta converter o registro. Se o objeto for de outro tipo, o resultado é null.
                DataRowView? registro = linha.DataBoundItem as DataRowView;
                if (registro == null || !EhAtletaEmEdicao(registro.Row))
                {
                    continue;
                }

                dataGridViewAtletas.CurrentCell = linha.Cells["nome"];
                linha.Selected = true;
                return registro;
            }

            return null;
        }

        private void AtletaSelecionado(object? sender, EventArgs e)
        {
            if (_atualizandoGrid || _ocupado || dataGridViewAtletas.SelectedRows.Count != 1)
            {
                return;
            }

            DataRowView? registro = dataGridViewAtletas.SelectedRows[0].DataBoundItem as DataRowView;
            if (registro == null || EhAtletaEmEdicao(registro.Row))
            {
                return;
            }

            if (!ConfirmarSaida())
            {
                SelecionarFichaNoGrid();
                return;
            }

            PreencherFicha(registro);
            if (_podeEditarDados)
            {
                ExibirStatus("");
            }
            else
            {
                ExibirStatus("Atleta compartilhado. Você pode editar a camisa e a titularidade desta equipe.");
            }
        }

        private void PreencherFicha(DataRowView registro)
        {
            _editandoAtleta = true;
            _atletaEmEdicaoId = registro.Row.Field<int>("id");
            _equipeEmEdicaoId = registro.Row.Field<int>("equipe_id");
            _podeEditarDados = Convert.ToBoolean(registro["pode_editar_dados"]);
            txtbNomeCompleto.Text = registro["nome"].ToString();
            txtbNomeCamisa.Text = registro["apelido"].ToString();
            txtbNumCamisa.Text = registro["numero_camisa"].ToString();
            cmbEquipeAtleta.SelectedValue = _equipeEmEdicaoId;
            checkBoxAtletaTitular.Checked = Convert.ToBoolean(registro["titular"]);
            DateTime? nascimento = registro.Row.Field<DateTime?>("data_nascimento");
            DateTime exibido = DateTime.Today;
            if (nascimento.HasValue)
            {
                exibido = nascimento.Value;
            }

            if (exibido < dateTimePicker1.MinDate)
            {
                exibido = dateTimePicker1.MinDate;
            }

            if (exibido > dateTimePicker1.MaxDate)
            {
                exibido = dateTimePicker1.MaxDate;
            }

            DateTime? dataLegada;
            if (nascimento.HasValue && nascimento.Value != exibido)
            {
                dataLegada = nascimento;
            }
            else
            {
                dataLegada = null;
            }

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
            List<int> posicoes = LerPosicaoIds(registro.Row);
            for (int i = 0; i < listaPosicoes.Items.Count; i++)
            {
                listaPosicoes.SetItemChecked(i, posicoes.Contains(((PosicaoCadastro)listaPosicoes.Items[i]).GetId()));
            }

            label2.Text = "Editar atleta";
            _estadoOriginal = CapturarFicha();
            AtualizarDisponibilidade();
        }

        private void NovoCadastro(object? sender, EventArgs e)
        {
            if (!ConfirmarSaida())
            {
                return;
            }

            dataGridViewAtletas.ClearSelection();
            dataGridViewAtletas.CurrentCell = null;
            LimparFicha();
            ExibirStatus("");
            if (sender == btnCadastrarAtletas)
            {
                panel1.ScrollControlIntoView(panelFichaAtleta);
            }

            txtbNomeCompleto.Focus();
        }

        private void LimparFicha()
        {
            _editandoAtleta = false;
            _atletaEmEdicaoId = 0;
            _equipeEmEdicaoId = 0;
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
            {
                listaPosicoes.SetItemChecked(i, false);
            }

            label2.Text = "Novo atleta";
            _estadoOriginal = CapturarFicha();
            AtualizarDisponibilidade();
        }

        private List<int> ObterPosicoesDaFicha()
        {
            List<int> posicoes = new List<int>();
            foreach (PosicaoCadastro posicao in listaPosicoes.CheckedItems)
            {
                posicoes.Add(posicao.GetId());
            }

            return posicoes;
        }

        private DateTime? ObterNascimentoDaFicha()
        {
            if (!dateTimePicker1.Checked)
            {
                return null;
            }

            if (_nascimentoForaDaFaixa.HasValue)
            {
                return _nascimentoForaDaFaixa.Value;
            }

            return dateTimePicker1.Value.Date;
        }

        private EstadoFicha CapturarFicha()
        {
            int equipeId = 0;
            if (cmbEquipeAtleta.SelectedValue is int)
            {
                equipeId = (int)cmbEquipeAtleta.SelectedValue;
            }

            return new EstadoFicha(
                txtbNomeCompleto.Text,
                txtbNomeCamisa.Text,
                txtbNumCamisa.Text,
                equipeId,
                ObterNascimentoDaFicha(),
                checkBoxAtletaTitular.Checked,
                string.Join(",", ObterPosicoesDaFicha()));
        }

        private void SelecionarEquipeDoCadastro()
        {
            if (cmbEquipeAtleta.Items.Count == 0)
            {
                return;
            }

            int equipeId = 0;
            if (cmbEquipe.SelectedValue is int)
            {
                int idSelecionado = (int)cmbEquipe.SelectedValue;
                if (idSelecionado > 0)
                {
                    equipeId = idSelecionado;
                }
            }

            cmbEquipeAtleta.SelectedValue = equipeId;
            if (cmbEquipeAtleta.SelectedIndex == 0 && cmbEquipeAtleta.Items.Count == 2)
            {
                cmbEquipeAtleta.SelectedIndex = 1;
            }
        }

        private void btnSalvarAtleta_Click(object? sender, EventArgs e)
        {
            if (_usuario == null || _ocupado || !_dadosCarregados)
            {
                return;
            }

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

            int equipeId = 0;
            if (cmbEquipeAtleta.SelectedValue is int)
            {
                equipeId = (int)cmbEquipeAtleta.SelectedValue;
            }

            if (equipeId <= 0)
            {
                ExibirStatus("Selecione a equipe do atleta na ficha.", erro: true);
                cmbEquipeAtleta.Focus();
                return;
            }

            List<int> posicoes = ObterPosicoesDaFicha();
            if (!_editandoAtleta && posicoes.Count == 0)
            {
                ExibirStatus("Selecione ao menos uma posição.", erro: true);
                listaPosicoes.Focus();
                return;
            }

            Atleta atleta = new Atleta(
                _atletaEmEdicaoId,
                nome,
                txtbNomeCamisa.Text.Trim(),
                camisa,
                ObterNascimentoDaFicha(),
                equipeId,
                checkBoxAtletaTitular.Checked);
            bool salvo = false;
            DefinirOcupado(true);
            ExibirStatus("Salvando atleta…");
            try
            {
                int atletaId = DadosAtletas.Salvar(_usuario.GetId(), atleta, posicoes);
                salvo = true;
                _editandoAtleta = true;
                _atletaEmEdicaoId = atletaId;
                _equipeEmEdicaoId = equipeId;
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
                if (salvo)
                {
                    ExibirStatus("Atleta salvo. Reabra a tela para atualizar a lista.", erro: true);
                }
                else
                {
                    ExibirStatus("Não foi possível salvar o atleta. Verifique a conexão e tente novamente.", erro: true);
                }
            }
            finally
            {
                DefinirOcupado(false);
            }
        }

        private void btnExcluirAtleta_Click(object? sender, EventArgs e)
        {
            if (_usuario == null || _ocupado || !_editandoAtleta || _estadoOriginal == null)
            {
                return;
            }

            string mensagem = $"Tem certeza que quer excluir {_estadoOriginal.Nome}?\n\n" +
                $"A exclusão vale para o elenco de {cmbEquipeAtleta.Text}. " +
                "As táticas salvas manterão o vínculo e o número da camisa ficará reservado nesta equipe.";
            if (TemAlteracoesPendentes())
            {
                mensagem += "\n\nAs alterações pendentes nesta ficha serão descartadas.";
            }

            if (MessageBox.Show(this, mensagem, "Excluir atleta do elenco", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            DefinirOcupado(true);
            ExibirStatus("Excluindo atleta do elenco…");
            try
            {
                DadosAtletas.Excluir(_usuario.GetId(), _atletaEmEdicaoId, _equipeEmEdicaoId);
                DataRow? removida = null;
                foreach (DataRow linha in _atletas.Rows)
                {
                    if (EhAtletaEmEdicao(linha))
                    {
                        removida = linha;
                        break;
                    }
                }

                if (removida != null)
                {
                    _atletas.Rows.Remove(removida);
                }

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
            btnExcluirAtleta.Enabled = disponivel && _editandoAtleta;
            cmbEquipeAtleta.Enabled = disponivel && temEquipe && !_editandoAtleta;
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
            if (erro)
            {
                lblStatusAtleta.ForeColor = Color.FromArgb(155, 39, 64);
            }
            else
            {
                if (sucesso)
                {
                    lblStatusAtleta.ForeColor = Color.FromArgb(26, 103, 67);
                }
                else
                {
                    lblStatusAtleta.ForeColor = Color.FromArgb(71, 85, 105);
                }
            }

            lblStatusAtleta.Text = texto;
            lblStatusAtleta.Visible = texto.Length > 0;
        }
    }
}
