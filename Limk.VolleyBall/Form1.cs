using Limk.VolleyBall.User_Constrols;

namespace Limk.VolleyBall
{
    public partial class Form1 : Form
    {
        private string? _nomeUsuario;

        public Form1()
        {
            InitializeComponent();

            menuLateral1.OnMenuSelecionado += MenuLateral_OnMenuSelecionado;

            MostrarTela(new TelaInicial());
        }

        private void MenuLateral_OnMenuSelecionado(object sender, UserControl TelaExibir)
        {
            MostrarTela(TelaExibir);
        }

        // Depois do login, informe o nome aqui para mantê-lo ao voltar ao início.
        public void DefinirUsuario(string? nomeUsuario)
        {
            _nomeUsuario = nomeUsuario;
            if (panelTelas.Controls.Count > 0 && panelTelas.Controls[0] is TelaInicial inicio)
                inicio.DefinirUsuario(_nomeUsuario);
        }

        private void MostrarTela(UserControl tela)
        {
            if (tela is TelaInicial inicio)
            {
                inicio.DefinirUsuario(_nomeUsuario);
                inicio.TelaSolicitada += MenuLateral_OnMenuSelecionado;
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
