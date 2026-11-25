using Limk.VolleyBall.Backend;
using System.Drawing.Drawing2D;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class PranchetaControl : UserControl
    {
        private readonly List<ElementoQuadra> _elementos = new List<ElementoQuadra>();
        // Avisa a TelaPranchetaTatica para atualizar o painel de propriedades ao selecionar ou desmarcar um elemento.
        // Envia esta prancheta em sender e EventArgs.Empty; a tela consulta GetElementoSelecionado().
        public event EventHandler? SelecaoAlterada;
        // Avisa a TelaPranchetaTatica para atualizar as coordenadas quando o elemento selecionado é arrastado.
        // Também usa EventArgs.Empty, pois a tela consulta a posição diretamente no elemento selecionado.
        public event EventHandler? PosicaoAlterada;
        private readonly Image _imagemQuadra = Properties.Resources.Quadra;
        private float _zoom = 1f;
        private PointF _deslocamentoVisao = PointF.Empty;
        private ElementoQuadra? _selecionado;
        private bool _arrastando;
        private PointF _distanciaDoClique;
        private Color _corAtletaPrincipal;
        private Color _corAtletaAdversario;
        private Color _corBola;
        public PranchetaControl()
        {
            InitializeComponent();
            // Desenha primeiro em memória para evitar que a quadra pisque durante os movimentos.
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            AplicarPreferencias(PreferenciasUsuario.CriarPadrao());
        }

        public List<ElementoQuadra> GetElementos()
        {
            return _elementos;
        }

        public ElementoQuadra? GetElementoSelecionado()
        {
            return _selecionado;
        }

        internal void AplicarPreferencias(PreferenciasUsuario preferencias)
        {
            _corAtletaPrincipal = ColorTranslator.FromHtml(preferencias.GetCorAtletaPrincipal());
            _corAtletaAdversario = ColorTranslator.FromHtml(preferencias.GetCorAtletaAdversario());
            _corBola = ColorTranslator.FromHtml(preferencias.GetCorBola());
            Invalidate();
        }

        public void SelecionarElemento(ElementoQuadra? elemento)
        {
            if (elemento != null && !_elementos.Contains(elemento))
            {
                throw new ArgumentException("O elemento deve pertencer à prancheta.", nameof(elemento));
            }

            _arrastando = false;
            Capture = false;
            _selecionado = elemento;
            Invalidate();
            if (SelecaoAlterada != null)
            {
                SelecaoAlterada(this, EventArgs.Empty);
            }
        }

        // A imagem cabe no controle com 20 pixels de margem. Escalar os dois lados
        // pelo mesmo fator mantém as proporções da quadra ao redimensionar a janela.
        private RectangleF CalcularQuadraNaTela()
        {
            const float margem = 20f;
            float larguraDisponivel = Math.Max(0f, ClientSize.Width - 2 * margem);
            float alturaDisponivel = Math.Max(0f, ClientSize.Height - 2 * margem);
            float escala = Math.Min(larguraDisponivel / _imagemQuadra.Width, alturaDisponivel / _imagemQuadra.Height);
            float largura = _imagemQuadra.Width * escala;
            float altura = _imagemQuadra.Height * escala;
            float esquerda = (ClientSize.Width - largura) / 2;
            float topo = (ClientSize.Height - altura) / 2;
            return new RectangleF(esquerda * _zoom + _deslocamentoVisao.X, topo * _zoom + _deslocamentoVisao.Y, largura * _zoom, altura * _zoom);
        }

        private PointF CalcularCentroNaTela(ElementoQuadra elemento, RectangleF quadra)
        {
            // X = 0,25 coloca o centro a um quarto da largura, a partir da esquerda.
            float centroX = quadra.Left + elemento.GetX() * quadra.Width;
            float centroY = quadra.Top + elemento.GetY() * quadra.Height;
            return new PointF(centroX, centroY);
        }

        private float CalcularRaio(ElementoQuadra elemento, RectangleF quadra)
        {
            float ladoMenor = Math.Min(quadra.Width, quadra.Height);
            if (elemento is PosicaoBola)
            {
                return ladoMenor * 0.025f;
            }

            return ladoMenor * 0.04f;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleF quadra = CalcularQuadraNaTela();
            if (quadra.Width <= 0 || quadra.Height <= 0)
            {
                return;
            }

            Graphics desenho = e.Graphics;
            // A suavização deixa as bordas dos círculos e a imagem menos serrilhadas.
            desenho.SmoothingMode = SmoothingMode.AntiAlias;
            desenho.InterpolationMode = InterpolationMode.HighQualityBicubic;
            desenho.DrawImage(_imagemQuadra, quadra);
            foreach (ElementoQuadra elemento in _elementos)
            {
                DesenharElemento(desenho, elemento, quadra);
            }
        }

        private void DesenharElemento(Graphics desenho, ElementoQuadra elemento, RectangleF quadra)
        {
            PointF centro = CalcularCentroNaTela(elemento, quadra);
            float raio = CalcularRaio(elemento, quadra);
            RectangleF circulo = new RectangleF(centro.X - raio, centro.Y - raio, 2 * raio, 2 * raio);
            Color cor = _corBola;
            AtletaQuadro? atleta = elemento as AtletaQuadro;
            if (atleta != null)
            {
                cor = _corAtletaPrincipal;
                if (atleta.GetLado() == LadoAtleta.Adversario)
                {
                    cor = _corAtletaAdversario;
                }
            }

            SolidBrush preenchimento = new SolidBrush(cor);
            try
            {
                Pen contorno = new Pen(Color.White, 2 * _zoom);
                try
                {
                    desenho.FillEllipse(preenchimento, circulo);
                    desenho.DrawEllipse(contorno, circulo);
                    if (elemento == _selecionado)
                    {
                        Pen destaque = new Pen(Color.FromArgb(25, 35, 58), 2 * _zoom);
                        try
                        {
                            RectangleF selecao = RectangleF.Inflate(circulo, 3 * _zoom, 3 * _zoom);
                            desenho.DrawEllipse(destaque, selecao);
                        }
                        finally
                        {
                            destaque.Dispose();
                        }
                    }

                    if (atleta != null)
                    {
                        DesenharNumeroAtleta(desenho, atleta.GetNumeroCamisa(), circulo, cor);
                    }
                }
                finally
                {
                    contorno.Dispose();
                }
            }
            finally
            {
                preenchimento.Dispose();
            }
        }

        private void DesenharNumeroAtleta(Graphics desenho, int numero, RectangleF circulo, Color corFundo)
        {
            // O diâmetro já está em pixels e inclui o zoom.
            Font fonte = new Font(Font.FontFamily, circulo.Height * 0.45f, FontStyle.Bold, GraphicsUnit.Pixel);
            try
            {
                StringFormat formato = new StringFormat();
                formato.Alignment = StringAlignment.Center;
                formato.LineAlignment = StringAlignment.Center;
                formato.FormatFlags = StringFormatFlags.NoWrap;
                try
                {
                    // Esta conta estima se o fundo é claro para escolher um número preto ou branco.
                    int luminancia = (299 * corFundo.R + 587 * corFundo.G + 114 * corFundo.B) / 1000;
                    Brush corNumero = Brushes.White;
                    if (luminancia >= 145)
                    {
                        corNumero = Brushes.Black;
                    }

                    desenho.DrawString(numero.ToString(), fonte, corNumero, circulo, formato);
                }
                finally
                {
                    formato.Dispose();
                }
            }
            finally
            {
                fonte.Dispose();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta == 0)
            {
                return;
            }

            // Uma posição na tela é a posição original multiplicada pelo zoom,
            // mais o deslocamento. Invertemos essa conta para achar o ponto sob o mouse.
            float pontoOriginalX = (e.X - _deslocamentoVisao.X) / _zoom;
            float pontoOriginalY = (e.Y - _deslocamentoVisao.Y) / _zoom;
            float passosDaRoda = e.Delta / 120f;
            float fator = MathF.Pow(1.1f, passosDaRoda);
            // Clamp mantém o resultado entre os limites: aqui, de metade até quatro vezes o tamanho.
            _zoom = Math.Clamp(_zoom * fator, 0.5f, 4f);
            // O ponto original deve voltar ao mesmo pixel depois de ampliar ou reduzir.
            _deslocamentoVisao.X = e.X - pontoOriginalX * _zoom;
            _deslocamentoVisao.Y = e.Y - pontoOriginalY * _zoom;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            RectangleF quadra = CalcularQuadraNaTela();
            ElementoQuadra? clicado = EncontrarElemento(e.Location, quadra);
            SelecionarElemento(clicado);
            if (clicado == null)
            {
                return;
            }

            PointF centro = CalcularCentroNaTela(clicado, quadra);
            // Guardar a distância até o centro evita um salto ao clicar perto da borda.
            _distanciaDoClique.X = (e.X - centro.X) / _zoom;
            _distanciaDoClique.Y = (e.Y - centro.Y) / _zoom;
            _arrastando = true;
            Capture = true;
        }

        private ElementoQuadra? EncontrarElemento(Point mouse, RectangleF quadra)
        {
            if (quadra.Width <= 0 || quadra.Height <= 0)
            {
                return null;
            }

            // Em uma sobreposição, o último elemento desenhado está por cima.
            for (int indice = _elementos.Count - 1; indice >= 0; indice--)
            {
                ElementoQuadra elemento = _elementos[indice];
                PointF centro = CalcularCentroNaTela(elemento, quadra);
                float raio = CalcularRaio(elemento, quadra);
                float distanciaX = mouse.X - centro.X;
                float distanciaY = mouse.Y - centro.Y;
                if (distanciaX * distanciaX + distanciaY * distanciaY <= raio * raio)
                {
                    return elemento;
                }
            }

            return null;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!_arrastando || _selecionado == null)
            {
                return;
            }

            RectangleF quadra = CalcularQuadraNaTela();
            if (quadra.Width <= 0 || quadra.Height <= 0)
            {
                return;
            }

            float centroX = e.X - _distanciaDoClique.X * _zoom;
            float centroY = e.Y - _distanciaDoClique.Y * _zoom;
            float xRelativo = (centroX - quadra.Left) / quadra.Width;
            float yRelativo = (centroY - quadra.Top) / quadra.Height;
            _selecionado.SetX(Math.Clamp(xRelativo, 0f, 1f));
            _selecionado.SetY(Math.Clamp(yRelativo, 0f, 1f));
            Invalidate();
            if (PosicaoAlterada != null)
            {
                PosicaoAlterada(this, EventArgs.Empty);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                _arrastando = false;
                Capture = false;
            }
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            base.OnMouseCaptureChanged(e);
            if (!Capture)
            {
                _arrastando = false;
            }
        }
    }
}
