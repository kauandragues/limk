using Limk.VolleyBall.Backend;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class CartaoTaticaInicio : UserControl
    {
        public CartaoTaticaInicio()
        {
            InitializeComponent();
        }

        internal void ExibirTatica(TaticaInicio tatica)
        {
            lblEquipe.Text = tatica.Equipe;
            lblNome.Text = tatica.Nome;
            lblDescricao.Text = string.IsNullOrWhiteSpace(tatica.Descricao)
                ? "Sem descrição cadastrada." : tatica.Descricao;
            lblQuadros.Text = tatica.Quadros == 1 ? "1 quadro" : $"{tatica.Quadros:N0} quadros";
        }
    }
}
