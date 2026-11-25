using Limk.VolleyBall.Backend;
using System.ComponentModel;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaCadastroUsuario : UserControl
    {
        private bool _cadastrando;
        private bool _organizandoFormulario;
        // Avisa o FormLogin que o cadastro terminou; EventHandler<UsuarioAutenticado> envia esta tela e o usuário criado.
        internal event EventHandler<UsuarioAutenticado>? CadastroConcluido;
        // Pede ao FormLogin para voltar ao login; envia EventArgs.Empty porque não precisa passar dados adicionais.
        internal event EventHandler? VoltarParaLogin;

        internal IButtonControl GetBotaoCadastrar()
        {
            return btnCadastrar;
        }

        public TelaCadastroUsuario()
        {
            InitializeComponent();
            AjustarLayout();
        }

        public void FocarNome()
        {
            txtNome.Select();
        }

        private void TelaCadastroUsuario_SizeChanged(object? sender, EventArgs e)
        {
            AjustarLayout();
        }

        private void AjustarLayout()
        {
            // No designer do Visual Studio, o return impede que o layout seja ajustado automaticamente.
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            bool compacto = ClientSize.Width < LogicalToDeviceUnits(760);
            SuspendLayout();
            if (compacto)
            {
                pnlMarca.Dock = DockStyle.Top;
            }
            else
            {
                pnlMarca.Dock = DockStyle.Left;
            }

            pnlMarca.Height = LogicalToDeviceUnits(56);
            pnlMarca.Width = Math.Min(ClientSize.Width * 37 / 100, LogicalToDeviceUnits(400));
            if (compacto)
            {
                layoutMarca.Padding = new Padding(LogicalToDeviceUnits(18), 0, LogicalToDeviceUnits(18), 0);
            }
            else
            {
                layoutMarca.Padding = new Padding(LogicalToDeviceUnits(28));
            }

            if (compacto)
            {
                layoutMarca.RowStyles[2].Height = 0;
            }
            else
            {
                layoutMarca.RowStyles[2].Height = LogicalToDeviceUnits(150);
            }

            // LogicalToDeviceUnits adapta as medidas à escala de tela configurada no Windows.
            if (compacto)
            {
                layoutMarca.RowStyles[0].Height = LogicalToDeviceUnits(56);
                layoutLogo.Size = LogicalToDeviceUnits(new Size(190, 50));
                layoutLogo.ColumnStyles[0].Width = LogicalToDeviceUnits(56);
                imgLogo.Size = LogicalToDeviceUnits(new Size(42, 42));
            }
            else
            {
                layoutMarca.RowStyles[0].Height = LogicalToDeviceUnits(72);
                layoutLogo.Size = LogicalToDeviceUnits(new Size(227, 72));
                layoutLogo.ColumnStyles[0].Width = LogicalToDeviceUnits(84);
                imgLogo.Size = LogicalToDeviceUnits(new Size(64, 64));
            }
            imgQuadra.Visible = !compacto;
            lblPrancheta.Visible = !compacto;
            ResumeLayout(true);
            PosicionarFormulario();
        }

        private void pnlConteudo_Layout(object? sender, LayoutEventArgs e)
        {
            PosicionarFormulario();
        }

        private void PosicionarFormulario()
        {
            // Evita reposicionar o formulário no designer do Visual Studio, durante outro ajuste ou sem largura disponível.
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || _organizandoFormulario || pnlConteudo.Width <= 0)
            {
                return;
            }

            _organizandoFormulario = true;
            try
            {
                int margem = LogicalToDeviceUnits(18);
                int largura = Math.Max(1, Math.Min(LogicalToDeviceUnits(452), pnlConteudo.Width - 2 * margem));
                int altura = MedirFormulario(largura);
                if (altura + 2 * margem > pnlConteudo.Height)
                {
                    largura = Math.Max(1, Math.Min(largura, pnlConteudo.Width - 2 * margem - SystemInformation.VerticalScrollBarWidth));
                    altura = MedirFormulario(largura);
                }

                pnlConteudo.AutoScrollMinSize = new Size(0, altura + 2 * margem);
                int esquerda = Math.Max(0, (pnlConteudo.ClientSize.Width - largura) / 2);
                int topo = Math.Max(margem, (pnlConteudo.ClientSize.Height - altura) / 2);
                layoutFormulario.SetBounds(esquerda, topo + pnlConteudo.AutoScrollPosition.Y, largura, altura);
            }
            finally
            {
                _organizandoFormulario = false;
            }
        }

        private int MedirFormulario(int largura)
        {
            int larguraTexto = Math.Max(1, largura - layoutFormulario.Padding.Horizontal);
            lblInstrucao.MaximumSize = new Size(larguraTexto, 0);
            lblMensagem.MaximumSize = new Size(larguraTexto, 0);
            return layoutFormulario.GetPreferredSize(new Size(largura, 0)).Height;
        }

        private void chkMostrarSenhas_CheckedChanged(object? sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = !chkMostrarSenhas.Checked;
            txtConfirmarSenha.UseSystemPasswordChar = !chkMostrarSenhas.Checked;
            if (chkMostrarSenhas.Checked)
            {
                chkMostrarSenhas.Text = "Ocultar senhas";
            }
            else
            {
                chkMostrarSenhas.Text = "Mostrar senhas";
            }
        }

        private void campo_TextChanged(object? sender, EventArgs e)
        {
            if (!_cadastrando && lblMensagem.Text.Length > 0)
            {
                ExibirMensagem("");
            }
        }

        private void lnkVoltarLogin_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!_cadastrando)
            {
                if (VoltarParaLogin != null)
                {
                    VoltarParaLogin(this, EventArgs.Empty);
                }
            }
        }

        private void btnCadastrar_Click(object? sender, EventArgs e)
        {
            if (_cadastrando)
            {
                return;
            }

            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                txtConfirmarSenha.Select();
                ExibirMensagem("As senhas não coincidem.", erro: true);
                return;
            }

            DefinirCarregamento(true);
            UsuarioAutenticado usuario;
            try
            {
                usuario = DadosUsuario.Cadastrar(txtNome.Text, txtEmail.Text, txtSenha.Text);
            }
            catch (ValidacaoException erro)
            {
                ExibirMensagem(erro.Message, erro: true);
                // A exceção informa qual campo precisa ser corrigido.
                string campo = erro.GetCampo();
                if (campo == "nome")
                {
                    txtNome.Select();
                }
                else if (campo == "email")
                {
                    txtEmail.Select();
                }
                else if (campo == "senha")
                {
                    txtSenha.Select();
                }

                return;
            }
            catch (BancoDeDadosException)
            {
                ExibirMensagem("Não foi possível criar sua conta. Verifique a conexão e tente novamente.", erro: true);
                return;
            }
            finally
            {
                DefinirCarregamento(false);
            }

            txtSenha.Clear();
            txtConfirmarSenha.Clear();
            chkMostrarSenhas.Checked = false;
            if (CadastroConcluido != null)
            {
                CadastroConcluido(this, usuario);
            }
        }

        private void DefinirCarregamento(bool carregando)
        {
            _cadastrando = carregando;
            txtNome.ReadOnly = carregando;
            txtEmail.ReadOnly = carregando;
            txtSenha.ReadOnly = carregando;
            txtConfirmarSenha.ReadOnly = carregando;
            chkMostrarSenhas.Enabled = !carregando;
            btnCadastrar.Enabled = !carregando;
            lnkVoltarLogin.Enabled = !carregando;
            UseWaitCursor = carregando;
            if (carregando)
            {
                chkMostrarSenhas.Checked = false;
                ExibirMensagem("Criando sua conta...");
            }
            else if (lblMensagem.Text == "Criando sua conta...")
            {
                ExibirMensagem("");
            }
        }

        private void ExibirMensagem(string texto, bool erro = false)
        {
            if (erro)
            {
                lblMensagem.ForeColor = Color.FromArgb(155, 39, 64);
            }
            else
            {
                lblMensagem.ForeColor = Color.FromArgb(100, 116, 139);
            }

            lblMensagem.Text = texto;
            lblMensagem.Visible = texto.Length > 0;
            PosicionarFormulario();
            if (texto.Length > 0)
            {
                pnlConteudo.ScrollControlIntoView(lblMensagem);
            }
        }
    }
}
