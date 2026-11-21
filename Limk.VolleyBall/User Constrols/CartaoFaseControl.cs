using System.ComponentModel;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class CartaoFaseControl : UserControl
    {
        private int _numero;
        private string _nomeFase = string.Empty;
        private string _tipo = string.Empty;
        private TimeSpan _inicio;
        private bool _ativo;
        private Point _inicioArraste;
        private bool _mousePressionado;

        public event EventHandler? ArrasteSolicitado;

        // Category e DefaultValue organizam estas propriedades no Designer do Visual Studio.
        [Category("Fase"), DefaultValue(0)]
        public int Numero
        {
            get { return _numero; }
            set
            {
                if (_numero == value) return;
                _numero = value;
                AtualizarCartao();
            }
        }

        [Category("Fase"), DefaultValue("")]
        public string NomeFase
        {
            get { return _nomeFase; }
            set
            {
                if (_nomeFase == value) return;
                _nomeFase = value;
                AtualizarCartao();
            }
        }

        [Category("Fase"), DefaultValue("")]
        public string Tipo
        {
            get { return _tipo; }
            set
            {
                if (_tipo == value) return;
                _tipo = value;
                AtualizarCartao();
            }
        }

        [Category("Fase"), DefaultValue(typeof(TimeSpan), "00:00:00")]
        public TimeSpan Inicio
        {
            get { return _inicio; }
            set
            {
                if (_inicio == value) return;
                _inicio = value;
                AtualizarCartao();
            }
        }

        [Category("Fase"), DefaultValue(false)]
        public bool Ativo
        {
            get { return _ativo; }
            set
            {
                if (_ativo == value) return;
                _ativo = value;
                AtualizarCartao();
            }
        }

        [Category("Fase"), DefaultValue(typeof(decimal), "2")]
        public decimal Duracao { get; set; } = 2m;

        public CartaoFaseControl()
        {
            InitializeComponent();
        }

        private void btnSelecionarFase_Click(object? sender, EventArgs e)
        {
            // Encaminha o clique do botão para o cartão, usado pela tela da prancheta.
            OnClick(EventArgs.Empty);
        }

        private void btnSelecionarFase_MouseDown(object? sender, MouseEventArgs e)
        {
            _mousePressionado = e.Button == MouseButtons.Left;
            _inicioArraste = e.Location;
        }

        private void btnSelecionarFase_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!_mousePressionado || e.Button != MouseButtons.Left)
                return;
            // Um pequeno movimento ainda conta como clique, seguindo a tolerância do Windows.
            Size tolerancia = SystemInformation.DragSize;
            var areaClique = new Rectangle(_inicioArraste.X - tolerancia.Width / 2,
                _inicioArraste.Y - tolerancia.Height / 2, tolerancia.Width, tolerancia.Height);
            if (areaClique.Contains(e.Location))
                return;
            _mousePressionado = false;
            btnSelecionarFase.Capture = false;
            ArrasteSolicitado?.Invoke(this, EventArgs.Empty);
        }

        private void btnSelecionarFase_MouseUp(object? sender, MouseEventArgs e)
        {
            _mousePressionado = false;
        }

        private void AtualizarCartao()
        {
            string titulo = $"FASE {Numero:00}";
            if (Ativo)
                titulo += " • ATIVA";

            long minutos = Inicio.Ticks / TimeSpan.TicksPerMinute;
            string horario = $"{minutos:00}:{Inicio.Seconds:00}.{Inicio.Milliseconds / 100}";
            btnSelecionarFase.Text = $"{titulo}\n{NomeFase}\n{Tipo} • {horario}";

            if (Ativo)
            {
                btnSelecionarFase.BackColor = Color.FromArgb(243, 237, 255);
                btnSelecionarFase.ForeColor = Color.FromArgb(124, 58, 237);
                btnSelecionarFase.FlatAppearance.BorderColor = btnSelecionarFase.ForeColor;
            }
            else
            {
                btnSelecionarFase.BackColor = Color.White;
                btnSelecionarFase.ForeColor = Color.FromArgb(25, 35, 58);
                btnSelecionarFase.FlatAppearance.BorderColor = Color.Gainsboro;
            }
        }
    }
}
