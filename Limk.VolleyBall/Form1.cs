using Limk.VolleyBall.User_Constrols;

namespace Limk.VolleyBall
{
    public partial class Form1 : Form
    {
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

        private void MostrarTela(UserControl Tela)
        {
            panelTelas.Controls.Clear();
            Tela.Dock = DockStyle.Fill;
            panelTelas.Controls.Add(Tela);
            Tela.BringToFront();
        }
    }
}
