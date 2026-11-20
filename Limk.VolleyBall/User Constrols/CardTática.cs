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
        private int _taticaId;
        public event EventHandler<int>? AbrirTatica;
        public event EventHandler<int>? ApresentarTatica;
        public CardTática()
        {
            InitializeComponent();
            button1.Click += (sender, e) => AbrirTatica?.Invoke(this, _taticaId);
            button2.Click += (sender, e) => ApresentarTatica?.Invoke(this, _taticaId);
        }

        internal void ExibirTatica(DataRow tatica)
        {
            _taticaId = Convert.ToInt32(tatica["id"]);
            label1.Text = tatica["equipe"].ToString();
            label2.Text = tatica["nome"].ToString();
            label3.Text = tatica["descricao"] == DBNull.Value ? "Sem descrição cadastrada." : tatica["descricao"].ToString();
            label4.Text = $"{tatica["quadros"]} quadros configurados";
            button1.Enabled = _taticaId > 0;
            button2.Enabled = _taticaId > 0 && Convert.ToInt32(tatica["quadros"]) > 0;
        }
    }
}
