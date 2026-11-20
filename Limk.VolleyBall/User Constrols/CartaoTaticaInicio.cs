using Limk.VolleyBall.Backend;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class CartaoTaticaInicio : UserControl
    {
        private int _taticaId;
        public event EventHandler<int>? AbrirTatica;
        public CartaoTaticaInicio()
        {
            InitializeComponent();
            btnAbrirTatica.Click += (sender, e) => AbrirTatica?.Invoke(this, _taticaId);
        }

        internal void ExibirTatica(TaticaInicio tatica)
        {
            _taticaId = tatica.Id;
            btnAbrirTatica.Enabled = _taticaId > 0;
            lblEquipe.Text = tatica.Equipe;
            lblNome.Text = tatica.Nome;
            lblDescricao.Text = string.IsNullOrWhiteSpace(tatica.Descricao)
                ? "Sem descrição cadastrada." : tatica.Descricao;
            lblQuadros.Text = tatica.Quadros == 1 ? "1 quadro" : $"{tatica.Quadros:N0} quadros";
        }
    }
}
