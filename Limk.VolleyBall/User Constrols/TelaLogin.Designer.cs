using System.Windows.Forms.Automation;

namespace Limk.VolleyBall.User_Constrols
{
    partial class TelaLogin
    {
        private static readonly Color CorMarca = Color.FromArgb(30, 21, 42);
        private static readonly Color CorFundo = Color.FromArgb(248, 249, 254);
        private static readonly Color CorBorda = Color.FromArgb(222, 215, 236);
        private static readonly Color CorTexto = Color.FromArgb(30, 27, 45);
        private static readonly Color CorTextoSecundario = Color.FromArgb(100, 116, 139);
        private static readonly Color CorRoxa = Color.FromArgb(92, 48, 154);
        private static readonly Color CorAcao = Color.FromArgb(124, 58, 237);

        private Panel pnlMarca;
        private TableLayoutPanel layoutMarca;
        private TableLayoutPanel layoutLogo;
        private PictureBox imgLogo;
        private Label lblMarca;
        private PictureBox imgQuadra;
        private Label lblPrancheta;
        private Panel pnlConteudo;
        private TableLayoutPanel layoutFormulario;
        private Label lblInstrucao;
        private TextBox txtEmail;
        private TextBox txtSenha;
        private CheckBox chkMostrarSenha;
        private Button btnEntrar;
        private Label lblMensagem;

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
            Label lblTitulo = new Label();
            lblInstrucao = new Label();
            Label lblEmail = new Label();
            txtEmail = new TextBox();
            Label lblSenha = new Label();
            txtSenha = new TextBox();
            chkMostrarSenha = new CheckBox();
            btnEntrar = new Button();
            lblMensagem = new Label();
            SuspendLayout();
            pnlMarca.SuspendLayout();
            layoutMarca.SuspendLayout();
            layoutLogo.SuspendLayout();
            pnlConteudo.SuspendLayout();
            layoutFormulario.SuspendLayout();

            pnlMarca.Name = "pnlMarca";
            pnlMarca.BackColor = CorMarca;
            pnlMarca.Dock = DockStyle.Left;
            pnlMarca.Width = 325;
            pnlMarca.TabStop = false;
            pnlMarca.Controls.Add(layoutMarca);

            layoutMarca.Dock = DockStyle.Fill;
            layoutMarca.Margin = new Padding(0);
            layoutMarca.Padding = new Padding(28);
            layoutMarca.ColumnCount = 1;
            layoutMarca.RowCount = 5;
            layoutMarca.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMarca.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            layoutMarca.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutMarca.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            layoutMarca.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutMarca.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutMarca.Controls.Add(layoutLogo, 0, 0);
            layoutMarca.Controls.Add(imgQuadra, 0, 2);
            layoutMarca.Controls.Add(lblPrancheta, 0, 3);

