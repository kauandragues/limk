using Limk.VolleyBall.Backend;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Limk.VolleyBall.User_Constrols
{
    public class PranchetaControl : Control
    {
        public List<PosicaoElementos> Elementos { get; } = new();
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
                return;
            }

            // Desfaz o zoom e o deslocamento da visualização.
            PointF mouseCena = TelaParaCena(e.Location);

            // Começa pelo último desenhado, que aparece por cima.
            for (int i = Elementos.Count - 1; i >= 0; i--)
            {
                PosicaoElementos posicao = Elementos[i];
                float raio = ObterRaioElemento(posicao, area);

                PointF centro = ParaTela(posicao, area);

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
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!_arrastando || _selecionado is null)
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

            if (_imagemQuadra is null)
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
                    PointF centro = ParaTela(posicao, area);
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
                }
            }
            finally
            {
                // Restaura as configurações, inclusive a transformação.
                g.Restore(estadoAnterior);
            }
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

        private PointF ParaTela(
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
