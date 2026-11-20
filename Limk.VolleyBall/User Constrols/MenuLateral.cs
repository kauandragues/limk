using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class MenuLateral : UserControl
    {
        private Button btnAtual = null;

        private Color corBtnPadrao = Color.FromArgb(30, 21, 42);
        private Color corBtnAtual = Color.FromArgb(54, 44, 66);

        public event EventHandler<UserControl> OnMenuSelecionado;

        public MenuLateral()
        {
            InitializeComponent();
        }

        // Os atalhos da tela inicial usam a mesma seleção visual do menu.
        public void SelecionarTela(UserControl tela)
        {
            if (tela is TelaInicial)
                MudarCorBotao(btnInicio);
            else if (tela is TelaPranchetaTatica)
                MudarCorBotao(btnPranchetaTatica);
            else if (tela is TelaTaticas)
                MudarCorBotao(btnTaticas);
            else if (tela is TelaEquipes)
                MudarCorBotao(btnEquipes);
            else if (tela is TelaAtletas)
                MudarCorBotao(btnAtletas);
        }

        private void MudarCorBotao(Button botao)
        {
            if (btnAtual != null)
            {
                btnAtual.BackColor = corBtnPadrao;
            }

            botao.BackColor = corBtnAtual;
            btnAtual = botao;
        }

        private void btnPranchetaTatica_Click(object sender, EventArgs e)
        {
            MudarCorBotao((Button)sender);

            OnMenuSelecionado?.Invoke(this, new TelaPranchetaTatica());
        }

        private void btnInicio_Click_1(object sender, EventArgs e)
        {
            MudarCorBotao((Button)sender);

            OnMenuSelecionado?.Invoke(this, new TelaInicial());
        }

        private void btnTaticas_Click(object sender, EventArgs e)
        {
            MudarCorBotao((Button)sender);

            OnMenuSelecionado?.Invoke(this, new TelaTaticas());
        }

        private void btnEquipes_Click(object sender, EventArgs e)
        {
            MudarCorBotao((Button)sender);

            OnMenuSelecionado?.Invoke(this, new TelaEquipes());
        }

        private void btnAtletas_Click(object sender, EventArgs e)
        {
            MudarCorBotao((Button)sender);

            OnMenuSelecionado?.Invoke(this, new TelaAtletas());
        }
    }
}
