namespace Limk.VolleyBall.User_Constrols
{
    public partial class CartaoFaseControl : UserControl
    {
        private int _numero;
        private string _nomeFase = "";
        private string _tipo = "";
        private TimeSpan _inicio;
        private bool _ativo;
        private int _duracaoMs = 2000;
        private Point _inicioArraste;
        private bool _mousePressionado;
        // Avisa a TelaPranchetaTatica para iniciar o arraste do cartão e permitir reordenar os quadros.
        // EventHandler envia este cartão em sender e EventArgs.Empty, pois não há dados extras.
        public event EventHandler? ArrasteSolicitado;
        public int GetNumero()
        {
            return _numero;
        }

        public void SetNumero(int numero)
        {
            if (_numero != numero)
            {
                _numero = numero;
                AtualizarCartao();
            }
        }

        public string GetNomeFase()
        {
            return _nomeFase;
        }

        public void SetNomeFase(string nomeFase)
        {
            if (_nomeFase != nomeFase)
            {
                _nomeFase = nomeFase;
                AtualizarCartao();
            }
        }

        public string GetTipo()
        {
            return _tipo;
        }

        public void SetTipo(string tipo)
        {
            if (_tipo != tipo)
            {
                _tipo = tipo;
                AtualizarCartao();
            }
        }

        public TimeSpan GetInicio()
        {
            return _inicio;
        }

        public void SetInicio(TimeSpan inicio)
        {
            if (_inicio != inicio)
            {
                _inicio = inicio;
                AtualizarCartao();
            }
        }

        public bool GetAtivo()
        {
            return _ativo;
        }

        public void SetAtivo(bool ativo)
        {
            if (_ativo != ativo)
            {
                _ativo = ativo;
                AtualizarCartao();
            }
        }

        public int GetDuracaoMs()
        {
            return _duracaoMs;
        }

        public void SetDuracaoMs(int duracaoMs)
        {
            _duracaoMs = duracaoMs;
        }

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
            {
                return;
            }

            // Um pequeno movimento ainda conta como clique, seguindo a tolerância do Windows.
            Size tolerancia = SystemInformation.DragSize;
            Rectangle areaClique = new Rectangle(_inicioArraste.X - tolerancia.Width / 2, _inicioArraste.Y - tolerancia.Height / 2, tolerancia.Width, tolerancia.Height);
            if (areaClique.Contains(e.Location))
            {
                return;
            }

            _mousePressionado = false;
            btnSelecionarFase.Capture = false;
            if (ArrasteSolicitado != null)
            {
                ArrasteSolicitado(this, EventArgs.Empty);
            }
        }

        private void btnSelecionarFase_MouseUp(object? sender, MouseEventArgs e)
        {
            _mousePressionado = false;
        }

        private void AtualizarCartao()
        {
            string titulo = $"FASE {_numero:00}";
            if (_ativo)
            {
                titulo += " • ATIVA";
            }

            long minutos = _inicio.Ticks / TimeSpan.TicksPerMinute;
            string horario = $"{minutos:00}:{_inicio.Seconds:00}.{_inicio.Milliseconds / 100}";
            btnSelecionarFase.Text = $"{titulo}\n{_nomeFase}\n{_tipo} • {horario}";
            if (_ativo)
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
