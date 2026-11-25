using Limk.VolleyBall.Backend;
using System.ComponentModel;
using System.Net.Mail;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaLogin : UserControl
    {
        private static readonly Color CorBorda = Color.FromArgb(222, 215, 236);
        private static readonly Color CorTextoSecundario = Color.FromArgb(100, 116, 139);
        private static readonly Color CorAcao = Color.FromArgb(124, 58, 237);
        private const string MensagemCarregando = "Verificando seu acesso…";
        private bool autenticando;
        private bool organizandoFormulario;
        internal event EventHandler<UsuarioAutenticado>? LoginConcluido;
        internal event EventHandler? CriarContaSolicitada;
        internal IButtonControl BotaoEntrar
        {
            get
            {
                return btnEntrar;
            }
        }

        public TelaLogin()
        {
            InitializeComponent();
            AjustarLayout();
        }

        public void FocarEmail()
        {
            txtEmail.Select();
        }

        internal void MostrarContaCriada(string email)
        {
            txtEmail.Text = email;
            ExibirMensagem("Conta criada com sucesso. Entre com sua senha.");
            txtSenha.Select();
        }

        private void TelaLogin_SizeChanged(object? sender, EventArgs e)
        {
            AjustarLayout();
        }

        private void layoutFormulario_Paint(object? sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, layoutFormulario.ClientRectangle, CorBorda, ButtonBorderStyle.Solid);
        }

        private void AjustarLayout()
        {
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
            layoutLogo.PerformLayout();
            PosicionarFormulario();
        }

        private void pnlConteudo_Layout(object? sender, LayoutEventArgs e)
        {
            PosicionarFormulario();
        }

        private void PosicionarFormulario()
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || organizandoFormulario || pnlConteudo.Width <= 0)
            {
                return;
            }

            organizandoFormulario = true;
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
                organizandoFormulario = false;
            }
        }

        private int MedirFormulario(int largura)
        {
            int larguraTexto = Math.Max(1, largura - layoutFormulario.Padding.Horizontal);
            lblInstrucao.MaximumSize = new Size(larguraTexto, 0);
            lblMensagem.MaximumSize = new Size(larguraTexto, 0);
            return layoutFormulario.GetPreferredSize(new Size(largura, 0)).Height;
        }

        private void campo_Paint(object? sender, PaintEventArgs e)
        {
            Control? campo = sender as Control;
            if (campo != null)
            {
                Color cor = CorBorda;
                if (campo.ContainsFocus)
                {
                    cor = CorAcao;
                }
                ControlPaint.DrawBorder(e.Graphics, campo.ClientRectangle, cor, ButtonBorderStyle.Solid);
            }
        }

        private void campo_FocoAlterado(object? sender, EventArgs e)
        {
            Control? campo = sender as Control;
            if (campo != null)
            {
                campo.Invalidate();
            }
        }

        private void chkMostrarSenha_CheckedChanged(object? sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = !chkMostrarSenha.Checked;
            if (chkMostrarSenha.Checked)
            {
                chkMostrarSenha.Text = "Ocultar";
            }
            else
            {
                chkMostrarSenha.Text = "Mostrar";
            }

            if (chkMostrarSenha.Checked)
            {
                chkMostrarSenha.AccessibleName = "Ocultar senha";
            }
            else
            {
                chkMostrarSenha.AccessibleName = "Mostrar senha";
            }
        }

        private void campo_TextChanged(object? sender, EventArgs e)
        {
            if (!autenticando && lblMensagem.Text.Length > 0)
            {
                ExibirMensagem("");
            }
        }

        private void lnkCriarConta_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!autenticando)
            {
                if (CriarContaSolicitada != null)
                {
                    CriarContaSolicitada(this, EventArgs.Empty);
                }
            }
        }

        private void btnEntrar_Click(object? sender, EventArgs e)
        {
            if (autenticando)
            {
                return;
            }

            string email = txtEmail.Text.Trim();
            if (!MailAddress.TryCreate(email, out MailAddress? endereco) || endereco.Address != email)
            {
                txtEmail.Select();
                ExibirMensagem("Informe um e-mail válido.", erro: true);
                return;
            }

            if (txtSenha.TextLength == 0)
            {
                txtSenha.Select();
                ExibirMensagem("Digite sua senha.", erro: true);
                return;
            }

            DefinirCarregamento(true);
            UsuarioAutenticado? usuario;
            try
            {
                usuario = DadosUsuario.Autenticar(email, txtSenha.Text);
            }
            catch (BancoDeDadosException)
            {
                ExibirMensagem("Não foi possível conectar ao banco. Verifique a conexão e tente novamente.", erro: true);
                return;
            }
            finally
            {
                DefinirCarregamento(false);
            }

            txtSenha.Clear();
            chkMostrarSenha.Checked = false;
            if (usuario == null)
            {
                txtSenha.Select();
                ExibirMensagem("E-mail ou senha inválidos.", erro: true);
                return;
            }

            if (LoginConcluido != null)
            {
                LoginConcluido(this, usuario);
            }
        }

        private void DefinirCarregamento(bool carregando)
        {
            autenticando = carregando;
            txtEmail.ReadOnly = carregando;
            txtSenha.ReadOnly = carregando;
            chkMostrarSenha.Enabled = !carregando;
            btnEntrar.Enabled = !carregando;
            lnkCriarConta.Enabled = !carregando;
            UseWaitCursor = carregando;
            if (carregando)
            {
                chkMostrarSenha.Checked = false;
                ExibirMensagem(MensagemCarregando);
            }
            else if (lblMensagem.Text == MensagemCarregando)
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
                lblMensagem.ForeColor = CorTextoSecundario;
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
