using Limk.VolleyBall.Backend;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class CartaoTaticaInicio : UserControl
    {
        private int _taticaId;
        // Avisa a TelaInicial para solicitar a abertura da tática na prancheta.
        // EventHandler<int> envia este cartão em sender e o ID da tática como segundo argumento.
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
            if (string.IsNullOrWhiteSpace(tatica.GetDescricao()))
            {
                lblDescricao.Text = "Sem descrição cadastrada.";
            }
            else
            {
                lblDescricao.Text = tatica.GetDescricao();
            }

            if (tatica.GetQuadros() == 1)
            {
                lblQuadros.Text = "1 quadro";
            }
            else
            {
                lblQuadros.Text = $"{tatica.GetQuadros():N0} quadros";
            }
        }

        private void SolicitarAbertura(object? sender, EventArgs e)
        {
            if (AbrirTatica != null)
            {
                AbrirTatica(this, _taticaId);
            }
        }
    }
}
