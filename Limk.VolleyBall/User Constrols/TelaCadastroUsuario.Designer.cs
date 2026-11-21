using System.Windows.Forms.Automation;

namespace Limk.VolleyBall.User_Constrols
{
    partial class TelaCadastroUsuario
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                _encerramento.Cancel();
                _encerramento.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlMarca = new Panel();
            layoutMarca = new TableLayoutPanel();
            layoutLogo = new TableLayoutPanel();
            imgLogo = new PictureBox();
            lblMarca = new Label();
            imgQuadra = new PictureBox();
            lblPrancheta = new Label();
            pnlConteudo = new Panel();
            layoutFormulario = new TableLayoutPanel();
            lblTitulo = new Label();
            lblInstrucao = new Label();
            lblNome = new Label();
            txtNome = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblSenha = new Label();
            txtSenha = new TextBox();
            lblConfirmarSenha = new Label();
            txtConfirmarSenha = new TextBox();
            chkMostrarSenhas = new CheckBox();
            btnCadastrar = new Button();
            lblMensagem = new Label();
            lnkVoltarLogin = new LinkLabel();
            pnlMarca.SuspendLayout();
            layoutMarca.SuspendLayout();
            layoutLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imgLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imgQuadra).BeginInit();
            pnlConteudo.SuspendLayout();
            layoutFormulario.SuspendLayout();
            SuspendLayout();

            pnlMarca.BackColor = Color.FromArgb(30, 21, 42);
            pnlMarca.Controls.Add(layoutMarca);
            pnlMarca.Dock = DockStyle.Left;
            pnlMarca.Name = "pnlMarca";
            pnlMarca.Size = new Size(325, 590);
            pnlMarca.TabStop = false;

            layoutMarca.ColumnCount = 1;
            layoutMarca.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMarca.Controls.Add(layoutLogo, 0, 0);
            layoutMarca.Controls.Add(imgQuadra, 0, 2);
            layoutMarca.Controls.Add(lblPrancheta, 0, 3);
            layoutMarca.Dock = DockStyle.Fill;
            layoutMarca.Margin = new Padding(0);
            layoutMarca.Name = "layoutMarca";
            layoutMarca.Padding = new Padding(28);
            layoutMarca.RowCount = 5;
            layoutMarca.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            layoutMarca.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutMarca.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            layoutMarca.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutMarca.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            layoutLogo.Anchor = AnchorStyles.Left;
            layoutLogo.ColumnCount = 2;
            layoutLogo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 84F));
            layoutLogo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutLogo.Controls.Add(imgLogo, 0, 0);
            layoutLogo.Controls.Add(lblMarca, 1, 0);
            layoutLogo.Margin = new Padding(0);
            layoutLogo.Name = "layoutLogo";
            layoutLogo.RowCount = 1;
            layoutLogo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutLogo.Size = new Size(227, 72);

            imgLogo.AccessibleName = "LIMK Volleyball";
            imgLogo.Anchor = AnchorStyles.Left;
            imgLogo.Image = Properties.Resources.LogoMenuLateral;
            imgLogo.Margin = new Padding(0);
            imgLogo.Name = "imgLogo";
            imgLogo.Size = new Size(64, 64);
            imgLogo.SizeMode = PictureBoxSizeMode.Zoom;
            imgLogo.TabStop = false;

            lblMarca.Dock = DockStyle.Fill;
            lblMarca.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblMarca.ForeColor = Color.White;
            lblMarca.Margin = new Padding(0);
            lblMarca.Name = "lblMarca";
            lblMarca.Text = "LIMK";
            lblMarca.TextAlign = ContentAlignment.MiddleLeft;

            imgQuadra.Dock = DockStyle.Fill;
            imgQuadra.Image = Properties.Resources.Quadra;
            imgQuadra.Margin = new Padding(0);
            imgQuadra.Name = "imgQuadra";
            imgQuadra.SizeMode = PictureBoxSizeMode.Zoom;
            imgQuadra.TabStop = false;

            lblPrancheta.AutoSize = true;
            lblPrancheta.Dock = DockStyle.Top;
            lblPrancheta.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPrancheta.ForeColor = Color.White;
            lblPrancheta.Margin = new Padding(0, 12, 0, 0);
            lblPrancheta.Name = "lblPrancheta";
            lblPrancheta.Text = "Prancheta tática de voleibol";
            lblPrancheta.TextAlign = ContentAlignment.TopCenter;

            pnlConteudo.AutoScroll = true;
            pnlConteudo.Controls.Add(layoutFormulario);
            pnlConteudo.Dock = DockStyle.Fill;
            pnlConteudo.Name = "pnlConteudo";
            pnlConteudo.TabIndex = 0;

            layoutFormulario.BackColor = Color.White;
            layoutFormulario.ColumnCount = 1;
            layoutFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutFormulario.Controls.Add(lblTitulo, 0, 0);
            layoutFormulario.Controls.Add(lblInstrucao, 0, 1);
            layoutFormulario.Controls.Add(lblNome, 0, 2);
            layoutFormulario.Controls.Add(txtNome, 0, 3);
            layoutFormulario.Controls.Add(lblEmail, 0, 4);
            layoutFormulario.Controls.Add(txtEmail, 0, 5);
            layoutFormulario.Controls.Add(lblSenha, 0, 6);
            layoutFormulario.Controls.Add(txtSenha, 0, 7);
            layoutFormulario.Controls.Add(lblConfirmarSenha, 0, 8);
            layoutFormulario.Controls.Add(txtConfirmarSenha, 0, 9);
            layoutFormulario.Controls.Add(chkMostrarSenhas, 0, 10);
            layoutFormulario.Controls.Add(btnCadastrar, 0, 11);
            layoutFormulario.Controls.Add(lblMensagem, 0, 12);
            layoutFormulario.Controls.Add(lnkVoltarLogin, 0, 13);
            layoutFormulario.Location = new Point(61, 18);
            layoutFormulario.Name = "layoutFormulario";
            layoutFormulario.Padding = new Padding(26, 24, 26, 24);
            layoutFormulario.RowCount = 14;
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.Size = new Size(452, 550);

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTitulo.Margin = new Padding(0, 0, 0, 4);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Text = "Criar conta";

            lblInstrucao.AutoSize = true;
            lblInstrucao.Font = new Font("Segoe UI", 10F);
            lblInstrucao.ForeColor = Color.FromArgb(100, 116, 139);
            lblInstrucao.Margin = new Padding(0, 0, 0, 18);
            lblInstrucao.Name = "lblInstrucao";
            lblInstrucao.Text = "Preencha seus dados. Use uma senha com pelo menos 8 caracteres.";

            lblNome.AutoSize = true;
            lblNome.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNome.Margin = new Padding(0, 0, 0, 6);
            lblNome.Name = "lblNome";
            lblNome.Text = "Nome";

            txtNome.AccessibleName = "Nome";
            txtNome.Dock = DockStyle.Top;
            txtNome.Font = new Font("Segoe UI", 11F);
            txtNome.Margin = new Padding(0, 0, 0, 12);
            txtNome.MaxLength = 100;
            txtNome.Name = "txtNome";
            txtNome.PlaceholderText = "Seu nome";
            txtNome.TabIndex = 0;
            txtNome.TextChanged += campo_TextChanged;

            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmail.Margin = new Padding(0, 0, 0, 6);
            lblEmail.Name = "lblEmail";
            lblEmail.Text = "E-mail";

            txtEmail.AccessibleName = "E-mail";
            txtEmail.Dock = DockStyle.Top;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.Margin = new Padding(0, 0, 0, 12);
            txtEmail.MaxLength = 254;
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "nome@exemplo.com";
            txtEmail.TabIndex = 1;
            txtEmail.TextChanged += campo_TextChanged;

            lblSenha.AutoSize = true;
            lblSenha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSenha.Margin = new Padding(0, 0, 0, 6);
            lblSenha.Name = "lblSenha";
            lblSenha.Text = "Senha";

            txtSenha.AccessibleName = "Senha";
            txtSenha.Dock = DockStyle.Top;
            txtSenha.Font = new Font("Segoe UI", 11F);
            txtSenha.Margin = new Padding(0, 0, 0, 12);
            txtSenha.MaxLength = 72;
            txtSenha.Name = "txtSenha";
            txtSenha.TabIndex = 2;
            txtSenha.UseSystemPasswordChar = true;
            txtSenha.TextChanged += campo_TextChanged;

            lblConfirmarSenha.AutoSize = true;
            lblConfirmarSenha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblConfirmarSenha.Margin = new Padding(0, 0, 0, 6);
            lblConfirmarSenha.Name = "lblConfirmarSenha";
            lblConfirmarSenha.Text = "Confirmar senha";

            txtConfirmarSenha.AccessibleName = "Confirmar senha";
            txtConfirmarSenha.Dock = DockStyle.Top;
            txtConfirmarSenha.Font = new Font("Segoe UI", 11F);
            txtConfirmarSenha.Margin = new Padding(0, 0, 0, 12);
            txtConfirmarSenha.MaxLength = 72;
            txtConfirmarSenha.Name = "txtConfirmarSenha";
            txtConfirmarSenha.TabIndex = 3;
            txtConfirmarSenha.UseSystemPasswordChar = true;
            txtConfirmarSenha.TextChanged += campo_TextChanged;

            chkMostrarSenhas.AutoSize = true;
            chkMostrarSenhas.Margin = new Padding(0, 0, 0, 12);
            chkMostrarSenhas.Name = "chkMostrarSenhas";
            chkMostrarSenhas.TabIndex = 4;
            chkMostrarSenhas.Text = "Mostrar senhas";
            chkMostrarSenhas.CheckedChanged += chkMostrarSenhas_CheckedChanged;

            btnCadastrar.BackColor = Color.FromArgb(124, 58, 237);
            btnCadastrar.Cursor = Cursors.Hand;
            btnCadastrar.Dock = DockStyle.Top;
            btnCadastrar.FlatAppearance.BorderSize = 0;
            btnCadastrar.FlatStyle = FlatStyle.Flat;
            btnCadastrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCadastrar.ForeColor = Color.White;
            btnCadastrar.Margin = new Padding(0);
            btnCadastrar.MinimumSize = new Size(0, 42);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(400, 42);
            btnCadastrar.TabIndex = 5;
            btnCadastrar.Text = "Criar conta";
            btnCadastrar.UseVisualStyleBackColor = false;
            btnCadastrar.Click += btnCadastrar_Click;

            lblMensagem.AutoSize = true;
            lblMensagem.ForeColor = Color.FromArgb(100, 116, 139);
            lblMensagem.LiveSetting = AutomationLiveSetting.Polite;
            lblMensagem.Margin = new Padding(0, 10, 0, 0);
            lblMensagem.Name = "lblMensagem";
            lblMensagem.Visible = false;

            lnkVoltarLogin.AutoSize = true;
            lnkVoltarLogin.Dock = DockStyle.Top;
            lnkVoltarLogin.LinkColor = Color.FromArgb(124, 58, 237);
            lnkVoltarLogin.Margin = new Padding(0, 14, 0, 0);
            lnkVoltarLogin.Name = "lnkVoltarLogin";
            lnkVoltarLogin.TabIndex = 6;
            lnkVoltarLogin.TabStop = true;
            lnkVoltarLogin.Text = "Já tenho conta. Entrar";
            lnkVoltarLogin.TextAlign = ContentAlignment.MiddleCenter;
            lnkVoltarLogin.LinkClicked += lnkVoltarLogin_LinkClicked;

            AccessibleName = "Criar conta no LIMK Volleyball";
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(248, 249, 254);
            Controls.Add(pnlConteudo);
            Controls.Add(pnlMarca);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(30, 27, 45);
            Name = "TelaCadastroUsuario";
            Size = new Size(900, 590);
            pnlMarca.ResumeLayout(false);
            layoutMarca.ResumeLayout(false);
            layoutMarca.PerformLayout();
            layoutLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)imgLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)imgQuadra).EndInit();
            pnlConteudo.ResumeLayout(false);
            layoutFormulario.ResumeLayout(false);
            layoutFormulario.PerformLayout();
            ResumeLayout(true);
            pnlConteudo.Layout += pnlConteudo_Layout;
            SizeChanged += TelaCadastroUsuario_SizeChanged;
        }

        private Panel pnlMarca;
        private TableLayoutPanel layoutMarca;
        private TableLayoutPanel layoutLogo;
        private PictureBox imgLogo;
        private Label lblMarca;
        private PictureBox imgQuadra;
        private Label lblPrancheta;
        private Panel pnlConteudo;
        private TableLayoutPanel layoutFormulario;
        private Label lblTitulo;
        private Label lblInstrucao;
        private Label lblNome;
        private TextBox txtNome;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblSenha;
        private TextBox txtSenha;
        private Label lblConfirmarSenha;
        private TextBox txtConfirmarSenha;
        private CheckBox chkMostrarSenhas;
        private Button btnCadastrar;
        private Label lblMensagem;
        private LinkLabel lnkVoltarLogin;
    }
}
