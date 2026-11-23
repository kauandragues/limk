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
            btnAbrirTatica.Click += SolicitarAbertura;
        }

        internal void ExibirTatica(TaticaInicio tatica)
        {
            _taticaId = tatica.GetId();
            btnAbrirTatica.Enabled = _taticaId > 0;
            lblEquipe.Text = tatica.GetEquipe();
            lblNome.Text = tatica.GetNome();
            lblDescricao.Text = string.IsNullOrWhiteSpace(tatica.GetDescricao())
                ? "Sem descrição cadastrada." : tatica.GetDescricao();
            lblQuadros.Text = tatica.GetQuadros() == 1 ? "1 quadro" : $"{tatica.GetQuadros():N0} quadros";
        }

        private void SolicitarAbertura(object? sender, EventArgs e)
        {
            AbrirTatica?.Invoke(this, _taticaId);
        }
    }
}
