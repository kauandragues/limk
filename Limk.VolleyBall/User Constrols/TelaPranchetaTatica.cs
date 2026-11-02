using Limk.VolleyBall.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaPranchetaTatica : UserControl
    {
        public PranchetaControl prancheta = new PranchetaControl();
        public TelaPranchetaTatica()
        {
            InitializeComponent();
            prancheta.Dock = DockStyle.Fill;

            prancheta.Elementos.Add(new PosicaoJogador
            {
                JogadorId = 1,
                X = 0.25f,
                Y = 0.5f
            });

            panelPranchetaTatica.Controls.Add(prancheta);
        }

        private void btnAdicionarBola_Click(object sender, EventArgs e)
        {
            prancheta.Elementos.Add(new PosicaoBola
            {
                X = 0.5f,
                Y = 0.5f
            });
            prancheta.Invalidate();
        }

        private void btnAdicionarAtleta_Click(object sender, EventArgs e)
        {
            prancheta.Elementos.Add(new PosicaoJogador
            {
                X = 0.5f,
                Y = 0.5f
            });
            prancheta.Invalidate();
        }
    }
}
