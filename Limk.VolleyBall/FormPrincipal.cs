using Limk.VolleyBall.User_Constrols;
using Limk.VolleyBall.Backend;

namespace Limk.VolleyBall
{
    public partial class FormPrincipal : Form
    {
        private UsuarioAutenticado? _usuario;

        public FormPrincipal()
        {
            InitializeComponent();

            menuLateral1.OnMenuSelecionado += MenuLateral_OnMenuSelecionado;
            menuLateral1.SaidaDaContaSolicitada += SairDaConta;

            menuLateral1.Enabled = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            ColumnStyle colunaMenu = tableLayoutPanel1.ColumnStyles[0];
            colunaMenu.Width = Math.Max(colunaMenu.Width, menuLateral1.ObterLarguraMinima());

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (!e.Cancel && (e.CloseReason is CloseReason.UserClosing or CloseReason.ApplicationExitCall))
                e.Cancel = !ConfirmarSaidaDaTelaAtual();
        }

        private void MenuLateral_OnMenuSelecionado(object? sender, UserControl TelaExibir)
        {
            MostrarTela(TelaExibir);
        }

        internal bool EntrarNaConta()
        {
            using var login = new FormLogin();
            if (login.ShowDialog() != DialogResult.OK || login.Usuario is not { } usuario)
                return false;

            DefinirUsuario(usuario);
            return true;
        }

        private void SairDaConta(object? sender, EventArgs e)
        {
            if (!ConfirmarSaidaDaTelaAtual())
                return;

            _usuario = null;
            menuLateral1.Enabled = false;
            Hide();
            LimparTelaAtual();

            if (EntrarNaConta())
                Show();
            else
                Close();
        }

        public void DefinirUsuario(UsuarioAutenticado usuario)
        {
            if (usuario.Id <= 0)
                throw new ArgumentOutOfRangeException(nameof(usuario));

            _usuario = usuario;
            menuLateral1.Enabled = true;
            MostrarTela(new TelaInicial());
        }

        private void MostrarTela(UserControl tela)
        {
            if (_usuario == null || tela is not ITelaUsuario telaUsuario || !ConfirmarSaidaDaTelaAtual())
            {
                tela.Dispose();
                return;
            }

            telaUsuario.DefinirUsuario(_usuario);
            if (tela is TelaInicial inicio)
            {
                inicio.TelaSolicitada += MenuLateral_OnMenuSelecionado;
            }
            else if (tela is TelaEquipes equipes)
            {
                equipes.TelaSolicitada += MenuLateral_OnMenuSelecionado;
            }
            else if (tela is TelaTaticas taticas)
            {
                taticas.TelaSolicitada += MenuLateral_OnMenuSelecionado;
            }

            LimparTelaAtual();

            tela.Dock = DockStyle.Fill;
            panelTelas.Controls.Add(tela);
            tela.BringToFront();
            menuLateral1.SelecionarTela(tela);
        }

        private void LimparTelaAtual()
        {
            while (panelTelas.Controls.Count > 0)
                panelTelas.Controls[0].Dispose();
        }

        private bool ConfirmarSaidaDaTelaAtual() =>
            panelTelas.Controls.Count == 0 || panelTelas.Controls[0] is not TelaAtletas atletas || atletas.ConfirmarSaida();
    }
}
