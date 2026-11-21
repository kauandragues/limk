using System.Windows.Forms.Automation;

namespace Limk.VolleyBall.User_Constrols
{
    partial class TelaLogin
    {

        private Panel pnlMarca;
        private TableLayoutPanel layoutMarca;
        private TableLayoutPanel layoutLogo;
        private PictureBox imgLogo;
        private Label lblMarca;
        private PictureBox imgQuadra;
        private Label lblPrancheta;
        private Panel pnlConteudo;
        private TableLayoutPanel layoutFormulario;
        private TableLayoutPanel layoutCampoEmail;
        private TableLayoutPanel layoutCampoSenha;
        private Label lblTitulo;
        private Label lblEmail;
        private Label lblSenha;
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
            layoutCampoEmail = new TableLayoutPanel();
            layoutCampoSenha = new TableLayoutPanel();
            lblTitulo = new Label();
            lblInstrucao = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblSenha = new Label();
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
            layoutCampoEmail.SuspendLayout();
            layoutCampoSenha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imgLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imgQuadra).BeginInit();

            pnlMarca.Name = "pnlMarca";
            pnlMarca.BackColor = Color.FromArgb(30, 21, 42);
            pnlMarca.Dock = DockStyle.Left;
            pnlMarca.Width = 325;
            pnlMarca.TabStop = false;
            pnlMarca.Controls.Add(layoutMarca);

            layoutMarca.Name = "layoutMarca";
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

            imgLogo.Name = "imgLogo";
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

            imgQuadra.Name = "imgQuadra";
            imgQuadra.Dock = DockStyle.Fill;
            imgQuadra.Image = Properties.Resources.Quadra;
            imgQuadra.Margin = new Padding(0);
            imgQuadra.SizeMode = PictureBoxSizeMode.Zoom;
            imgQuadra.TabStop = false;

            lblPrancheta.Name = "lblPrancheta";
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
            layoutFormulario.Location = new Point(61, 110);
            layoutFormulario.Size = new Size(452, 360);
            layoutFormulario.Paint += layoutFormulario_Paint;
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutFormulario.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // layoutCampoEmail
            layoutCampoEmail.Name = "layoutCampoEmail";
            layoutCampoEmail.BackColor = Color.FromArgb(248, 249, 254);
            layoutCampoEmail.ColumnCount = 1;
            layoutCampoEmail.RowCount = 3;
            layoutCampoEmail.Dock = DockStyle.Top;
            layoutCampoEmail.Margin = new Padding(0, 0, 0, 14);
            layoutCampoEmail.Padding = new Padding(12, 1, 1, 1);
            layoutCampoEmail.MinimumSize = new Size(0, 42);
            layoutCampoEmail.Height = 42;
            layoutCampoEmail.TabIndex = 0;
            layoutCampoEmail.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCampoEmail.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutCampoEmail.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutCampoEmail.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutCampoEmail.Controls.Add(txtEmail, 0, 1);
            layoutCampoEmail.Paint += campo_Paint;
            layoutCampoEmail.Enter += campo_FocoAlterado;
            layoutCampoEmail.Leave += campo_FocoAlterado;

