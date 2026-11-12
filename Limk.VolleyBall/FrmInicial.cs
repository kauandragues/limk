using Limk.VolleyBall.User_Constrols;

namespace Limk.VolleyBall
{
    public partial class FrmInicial : Form
    {
        public FrmInicial()
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
