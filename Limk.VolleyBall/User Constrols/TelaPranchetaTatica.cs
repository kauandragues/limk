using Limk.VolleyBall.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaPranchetaTatica : UserControl
    {
        // O índice começa em 0; o número mostrado no cartão começa em 1.
        private int _indiceFaseSelecionada;

        public TelaPranchetaTatica()
        {
            InitializeComponent();

            // No Designer, mostra somente os controles, sem criar atletas de exemplo.
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            InicializarSequenciaVisual();
            AdicionarAtleta(0.25f, 0.5f);
        }

        private void btnAdicionarBola_Click(object sender, EventArgs e)
        {
            // Cada quadro tem uma bola. Um novo clique seleciona a bola que já existe.
            foreach (PosicaoElementos elemento in prancheta.Elementos)
            {
                if (elemento is PosicaoBola)
                {
                    prancheta.SelecionarElemento(elemento);
                    return;
                }
            }

            PosicaoBola bola = new PosicaoBola
            {
                X = 0.5f,
                Y = 0.5f
            };
            prancheta.Elementos.Add(bola);
            prancheta.SelecionarElemento(bola);
        }

        private void btnAdicionarAtleta_Click(object sender, EventArgs e)
        {
            AdicionarAtleta(0.5f, 0.5f);
        }

        private void AdicionarAtleta(float x, float y)
        {
            int proximoId = 1;
            int proximoNumero = 1;

            // Encontra valores livres, mesmo quando algum atleta foi removido da quadra.
            foreach (Atleta atleta in prancheta.Atletas)
            {
                proximoId = Math.Max(proximoId, atleta.Id + 1);
                proximoNumero = Math.Max(proximoNumero, atleta.Numero + 1);
            }

            Atleta novoAtleta = new Atleta
            {
                Id = proximoId,
                Nome = $"Atleta {proximoNumero}",
                Numero = proximoNumero
            };
            prancheta.Atletas.Add(novoAtleta);

            // O cadastro guarda o número; a posição guarda apenas o vínculo e as coordenadas.
            PosicaoAtleta posicao = new PosicaoAtleta
            {
                AtletaId = novoAtleta.Id,
                X = x,
                Y = y
            };
            prancheta.Elementos.Add(posicao);
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
            PosicaoElementos? elemento = prancheta.ElementoSelecionado;
            bool temSelecao = elemento != null;

            layoutCamposAtleta.Visible = elemento is PosicaoAtleta;
            layoutCoordenadas.Enabled = temSelecao;
            btnAplicarPropriedades.Enabled = temSelecao;
            btnRemoverElemento.Enabled = temSelecao;
            labelMensagemPropriedades.ForeColor = Color.FromArgb(126, 144, 171);
            textNumeroCamisa.Clear();
            comboAtletaVinculado.Items.Clear();

            foreach (Atleta atleta in prancheta.Atletas)
            {
                comboAtletaVinculado.Items.Add(atleta);
            }
            comboAtletaVinculado.SelectedIndex = -1;

            if (elemento == null)
            {
                labelResumoElemento.Text = "NENHUM ELEMENTO\nSelecione na quadra";
                labelMensagemPropriedades.Text = "Clique em um atleta ou em uma bola para ver suas propriedades.";
            }
            else
            {
                labelMensagemPropriedades.Text = "As coordenadas acompanham o arraste na quadra.";

                if (elemento is PosicaoAtleta posicaoAtleta)
                {
                    // Procura o cadastro cujo Id corresponde ao AtletaId desta posição.
                    Atleta? atleta = prancheta.Atletas.Find(j => j.Id == posicaoAtleta.AtletaId);
                    if (atleta == null)
                    {
                        labelResumoElemento.Text = "ATLETA\nSem atleta vinculado";
                    }
                    else
                    {
                        comboAtletaVinculado.SelectedItem = atleta;
                        textNumeroCamisa.Text = atleta.Numero.ToString();
                        labelResumoElemento.Text = $"#{atleta.Numero}  •  ATLETA\n{atleta.Nome}";
                    }
                }
                else
                {
                    labelResumoElemento.Text = "BOLA\nPosição na quadra";
                }
            }

            AtualizarCoordenadas();
        }

        private void AtualizarCoordenadas()
        {
            PosicaoElementos? elemento = prancheta.ElementoSelecionado;
            if (elemento == null)
            {
                numericCoordenadaX.Value = 0;
                numericCoordenadaY.Value = 0;
                return;
            }

            // A posição usa 0 a 1; os campos da tela mostram 0% a 100%.
            numericCoordenadaX.Value = (decimal)Math.Clamp(elemento.X, 0f, 1f) * 100m;
            numericCoordenadaY.Value = (decimal)Math.Clamp(elemento.Y, 0f, 1f) * 100m;
        }

        private void comboAtletaVinculado_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Atleta? atleta = comboAtletaVinculado.SelectedItem as Atleta;
            if (atleta != null)
                textNumeroCamisa.Text = atleta.Numero.ToString();
        }

        private void btnAplicarPropriedades_Click(object? sender, EventArgs e)
        {
            PosicaoElementos? elemento = prancheta.ElementoSelecionado;
            if (elemento == null)
                return;

            if (elemento is PosicaoAtleta posicaoAtleta)
            {
                Atleta? atleta = comboAtletaVinculado.SelectedItem as Atleta;
                int numero;

                // TryParse verifica o texto sem causar erro quando ele não é um número.
                if (atleta == null || !int.TryParse(textNumeroCamisa.Text, out numero) || numero < 0 || numero > 999)
                {
                    labelMensagemPropriedades.ForeColor = Color.FromArgb(231, 46, 89);
                    labelMensagemPropriedades.Text = "Escolha um atleta e informe um número entre 0 e 999.";
                    return;
                }
                posicaoAtleta.AtletaId = atleta.Id;
                atleta.Numero = numero;
            }

            // Atualiza somente a prévia em memória, sem salvar no banco.
            elemento.X = (float)(numericCoordenadaX.Value / 100m);
            elemento.Y = (float)(numericCoordenadaY.Value / 100m);
            prancheta.Invalidate();
            AtualizarPropriedades();
        }

        private void btnRemoverElemento_Click(object? sender, EventArgs e)
        {
            PosicaoElementos? elemento = prancheta.ElementoSelecionado;
            if (elemento == null)
                return;

            prancheta.Elementos.Remove(elemento);
            prancheta.SelecionarElemento(null);
        }

        private void InicializarSequenciaVisual()
        {
            for (int i = 0; i < flowFases.Controls.Count; i++)
            {
                CartaoFaseControl cartao = (CartaoFaseControl)flowFases.Controls[i];
                if (cartao.Ativo)
                {
                    _indiceFaseSelecionada = i;
                    break;
                }
            }
            AtualizarSequenciaVisual();
        }

        private void btnNovaFase_Click(object? sender, EventArgs e)
        {
            CartaoFaseControl novaFase = new CartaoFaseControl
            {
                NomeFase = "Nova fase",
                Tipo = "Preparação",
                Duracao = 2m
            };
            AdicionarCartao(novaFase, flowFases.Controls.Count);
        }

        private void AdicionarCartao(CartaoFaseControl cartao, int indice)
        {
            cartao.Margin = new Padding(0, 0, 10, 0);
            cartao.Click += CartaoFase_Click;
            flowFases.Controls.Add(cartao);
            flowFases.Controls.SetChildIndex(cartao, indice);
            _indiceFaseSelecionada = indice;
            AtualizarSequenciaVisual();
        }

        private void btnFaseAnterior_Click(object? sender, EventArgs e)
        {
            SelecionarFaseVisual(_indiceFaseSelecionada - 1);
        }

        private void btnProximaFase_Click(object? sender, EventArgs e)
        {
            SelecionarFaseVisual(_indiceFaseSelecionada + 1);
        }

        private void btnDuplicarFase_Click(object? sender, EventArgs e)
        {
            if (flowFases.Controls.Count == 0)
                return;

            CartaoFaseControl faseAtual = (CartaoFaseControl)flowFases.Controls[_indiceFaseSelecionada];
            CartaoFaseControl copia = new CartaoFaseControl
            {
                NomeFase = faseAtual.NomeFase + " (cópia)",
                Tipo = faseAtual.Tipo,
                Duracao = faseAtual.Duracao
            };
            AdicionarCartao(copia, _indiceFaseSelecionada + 1);
        }

        private void btnExcluirFase_Click(object? sender, EventArgs e)
        {
            // Mantém ao menos uma fase na sequência.
            if (flowFases.Controls.Count <= 1)
                return;

            Control cartao = flowFases.Controls[_indiceFaseSelecionada];
            flowFases.Controls.Remove(cartao);
            cartao.Dispose();
            _indiceFaseSelecionada = Math.Min(_indiceFaseSelecionada, flowFases.Controls.Count - 1);
            AtualizarSequenciaVisual();
        }

        private void CartaoFase_Click(object? sender, EventArgs e)
        {
            CartaoFaseControl? cartao = sender as CartaoFaseControl;
            if (cartao != null)
                SelecionarFaseVisual(flowFases.Controls.GetChildIndex(cartao));
        }

        private void SelecionarFaseVisual(int indice)
        {
            if (indice < 0 || indice >= flowFases.Controls.Count)
                return;

            _indiceFaseSelecionada = indice;
            AtualizarSequenciaVisual();
        }

        private void AtualizarSequenciaVisual()
        {
            // Nesta prévia, os próprios cartões guardam os dados das fases.
            TimeSpan inicio = TimeSpan.Zero;
            for (int i = 0; i < flowFases.Controls.Count; i++)
            {
                CartaoFaseControl cartao = (CartaoFaseControl)flowFases.Controls[i];
                cartao.Numero = i + 1;
                cartao.Inicio = inicio;
                cartao.Ativo = i == _indiceFaseSelecionada;
                cartao.Name = $"cartaoFase{i + 1}";
                cartao.TabIndex = i;
                cartao.AccessibleName = $"Fase {i + 1}: {cartao.NomeFase}";
                inicio = inicio.Add(TimeSpan.FromSeconds((double)cartao.Duracao));
            }

            btnFaseAnterior.Enabled = _indiceFaseSelecionada > 0;
            btnProximaFase.Enabled = _indiceFaseSelecionada < flowFases.Controls.Count - 1;
            btnExcluirFase.Enabled = flowFases.Controls.Count > 1;
            btnDuplicarFase.Enabled = flowFases.Controls.Count > 0;

            if (flowFases.Controls.Count == 0)
            {
                labelResumoSequencia.Text = "Prévia visual • Nenhuma fase";
            }
            else
            {
                labelResumoSequencia.Text = $"Prévia visual • {flowFases.Controls.Count} fases • Fase {_indiceFaseSelecionada + 1:00} selecionada";
                flowFases.ScrollControlIntoView(flowFases.Controls[_indiceFaseSelecionada]);
            }
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

        public PranchetaControl()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
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

                foreach (PosicaoElementos posicao in Elementos)
                {
                    PointF centro = PosicaoParaCena(posicao, area);
                    float raio = ObterRaioElemento(posicao, area);
                    Brush preenchimento = posicao is PosicaoBola
                        ? Brushes.Gold
                        : Brushes.RoyalBlue;

                    RectangleF circulo = new RectangleF(
                        centro.X - raio,
                        centro.Y - raio,
                        raio * 2f,
                        raio * 2f
                    );

                    g.FillEllipse(
                        ReferenceEquals(posicao, _selecionado)
                            ? Brushes.OrangeRed
                            : preenchimento,
                        circulo
                    );
                    g.DrawEllipse(contorno, circulo);

                    if (posicao is PosicaoAtleta posicaoAtleta)
                    {
                        Atleta? atleta = Atletas.Find(j => j.Id == posicaoAtleta.AtletaId);
                        if (atleta != null)
                            DesenharNumeroAtleta(g, atleta.Numero, circulo);
                    }
                }
            }
            finally
            {
                // Restaura as configurações, inclusive a transformação.
                g.Restore(estadoAnterior);
            }
        }

        private void DesenharNumeroAtleta(Graphics g, int numero, RectangleF circulo)
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

            g.DrawString(numero.ToString(), fonte, Brushes.White, circulo, formato);
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
