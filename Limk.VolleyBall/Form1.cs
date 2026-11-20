using Limk.VolleyBall.User_Constrols;
using Limk.VolleyBall.Backend;

namespace Limk.VolleyBall
{
    public partial class Form1 : Form
    {
        private UsuarioAutenticado? _usuario;

        public Form1()
        {
            InitializeComponent();

            menuLateral1.OnMenuSelecionado += MenuLateral_OnMenuSelecionado;

            menuLateral1.Enabled = false;
        }

        private void MenuLateral_OnMenuSelecionado(object? sender, UserControl TelaExibir)
        {
            MostrarTela(TelaExibir);
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
            if (_usuario == null || tela is not ITelaUsuario telaUsuario)
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

            // Liberar a tela anterior também encerra seus controles e eventos.
            while (panelTelas.Controls.Count > 0)
                panelTelas.Controls[0].Dispose();

            tela.Dock = DockStyle.Fill;
            panelTelas.Controls.Add(tela);
            tela.BringToFront();
            menuLateral1.SelecionarTela(tela);
        }
    }
}