            // layoutCampoSenha
            layoutCampoSenha.Name = "layoutCampoSenha";
            layoutCampoSenha.BackColor = Color.FromArgb(248, 249, 254);
            layoutCampoSenha.ColumnCount = 2;
            layoutCampoSenha.RowCount = 3;
            layoutCampoSenha.Dock = DockStyle.Top;
            layoutCampoSenha.Margin = new Padding(0, 0, 0, 18);
            layoutCampoSenha.Padding = new Padding(12, 1, 1, 1);
            layoutCampoSenha.MinimumSize = new Size(0, 42);
            layoutCampoSenha.Height = 42;
            layoutCampoSenha.TabIndex = 1;
            layoutCampoSenha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCampoSenha.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 82F));
            layoutCampoSenha.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutCampoSenha.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutCampoSenha.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutCampoSenha.Controls.Add(txtSenha, 0, 1);
            layoutCampoSenha.Controls.Add(chkMostrarSenha, 1, 0);
            layoutCampoSenha.SetRowSpan(chkMostrarSenha, 3);
            layoutCampoSenha.Paint += campo_Paint;
            layoutCampoSenha.Enter += campo_FocoAlterado;
            layoutCampoSenha.Leave += campo_FocoAlterado;

            lblTitulo.Name = "lblTitulo";
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTitulo.Margin = new Padding(0, 0, 0, 4);
            lblTitulo.Text = "Entre na sua conta";

            lblInstrucao.Name = "lblInstrucao";
            lblInstrucao.AutoSize = true;
            lblInstrucao.Font = new Font("Segoe UI", 10F);
            lblInstrucao.ForeColor = Color.FromArgb(100, 116, 139);
            lblInstrucao.Margin = new Padding(0, 0, 0, 19);
            lblInstrucao.Text = "Use o e-mail cadastrado no LIMK.";

            lblEmail.Name = "lblEmail";
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEmail.Margin = new Padding(0, 0, 0, 6);
            lblEmail.Text = "E-mail";

            txtEmail.Name = "txtEmail";
            txtEmail.BackColor = Color.FromArgb(248, 249, 254);
            txtEmail.BorderStyle = BorderStyle.None;
            txtEmail.Dock = DockStyle.Fill;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.Margin = new Padding(0);
            txtEmail.AccessibleName = "E-mail";
            txtEmail.MaxLength = 254;
            txtEmail.PlaceholderText = "nome@exemplo.com";
            txtEmail.TabIndex = 0;
            txtEmail.TextChanged += campo_TextChanged;

            lblSenha.Name = "lblSenha";
            lblSenha.AutoSize = true;
            lblSenha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSenha.Margin = new Padding(0, 0, 0, 6);
            lblSenha.Text = "Senha";

            txtSenha.Name = "txtSenha";
            txtSenha.BackColor = Color.FromArgb(248, 249, 254);
            txtSenha.BorderStyle = BorderStyle.None;
            txtSenha.Dock = DockStyle.Fill;
            txtSenha.Font = new Font("Segoe UI", 11F);
            txtSenha.Margin = new Padding(0);
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
            chkMostrarSenha.ForeColor = Color.FromArgb(92, 48, 154);
            chkMostrarSenha.Margin = new Padding(8, 3, 3, 3);
            chkMostrarSenha.TabIndex = 1;
            chkMostrarSenha.Text = "Mostrar";
            chkMostrarSenha.TextAlign = ContentAlignment.MiddleCenter;
            chkMostrarSenha.UseVisualStyleBackColor = false;
            chkMostrarSenha.CheckedChanged += chkMostrarSenha_CheckedChanged;

            btnEntrar.Name = "btnEntrar";
            btnEntrar.BackColor = Color.FromArgb(124, 58, 237);
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
            lblMensagem.ForeColor = Color.FromArgb(100, 116, 139);
            lblMensagem.LiveSetting = AutomationLiveSetting.Polite;
            lblMensagem.Margin = new Padding(0, 10, 0, 0);
            lblMensagem.Visible = false;

            layoutFormulario.Controls.Add(lblTitulo, 0, 0);
            layoutFormulario.Controls.Add(lblInstrucao, 0, 1);
            layoutFormulario.Controls.Add(lblEmail, 0, 2);
            layoutFormulario.Controls.Add(layoutCampoEmail, 0, 3);
            layoutFormulario.Controls.Add(lblSenha, 0, 4);
            layoutFormulario.Controls.Add(layoutCampoSenha, 0, 5);
            layoutFormulario.Controls.Add(btnEntrar, 0, 6);
            layoutFormulario.Controls.Add(lblMensagem, 0, 7);

            AccessibleName = "Acesso ao LIMK Volleyball";
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(248, 249, 254);
            Controls.Add(pnlConteudo);
            Controls.Add(pnlMarca);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(30, 27, 45);
            Name = "TelaLogin";
            Size = new Size(900, 590);
            layoutCampoEmail.ResumeLayout(false);
            layoutCampoEmail.PerformLayout();
            layoutCampoSenha.ResumeLayout(false);
            layoutCampoSenha.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)imgLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)imgQuadra).EndInit();
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
