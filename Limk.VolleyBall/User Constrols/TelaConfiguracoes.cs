using Limk.VolleyBall.Backend;
using System.ComponentModel;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaConfiguracoes : UserControl, ITelaUsuario
    {
        private UsuarioAutenticado? usuario;
        private bool ocupado;
        private bool preferenciasCarregadas;
        public TelaConfiguracoes()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            DefinirOcupado(false);
        }

        public void DefinirUsuario(UsuarioAutenticado usuarioAutenticado)
        {
            usuario = usuarioAutenticado;
            lblNomeUsuario.Text = usuarioAutenticado.GetNome();
            lblEmailUsuario.Text = usuarioAutenticado.GetEmail();
        }

        private void TelaConfiguracoes_Load(object? sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            Carregar();
        }

        private void btnRecarregar_Click(object? sender, EventArgs e)
        {
            Carregar();
        }

        private void Carregar()
        {
            if (usuario == null || ocupado)
            {
                return;
            }

            preferenciasCarregadas = false;
            btnRecarregar.Visible = false;
            DefinirOcupado(true);
            ExibirStatus("Carregando suas preferências…");
            try
            {
                PreferenciasUsuario preferencias = DadosPreferenciasUsuario.Carregar(usuario.GetId());
                ExibirPreferencias(preferencias);
                preferenciasCarregadas = true;
                ExibirStatus("");
            }
            catch (BancoDeDadosException)
            {
                ExibirStatus("Não foi possível carregar suas preferências. Verifique a conexão e tente novamente.", erro: true);
                btnRecarregar.Visible = true;
            }
            finally
            {
                DefinirOcupado(false);
            }
        }

        private void btnSalvar_Click(object? sender, EventArgs e)
        {
            if (usuario == null || ocupado || !preferenciasCarregadas)
            {
                return;
            }

            PreferenciasUsuario preferencias = new PreferenciasUsuario(lblCorAtletaPrincipal.Text, lblCorAtletaAdversario.Text, lblCorBola.Text, decimal.ToInt32(numDuracaoQuadro.Value));
            DefinirOcupado(true);
            ExibirStatus("Salvando suas preferências…");
            try
            {
                DadosPreferenciasUsuario.Salvar(usuario.GetId(), preferencias);
                ExibirStatus("Preferências salvas.", sucesso: true);
            }
            catch (ValidacaoException erro)
            {
                ExibirStatus(erro.Message, erro: true);
            }
            catch (BancoDeDadosException)
            {
                ExibirStatus("Não foi possível salvar suas preferências. Verifique a conexão e tente novamente.", erro: true);
            }
            finally
            {
                DefinirOcupado(false);
                painelRolagem.ScrollControlIntoView(lblStatus);
            }
        }

        private void btnRestaurarPadrao_Click(object? sender, EventArgs e)
        {
            ExibirPreferencias(PreferenciasUsuario.CriarPadrao());
            ExibirStatus("Valores originais restaurados no formulário. Salve para aplicar.");
        }

        private void ExibirPreferencias(PreferenciasUsuario preferencias)
        {
            ExibirCor(amostraCorAtletaPrincipal, lblCorAtletaPrincipal, preferencias.GetCorAtletaPrincipal());
            ExibirCor(amostraCorAtletaAdversario, lblCorAtletaAdversario, preferencias.GetCorAtletaAdversario());
            ExibirCor(amostraCorBola, lblCorBola, preferencias.GetCorBola());
            numDuracaoQuadro.Value = preferencias.GetDuracaoQuadroPadraoMs();
        }

        private void AlterarCor(Panel amostra, Label rotulo)
        {
            ColorDialog dialogo = new ColorDialog();
            dialogo.Color = amostra.BackColor;
            dialogo.FullOpen = true;
            try
            {
                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                Color cor = dialogo.Color;
                ExibirCor(amostra, rotulo, $"#{cor.R:X2}{cor.G:X2}{cor.B:X2}");
                ExibirStatus("");
            }
            finally
            {
                dialogo.Dispose();
            }
        }

        private void btnCorAtletaPrincipal_Click(object? sender, EventArgs e)
        {
            AlterarCor(amostraCorAtletaPrincipal, lblCorAtletaPrincipal);
        }

        private void btnCorAtletaAdversario_Click(object? sender, EventArgs e)
        {
            AlterarCor(amostraCorAtletaAdversario, lblCorAtletaAdversario);
        }

        private void btnCorBola_Click(object? sender, EventArgs e)
        {
            AlterarCor(amostraCorBola, lblCorBola);
        }

        private void numDuracaoQuadro_ValueChanged(object? sender, EventArgs e)
        {
            if (!ocupado && preferenciasCarregadas)
            {
                ExibirStatus("");
            }
        }

        private static void ExibirCor(Panel amostra, Label rotulo, string cor)
        {
            amostra.BackColor = ColorTranslator.FromHtml(cor);
            rotulo.Text = cor;
        }

        private void DefinirOcupado(bool valor)
        {
            ocupado = valor;
            bool editar = preferenciasCarregadas && !ocupado;
            btnCorAtletaPrincipal.Enabled = editar;
            btnCorAtletaAdversario.Enabled = editar;
            btnCorBola.Enabled = editar;
            numDuracaoQuadro.Enabled = editar;
            btnRestaurarPadrao.Enabled = editar;
            btnSalvar.Enabled = editar;
            btnRecarregar.Enabled = !ocupado;
            UseWaitCursor = ocupado;
        }

        private void ExibirStatus(string texto, bool erro = false, bool sucesso = false)
        {
            if (erro)
            {
                lblStatus.ForeColor = Color.FromArgb(155, 39, 64);
            }
            else
            {
                if (sucesso)
                {
                    lblStatus.ForeColor = Color.FromArgb(26, 103, 67);
                }
                else
                {
                    lblStatus.ForeColor = Color.FromArgb(100, 116, 139);
                }
            }

            lblStatus.Text = texto;
            lblStatus.Visible = texto.Length > 0;
        }

        private void AjustarLayout(object? sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            int largura = Math.Max(1, layoutConteudo.ClientSize.Width - layoutConteudo.Padding.Horizontal);
            lblTitulo.MaximumSize = new Size(largura, 0);
            lblDescricao.MaximumSize = new Size(largura, 0);
            int larguraConta = Math.Max(1, largura - layoutConta.Padding.Horizontal);
            lblNomeUsuario.MaximumSize = new Size(larguraConta, 0);
            lblEmailUsuario.MaximumSize = new Size(larguraConta, 0);
            int larguraPreferencias = Math.Max(1, largura - layoutPreferencias.Padding.Horizontal);
            lblStatus.MaximumSize = new Size(larguraPreferencias, 0);
            foreach (Control item in layoutPreferencias.Controls)
            {
                FlowLayoutPanel? linha = item as FlowLayoutPanel;
                if (linha == null)
                {
                    continue;
                }

                int larguraNecessaria = 0;
                foreach (Control controle in linha.Controls)
                {
                    if (!controle.Visible)
                    {
                        continue;
                    }

                    int larguraControle = controle.Width;
                    if (controle.AutoSize)
                    {
                        larguraControle = controle.PreferredSize.Width;
                    }

                    larguraNecessaria += larguraControle + controle.Margin.Horizontal;
                }

                linha.WrapContents = larguraNecessaria > larguraPreferencias;
            }
        }
    }
}
