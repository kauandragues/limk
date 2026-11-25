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
            // O += registra o método que será chamado quando o menu disparar o evento de navegação.
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
            if (!e.Cancel && (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.ApplicationExitCall))
            {
                e.Cancel = !ConfirmarSaidaDaTelaAtual();
            }
        }

        // Recebe a tela enviada pelo menu ou por um atalho e encaminha o pedido para MostrarTela.
        private void MenuLateral_OnMenuSelecionado(object? sender, UserControl TelaExibir)
        {
            MostrarTela(TelaExibir);
        }

        internal bool EntrarNaConta()
        {
            FormLogin login = new FormLogin();
            try
            {
                if (login.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                UsuarioAutenticado? usuario = login.GetUsuario();
                if (usuario == null)
                {
                    return false;
                }

                DefinirUsuario(usuario);
                return true;
            }
            finally
            {
                login.Dispose();
            }
        }

        private void SairDaConta(object? sender, EventArgs e)
        {
            if (!ConfirmarSaidaDaTelaAtual())
            {
                return;
            }

            _usuario = null;
            menuLateral1.Enabled = false;
            Hide();
            LimparTelaAtual();
            if (EntrarNaConta())
            {
                Show();
            }
            else
            {
                Close();
            }
        }

        public void DefinirUsuario(UsuarioAutenticado usuario)
        {
            if (usuario.GetId() <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(usuario));
            }

            _usuario = usuario;
            menuLateral1.Enabled = true;
            MostrarTela(new TelaInicial());
        }

        private void MostrarTela(UserControl tela)
        {
            if (_usuario == null || !ConfirmarSaidaDaTelaAtual())
            {
                tela.Dispose();
                return;
            }

            // Cada tela recebe o usuário antes de ser adicionada ao painel.
            if (tela is TelaInicial)
            {
                TelaInicial inicio = (TelaInicial)tela;
                inicio.DefinirUsuario(_usuario);
                inicio.TelaSolicitada += MenuLateral_OnMenuSelecionado;
            }
            else if (tela is TelaEquipes)
            {
                TelaEquipes equipes = (TelaEquipes)tela;
                equipes.DefinirUsuario(_usuario);
                equipes.TelaSolicitada += MenuLateral_OnMenuSelecionado;
            }
            else if (tela is TelaTaticas)
            {
                TelaTaticas taticas = (TelaTaticas)tela;
                taticas.DefinirUsuario(_usuario);
                taticas.TelaSolicitada += MenuLateral_OnMenuSelecionado;
            }
            else if (tela is TelaAtletas)
            {
                TelaAtletas atletas = (TelaAtletas)tela;
                atletas.DefinirUsuario(_usuario);
            }
            else if (tela is TelaPranchetaTatica)
            {
                TelaPranchetaTatica prancheta = (TelaPranchetaTatica)tela;
                prancheta.DefinirUsuario(_usuario);
            }
            else if (tela is TelaConfiguracoes)
            {
                TelaConfiguracoes configuracoes = (TelaConfiguracoes)tela;
                configuracoes.DefinirUsuario(_usuario);
            }
            else
            {
                tela.Dispose();
                return;
            }

            // Descarta a tela anterior e coloca a nova no painel do formulário principal.
            LimparTelaAtual();
            tela.Dock = DockStyle.Fill;
            panelTelas.Controls.Add(tela);
            tela.BringToFront();
            menuLateral1.SelecionarTela(tela);
        }

        private void LimparTelaAtual()
        {
            while (panelTelas.Controls.Count > 0)
            {
                panelTelas.Controls[0].Dispose();
            }
        }

        private bool ConfirmarSaidaDaTelaAtual()
        {
            if (panelTelas.Controls.Count == 0)
            {
                return true;
            }

            TelaAtletas? atletas = panelTelas.Controls[0] as TelaAtletas;
            if (atletas == null)
            {
                return true;
            }

            return atletas.ConfirmarSaida();
        }
    }
}
