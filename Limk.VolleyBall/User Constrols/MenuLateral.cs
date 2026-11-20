namespace Limk.VolleyBall.User_Constrols
{
    public partial class MenuLateral : UserControl
    {
        private Button? btnAtual;

        private static readonly Color CorSelecionada = Color.FromArgb(92, 48, 154);
        private static readonly Color CorHover = Color.FromArgb(52, 37, 71);

        public event EventHandler<UserControl>? OnMenuSelecionado;

        public MenuLateral()
        {
            InitializeComponent();

            foreach (Button botao in new[] { btnInicio, btnPranchetaTatica, btnTaticas, btnAtletas, btnEquipes })
            {
                botao.BackColor = BackColor;
                botao.ForeColor = Color.FromArgb(244, 239, 250);
                botao.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                botao.TextAlign = ContentAlignment.MiddleLeft;
                botao.Padding = new Padding(12, 0, 4, 0);
                botao.Cursor = Cursors.Hand;
                botao.FlatAppearance.MouseOverBackColor = CorHover;
                botao.FlatAppearance.MouseDownBackColor = CorSelecionada;
            }

            SelecionarBotao(btnInicio);
        }

        // Os atalhos da tela inicial usam a mesma seleção visual do menu.
        public void SelecionarTela(UserControl tela)
        {
            if (tela is TelaInicial)
                SelecionarBotao(btnInicio);
            else if (tela is TelaPranchetaTatica)
                SelecionarBotao(btnPranchetaTatica);
            else if (tela is TelaTaticas)
                SelecionarBotao(btnTaticas);
            else if (tela is TelaEquipes)
                SelecionarBotao(btnEquipes);
            else if (tela is TelaAtletas)
                SelecionarBotao(btnAtletas);
        }

        private void SelecionarBotao(Button botao)
        {
            if (btnAtual != null)
            {
                btnAtual.BackColor = BackColor;
                btnAtual.FlatAppearance.MouseOverBackColor = CorHover;
                btnAtual.FlatAppearance.BorderSize = 0;
            }

            botao.BackColor = CorSelecionada;
            botao.FlatAppearance.MouseOverBackColor = CorSelecionada;
            botao.FlatAppearance.BorderColor = Color.FromArgb(185, 146, 248);
            botao.FlatAppearance.BorderSize = 1;
            btnAtual = botao;
        }

        private void btnPranchetaTatica_Click(object? sender, EventArgs e)
        {
            SelecionarBotao(btnPranchetaTatica);

            OnMenuSelecionado?.Invoke(this, new TelaPranchetaTatica());
        }

        private void btnInicio_Click_1(object? sender, EventArgs e)
        {
            SelecionarBotao(btnInicio);

            OnMenuSelecionado?.Invoke(this, new TelaInicial());
        }

        private void btnTaticas_Click(object? sender, EventArgs e)
        {
            SelecionarBotao(btnTaticas);

            OnMenuSelecionado?.Invoke(this, new TelaTaticas());
        }

        private void btnEquipes_Click(object? sender, EventArgs e)
        {
            SelecionarBotao(btnEquipes);

            OnMenuSelecionado?.Invoke(this, new TelaEquipes());
        }

        private void btnAtletas_Click(object? sender, EventArgs e)
        {
            SelecionarBotao(btnAtletas);

            OnMenuSelecionado?.Invoke(this, new TelaAtletas());
        }
    }
}
