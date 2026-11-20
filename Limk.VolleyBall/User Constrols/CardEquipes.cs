using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class CardEquipes : UserControl
    {
        private Image? _escudo;
        public event EventHandler? VerElenco;

        public CardEquipes()
        {
            InitializeComponent();
            button8.Click += (sender, e) => VerElenco?.Invoke(this, e);
            Disposed += (sender, e) => _escudo?.Dispose();
        }

        internal void ExibirEquipe(DataRow equipe)
        {
            label8.Text = equipe["nome"].ToString();
            label9.Text = $"{equipe["atletas"]} atletas";
            label10.Text = $"{equipe["taticas"]} táticas";
            _escudo?.Dispose();
            _escudo = null;
            pictureBox3.Image = null;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            if (equipe["escudo"] is byte[] imagem && imagem.Length > 0)
            {
                try
                {
                    using MemoryStream arquivo = new MemoryStream(imagem);
                    using Image original = Image.FromStream(arquivo);
                    _escudo = new Bitmap(original);
                    pictureBox3.Image = _escudo;
                }
                catch (ArgumentException)
                {
                    // Um arquivo inválido não impede a leitura dos dados da equipe.
                }
            }
        }
    }
}
