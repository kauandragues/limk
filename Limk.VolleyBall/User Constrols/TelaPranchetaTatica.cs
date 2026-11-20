using Limk.VolleyBall.Backend;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaPranchetaTatica : UserControl, ITelaUsuario
    {
        private UsuarioAutenticado? _usuario;
        private Tatica _tatica = new Tatica();
        private int _indiceFaseSelecionada;
        private PreferenciasUsuario _preferencias = PreferenciasUsuario.Padrao;
        private readonly CancellationTokenSource _encerramento = new CancellationTokenSource();
        private readonly int _taticaParaAbrir;
        private readonly bool _apresentarAoAbrir;
        private DataTable _equipes = new DataTable();
        private DataTable _tipos = new DataTable();
        private DataTable _sistemas = new DataTable();
        private List<AtletaDaEquipe> _elenco = new List<AtletaDaEquipe>();
        private bool _pronto;
        private bool _operacao;
        private bool _apresentando;
        private bool _atualizandoPropriedades;

        public TelaPranchetaTatica()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            _tatica.Quadros.Add(new QuadroTatica { Nome = "Fase 1" });
            AtualizarPropriedades();
            AtualizarSequenciaVisual();
        }

        public TelaPranchetaTatica(int taticaId, bool apresentar = false) : this()
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(taticaId);
            _taticaParaAbrir = taticaId;
            _apresentarAoAbrir = apresentar;
        }

        public void DefinirUsuario(UsuarioAutenticado usuario)
        {
            _usuario = usuario;
            labelNomeTatica.Text = "Nova tática";
        }

        private async void TelaPranchetaTatica_Load(object? sender, EventArgs e)
        {
            if (_usuario == null || _pronto || _operacao || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            CancellationToken token = _encerramento.Token;
            _operacao = true;
            AtualizarAcoes();
            MostrarStatus("Carregando a tática e suas equipes...");
            try
            {
                _equipes = await DadosUsuario.CarregarEquipesAsync(_usuario.Id);
                _tipos = await DadosPrancheta.CarregarTiposAsync(token);
                _sistemas = await DadosPrancheta.CarregarSistemasAsync(token);
                try { _preferencias = await DadosPreferenciasUsuario.CarregarAsync(_usuario.Id, token); }
                catch (FalhaPreferenciasUsuarioException) { _preferencias = PreferenciasUsuario.Padrao; }
                if (IsDisposed || token.IsCancellationRequested)
                    return;
                prancheta.AplicarPreferencias(_preferencias);

                if (_taticaParaAbrir > 0)
                {
                    _tatica = await DadosPrancheta.CarregarAsync(_usuario.Id, _taticaParaAbrir, token);
                }
                else
                {
                    _tatica.EquipeId = _equipes.Rows.Count > 0 ? Convert.ToInt32(_equipes.Rows[0]["id"]) : 0;
                    _tatica.Quadros[0].DuracaoMs = _preferencias.DuracaoQuadroPadraoMs;
                }
                if (_tatica.Quadros.Count == 0)
                    _tatica.Quadros.Add(new QuadroTatica { Nome = "Fase 1", DuracaoMs = _preferencias.DuracaoQuadroPadraoMs });
                _elenco = _tatica.EquipeId > 0
                    ? await DadosPrancheta.CarregarElencoAsync(_usuario.Id, _tatica.EquipeId, token) : new List<AtletaDaEquipe>();
                if (IsDisposed || token.IsCancellationRequested)
                    return;
                PrepararCamposTatica();
                _pronto = true;
                _indiceFaseSelecionada = 0;
                MostrarFaseAtual();
                MostrarStatus(_equipes.Rows.Count == 0
                    ? "Seu usuário não possui equipe vinculada para salvar táticas."
                    : "Sem um elemento selecionado, edite a tática e a fase nas propriedades à direita.");
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested) { }
            catch (UnauthorizedAccessException erro) { if (!IsDisposed) MostrarStatus(erro.Message, true); }
            catch (Exception) { if (!IsDisposed) MostrarStatus("Não foi possível carregar a prancheta. Verifique a conexão e abra a tela novamente.", true); }
            finally
            {
                _operacao = false;
                if (!IsDisposed) AtualizarSequenciaVisual();
            }
            if (_pronto && _apresentarAoAbrir && !IsDisposed)
                AlternarApresentacao();
        }

        private void MostrarStatus(string mensagem, bool erro = false)
        {
            labelStatusPrancheta.ForeColor = erro ? Color.FromArgb(180, 40, 60) : Color.FromArgb(100, 116, 139);
            labelStatusPrancheta.Text = mensagem;
        }

        // A quadra é a edição da fase selecionada. Antes de trocar de fase, copiamos seus elementos.
        private void GuardarFaseAtual()
        {
            if (!_pronto || _indiceFaseSelecionada >= _tatica.Quadros.Count)
                return;
            GuardarDadosTatica();
            QuadroTatica quadro = _tatica.Quadros[_indiceFaseSelecionada];
            quadro.Atletas.Clear();
            quadro.Bola = null;
            foreach (PosicaoElementos elemento in prancheta.Elementos)
            {
                if (elemento is PosicaoBola bola)
                    quadro.Bola = new PosicaoBola { X = bola.X, Y = bola.Y };
                else if (elemento is PosicaoAtleta posicao)
                {
                    Atleta? atleta = prancheta.Atletas.Find(a => a.Id == posicao.AtletaId);
                    if (atleta == null) continue;
                    quadro.Atletas.Add(new AtletaQuadro
                    {
                        EquipeAtletaId = posicao.EquipeAtletaId, Nome = atleta.Nome,
                        NumeroCamisa = atleta.NumeroCamisa, Lado = posicao.Lado, X = posicao.X, Y = posicao.Y
                    });
                }
            }
            AtualizarSequenciaVisual();
        }

        private void MostrarFaseAtual()
        {
            prancheta.SelecionarElemento(null);
            prancheta.Elementos.Clear();
            prancheta.Atletas.Clear();
            QuadroTatica quadro = _tatica.Quadros[_indiceFaseSelecionada];
            foreach (AtletaQuadro salvo in quadro.Atletas)
            {
                // Este ID liga os dois objetos do desenho; o vínculo persistido é EquipeAtletaId.
                int idLocal = prancheta.Atletas.Count + 1;
                prancheta.Atletas.Add(new Atleta { Id = idLocal, Nome = salvo.Nome, NumeroCamisa = salvo.NumeroCamisa });
                prancheta.Elementos.Add(new PosicaoAtleta
                {
                    AtletaId = idLocal, EquipeAtletaId = salvo.EquipeAtletaId, Lado = salvo.Lado, X = salvo.X, Y = salvo.Y
                });
            }
            if (quadro.Bola != null)
                prancheta.Elementos.Add(new PosicaoBola { X = quadro.Bola.X, Y = quadro.Bola.Y });
            labelNomeTatica.Text = _tatica.Nome;
            PreencherDadosTatica();
            AtualizarPropriedades();
            AtualizarSequenciaVisual();
            prancheta.Invalidate();
        }

        private void PrepararCamposTatica()
        {
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
            QuadroTatica fase = _tatica.Quadros[_indiceFaseSelecionada];
            textNomeTatica.Text = _tatica.Nome;
            textDescricaoTatica.Text = _tatica.Descricao;
            comboEquipeTatica.SelectedValue = _tatica.EquipeId;
            comboTipoTatica.SelectedValue = _tatica.TipoTaticaId ?? 0;
            comboSistemaTatica.SelectedValue = _tatica.SistemaJogoId ?? 0;
            textNomeFase.Text = fase.Nome;
            numericDuracaoFase.Value = Math.Clamp(fase.DuracaoMs / 1000m, numericDuracaoFase.Minimum, numericDuracaoFase.Maximum);
            comboRotacaoFase.SelectedIndex = fase.Rotacao ?? 0;
            _atualizandoPropriedades = false;
        }

        private void GuardarDadosTatica()
        {
            // Os campos permanecem preenchidos ao selecionar um atleta ou a bola.
            // Também guardamos o que foi digitado antes de salvar ou trocar de fase.
            _tatica.Nome = textNomeTatica.Text.Trim();
            _tatica.Descricao = textDescricaoTatica.Text.Trim();
            _tatica.TipoTaticaId = comboTipoTatica.SelectedValue is int tipo && tipo > 0 ? tipo : null;
            _tatica.SistemaJogoId = comboSistemaTatica.SelectedValue is int sistema && sistema > 0 ? sistema : null;
            QuadroTatica fase = _tatica.Quadros[_indiceFaseSelecionada];
            fase.Nome = textNomeFase.Text.Trim();
            fase.DuracaoMs = (int)(numericDuracaoFase.Value * 1000);
            fase.Rotacao = comboRotacaoFase.SelectedIndex > 0 ? comboRotacaoFase.SelectedIndex : null;
            labelNomeTatica.Text = _tatica.Nome;
        }

        private async void comboEquipeTatica_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_atualizandoPropriedades || !_pronto || _operacao || _apresentando || _tatica.Id > 0 ||
                comboEquipeTatica.SelectedValue is not int equipeId || equipeId == _tatica.EquipeId)
                return;
            GuardarFaseAtual();
            _tatica.EquipeId = equipeId;
            // Uma nova equipe não pode herdar os vínculos do elenco anterior.
            foreach (QuadroTatica quadro in _tatica.Quadros)
                foreach (AtletaQuadro atleta in quadro.Atletas)
                {
                    atleta.EquipeAtletaId = null;
                    atleta.Nome = $"Atleta {atleta.NumeroCamisa}";
                }
            _elenco.Clear();
            _operacao = true;
            AtualizarAcoes();
            try
            {
                _elenco = await DadosPrancheta.CarregarElencoAsync(_usuario!.Id, equipeId, _encerramento.Token);
                if (!IsDisposed) MostrarStatus("Equipe atualizada na edição. Clique em Salvar tática para gravar no banco.");
            }
            catch (OperationCanceledException) { }
            catch (FalhaPranchetaException) { if (!IsDisposed) MostrarStatus("Não foi possível carregar o elenco da equipe.", true); }
            finally
            {
                _operacao = false;
                if (!IsDisposed) MostrarFaseAtual();
            }
        }

        private async void btnSalvarTatica_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || _apresentando || _usuario == null || _equipes.Rows.Count == 0)
                return;
            GuardarFaseAtual();
            CancellationToken token = _encerramento.Token;
            _operacao = true;
            AtualizarAcoes();
            MostrarStatus("Salvando a tática e suas fases...");
            try
            {
                int id = await DadosPrancheta.SalvarAsync(_usuario.Id, _tatica, token);
                if (!IsDisposed)
                {
                    _tatica.Id = id;
                    MostrarStatus("Tática salva no banco de dados.");
                }
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested) { }
            catch (ArgumentException erro)
            {
                if (!IsDisposed)
                {
                    prancheta.SelecionarElemento(null);
                    MostrarStatus(erro.Message, true);
                }
            }
            catch (UnauthorizedAccessException erro) { if (!IsDisposed) MostrarStatus(erro.Message, true); }
            catch (Exception) { if (!IsDisposed) MostrarStatus("Não foi possível salvar. A edição foi mantida; verifique a conexão e tente novamente.", true); }
            finally
            {
                _operacao = false;
                if (!IsDisposed) AtualizarAcoes();
            }
        }

        private void btnAdicionarBola_Click(object sender, EventArgs e)
        {
            if (!_pronto || _operacao || _apresentando) return;
            var existente = prancheta.Elementos.OfType<PosicaoBola>().FirstOrDefault();
            if (existente != null) { prancheta.SelecionarElemento(existente); return; }
            var bola = new PosicaoBola { X = 0.5f, Y = 0.5f };
            prancheta.Elementos.Add(bola);
            prancheta.SelecionarElemento(bola);
        }

        private void btnAdicionarAtleta_Click(object sender, EventArgs e)
        {
            if (!_pronto || _operacao || _apresentando) return;
            int numero = Enumerable.Range(1, 99).FirstOrDefault(n => !prancheta.Elementos.OfType<PosicaoAtleta>()
                .Where(p => p.Lado == LadoAtleta.Principal).Any(p => prancheta.Atletas.Find(a => a.Id == p.AtletaId)?.NumeroCamisa == n));
            if (numero == 0) { MostrarStatus("Não há números disponíveis para outro atleta deste lado.", true); return; }
            int id = prancheta.Atletas.Count == 0 ? 1 : prancheta.Atletas.Max(a => a.Id) + 1;
            prancheta.Atletas.Add(new Atleta { Id = id, Nome = $"Atleta {numero}", NumeroCamisa = numero });
            var posicao = new PosicaoAtleta { AtletaId = id, X = 0.5f, Y = 0.5f };
            prancheta.Elementos.Add(posicao);
            prancheta.SelecionarElemento(posicao);
        }

        private void prancheta_SelecaoAlterada(object? sender, EventArgs e) => AtualizarPropriedades();
        private void prancheta_PosicaoAlterada(object? sender, EventArgs e) => AtualizarCoordenadas();

        private void AtualizarPropriedades()
        {
            _atualizandoPropriedades = true;
            PosicaoElementos? elemento = prancheta.ElementoSelecionado;
            layoutDadosTatica.Visible = elemento == null;
            layoutCamposAtleta.Visible = elemento is PosicaoAtleta;
            labelTituloCoordenadas.Visible = elemento != null;
            layoutCoordenadas.Visible = elemento != null;
            btnRemoverElemento.Visible = elemento != null;
            textNumeroCamisa.Clear();
            comboAtletaVinculado.Items.Clear();
            comboAtletaVinculado.Items.Add(new AtletaDaEquipe());
            foreach (AtletaDaEquipe atleta in _elenco)
                comboAtletaVinculado.Items.Add(atleta);
            comboAtletaVinculado.SelectedIndex = 0;
            comboLadoAtleta.SelectedIndex = 0;
            if (elemento is PosicaoAtleta posicao)
            {
                Atleta? atleta = prancheta.Atletas.Find(a => a.Id == posicao.AtletaId);
                if (atleta != null)
                {
                    AtletaDaEquipe? vinculo = _elenco.Find(a => a.EquipeAtletaId == posicao.EquipeAtletaId);
                    if (vinculo == null && posicao.EquipeAtletaId.HasValue)
                    {
                        vinculo = new AtletaDaEquipe { EquipeAtletaId = posicao.EquipeAtletaId.Value, Nome = atleta.Nome, NumeroCamisa = atleta.NumeroCamisa };
                        comboAtletaVinculado.Items.Add(vinculo);
                    }
                    if (vinculo != null) comboAtletaVinculado.SelectedItem = vinculo;
                    textNumeroCamisa.Text = atleta.NumeroCamisa.ToString();
                    labelResumoElemento.Text = $"#{atleta.NumeroCamisa}  •  ATLETA\n{atleta.Nome}";
                }
                comboLadoAtleta.SelectedIndex = posicao.Lado == LadoAtleta.Adversario ? 1 : 0;
            }
            else labelResumoElemento.Text = elemento is PosicaoBola ? "BOLA\nPosição na quadra" : $"TÁTICA E FASE\nFase {_indiceFaseSelecionada + 1:00}";
            labelMensagemPropriedades.Text = elemento == null ? "Clique em Salvar Tática para gravar os dados e as fases no banco." : "As alterações pertencem somente à fase selecionada.";
            labelMensagemPropriedades.ForeColor = Color.FromArgb(100, 116, 139);
            _atualizandoPropriedades = false;
            AtualizarCoordenadas();
            AtualizarAcoes();
        }

        private void AtualizarCoordenadas()
        {
            var elemento = prancheta.ElementoSelecionado;
            numericCoordenadaX.Value = elemento == null ? 0 : (decimal)Math.Clamp(elemento.X, 0f, 1f) * 100m;
            numericCoordenadaY.Value = elemento == null ? 0 : (decimal)Math.Clamp(elemento.Y, 0f, 1f) * 100m;
        }

        private void comboAtletaVinculado_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!_atualizandoPropriedades && comboAtletaVinculado.SelectedItem is AtletaDaEquipe atleta && atleta.EquipeAtletaId > 0)
                textNumeroCamisa.Text = atleta.NumeroCamisa.ToString();
        }

        private void btnAplicarPropriedades_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || _apresentando) return;
            var elemento = prancheta.ElementoSelecionado;
            if (elemento == null)
            {
                GuardarFaseAtual();
                try
                {
                    DadosPrancheta.Validar(_tatica);
                    labelMensagemPropriedades.Text = "Dados aplicados na edição. Clique em Salvar Tática para gravar.";
                    labelMensagemPropriedades.ForeColor = Color.FromArgb(100, 116, 139);
                }
                catch (ArgumentException erro)
                {
                    labelMensagemPropriedades.Text = erro.Message;
                    labelMensagemPropriedades.ForeColor = Color.FromArgb(180, 40, 60);
                }
                return;
            }
            if (elemento is PosicaoAtleta posicao)
            {
                if (!int.TryParse(textNumeroCamisa.Text, out int numero) || numero < 1 || numero > 99)
                { labelMensagemPropriedades.Text = "Informe um número entre 1 e 99."; return; }
                LadoAtleta lado = comboLadoAtleta.SelectedIndex == 1 ? LadoAtleta.Adversario : LadoAtleta.Principal;
                var escolhido = comboAtletaVinculado.SelectedItem as AtletaDaEquipe;
                int? vinculo = escolhido != null && escolhido.EquipeAtletaId > 0 ? escolhido.EquipeAtletaId : null;
                bool repetido = prancheta.Elementos.OfType<PosicaoAtleta>().Any(outro => outro != posicao &&
                    ((outro.Lado == lado && prancheta.Atletas.Find(a => a.Id == outro.AtletaId)?.NumeroCamisa == numero) ||
                    (vinculo.HasValue && outro.EquipeAtletaId == vinculo)));
                if (repetido) { labelMensagemPropriedades.Text = "O número ou o atleta já está em uso nesta fase."; return; }
                var atleta = prancheta.Atletas.Find(a => a.Id == posicao.AtletaId)!;
                atleta.NumeroCamisa = numero;
                atleta.Nome = vinculo.HasValue ? escolhido!.Nome : $"Atleta {numero}";
                posicao.EquipeAtletaId = vinculo;
                posicao.Lado = lado;
            }
            elemento.X = (float)(numericCoordenadaX.Value / 100m);
            elemento.Y = (float)(numericCoordenadaY.Value / 100m);
            prancheta.Invalidate();
            AtualizarPropriedades();
        }

        private void btnRemoverElemento_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || _apresentando) return;
            var elemento = prancheta.ElementoSelecionado;
            if (elemento == null) return;
            prancheta.Elementos.Remove(elemento);
            if (elemento is PosicaoAtleta posicao)
                prancheta.Atletas.RemoveAll(a => a.Id == posicao.AtletaId);
            prancheta.SelecionarElemento(null);
            prancheta.Invalidate();
        }

        private void btnNovaFase_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || _apresentando) return;
            GuardarFaseAtual();
            _tatica.Quadros.Add(new QuadroTatica { Nome = $"Fase {_tatica.Quadros.Count + 1}", DuracaoMs = _preferencias.DuracaoQuadroPadraoMs });
            _indiceFaseSelecionada = _tatica.Quadros.Count - 1;
            MostrarFaseAtual();
        }

        private void btnDuplicarFase_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || _apresentando) return;
            GuardarFaseAtual();
            QuadroTatica copia = _tatica.Quadros[_indiceFaseSelecionada].Copiar();
            copia.Nome = (copia.Nome.Length > 91 ? copia.Nome.Substring(0, 91) : copia.Nome) + " (cópia)";
            _tatica.Quadros.Insert(_indiceFaseSelecionada + 1, copia);
            _indiceFaseSelecionada++;
            MostrarFaseAtual();
        }

        private void btnExcluirFase_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || _apresentando || _tatica.Quadros.Count <= 1) return;
            GuardarDadosTatica();
            _tatica.Quadros.RemoveAt(_indiceFaseSelecionada);
            _indiceFaseSelecionada = Math.Min(_indiceFaseSelecionada, _tatica.Quadros.Count - 1);
            MostrarFaseAtual();
        }

        private void btnFaseAnterior_Click(object? sender, EventArgs e) => SelecionarFaseVisual(_indiceFaseSelecionada - 1);
        private void btnProximaFase_Click(object? sender, EventArgs e) => SelecionarFaseVisual(_indiceFaseSelecionada + 1);
        private void CartaoFase_Click(object? sender, EventArgs e)
        {
            if (sender is CartaoFaseControl cartao && !_apresentando)
                SelecionarFaseVisual(flowFases.Controls.GetChildIndex(cartao));
        }

        private void CartaoFase_ArrasteSolicitado(object? sender, EventArgs e)
        {
            if (_pronto && !_operacao && !_apresentando && _tatica.Quadros.Count > 1 && sender is CartaoFaseControl cartao)
                cartao.DoDragDrop(cartao, DragDropEffects.Move);
        }

        private CartaoFaseControl? ObterCartaoArrastado(DragEventArgs e)
        {
            if (!_pronto || _operacao || _apresentando || (e.AllowedEffect & DragDropEffects.Move) == 0 ||
                e.Data?.GetData(typeof(CartaoFaseControl)) is not CartaoFaseControl cartao ||
                !flowFases.Controls.Contains(cartao))
                return null;
            return cartao;
        }

        private void flowFases_DragOver(object? sender, DragEventArgs e)
        {
            e.Effect = ObterCartaoArrastado(e) != null ? DragDropEffects.Move : DragDropEffects.None;
            if (e.Effect == DragDropEffects.None) return;
            Point ponto = flowFases.PointToClient(new Point(e.X, e.Y));
            // Rola a sequência ao aproximar o cartão de uma das bordas.
            int passo = ponto.X < 24 ? -20 : ponto.X > flowFases.ClientSize.Width - 24 ? 20 : 0;
            if (passo != 0)
                flowFases.AutoScrollPosition = new Point(Math.Max(0, -flowFases.AutoScrollPosition.X + passo), 0);
        }

        private void flowFases_DragDrop(object? sender, DragEventArgs e)
        {
            CartaoFaseControl? cartao = ObterCartaoArrastado(e);
            e.Effect = cartao != null ? DragDropEffects.Move : DragDropEffects.None;
            if (cartao == null) return;
            Point ponto = flowFases.PointToClient(new Point(e.X, e.Y));
            int origem = flowFases.Controls.GetChildIndex(cartao);
            int destino = 0;
            // Soltar à esquerda do centro insere antes; à direita, depois do cartão.
            foreach (Control outro in flowFases.Controls)
            {
                if (ponto.X < outro.Left + outro.Width / 2) break;
                destino++;
            }
            if (destino > origem) destino--;
            if (destino == origem) return;

            GuardarFaseAtual();
            QuadroTatica selecionada = _tatica.Quadros[_indiceFaseSelecionada];
            QuadroTatica movida = _tatica.Quadros[origem];
            _tatica.Quadros.RemoveAt(origem);
            _tatica.Quadros.Insert(destino, movida);
            _indiceFaseSelecionada = _tatica.Quadros.IndexOf(selecionada);
            flowFases.Controls.SetChildIndex(cartao, destino);
            AtualizarSequenciaVisual();
            flowFases.ScrollControlIntoView(cartao);
            if (prancheta.ElementoSelecionado == null)
                labelResumoElemento.Text = $"TÁTICA E FASE\nFase {_indiceFaseSelecionada + 1:00}";
            MostrarStatus("Sequência reordenada. Clique em Salvar Tática para gravar a nova ordem.");
        }

        private void SelecionarFaseVisual(int indice)
        {
            if (!_pronto || _operacao || indice < 0 || indice >= _tatica.Quadros.Count) return;
            GuardarFaseAtual();
            _indiceFaseSelecionada = indice;
            MostrarFaseAtual();
        }

        private void btnExecutarSequencia_Click(object? sender, EventArgs e) => AlternarApresentacao();
        private void AlternarApresentacao()
        {
            if (!_pronto || _operacao) return;
            if (_apresentando) { PararApresentacao(); return; }
            GuardarFaseAtual();
            _apresentando = true;
            SelecionarFaseVisual(0);
            timerSequencia.Interval = _tatica.Quadros[0].DuracaoMs;
            timerSequencia.Start();
            AtualizarAcoes();
        }

        private void timerSequencia_Tick(object? sender, EventArgs e)
        {
            if (_indiceFaseSelecionada + 1 >= _tatica.Quadros.Count) { PararApresentacao(); return; }
            SelecionarFaseVisual(_indiceFaseSelecionada + 1);
            timerSequencia.Interval = _tatica.Quadros[_indiceFaseSelecionada].DuracaoMs;
        }

        private void PararApresentacao()
        {
            timerSequencia.Stop();
            _apresentando = false;
            AtualizarAcoes();
        }

        private void AtualizarSequenciaVisual()
        {
            if (flowFases.Controls.Count != _tatica.Quadros.Count)
            {
                while (flowFases.Controls.Count > 0) flowFases.Controls[0].Dispose();
                foreach (QuadroTatica fase in _tatica.Quadros)
                {
                    var cartao = new CartaoFaseControl { Margin = new Padding(0, 0, 10, 0) };
                    cartao.Click += CartaoFase_Click;
                    cartao.ArrasteSolicitado += CartaoFase_ArrasteSolicitado;
                    dicasPrancheta.SetToolTip(cartao, "Arraste para alterar a ordem dos quadros.");
                    flowFases.Controls.Add(cartao);
                }
            }
            TimeSpan inicio = TimeSpan.Zero;
            for (int i = 0; i < _tatica.Quadros.Count; i++)
            {
                var fase = _tatica.Quadros[i];
                var cartao = (CartaoFaseControl)flowFases.Controls[i];
                cartao.Numero = i + 1; cartao.NomeFase = fase.Nome; cartao.Ativo = i == _indiceFaseSelecionada;
                cartao.Tipo = fase.Rotacao.HasValue ? $"Rotação P{fase.Rotacao}" : "Sem rotação";
                cartao.Duracao = fase.DuracaoMs / 1000m; cartao.Inicio = inicio;
                cartao.Name = $"cartaoFase{i + 1}";
                inicio = inicio.Add(TimeSpan.FromMilliseconds(fase.DuracaoMs));
            }
            labelResumoSequencia.Text = $"{_tatica.Quadros.Count} fases · Fase {_indiceFaseSelecionada + 1:00} selecionada";
            if (flowFases.Controls.Count > _indiceFaseSelecionada)
                flowFases.ScrollControlIntoView(flowFases.Controls[_indiceFaseSelecionada]);
            AtualizarAcoes();
        }

        private void AtualizarAcoes()
        {
            bool editar = _pronto && !_operacao && !_apresentando;
            btnSalvarTatica.Enabled = editar && _equipes.Rows.Count > 0;
            layoutDadosTatica.Enabled = editar;
            comboEquipeTatica.Enabled = editar && _tatica.Id == 0 && _equipes.Rows.Count > 0;
            btnAdicionarAtleta.Enabled = editar; btnAdicionarBola.Enabled = editar;
            btnNovaFase.Enabled = editar; btnDuplicarFase.Enabled = editar;
            btnExcluirFase.Enabled = editar && _tatica.Quadros.Count > 1;
            btnFaseAnterior.Enabled = editar && _indiceFaseSelecionada > 0;
            btnProximaFase.Enabled = editar && _indiceFaseSelecionada + 1 < _tatica.Quadros.Count;
            btnExecutarSequencia.Enabled = _pronto && !_operacao;
            btnExecutarSequencia.Text = _apresentando ? "■ Parar" : "▶ Executar";
            prancheta.Enabled = editar;
            layoutCoordenadas.Enabled = editar && prancheta.ElementoSelecionado != null;
            layoutCamposAtleta.Enabled = editar;
            btnAplicarPropriedades.Enabled = editar;
            btnRemoverElemento.Enabled = editar && prancheta.ElementoSelecionado != null;
        }
    }

    public class PranchetaControl : Control
    {
        // Cadastro dos atletas, separado das posições de cada elemento.
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Atleta> Atletas { get; } = new List<Atleta>();
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<PosicaoElementos> Elementos { get; } = new List<PosicaoElementos>();
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PosicaoElementos? ElementoSelecionado
        {
            get { return _selecionado; }
        }
        public event EventHandler? SelecaoAlterada;
        public event EventHandler? PosicaoAlterada;
        private readonly Image _imagemQuadra = Properties.Resources.Quadra;
        private float _zoom = 1f;
        private PointF _deslocamentoVisao = PointF.Empty;
        private PosicaoElementos? _selecionado;
        private bool _arrastando;
        private PointF _offsetArraste;
        private Color _corAtletaPrincipal;
        private Color _corAtletaAdversario;
        private Color _corBola;

        public PranchetaControl()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            AplicarPreferencias(PreferenciasUsuario.Padrao);
        }

        internal void AplicarPreferencias(PreferenciasUsuario preferencias)
        {
            _corAtletaPrincipal = ColorTranslator.FromHtml(preferencias.CorAtletaPrincipal);
            _corAtletaAdversario = ColorTranslator.FromHtml(preferencias.CorAtletaAdversario);
            _corBola = ColorTranslator.FromHtml(preferencias.CorBola);
            Invalidate();
        }

        public void SelecionarElemento(PosicaoElementos? elemento)
        {
            if (elemento != null && !Elementos.Contains(elemento))
                throw new ArgumentException("O elemento deve pertencer à prancheta.", nameof(elemento));

            _arrastando = false;
            Capture = false;
            _selecionado = elemento;
            Invalidate();
            SelecaoAlterada?.Invoke(this, EventArgs.Empty);
        }

        private float ObterRaioElemento(PosicaoElementos elemento, RectangleF area)
        {
            // A bola é menor, mas todos os círculos acompanham a quadra.
            float proporcao = elemento is PosicaoBola ? 0.025f : 0.04f;
            return Math.Min(area.Width, area.Height) * proporcao;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Focus();

            if (e.Button != MouseButtons.Left)
                return;

            _selecionado = null;
            _arrastando = false;

            RectangleF area = ObterAreaQuadra();

            if (area.Width <= 0 || area.Height <= 0)
            {
                Invalidate();
                SelecaoAlterada?.Invoke(this, EventArgs.Empty);
                return;
            }

            // Desfaz o zoom e o deslocamento da visualização.
            PointF mouseCena = TelaParaCena(e.Location);

            // Começa pelo último desenhado, que aparece por cima.
            for (int i = Elementos.Count - 1; i >= 0; i--)
            {
                PosicaoElementos posicao = Elementos[i];
                float raio = ObterRaioElemento(posicao, area);

                PointF centro = PosicaoParaCena(posicao, area);

                float dx = mouseCena.X - centro.X;
                float dy = mouseCena.Y - centro.Y;

                // Se o clique está fora do círculo, tenta o próximo.
                if (dx * dx + dy * dy > raio * raio)
                    continue;

                _selecionado = posicao;

                _offsetArraste = new PointF(
                    mouseCena.X - centro.X,
                    mouseCena.Y - centro.Y
                );

                _arrastando = true;
                Capture = true;

                break;
            }

            Invalidate();
            SelecaoAlterada?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!_arrastando || _selecionado == null)
                return;

            RectangleF area = ObterAreaQuadra();

            if (area.Width <= 0 || area.Height <= 0)
                return;

            PointF mouseCena = TelaParaCena(e.Location);

            // Encontra o centro desejado, preservando o ponto clicado.
            float centroX = mouseCena.X - _offsetArraste.X;
            float centroY = mouseCena.Y - _offsetArraste.Y;

            // Converte para coordenadas relativas à quadra.
            float x = (centroX - area.Left) / area.Width;
            float y = (centroY - area.Top) / area.Height;

            // Mantém o centro do elemento dentro da quadra.
            _selecionado.X = Math.Clamp(x, 0f, 1f);
            _selecionado.Y = Math.Clamp(y, 0f, 1f);

            Invalidate();
            PosicaoAlterada?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button != MouseButtons.Left)
                return;

            _arrastando = false;
            Capture = false;
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            base.OnMouseCaptureChanged(e);

            if (!Capture)
                _arrastando = false;
        }
        private PointF TelaParaCena(Point ponto)
        {
            return new PointF(
                (ponto.X - _deslocamentoVisao.X) / _zoom,
                (ponto.Y - _deslocamentoVisao.Y) / _zoom
            );
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta == 0)
                return;

            // Descobre o ponto sob o cursor antes de alterar o zoom.
            PointF pontoCena = TelaParaCena(e.Location);

            // Cada passo da roda aumenta ou diminui a escala.
            float fator = MathF.Pow(1.1f, e.Delta / 120f);

            // Limita o zoom entre 50% e 400%.
            float novoZoom = Math.Clamp(
                _zoom * fator,
                0.5f,
                4f
            );

            // Mantém o mesmo ponto do desenho sob o cursor.
            _deslocamentoVisao = new PointF(
                e.X - pontoCena.X * novoZoom,
                e.Y - pontoCena.Y * novoZoom
            );

            _zoom = novoZoom;

            // Solicita que OnPaint desenhe a nova visualização.
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_imagemQuadra == null)
                return;

            RectangleF area = ObterAreaQuadra();

            if (area.Width <= 0 || area.Height <= 0)
                return;

            Graphics g = e.Graphics;

            // Guarda as configurações gráficas anteriores.
            GraphicsState estadoAnterior = g.Save();

            try
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Aplica zoom e deslocamento a todo o desenho.
                using var transformacao = new Matrix(
                    _zoom, 0f,
                    0f, _zoom,
                    _deslocamentoVisao.X,
                    _deslocamentoVisao.Y
                );

                g.Transform = transformacao;

                // Desenha a imagem da quadra.
                g.DrawImage(_imagemQuadra, area);

                // Desenha na ordem da lista; os últimos ficam por cima.
                using var contorno = new Pen(Color.White, 2f);
                using var contornoSelecao = new Pen(Color.FromArgb(25, 35, 58), 2f);
                using var preenchimento = new SolidBrush(_corAtletaPrincipal);

                foreach (PosicaoElementos posicao in Elementos)
                {
                    PointF centro = PosicaoParaCena(posicao, area);
                    float raio = ObterRaioElemento(posicao, area);
                    preenchimento.Color = posicao switch
                    {
                        PosicaoBola => _corBola,
                        PosicaoAtleta { Lado: LadoAtleta.Adversario } => _corAtletaAdversario,
                        _ => _corAtletaPrincipal
                    };

                    RectangleF circulo = new RectangleF(
                        centro.X - raio,
                        centro.Y - raio,
                        raio * 2f,
                        raio * 2f
                    );

                    g.FillEllipse(preenchimento, circulo);
                    g.DrawEllipse(contorno, circulo);
                    if (ReferenceEquals(posicao, _selecionado))
                        g.DrawEllipse(contornoSelecao, RectangleF.Inflate(circulo, 3f, 3f));

                    if (posicao is PosicaoAtleta posicaoAtleta)
                    {
                        Atleta? atleta = Atletas.Find(j => j.Id == posicaoAtleta.AtletaId);
                        if (atleta != null)
                            DesenharNumeroAtleta(g, atleta.NumeroCamisa, circulo, preenchimento.Color);
                    }
                }
            }
            finally
            {
                // Restaura as configurações, inclusive a transformação.
                g.Restore(estadoAnterior);
            }
        }

        private void DesenharNumeroAtleta(Graphics g, int numero, RectangleF circulo, Color corFundo)
        {
            // A fonte usa as coordenadas da cena; o zoom já é aplicado pelo Graphics.
            using var fonte = new Font(
                Font.FontFamily,
                circulo.Height * 0.45f,
                FontStyle.Bold,
                GraphicsUnit.Pixel
            );
            using var formato = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.NoWrap
            };

            int luminancia = (299 * corFundo.R + 587 * corFundo.G + 114 * corFundo.B) / 1000;
            Brush corNumero = luminancia >= 145 ? Brushes.Black : Brushes.White;
            g.DrawString(numero.ToString(), fonte, corNumero, circulo, formato);
        }

        private RectangleF ObterAreaQuadra()
        {
            float disponivelX = Math.Max(0f, ClientSize.Width - 40f);
            float disponivelY = Math.Max(0f, ClientSize.Height - 40f);

            float proporcao =
                (float)_imagemQuadra.Width / _imagemQuadra.Height;

            float largura = Math.Min(
                disponivelX,
                disponivelY * proporcao
            );

            float altura = largura / proporcao;

            return new RectangleF(
                (ClientSize.Width - largura) / 2f,
                (ClientSize.Height - altura) / 2f,
                largura,
                altura
            );
        }

        // Converte a posição normalizada em um ponto da cena, antes de aplicar o zoom.
        private PointF PosicaoParaCena(
            PosicaoElementos posicao,
            RectangleF area)
        {
            return new PointF(
                area.Left + posicao.X * area.Width,
                area.Top + posicao.Y * area.Height
            );
        }
    }
}
