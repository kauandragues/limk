using Limk.VolleyBall.Backend;
using System.ComponentModel;
using System.Data;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class CardEquipes : UserControl
    {
        private Image? _escudo;
        private readonly Image? _escudoPadrao;
        private readonly Image? _iconeExcluir;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EquipeId { get; private set; }
        public event EventHandler? VerElenco;
        public event EventHandler? ExcluirEquipe;

        public CardEquipes()
        {
            InitializeComponent();
            _escudoPadrao = pictureBox3.Image;
            _iconeExcluir = button1.Image;
            Disposed += LiberarImagensDoCartao;
        }

        private void LiberarImagensDoCartao(object? sender, EventArgs e)
        {
            Disposed -= LiberarImagensDoCartao;
            _escudoPadrao?.Dispose();
            _iconeExcluir?.Dispose();
        }

        internal void ExibirEquipe(DataRow equipe)
        {
            EquipeId = Convert.ToInt32(equipe["id"]);
            label8.Text = equipe["nome"].ToString();
            label9.Text = $"{equipe["atletas"]} atletas";
            label10.Text = $"{equipe["taticas"]} táticas";
            LiberarEscudo();
            _escudo = ImagemEscudo.CriarImagem(equipe["escudo"] as byte[]);
            pictureBox3.Image = _escudo ?? _escudoPadrao;
            pictureBox3.BackColor = _escudo == null ? Color.MediumSlateBlue : Color.White;
        }

        internal void DefinirSelecionado(bool selecionado)
        {
            panel3.BackColor = selecionado ? Color.FromArgb(243, 237, 255) : Color.White;
            label8.ForeColor = selecionado ? Color.FromArgb(92, 48, 154) : Color.Black;
        }

        private void SelecionarCartao(object? sender, EventArgs e)
        {
            Focus();
            OnClick(EventArgs.Empty);
        }

        private void button8_Click(object? sender, EventArgs e)
        {
            VerElenco?.Invoke(this, e);
        }
        private void button1_Click(object? sender, EventArgs e)
        {
            ExcluirEquipe?.Invoke(this, e);
        }

        private void LiberarEscudo()
        {
            pictureBox3.Image = null;
            _escudo?.Dispose();
            _escudo = null;
        }
    }
}
