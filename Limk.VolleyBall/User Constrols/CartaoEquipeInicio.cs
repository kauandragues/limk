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
            lblNome.Text = equipe.Nome;
            lblInicial.Text = string.IsNullOrWhiteSpace(equipe.Nome)
                ? "E" : equipe.Nome.Trim().Substring(0, 1).ToUpper();
            string atletas = equipe.Atletas == 1 ? "1 atleta" : $"{equipe.Atletas:N0} atletas";
            string taticas = equipe.Taticas == 1 ? "1 tática" : $"{equipe.Taticas:N0} táticas";
            lblResumo.Text = $"{atletas} · {taticas}";
        }
    }
}
