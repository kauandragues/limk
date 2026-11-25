using Limk.VolleyBall.Backend;
using System.Data;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class CardEquipes : UserControl
    {
        private int _equipeId;
        private Image? _escudo;
        private readonly Image? _escudoPadrao;
        private readonly Image? _iconeExcluir;

        // EventHandler envia este cartão em sender e os argumentos do clique aos métodos inscritos com +=.
        // O ? indica que pode não haver nenhum método inscrito no evento.
        // Pede à TelaEquipes para abrir o elenco; ela identifica a equipe por GetEquipeId() no cartão recebido.
        public event EventHandler? VerElenco;
        // Pede à TelaEquipes para confirmar a exclusão da equipe identificada pelo cartão recebido em sender.
        public event EventHandler? ExcluirEquipe;

        // Expõe o ID por um método, sem criar uma propriedade para o Designer exibir ou gravar.
        internal int GetEquipeId()
        {
            return _equipeId;
        }

        private void SetEquipeId(int equipeId)
        {
            _equipeId = equipeId;
        }

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
            SetEquipeId(Convert.ToInt32(equipe["id"]));
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
