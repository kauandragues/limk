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
            lblInicial.Text = string.IsNullOrWhiteSpace(equipe.GetNome())
                ? "E" : equipe.GetNome().Trim().Substring(0, 1).ToUpper();
            string atletas = equipe.GetAtletas() == 1 ? "1 atleta" : $"{equipe.GetAtletas():N0} atletas";
            string taticas = equipe.GetTaticas() == 1 ? "1 tática" : $"{equipe.GetTaticas():N0} táticas";
            lblResumo.Text = $"{atletas} · {taticas}";
        }
    }
}
