using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class CardTática : UserControl
    {
        public CardTática()
        {
            InitializeComponent();
        }

        internal void ExibirTatica(DataRow tatica)
        {
            label1.Text = tatica["equipe"].ToString();
            label2.Text = tatica["nome"].ToString();
            label3.Text = tatica["descricao"] == DBNull.Value ? "Sem descrição cadastrada." : tatica["descricao"].ToString();
            label4.Text = $"{tatica["quadros"]} quadros configurados";
            // A abertura e a apresentação de táticas salvas ainda serão implementadas.
            button1.Enabled = false;
            button2.Enabled = false;
        }
    }
}
