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
        // Pede à TelaTaticas para abrir a prancheta. EventHandler<int> envia este cartão (sender) e o ID da tática.
        public event EventHandler<int>? AbrirTatica;
        // Pede à TelaTaticas para apresentar a tática, enviando o ID como segundo argumento do evento.
        public event EventHandler<int>? ApresentarTatica;
        // Pede à TelaTaticas para excluir a tática. EventHandler<DataRow> envia o registro usado na confirmação.
        internal event EventHandler<DataRow>? ExcluirTatica;
        private DataRow? registro;
        public CardTática()
        {
            InitializeComponent();
            button1.Click += SolicitarAbertura;
            button2.Click += SolicitarApresentacao;
            btnExcluirTatica.Click += SolicitarExclusao;
        }

        internal void ExibirTatica(DataRow tatica)
        {
            registro = tatica;
            _taticaId = Convert.ToInt32(tatica["id"]);
            label1.Text = tatica["equipe"].ToString();
            label2.Text = tatica["nome"].ToString();
            if (tatica["descricao"] == DBNull.Value)
            {
                label3.Text = "Sem descrição cadastrada.";
            }
            else
            {
                label3.Text = tatica["descricao"].ToString();
            }

            label4.Text = $"{tatica["quadros"]} quadros configurados";
            button1.Enabled = _taticaId > 0;
            btnExcluirTatica.Enabled = _taticaId > 0;
            button2.Enabled = _taticaId > 0 && Convert.ToInt32(tatica["quadros"]) > 0;
        }

        private void SolicitarAbertura(object? sender, EventArgs e)
        {
            if (AbrirTatica != null)
            {
                AbrirTatica(this, _taticaId);
            }
        }

        private void SolicitarApresentacao(object? sender, EventArgs e)
        {
            if (ApresentarTatica != null)
            {
                ApresentarTatica(this, _taticaId);
            }
        }

        private void SolicitarExclusao(object? sender, EventArgs e)
        {
            if (registro != null)
            {
                if (ExcluirTatica != null)
                {
                    ExcluirTatica(this, registro);
                }
            }
        }
    }
}
