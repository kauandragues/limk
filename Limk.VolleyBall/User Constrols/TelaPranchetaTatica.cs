using Limk.VolleyBall.Backend;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaPranchetaTatica : UserControl
    {
        private UsuarioAutenticado? _usuario;
        private Tatica _tatica = new Tatica();
        private int _indiceFaseSelecionada;
        private PreferenciasUsuario _preferencias = PreferenciasUsuario.CriarPadrao();
        private readonly int _taticaParaAbrir;
        private readonly bool _apresentarAoAbrir;
        private DataTable _equipes = new DataTable();
        private DataTable _tipos = new DataTable();
        private DataTable _sistemas = new DataTable();
        private List<AtletaDaEquipe> _elenco = new List<AtletaDaEquipe>();
        private bool _pronto;
        private bool _operacao;
        private bool _apresentando;
        private bool _emTransicao;
        private int _indiceFaseDestino = -1;
        private int _duracaoTransicaoMs;
        private readonly Stopwatch _tempoTransicao = new Stopwatch();
        private readonly List<MovimentoElemento> _movimentos = new List<MovimentoElemento>();
        private bool _atualizandoPropriedades;
        public TelaPranchetaTatica()
        {
            PrepararTela();
        }

        public TelaPranchetaTatica(int taticaId, bool apresentar = false)
        {
            PrepararTela();
            if (taticaId <= 0)
            {
                throw new ArgumentOutOfRangeException("taticaId");
            }

            _taticaParaAbrir = taticaId;
            _apresentarAoAbrir = apresentar;
        }

        private void PrepararTela()
        {
            InitializeComponent();
            // No designer do Visual Studio, o return encerra a preparação antes de criar a fase inicial da tática.
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            QuadroTatica novaFase = new QuadroTatica("Fase 1");
            _tatica.GetQuadros().Add(novaFase);
            AtualizarPropriedades();
            AtualizarSequenciaVisual();
        }

        public void DefinirUsuario(UsuarioAutenticado usuario)
        {
            _usuario = usuario;
            labelNomeTatica.Text = "Nova tática";
        }

        private void TelaPranchetaTatica_Load(object? sender, EventArgs e)
        {
            // Evita carregar a tática no designer do Visual Studio, sem usuário, com a tela já pronta ou durante uma operação.
            if (_usuario == null || _pronto || _operacao || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            _operacao = true;
            AtualizarAcoes();
            MostrarStatus("Carregando a tática e suas equipes...");
            try
            {
                _equipes = DadosEquipe.Listar(_usuario.GetId());
                _tipos = DadosPrancheta.CarregarTipos();
                _sistemas = DadosPrancheta.CarregarSistemas();
                try
                {
                    _preferencias = DadosPreferenciasUsuario.Carregar(_usuario.GetId());
                }
                catch (BancoDeDadosException)
                {
                    _preferencias = PreferenciasUsuario.CriarPadrao();
                }

                prancheta.AplicarPreferencias(_preferencias);
                if (_taticaParaAbrir > 0)
                {
                    _tatica = DadosPrancheta.Carregar(_usuario.GetId(), _taticaParaAbrir);
                }
                else
                {
                    if (_equipes.Rows.Count > 0)
                    {
                        _tatica.SetEquipeId(Convert.ToInt32(_equipes.Rows[0]["id"]));
                    }

                    _tatica.GetQuadros()[0].SetDuracaoMs(_preferencias.GetDuracaoQuadroPadraoMs());
                }

                if (_tatica.GetQuadros().Count == 0)
                {
                    QuadroTatica novaFase = new QuadroTatica("Fase 1", _preferencias.GetDuracaoQuadroPadraoMs());
                    _tatica.GetQuadros().Add(novaFase);
                }

                if (_tatica.GetEquipeId() > 0)
                {
                    _elenco = DadosPrancheta.CarregarElenco(_usuario.GetId(), _tatica.GetEquipeId());
                }
                else
                {
                    _elenco.Clear();
                }

                PrepararCamposTatica();
                _indiceFaseSelecionada = 0;
                MostrarFaseAtual();
                _pronto = true;
                if (_equipes.Rows.Count == 0)
                {
                    MostrarStatus("Seu usuário não possui equipe vinculada para salvar táticas.");
                }
                else
                {
                    MostrarStatus("Sem um elemento selecionado, edite a tática e a fase nas propriedades à direita.");
                }
            }
            catch (UnauthorizedAccessException erro)
            {
                MostrarStatus(erro.Message, true);
            }
            catch (Exception)
            {
                MostrarStatus("Não foi possível carregar a prancheta. Verifique a conexão e abra a tela novamente.", true);
            }
            finally
            {
                _operacao = false;
                AtualizarSequenciaVisual();
            }

            if (_pronto && _apresentarAoAbrir && !IsDisposed)
            {
                AlternarApresentacao();
            }
        }

        private void MostrarStatus(string mensagem, bool erro = false)
        {
            if (erro)
            {
                labelStatusPrancheta.ForeColor = Color.FromArgb(180, 40, 60);
            }
            else
            {
                labelStatusPrancheta.ForeColor = Color.FromArgb(100, 116, 139);
            }

            labelStatusPrancheta.Text = mensagem;
        }

        // A quadra é a edição da fase selecionada. Antes de trocar de fase, copiamos seus elementos.
        private void GuardarFaseAtual()
        {
            if (!_pronto || _emTransicao || _apresentando || _indiceFaseSelecionada >= _tatica.GetQuadros().Count)
            {
                return;
            }

            GuardarDadosTatica();
            QuadroTatica quadro = _tatica.GetQuadros()[_indiceFaseSelecionada];
            quadro.GetAtletas().Clear();
            quadro.SetBola(null);
            foreach (ElementoQuadra elemento in prancheta.GetElementos())
            {
                if (elemento is PosicaoBola)
                {
                    PosicaoBola bola = (PosicaoBola)elemento;
                    quadro.SetBola(bola.Copiar());
                }
                else if (elemento is AtletaQuadro)
                {
                    AtletaQuadro atleta = (AtletaQuadro)elemento;
                    quadro.GetAtletas().Add(atleta.Copiar());
                }
            }

            AtualizarSequenciaVisual();
        }

        private void MostrarFaseAtual()
        {
            prancheta.SelecionarElemento(null);
            prancheta.GetElementos().Clear();
            QuadroTatica quadro = _tatica.GetQuadros()[_indiceFaseSelecionada];
            foreach (AtletaQuadro salvo in quadro.GetAtletas())
            {
                prancheta.GetElementos().Add(salvo.Copiar());
            }

            PosicaoBola? bola = quadro.GetBola();
            if (bola != null)
            {
                prancheta.GetElementos().Add(bola.Copiar());
            }

            labelNomeTatica.Text = _tatica.GetNome();
            PreencherDadosTatica();
            AtualizarPropriedades();
            AtualizarSequenciaVisual();
            prancheta.Invalidate();
        }

        private void PrepararCamposTatica()
        {
            // Preencher os campos também dispara eventos. Esta variável evita tratar isso como edição do usuário.
            _atualizandoPropriedades = true;
            comboEquipeTatica.DisplayMember = "nome";
            comboEquipeTatica.ValueMember = "id";
            comboEquipeTatica.DataSource = _equipes.Copy();
            PreencherOpcional(comboTipoTatica, _tipos);
            PreencherOpcional(comboSistemaTatica, _sistemas);
            _atualizandoPropriedades = false;
        }

        private static void PreencherOpcional(ComboBox combo, DataTable origem)
        {
            DataTable opcoes = origem.Copy();
            DataRow vazio = opcoes.NewRow();
            vazio["id"] = 0;
            vazio["nome"] = "Sem definição";
            opcoes.Rows.InsertAt(vazio, 0);
            combo.DisplayMember = "nome";
            combo.ValueMember = "id";
            combo.DataSource = opcoes;
        }

        private void PreencherDadosTatica()
        {
            _atualizandoPropriedades = true;
            QuadroTatica fase = _tatica.GetQuadros()[_indiceFaseSelecionada];
            textNomeTatica.Text = _tatica.GetNome();
            textDescricaoTatica.Text = _tatica.GetDescricao();
            comboEquipeTatica.SelectedValue = _tatica.GetEquipeId();
            // GetValueOrDefault devolve 0 quando o número opcional não foi informado.
            comboTipoTatica.SelectedValue = _tatica.GetTipoTaticaId().GetValueOrDefault();
            comboSistemaTatica.SelectedValue = _tatica.GetSistemaJogoId().GetValueOrDefault();
            textNomeFase.Text = fase.GetNome();
            numericDuracaoFase.Value = Math.Clamp(fase.GetDuracaoMs(), numericDuracaoFase.Minimum, numericDuracaoFase.Maximum);
            comboRotacaoFase.SelectedIndex = fase.GetRotacao().GetValueOrDefault();
            _atualizandoPropriedades = false;
        }

        private int? LerOpcaoDoCatalogo(ComboBox lista)
        {
            if (lista.SelectedValue is int)
            {
                int codigo = (int)lista.SelectedValue;
                if (codigo > 0)
                {
                    return codigo;
                }
            }

            return null;
        }

        private void GuardarDadosTatica()
        {
            // Os campos permanecem preenchidos ao selecionar um atleta ou a bola.
            // Também guardamos o que foi digitado antes de salvar ou trocar de fase.
            _tatica.SetNome(textNomeTatica.Text.Trim());
            _tatica.SetDescricao(textDescricaoTatica.Text.Trim());
            _tatica.SetTipoTaticaId(LerOpcaoDoCatalogo(comboTipoTatica));
            _tatica.SetSistemaJogoId(LerOpcaoDoCatalogo(comboSistemaTatica));
            QuadroTatica fase = _tatica.GetQuadros()[_indiceFaseSelecionada];
            fase.SetNome(textNomeFase.Text.Trim());
            fase.SetDuracaoMs((int)numericDuracaoFase.Value);
            if (comboRotacaoFase.SelectedIndex > 0)
            {
                fase.SetRotacao(comboRotacaoFase.SelectedIndex);
            }
            else
            {
                fase.SetRotacao(null);
            }

            labelNomeTatica.Text = _tatica.GetNome();
        }

        private void comboEquipeTatica_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_atualizandoPropriedades || !PodeEditarFase() || _tatica.GetId() > 0 || _usuario == null)
            {
                return;
            }

            if (!(comboEquipeTatica.SelectedValue is int))
            {
                return;
            }

            int equipeId = (int)comboEquipeTatica.SelectedValue;
            if (equipeId <= 0 || equipeId == _tatica.GetEquipeId())
            {
                return;
            }

            GuardarFaseAtual();
            _operacao = true;
            AtualizarAcoes();
            try
            {
                List<AtletaDaEquipe> novoElenco = DadosPrancheta.CarregarElenco(_usuario.GetId(), equipeId);
                // Só troca os vínculos da edição depois de confirmar o acesso ao novo elenco.
                _tatica.SetEquipeId(equipeId);
                _elenco = novoElenco;
                foreach (QuadroTatica quadro in _tatica.GetQuadros())
                {
                    foreach (AtletaQuadro atleta in quadro.GetAtletas())
                    {
                        atleta.SetEquipeAtletaId(null);
                        atleta.SetNome($"Atleta {atleta.GetNumeroCamisa()}");
                    }
                }

                MostrarStatus("Equipe atualizada na edição. Clique em Salvar tática para gravar no banco.");
            }
            catch (UnauthorizedAccessException erro)
            {
                MostrarStatus(erro.Message, true);
            }
            catch (BancoDeDadosException)
            {
                MostrarStatus("Não foi possível carregar o elenco da equipe.", true);
            }
            finally
            {
                _operacao = false;
                MostrarFaseAtual();
            }
        }

        private void btnSalvarTatica_Click(object? sender, EventArgs e)
        {
            if (!PodeEditarFase() || _usuario == null || _equipes.Rows.Count == 0)
            {
                return;
            }

            GuardarFaseAtual();
            _operacao = true;
            AtualizarAcoes();
            MostrarStatus("Salvando a tática e suas fases...");
            try
            {
                int id = DadosPrancheta.Salvar(_usuario.GetId(), _tatica);
                _tatica.SetId(id);
                MostrarStatus("Tática salva no banco de dados.");
            }
            catch (ValidacaoException erro)
            {
                prancheta.SelecionarElemento(null);
                MostrarStatus(erro.Message, true);
            }
            catch (UnauthorizedAccessException erro)
            {
                MostrarStatus(erro.Message, true);
            }
            catch (Exception)
            {
                MostrarStatus("Não foi possível salvar. A edição foi mantida; verifique a conexão e tente novamente.", true);
            }
            finally
            {
                _operacao = false;
                AtualizarAcoes();
            }
        }

        private bool PodeEditarFase()
        {
            return _pronto && !_operacao && !_apresentando && !_emTransicao;
        }

        private void btnAdicionarBola_Click(object sender, EventArgs e)
        {
            if (!PodeEditarFase())
            {
                return;
            }

            foreach (ElementoQuadra elemento in prancheta.GetElementos())
            {
                if (elemento is PosicaoBola)
                {
                    prancheta.SelecionarElemento(elemento);
                    return;
                }
            }

            PosicaoBola bola = new PosicaoBola(0.5f, 0.5f);
            prancheta.GetElementos().Add(bola);
            prancheta.SelecionarElemento(bola);
        }

        private void btnAdicionarAtleta_Click(object sender, EventArgs e)
        {
            if (!PodeEditarFase())
            {
                return;
            }

            int numero = 1;
            while (numero <= 99 && AtletaJaUsadoNaFase(numero, LadoAtleta.Principal, null, null))
            {
                numero++;
            }

            if (numero > 99)
            {
                MostrarStatus("Não há números disponíveis para outro atleta deste lado.", true);
                return;
            }

            AtletaQuadro posicao = new AtletaQuadro(null, $"Atleta {numero}", numero, LadoAtleta.Principal, 0.5f, 0.5f);
            prancheta.GetElementos().Add(posicao);
            prancheta.SelecionarElemento(posicao);
        }

        private void prancheta_SelecaoAlterada(object? sender, EventArgs e)
        {
            AtualizarPropriedades();
        }

        private void prancheta_PosicaoAlterada(object? sender, EventArgs e)
        {
            AtualizarCoordenadas();
        }

        private void AtualizarPropriedades()
        {
            _atualizandoPropriedades = true;
            ElementoQuadra? elemento = prancheta.GetElementoSelecionado();
            layoutDadosTatica.Visible = elemento == null;
            layoutCamposAtleta.Visible = elemento is AtletaQuadro;
            labelTituloCoordenadas.Visible = elemento != null;
            layoutCoordenadas.Visible = elemento != null;
            btnRemoverElemento.Visible = elemento != null;
            textNumeroCamisa.Clear();
            comboAtletaVinculado.Items.Clear();
            comboAtletaVinculado.Items.Add(new AtletaDaEquipe());
            foreach (AtletaDaEquipe atleta in _elenco)
            {
                comboAtletaVinculado.Items.Add(atleta);
            }

            comboAtletaVinculado.SelectedIndex = 0;
            comboLadoAtleta.SelectedIndex = 0;
            if (elemento is AtletaQuadro)
            {
                AtletaQuadro posicao = (AtletaQuadro)elemento;
                int? vinculoId = posicao.GetEquipeAtletaId();
                AtletaDaEquipe? vinculo = null;
                foreach (AtletaDaEquipe atletaDoElenco in _elenco)
                {
                    if (atletaDoElenco.GetEquipeAtletaId() == vinculoId)
                    {
                        vinculo = atletaDoElenco;
                        break;
                    }
                }

                if (vinculo == null && vinculoId.HasValue)
                {
                    vinculo = new AtletaDaEquipe(vinculoId.Value, posicao.GetNome(), posicao.GetNumeroCamisa());
                    comboAtletaVinculado.Items.Add(vinculo);
                }

                if (vinculo != null)
                {
                    comboAtletaVinculado.SelectedItem = vinculo;
                }

                textNumeroCamisa.Text = posicao.GetNumeroCamisa().ToString();
                labelResumoElemento.Text = $"#{posicao.GetNumeroCamisa()}  •  ATLETA\n{posicao.GetNome()}";
                if (posicao.GetLado() == LadoAtleta.Adversario)
                {
                    comboLadoAtleta.SelectedIndex = 1;
                }
            }
            else if (elemento is PosicaoBola)
            {
                labelResumoElemento.Text = "BOLA\nPosição na quadra";
            }
            else
            {
                labelResumoElemento.Text = $"TÁTICA E FASE\nFase {_indiceFaseSelecionada + 1:00}";
            }

            if (elemento == null)
            {
                labelMensagemPropriedades.Text = "Clique em Salvar Tática para gravar os dados e as fases no banco.";
            }
            else
            {
                labelMensagemPropriedades.Text = "As alterações pertencem somente à fase selecionada.";
            }

            labelMensagemPropriedades.ForeColor = Color.FromArgb(100, 116, 139);
            _atualizandoPropriedades = false;
            AtualizarCoordenadas();
            AtualizarAcoes();
        }

        private void AtualizarCoordenadas()
        {
            ElementoQuadra? elemento = prancheta.GetElementoSelecionado();
            if (elemento == null)
            {
                numericCoordenadaX.Value = 0;
                numericCoordenadaY.Value = 0;
                return;
            }

            // As coordenadas de 0 a 1 viram porcentagens de 0 a 100 nos campos da tela.
            numericCoordenadaX.Value = (decimal)Math.Clamp(elemento.GetX(), 0f, 1f) * 100m;
            numericCoordenadaY.Value = (decimal)Math.Clamp(elemento.GetY(), 0f, 1f) * 100m;
        }

        private void comboAtletaVinculado_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AtletaDaEquipe? atleta = comboAtletaVinculado.SelectedItem as AtletaDaEquipe;
            if (!_atualizandoPropriedades && atleta != null && atleta.GetEquipeAtletaId() > 0)
            {
                textNumeroCamisa.Text = atleta.GetNumeroCamisa().ToString();
            }
        }

        private void btnAplicarPropriedades_Click(object? sender, EventArgs e)
        {
            if (!PodeEditarFase())
            {
                return;
            }

            ElementoQuadra? elemento = prancheta.GetElementoSelecionado();
            if (elemento == null)
            {
                GuardarFaseAtual();
                try
                {
                    _tatica.Validar();
                    labelMensagemPropriedades.Text = "Dados aplicados na edição. Clique em Salvar Tática para gravar.";
                    labelMensagemPropriedades.ForeColor = Color.FromArgb(100, 116, 139);
                }
                catch (ValidacaoException erro)
                {
                    labelMensagemPropriedades.Text = erro.Message;
                    labelMensagemPropriedades.ForeColor = Color.FromArgb(180, 40, 60);
                }

                return;
            }

            if (elemento is AtletaQuadro)
            {
                AtletaQuadro posicao = (AtletaQuadro)elemento;
                if (!int.TryParse(textNumeroCamisa.Text, out int numero) || numero < 1 || numero > 99)
                {
                    labelMensagemPropriedades.Text = "Informe um número entre 1 e 99.";
                    return;
                }

                LadoAtleta lado = LadoAtleta.Principal;
                if (comboLadoAtleta.SelectedIndex == 1)
                {
                    lado = LadoAtleta.Adversario;
                }

                AtletaDaEquipe? escolhido = comboAtletaVinculado.SelectedItem as AtletaDaEquipe;
                int? vinculo = null;
                string nome = $"Atleta {numero}";
                if (escolhido != null && escolhido.GetEquipeAtletaId() > 0)
                {
                    vinculo = escolhido.GetEquipeAtletaId();
                    nome = escolhido.GetNome();
                }

                if (AtletaJaUsadoNaFase(numero, lado, vinculo, posicao))
                {
                    labelMensagemPropriedades.Text = "O número ou o atleta já está em uso nesta fase.";
                    return;
                }

                posicao.SetNumeroCamisa(numero);
                posicao.SetNome(nome);
                posicao.SetEquipeAtletaId(vinculo);
                posicao.SetLado(lado);
            }

            elemento.SetX((float)(numericCoordenadaX.Value / 100m));
            elemento.SetY((float)(numericCoordenadaY.Value / 100m));
            prancheta.Invalidate();
            AtualizarPropriedades();
        }

        private bool AtletaJaUsadoNaFase(int numero, LadoAtleta lado, int? vinculo, AtletaQuadro? emEdicao)
        {
            foreach (ElementoQuadra elemento in prancheta.GetElementos())
            {
                AtletaQuadro? atleta = elemento as AtletaQuadro;
                if (atleta == null || atleta == emEdicao)
                {
                    continue;
                }

                if (atleta.GetLado() == lado && atleta.GetNumeroCamisa() == numero)
                {
                    return true;
                }

                if (vinculo.HasValue && atleta.GetEquipeAtletaId() == vinculo)
                {
                    return true;
                }
            }

            return false;
        }

        private void btnRemoverElemento_Click(object? sender, EventArgs e)
        {
            if (!PodeEditarFase())
            {
                return;
            }

            ElementoQuadra? elemento = prancheta.GetElementoSelecionado();
            if (elemento == null)
            {
                return;
            }

            prancheta.GetElementos().Remove(elemento);
            prancheta.SelecionarElemento(null);
            prancheta.Invalidate();
        }

        private void btnNovaFase_Click(object? sender, EventArgs e)
        {
            if (!PodeEditarFase())
            {
                return;
            }

            GuardarFaseAtual();
            QuadroTatica novaFase = new QuadroTatica($"Fase {_tatica.GetQuadros().Count + 1}", _preferencias.GetDuracaoQuadroPadraoMs());
            _tatica.GetQuadros().Add(novaFase);
            _indiceFaseSelecionada = _tatica.GetQuadros().Count - 1;
            MostrarFaseAtual();
        }

        private void btnDuplicarFase_Click(object? sender, EventArgs e)
        {
            if (!PodeEditarFase())
            {
                return;
            }

            GuardarFaseAtual();
            QuadroTatica copia = _tatica.GetQuadros()[_indiceFaseSelecionada].Copiar();
            const string sufixo = " (cópia)";
            string nomeOriginal = Texto.LimitarCaracteres(copia.GetNome(), 100 - sufixo.Length);
            copia.SetNome(nomeOriginal + sufixo);
            _tatica.GetQuadros().Insert(_indiceFaseSelecionada + 1, copia);
            _indiceFaseSelecionada++;
            MostrarFaseAtual();
        }

        private void btnExcluirFase_Click(object? sender, EventArgs e)
        {
            if (!PodeEditarFase() || _tatica.GetQuadros().Count <= 1)
            {
                return;
            }

            GuardarDadosTatica();
            _tatica.GetQuadros().RemoveAt(_indiceFaseSelecionada);
            _indiceFaseSelecionada = Math.Min(_indiceFaseSelecionada, _tatica.GetQuadros().Count - 1);
            MostrarFaseAtual();
        }

        private void btnFaseAnterior_Click(object? sender, EventArgs e)
        {
            SelecionarFaseVisual(_indiceFaseSelecionada - 1);
        }

        private void btnProximaFase_Click(object? sender, EventArgs e)
        {
            SelecionarFaseVisual(_indiceFaseSelecionada + 1);
        }

        private void CartaoFase_Click(object? sender, EventArgs e)
        {
            CartaoFaseControl? cartao = sender as CartaoFaseControl;
            if (cartao != null && !_apresentando && !_emTransicao)
            {
                SelecionarFaseVisual(flowFases.Controls.GetChildIndex(cartao));
            }
        }

        private void CartaoFase_ArrasteSolicitado(object? sender, EventArgs e)
        {
            CartaoFaseControl? cartao = sender as CartaoFaseControl;
            if (_pronto && !_operacao && !_apresentando && !_emTransicao && _tatica.GetQuadros().Count > 1 && cartao != null)
            {
                cartao.DoDragDrop(cartao, DragDropEffects.Move);
            }
        }

        private CartaoFaseControl? ObterCartaoArrastado(DragEventArgs e)
        {
            if (!PodeEditarFase() || e.Data == null)
            {
                return null;
            }

            // AllowedEffect reúne várias opções. HasFlag verifica se mover está entre elas.
            if (!e.AllowedEffect.HasFlag(DragDropEffects.Move))
            {
                return null;
            }

            // typeof informa qual tipo de objeto esperamos receber no arraste.
            CartaoFaseControl? cartao = e.Data.GetData(typeof(CartaoFaseControl)) as CartaoFaseControl;
            if (cartao == null || !flowFases.Controls.Contains(cartao))
            {
                return null;
            }

            return cartao;
        }

        private void flowFases_DragOver(object? sender, DragEventArgs e)
        {
            if (ObterCartaoArrastado(e) == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            e.Effect = DragDropEffects.Move;
            Point ponto = flowFases.PointToClient(new Point(e.X, e.Y));
            // Rola a sequência ao aproximar o cartão de uma das bordas.
            int passo = 0;
            if (ponto.X < 24)
            {
                passo = -20;
            }
            else if (ponto.X > flowFases.ClientSize.Width - 24)
            {
                passo = 20;
            }

            if (passo != 0)
            {
                flowFases.AutoScrollPosition = new Point(Math.Max(0, -flowFases.AutoScrollPosition.X + passo), 0);
            }
        }

        private void flowFases_DragDrop(object? sender, DragEventArgs e)
        {
            CartaoFaseControl? cartao = ObterCartaoArrastado(e);
            if (cartao == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            e.Effect = DragDropEffects.Move;
            Point ponto = flowFases.PointToClient(new Point(e.X, e.Y));
            int origem = flowFases.Controls.GetChildIndex(cartao);
            int destino = 0;
            // Soltar à esquerda do centro insere antes; à direita, depois do cartão.
            foreach (Control outro in flowFases.Controls)
            {
                if (ponto.X < outro.Left + outro.Width / 2)
                {
                    break;
                }

                destino++;
            }

            if (destino > origem)
            {
                destino--;
            }

            if (destino == origem)
            {
                return;
            }

            GuardarFaseAtual();
            QuadroTatica selecionada = _tatica.GetQuadros()[_indiceFaseSelecionada];
            QuadroTatica movida = _tatica.GetQuadros()[origem];
            _tatica.GetQuadros().RemoveAt(origem);
            _tatica.GetQuadros().Insert(destino, movida);
            _indiceFaseSelecionada = _tatica.GetQuadros().IndexOf(selecionada);
            flowFases.Controls.SetChildIndex(cartao, destino);
            AtualizarSequenciaVisual();
            flowFases.ScrollControlIntoView(cartao);
            if (prancheta.GetElementoSelecionado() == null)
            {
                labelResumoElemento.Text = $"TÁTICA E FASE\nFase {_indiceFaseSelecionada + 1:00}";
            }

            MostrarStatus("Sequência reordenada. Clique em Salvar Tática para gravar a nova ordem.");
        }

        private void SelecionarFaseVisual(int indice)
        {
            if (!PodeEditarFase() || indice == _indiceFaseSelecionada || indice < 0 || indice >= _tatica.GetQuadros().Count)
            {
                return;
            }

            GuardarFaseAtual();
            _indiceFaseSelecionada = indice;
            MostrarFaseAtual();
        }

        private void btnExecutarSequencia_Click(object? sender, EventArgs e)
        {
            AlternarApresentacao();
        }

        private void AlternarApresentacao()
        {
            if (!_pronto || _operacao)
            {
                return;
            }

            if (_apresentando || _emTransicao)
            {
                PararApresentacao();
                return;
            }

            GuardarFaseAtual();
            _apresentando = true;
            _indiceFaseSelecionada = 0;
            MostrarFaseAtual();
            IniciarEtapaApresentacao();
        }

        private void IniciarEtapaApresentacao()
        {
            if (_indiceFaseSelecionada + 1 < _tatica.GetQuadros().Count)
            {
                IniciarTransicao(_indiceFaseSelecionada + 1);
            }
            else
            {
                // O último quadro permanece visível pela duração configurada.
                _duracaoTransicaoMs = _tatica.GetQuadros()[_indiceFaseSelecionada].GetDuracaoMs();
                _tempoTransicao.Restart();
                timerSequencia.Start();
                AtualizarAcoes();
            }
        }

        private void IniciarTransicao(int indiceDestino)
        {
            prancheta.SelecionarElemento(null);
            _movimentos.Clear();
            QuadroTatica destino = _tatica.GetQuadros()[indiceDestino];
            foreach (ElementoQuadra elemento in prancheta.GetElementos())
            {
                ElementoQuadra? proximo = EncontrarNaProximaFase(elemento, destino);
                if (proximo != null)
                {
                    PointF pontoDestino = new PointF(proximo.GetX(), proximo.GetY());
                    _movimentos.Add(new MovimentoElemento(elemento, pontoDestino));
                }
            }

            _indiceFaseDestino = indiceDestino;
            _duracaoTransicaoMs = _tatica.GetQuadros()[_indiceFaseSelecionada].GetDuracaoMs();
            _emTransicao = true;
            _tempoTransicao.Restart();
            timerSequencia.Start();
            AtualizarAcoes();
            labelResumoSequencia.Text = $"{_tatica.GetQuadros().Count} fases · Transição {_indiceFaseSelecionada + 1:00} → {indiceDestino + 1:00}";
        }

        private ElementoQuadra? EncontrarNaProximaFase(ElementoQuadra elemento, QuadroTatica destino)
        {
            if (elemento is PosicaoBola)
            {
                return destino.GetBola();
            }

            AtletaQuadro atual = (AtletaQuadro)elemento;
            foreach (AtletaQuadro proximo in destino.GetAtletas())
            {
                // Um atleta do elenco conserva o vínculo, mesmo se mudar de camisa.
                if (atual.GetEquipeAtletaId() != proximo.GetEquipeAtletaId())
                {
                    continue;
                }

                if (atual.GetEquipeAtletaId().HasValue)
                {
                    return proximo;
                }

                // Para atletas sem vínculo, a camisa e o lado identificam quem se move.
                if (atual.GetNumeroCamisa() == proximo.GetNumeroCamisa() && atual.GetLado() == proximo.GetLado())
                {
                    return proximo;
                }
            }

            return null;
        }

        private void AtualizarTransicao(float progresso)
        {
            // Altera apenas os objetos do desenho. Os quadros salvos na edição não são modificados.
            // Progresso 0 mantém a posição inicial; 0,5 chega à metade; 1 alcança o destino.
            foreach (MovimentoElemento movimento in _movimentos)
            {
                float distanciaX = movimento.GetDestino().X - movimento.GetInicio().X;
                float distanciaY = movimento.GetDestino().Y - movimento.GetInicio().Y;
                movimento.GetElemento().SetX(movimento.GetInicio().X + distanciaX * progresso);
                movimento.GetElemento().SetY(movimento.GetInicio().Y + distanciaY * progresso);
            }

            prancheta.Invalidate();
        }

        private void timerSequencia_Tick(object? sender, EventArgs e)
        {
            if (_emTransicao)
            {
                // Usa o tempo real decorrido; atrasos na pintura não alongam o movimento.
                double tempoDecorrido = _tempoTransicao.Elapsed.TotalMilliseconds;
                float progresso = (float)Math.Clamp(tempoDecorrido / _duracaoTransicaoMs, 0d, 1d);
                AtualizarTransicao(progresso);
                if (progresso < 1f)
                {
                    return;
                }

                _emTransicao = false;
                _indiceFaseSelecionada = _indiceFaseDestino;
                _indiceFaseDestino = -1;
                _movimentos.Clear();
                timerSequencia.Stop();
                _tempoTransicao.Stop();
                MostrarFaseAtual();
                if (_apresentando)
                {
                    IniciarEtapaApresentacao();
                }
            }
            else if (_apresentando && _tempoTransicao.Elapsed.TotalMilliseconds >= _duracaoTransicaoMs)
            {
                PararApresentacao();
            }
        }

        private void PararApresentacao()
        {
            timerSequencia.Stop();
            _tempoTransicao.Reset();
            _movimentos.Clear();
            _emTransicao = false;
            _indiceFaseDestino = -1;
            _apresentando = false;
            // Ao interromper, restaura o quadro de origem, sem gravar posições intermediárias.
            MostrarFaseAtual();
            MostrarStatus("Prévia encerrada.");
        }

        private void AtualizarSequenciaVisual()
        {
            if (flowFases.Controls.Count != _tatica.GetQuadros().Count)
            {
                while (flowFases.Controls.Count > 0)
                {
                    flowFases.Controls[0].Dispose();
                }

                foreach (QuadroTatica fase in _tatica.GetQuadros())
                {
                    CartaoFaseControl cartao = new CartaoFaseControl();
                    cartao.Margin = new Padding(0, 0, 10, 0);
                    cartao.Click += CartaoFase_Click;
                    cartao.ArrasteSolicitado += CartaoFase_ArrasteSolicitado;
                    dicasPrancheta.SetToolTip(cartao, "Arraste para alterar a ordem dos quadros.");
                    flowFases.Controls.Add(cartao);
                }
            }

            TimeSpan inicio = TimeSpan.Zero;
            for (int i = 0; i < _tatica.GetQuadros().Count; i++)
            {
                QuadroTatica fase = _tatica.GetQuadros()[i];
                CartaoFaseControl cartao = (CartaoFaseControl)flowFases.Controls[i];
                cartao.SetNumero(i + 1);
                cartao.SetNomeFase(fase.GetNome());
                bool faseSelecionada = i == _indiceFaseSelecionada;
                cartao.SetAtivo(faseSelecionada);
                if (fase.GetRotacao().HasValue)
                {
                    cartao.SetTipo($"Rotação P{fase.GetRotacao()}");
                }
                else
                {
                    cartao.SetTipo("Sem rotação");
                }

                cartao.SetDuracaoMs(fase.GetDuracaoMs());
                cartao.SetInicio(inicio);
                cartao.Name = $"cartaoFase{i + 1}";
                inicio = inicio.Add(TimeSpan.FromMilliseconds(fase.GetDuracaoMs()));
            }

            labelResumoSequencia.Text = $"{_tatica.GetQuadros().Count} fases · Fase {_indiceFaseSelecionada + 1:00} selecionada";
            if (flowFases.Controls.Count > _indiceFaseSelecionada)
            {
                flowFases.ScrollControlIntoView(flowFases.Controls[_indiceFaseSelecionada]);
            }

            AtualizarAcoes();
        }

        private void AtualizarAcoes()
        {
            bool editar = _pronto && !_operacao && !_apresentando && !_emTransicao;
            btnSalvarTatica.Enabled = editar && _equipes.Rows.Count > 0;
            layoutDadosTatica.Enabled = editar;
            comboEquipeTatica.Enabled = editar && _tatica.GetId() == 0 && _equipes.Rows.Count > 0;
            btnAdicionarAtleta.Enabled = editar;
            btnAdicionarBola.Enabled = editar;
            btnNovaFase.Enabled = editar;
            btnDuplicarFase.Enabled = editar;
            btnExcluirFase.Enabled = editar && _tatica.GetQuadros().Count > 1;
            btnFaseAnterior.Enabled = editar && _indiceFaseSelecionada > 0;
            btnProximaFase.Enabled = editar && _indiceFaseSelecionada + 1 < _tatica.GetQuadros().Count;
            btnExecutarSequencia.Enabled = _pronto && !_operacao;
            if (_apresentando || _emTransicao)
            {
                btnExecutarSequencia.Text = "■ Parar";
            }
            else
            {
                btnExecutarSequencia.Text = "▶ Executar";
            }

            prancheta.Enabled = editar;
            layoutCoordenadas.Enabled = editar && prancheta.GetElementoSelecionado() != null;
            layoutCamposAtleta.Enabled = editar;
            btnAplicarPropriedades.Enabled = editar;
            btnRemoverElemento.Enabled = editar && prancheta.GetElementoSelecionado() != null;
        }
    }
}