            layoutLogo.Name = "layoutLogo";
            layoutLogo.Anchor = AnchorStyles.Left;
            layoutLogo.Margin = new Padding(0);
            layoutLogo.Size = new Size(227, 72);
            layoutLogo.ColumnCount = 2;
            layoutLogo.RowCount = 1;
            layoutLogo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 84F));
            layoutLogo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutLogo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutLogo.Controls.Add(imgLogo, 0, 0);
            layoutLogo.Controls.Add(lblMarca, 1, 0);

            imgLogo.Anchor = AnchorStyles.Left;
            imgLogo.AccessibleName = "LIMK Volleyball";
            imgLogo.Image = Properties.Resources.LogoMenuLateral;
            imgLogo.Margin = new Padding(0);
            imgLogo.Size = new Size(64, 64);
            imgLogo.SizeMode = PictureBoxSizeMode.Zoom;
            imgLogo.TabStop = false;

            lblMarca.Name = "lblMarca";
            lblMarca.Dock = DockStyle.Fill;
            lblMarca.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblMarca.ForeColor = Color.White;
            lblMarca.Margin = new Padding(0);
            lblMarca.Text = "LIMK";
            lblMarca.TextAlign = ContentAlignment.MiddleLeft;

            imgQuadra.Dock = DockStyle.Fill;
            imgQuadra.Image = Properties.Resources.Quadra;
            imgQuadra.Margin = new Padding(0);
            imgQuadra.SizeMode = PictureBoxSizeMode.Zoom;
            imgQuadra.TabStop = false;

            lblPrancheta.AutoSize = true;
            lblPrancheta.Dock = DockStyle.Top;
            lblPrancheta.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPrancheta.ForeColor = Color.White;
            lblPrancheta.Margin = new Padding(0, 12, 0, 0);
            lblPrancheta.Text = "Prancheta tática de voleibol";
            lblPrancheta.TextAlign = ContentAlignment.TopCenter;

            pnlConteudo.Name = "pnlConteudo";
            pnlConteudo.AutoScroll = true;
            pnlConteudo.Dock = DockStyle.Fill;
            pnlConteudo.TabIndex = 0;
            pnlConteudo.Controls.Add(layoutFormulario);

            layoutFormulario.Name = "layoutFormulario";
            layoutFormulario.BackColor = Color.White;
            layoutFormulario.ColumnCount = 1;
            layoutFormulario.RowCount = 8;
            layoutFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutFormulario.Padding = new Padding(26, 24, 26, 24);
            layoutFormulario.Size = new Size(452, 360);
            layoutFormulario.Paint += (_, e) => ControlPaint.DrawBorder(e.Graphics, layoutFormulario.ClientRectangle, CorBorda, ButtonBorderStyle.Solid);
            for (int linha = 0; linha < layoutFormulario.RowCount; linha++)
                layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTitulo.Margin = new Padding(0, 0, 0, 4);
            lblTitulo.Text = "Entre na sua conta";

            lblInstrucao.AutoSize = true;
            lblInstrucao.Font = new Font("Segoe UI", 10F);
            lblInstrucao.ForeColor = CorTextoSecundario;
            lblInstrucao.Margin = new Padding(0, 0, 0, 19);
            lblInstrucao.Text = "Use o e-mail cadastrado no LIMK.";

            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmail.Margin = new Padding(0, 0, 0, 6);
            lblEmail.Text = "E-mail";

            txtEmail.Name = "txtEmail";
            txtEmail.AccessibleName = "E-mail";
            txtEmail.MaxLength = 254;
            txtEmail.PlaceholderText = "nome@exemplo.com";
            txtEmail.TabIndex = 0;
            txtEmail.TextChanged += campo_TextChanged;

            lblSenha.AutoSize = true;
            lblSenha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSenha.Margin = new Padding(0, 0, 0, 6);
            lblSenha.Text = "Senha";

            txtSenha.Name = "txtSenha";
            txtSenha.AccessibleName = "Senha";
            txtSenha.MaxLength = 254;
            txtSenha.TabIndex = 0;
            txtSenha.UseSystemPasswordChar = true;
            txtSenha.TextChanged += campo_TextChanged;

            chkMostrarSenha.Name = "chkMostrarSenha";
            chkMostrarSenha.AccessibleName = "Mostrar senha";
            chkMostrarSenha.Appearance = Appearance.Button;
            chkMostrarSenha.BackColor = Color.FromArgb(245, 240, 255);
            chkMostrarSenha.Cursor = Cursors.Hand;
            chkMostrarSenha.Dock = DockStyle.Fill;
            chkMostrarSenha.FlatAppearance.BorderSize = 0;
            chkMostrarSenha.FlatAppearance.CheckedBackColor = Color.FromArgb(233, 221, 251);
            chkMostrarSenha.FlatAppearance.MouseOverBackColor = Color.FromArgb(233, 221, 251);
            chkMostrarSenha.FlatStyle = FlatStyle.Flat;
            chkMostrarSenha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chkMostrarSenha.ForeColor = CorRoxa;
            chkMostrarSenha.Margin = new Padding(8, 3, 3, 3);
            chkMostrarSenha.TabIndex = 1;
            chkMostrarSenha.Text = "Mostrar";
            chkMostrarSenha.TextAlign = ContentAlignment.MiddleCenter;
            chkMostrarSenha.UseVisualStyleBackColor = false;
            chkMostrarSenha.CheckedChanged += chkMostrarSenha_CheckedChanged;

            btnEntrar.Name = "btnEntrar";
            btnEntrar.BackColor = CorAcao;
            btnEntrar.Cursor = Cursors.Hand;
            btnEntrar.Dock = DockStyle.Top;
            btnEntrar.FlatAppearance.BorderSize = 0;
            btnEntrar.FlatAppearance.MouseDownBackColor = Color.FromArgb(76, 29, 149);
            btnEntrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(109, 40, 217);
            btnEntrar.FlatStyle = FlatStyle.Flat;
            btnEntrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEntrar.ForeColor = Color.White;
            btnEntrar.Margin = new Padding(0);
            btnEntrar.MinimumSize = new Size(0, 42);
            btnEntrar.TabIndex = 2;
            btnEntrar.Text = "Entrar";
            btnEntrar.UseVisualStyleBackColor = false;
            btnEntrar.Click += btnEntrar_Click;

            lblMensagem.Name = "lblMensagem";
            lblMensagem.AutoSize = true;
            lblMensagem.ForeColor = CorTextoSecundario;
            lblMensagem.LiveSetting = AutomationLiveSetting.Polite;
            lblMensagem.Margin = new Padding(0, 10, 0, 0);
            lblMensagem.Visible = false;

            layoutFormulario.Controls.Add(lblTitulo, 0, 0);
            layoutFormulario.Controls.Add(lblInstrucao, 0, 1);
            layoutFormulario.Controls.Add(lblEmail, 0, 2);
            layoutFormulario.Controls.Add(CriarCampo(txtEmail, null, 0, 14), 0, 3);
            layoutFormulario.Controls.Add(lblSenha, 0, 4);
            layoutFormulario.Controls.Add(CriarCampo(txtSenha, chkMostrarSenha, 1, 18), 0, 5);
            layoutFormulario.Controls.Add(btnEntrar, 0, 6);
            layoutFormulario.Controls.Add(lblMensagem, 0, 7);

            AccessibleName = "Acesso ao LIMK Volleyball";
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = CorFundo;
            Controls.Add(pnlConteudo);
            Controls.Add(pnlMarca);
            Font = new Font("Segoe UI", 9F);
            ForeColor = CorTexto;
            Name = "TelaLogin";
            Size = new Size(900, 590);
            layoutFormulario.ResumeLayout(false);
            pnlConteudo.ResumeLayout(false);
            layoutLogo.ResumeLayout(false);
            layoutMarca.ResumeLayout(false);
            pnlMarca.ResumeLayout(false);
            ResumeLayout(true);

            pnlConteudo.Layout += pnlConteudo_Layout;
            SizeChanged += TelaLogin_SizeChanged;
        }
    }
}
