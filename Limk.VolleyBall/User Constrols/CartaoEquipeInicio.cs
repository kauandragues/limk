using Limk.VolleyBall.Backend;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class CartaoEquipeInicio : UserControl
    {
        public CartaoEquipeInicio()
        {
            InitializeComponent();
        }

        internal void ExibirEquipe(EquipeInicio equipe)
        {
            lblNome.Text = equipe.GetNome();
            if (string.IsNullOrWhiteSpace(equipe.GetNome()))
            {
                lblInicial.Text = "E";
            }
            else
            {
                lblInicial.Text = equipe.GetNome().Trim().Substring(0, 1).ToUpper();
            }

            string atletas;
            if (equipe.GetAtletas() == 1)
            {
                atletas = "1 atleta";
            }
            else
            {
                atletas = $"{equipe.GetAtletas():N0} atletas";
            }

            string taticas;
            if (equipe.GetTaticas() == 1)
            {
                taticas = "1 tática";
            }
            else
            {
                taticas = $"{equipe.GetTaticas():N0} táticas";
            }

            lblResumo.Text = $"{atletas} · {taticas}";
        }
    }
}
