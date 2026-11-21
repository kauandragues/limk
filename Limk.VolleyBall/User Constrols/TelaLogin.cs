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
        private readonly CancellationTokenSource encerramento = new();
        private bool autenticando;
        private bool organizandoFormulario;

        internal event EventHandler<UsuarioAutenticado>? LoginConcluido;
        internal IButtonControl BotaoEntrar => btnEntrar;

        public TelaLogin()
        {
            InitializeComponent();
            AjustarLayout();
        }

        public void FocarEmail() => txtEmail.Select();

        protected override void Dispose(bool disposing)
        {
            if (disposing && !Disposing && !IsDisposed)
            {
                encerramento.Cancel();
                encerramento.Dispose();
            }

            base.Dispose(disposing);
        }

        private void TelaLogin_SizeChanged(object? sender, EventArgs e) => AjustarLayout();

        private void layoutFormulario_Paint(object? sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, layoutFormulario.ClientRectangle, CorBorda, ButtonBorderStyle.Solid);
        }

        private void AjustarLayout()
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            bool compacto = ClientSize.Width < LogicalToDeviceUnits(760);

            SuspendLayout();
            pnlMarca.Dock = compacto ? DockStyle.Top : DockStyle.Left;
            pnlMarca.Height = LogicalToDeviceUnits(56);
            pnlMarca.Width = Math.Min(ClientSize.Width * 37 / 100, LogicalToDeviceUnits(400));
            layoutMarca.Padding = compacto
                ? new Padding(LogicalToDeviceUnits(18), 0, LogicalToDeviceUnits(18), 0)
                : new Padding(LogicalToDeviceUnits(28));
            layoutMarca.RowStyles[0].Height = LogicalToDeviceUnits(compacto ? 56 : 72);
            layoutMarca.RowStyles[2].Height = compacto ? 0 : LogicalToDeviceUnits(150);
            layoutLogo.Size = LogicalToDeviceUnits(compacto ? new Size(190, 50) : new Size(227, 72));
            layoutLogo.ColumnStyles[0].Width = LogicalToDeviceUnits(compacto ? 56 : 84);
            imgLogo.Size = LogicalToDeviceUnits(compacto ? new Size(42, 42) : new Size(64, 64));
            imgQuadra.Visible = !compacto;
            lblPrancheta.Visible = !compacto;
            ResumeLayout(true);

            PosicionarFormulario();
        }

        private void pnlConteudo_Layout(object? sender, LayoutEventArgs e) => PosicionarFormulario();

        private void PosicionarFormulario()
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                organizandoFormulario || pnlConteudo.Width <= 0)
                return;

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
            if (sender is Control campo)
                ControlPaint.DrawBorder(e.Graphics, campo.ClientRectangle,
                    campo.ContainsFocus ? CorAcao : CorBorda, ButtonBorderStyle.Solid);
        }

        private void campo_FocoAlterado(object? sender, EventArgs e)
        {
            if (sender is Control campo)
                campo.Invalidate();
        }

        private void chkMostrarSenha_CheckedChanged(object? sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = !chkMostrarSenha.Checked;
            chkMostrarSenha.Text = chkMostrarSenha.Checked ? "Ocultar" : "Mostrar";
            chkMostrarSenha.AccessibleName = chkMostrarSenha.Checked ? "Ocultar senha" : "Mostrar senha";
        }

        private void campo_TextChanged(object? sender, EventArgs e)
        {
            if (!autenticando && lblMensagem.Text.Length > 0)
                ExibirMensagem(string.Empty);
        }

        private async void btnEntrar_Click(object? sender, EventArgs e)
        {
            if (autenticando)
                return;

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

            CancellationToken token = encerramento.Token;
            DefinirCarregamento(true);
            UsuarioAutenticado? usuario;
            try
            {
                usuario = await AutenticacaoUsuario.AutenticarAsync(email, txtSenha.Text, token);
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested)
            {
                return;
            }
            catch (FalhaAutenticacaoException)
            {
                if (!token.IsCancellationRequested)
                    ExibirMensagem("Não foi possível conectar ao banco. Verifique a conexão e tente novamente.", erro: true);
                return;
            }
            finally
            {
                if (!token.IsCancellationRequested)
                    DefinirCarregamento(false);
            }

            if (token.IsCancellationRequested)
                return;

            txtSenha.Clear();
            chkMostrarSenha.Checked = false;
            if (usuario is null)
            {
                txtSenha.Select();
                ExibirMensagem("E-mail ou senha inválidos.", erro: true);
                return;
            }

            LoginConcluido?.Invoke(this, usuario);
        }

        private void DefinirCarregamento(bool carregando)
        {
            autenticando = carregando;
            txtEmail.ReadOnly = carregando;
            txtSenha.ReadOnly = carregando;
            chkMostrarSenha.Enabled = !carregando;
            btnEntrar.Enabled = !carregando;
            UseWaitCursor = carregando;

            if (carregando)
            {
                chkMostrarSenha.Checked = false;
                ExibirMensagem(MensagemCarregando);
            }
            else if (lblMensagem.Text == MensagemCarregando)
            {
                ExibirMensagem(string.Empty);
            }
        }

        private void ExibirMensagem(string texto, bool erro = false)
        {
            lblMensagem.ForeColor = erro ? Color.FromArgb(155, 39, 64) : CorTextoSecundario;
            lblMensagem.Text = texto;
            lblMensagem.Visible = texto.Length > 0;
            PosicionarFormulario();

            if (texto.Length > 0)
                pnlConteudo.ScrollControlIntoView(lblMensagem);
        }
    }
}
