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
            if (_escudoPadrao != null)
            {
                _escudoPadrao.Dispose();
            }

            if (_iconeExcluir != null)
            {
                _iconeExcluir.Dispose();
            }
        }

        internal void ExibirEquipe(DataRow equipe)
        {
            EquipeId = Convert.ToInt32(equipe["id"]);
            label8.Text = equipe["nome"].ToString();
            label9.Text = $"{equipe["atletas"]} atletas";
            label10.Text = $"{equipe["taticas"]} táticas";
            LiberarEscudo();
            _escudo = ImagemEscudo.CriarImagem(equipe["escudo"] as byte[]);
            if (_escudo == null)
            {
                pictureBox3.Image = _escudoPadrao;
            }
            else
            {
                pictureBox3.Image = _escudo;
            }

            if (_escudo == null)
            {
                pictureBox3.BackColor = Color.MediumSlateBlue;
            }
            else
            {
                pictureBox3.BackColor = Color.White;
            }
        }

        internal void DefinirSelecionado(bool selecionado)
        {
            if (selecionado)
            {
                panel3.BackColor = Color.FromArgb(243, 237, 255);
            }
            else
            {
                panel3.BackColor = Color.White;
            }

            if (selecionado)
            {
                label8.ForeColor = Color.FromArgb(92, 48, 154);
            }
            else
            {
                label8.ForeColor = Color.Black;
            }
        }

        private void SelecionarCartao(object? sender, EventArgs e)
        {
            Focus();
            OnClick(EventArgs.Empty);
        }

        private void button8_Click(object? sender, EventArgs e)
        {
            if (VerElenco != null)
            {
                VerElenco(this, e);
            }
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            if (ExcluirEquipe != null)
            {
                ExcluirEquipe(this, e);
            }
        }

        private void LiberarEscudo()
        {
            pictureBox3.Image = null;
            if (_escudo != null)
            {
                _escudo.Dispose();
            }

            _escudo = null;
        }
    }
}
