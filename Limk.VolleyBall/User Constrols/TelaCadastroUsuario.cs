using Limk.VolleyBall.Backend;
using System.ComponentModel;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaCadastroUsuario : UserControl
    {
        private readonly CancellationTokenSource _encerramento = new();
        private bool _cadastrando;
        private bool _organizandoFormulario;

        internal event EventHandler<UsuarioAutenticado>? CadastroConcluido;
        internal event EventHandler? VoltarParaLogin;
        internal IButtonControl BotaoCadastrar => btnCadastrar;

        public TelaCadastroUsuario()
        {
            InitializeComponent();
            AjustarLayout();
        }

        public void FocarNome() => txtNome.Select();

        private void TelaCadastroUsuario_SizeChanged(object? sender, EventArgs e) => AjustarLayout();

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
                _organizandoFormulario || pnlConteudo.Width <= 0)
                return;

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
            chkMostrarSenhas.Text = chkMostrarSenhas.Checked ? "Ocultar senhas" : "Mostrar senhas";
        }

        private void campo_TextChanged(object? sender, EventArgs e)
        {
            if (!_cadastrando && lblMensagem.Text.Length > 0)
                ExibirMensagem(string.Empty);
        }

        private void lnkVoltarLogin_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!_cadastrando)
                VoltarParaLogin?.Invoke(this, EventArgs.Empty);
        }

        private async void btnCadastrar_Click(object? sender, EventArgs e)
        {
            if (_cadastrando)
                return;
            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                txtConfirmarSenha.Select();
                ExibirMensagem("As senhas não coincidem.", erro: true);
                return;
            }

            CancellationToken token = _encerramento.Token;
            DefinirCarregamento(true);
            UsuarioAutenticado usuario;
            try
            {
                usuario = await CadastroUsuario.CadastrarAsync(txtNome.Text, txtEmail.Text, txtSenha.Text, token);
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested)
            {
                return;
            }
            catch (ArgumentException erro)
            {
                if (!token.IsCancellationRequested)
                    ExibirMensagem(erro.Message, erro: true);
                return;
            }
            catch (FalhaCadastroUsuarioException)
            {
                if (!token.IsCancellationRequested)
                    ExibirMensagem("Não foi possível criar sua conta. Verifique a conexão e tente novamente.", erro: true);
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
            txtConfirmarSenha.Clear();
            chkMostrarSenhas.Checked = false;
            CadastroConcluido?.Invoke(this, usuario);
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
                ExibirMensagem(string.Empty);
            }
        }

        private void ExibirMensagem(string texto, bool erro = false)
        {
            lblMensagem.ForeColor = erro ? Color.FromArgb(155, 39, 64) : Color.FromArgb(100, 116, 139);
            lblMensagem.Text = texto;
            lblMensagem.Visible = texto.Length > 0;
            PosicionarFormulario();
            if (texto.Length > 0)
                pnlConteudo.ScrollControlIntoView(lblMensagem);
        }
    }
}
