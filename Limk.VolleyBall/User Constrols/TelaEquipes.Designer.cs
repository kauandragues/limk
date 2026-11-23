namespace Limk.VolleyBall.User_Constrols
{
    partial class TelaEquipes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                imagemEscudo.Image = null;
                _imagemEscudo?.Dispose();
                seletorEscudo.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            txtBuscaEquipe = new TextBox();
            btnNovaEquipe = new Button();
            panel4 = new Panel();
            painelListaEquipes = new Panel();
            layoutCartoesEquipes = new TableLayoutPanel();
            lblResumoEquipes = new Label();
            panel1 = new Panel();
            layoutEditorEquipe = new TableLayoutPanel();
            txtTituloFicha = new TextBox();
            label4 = new Label();
            txtNomeEquipe = new TextBox();
            label5 = new Label();
            imagemEscudo = new PictureBox();
            labelEscudoEquipe = new Label();
            layoutEscudoAcoes = new FlowLayoutPanel();
            btnEscolherEscudo = new Button();
            btnRemoverEscudo = new Button();
            layoutAcoesEquipe = new FlowLayoutPanel();
            btnLimparFicha = new Button();
            btnSalvarEquipe = new Button();
            labelStatusEquipe = new Label();
            seletorEscudo = new OpenFileDialog();
            panel4.SuspendLayout();
            painelListaEquipes.SuspendLayout();
            panel1.SuspendLayout();
            layoutEditorEquipe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imagemEscudo).BeginInit();
            layoutEscudoAcoes.SuspendLayout();
            layoutAcoesEquipe.SuspendLayout();
            SuspendLayout();
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 16.2F, FontStyle.Bold);
            label1.Location = new Point(17, 16);
            label1.Name = "label1";
            label1.Size = new Size(287, 26);
            label1.TabIndex = 4;
            label1.Text = "Gerenciamento de Equipes";
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 7.8F);
            label2.Location = new Point(24, 48);
            label2.Name = "label2";
            label2.Size = new Size(266, 14);
            label2.TabIndex = 3;
            label2.Text = "Cadastre suas equipes e atualize o nome e o escudo.";
            //
            // txtBuscaEquipe
            //
            txtBuscaEquipe.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscaEquipe.Location = new Point(360, 20);
            txtBuscaEquipe.Name = "txtBuscaEquipe";
            txtBuscaEquipe.PlaceholderText = "Buscar equipe...";
            txtBuscaEquipe.Size = new Size(185, 23);
            txtBuscaEquipe.TabIndex = 0;
            txtBuscaEquipe.TextChanged += txtBuscaEquipe_TextChanged;
            //
            // btnNovaEquipe
            //
            btnNovaEquipe.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNovaEquipe.BackColor = Color.FromArgb(124, 58, 237);
            btnNovaEquipe.FlatAppearance.BorderSize = 0;
            btnNovaEquipe.FlatStyle = FlatStyle.Flat;
            btnNovaEquipe.ForeColor = Color.White;
            btnNovaEquipe.Location = new Point(557, 16);
            btnNovaEquipe.Name = "btnNovaEquipe";
            btnNovaEquipe.Size = new Size(160, 32);
            btnNovaEquipe.TabIndex = 1;
            btnNovaEquipe.Text = "+ Cadastrar nova equipe";
            btnNovaEquipe.UseVisualStyleBackColor = false;
            btnNovaEquipe.Click += btnNovaEquipe_Click;
            //
            // panel4
            //
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel4.Controls.Add(painelListaEquipes);
            panel4.Controls.Add(panel1);
            panel4.Location = new Point(20, 84);
            panel4.Name = "panel4";
            panel4.Size = new Size(696, 506);
            panel4.TabIndex = 2;
            //
            // painelListaEquipes
            //
            painelListaEquipes.AutoScroll = true;
            painelListaEquipes.Controls.Add(layoutCartoesEquipes);
            painelListaEquipes.Controls.Add(lblResumoEquipes);
            painelListaEquipes.Dock = DockStyle.Fill;
            painelListaEquipes.Location = new Point(0, 0);
            painelListaEquipes.Name = "painelListaEquipes";
            painelListaEquipes.Padding = new Padding(0, 0, 12, 0);
            painelListaEquipes.Size = new Size(379, 506);
            painelListaEquipes.TabIndex = 0;
            //
            // layoutCartoesEquipes
            //
            layoutCartoesEquipes.AutoSize = true;
            layoutCartoesEquipes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutCartoesEquipes.ColumnCount = 1;
            layoutCartoesEquipes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCartoesEquipes.Dock = DockStyle.Top;
            layoutCartoesEquipes.Location = new Point(0, 23);
            layoutCartoesEquipes.Margin = new Padding(0);
            layoutCartoesEquipes.Name = "layoutCartoesEquipes";
            layoutCartoesEquipes.Size = new Size(367, 0);
            layoutCartoesEquipes.TabIndex = 0;
            //
            // lblResumoEquipes
            //
            lblResumoEquipes.AutoSize = true;
            lblResumoEquipes.Dock = DockStyle.Top;
            lblResumoEquipes.Location = new Point(0, 0);
            lblResumoEquipes.Name = "lblResumoEquipes";
            lblResumoEquipes.Padding = new Padding(0, 0, 0, 8);
            lblResumoEquipes.Size = new Size(75, 23);
            lblResumoEquipes.TabIndex = 1;
            lblResumoEquipes.Text = "Suas equipes";
            //
            // panel1
            //
            panel1.AutoScroll = true;
            panel1.BackColor = Color.White;
            panel1.Controls.Add(layoutEditorEquipe);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(379, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(317, 506);
            panel1.TabIndex = 1;
            //
            // layoutEditorEquipe
            //
            layoutEditorEquipe.AutoSize = true;
            layoutEditorEquipe.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutEditorEquipe.ColumnCount = 1;
            layoutEditorEquipe.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEditorEquipe.Controls.Add(txtTituloFicha, 0, 0);
            layoutEditorEquipe.Controls.Add(label4, 0, 1);
            layoutEditorEquipe.Controls.Add(txtNomeEquipe, 0, 2);
            layoutEditorEquipe.Controls.Add(label5, 0, 3);
            layoutEditorEquipe.Controls.Add(imagemEscudo, 0, 4);
            layoutEditorEquipe.Controls.Add(labelEscudoEquipe, 0, 5);
            layoutEditorEquipe.Controls.Add(layoutEscudoAcoes, 0, 6);
            layoutEditorEquipe.Controls.Add(layoutAcoesEquipe, 0, 7);
            layoutEditorEquipe.Controls.Add(labelStatusEquipe, 0, 8);
            layoutEditorEquipe.Dock = DockStyle.Top;
            layoutEditorEquipe.Location = new Point(0, 0);
            layoutEditorEquipe.Name = "layoutEditorEquipe";
            layoutEditorEquipe.Padding = new Padding(16);
            layoutEditorEquipe.RowCount = 9;
            layoutEditorEquipe.RowStyles.Add(new RowStyle());
            layoutEditorEquipe.RowStyles.Add(new RowStyle());
            layoutEditorEquipe.RowStyles.Add(new RowStyle());
            layoutEditorEquipe.RowStyles.Add(new RowStyle());
            layoutEditorEquipe.RowStyles.Add(new RowStyle());
            layoutEditorEquipe.RowStyles.Add(new RowStyle());
            layoutEditorEquipe.RowStyles.Add(new RowStyle());
            layoutEditorEquipe.RowStyles.Add(new RowStyle());
            layoutEditorEquipe.RowStyles.Add(new RowStyle());
            layoutEditorEquipe.Size = new Size(317, 371);
            layoutEditorEquipe.TabIndex = 0;
            //
            // txtTituloFicha
            //
            txtTituloFicha.BackColor = Color.White;
            txtTituloFicha.BorderStyle = BorderStyle.None;
            txtTituloFicha.Dock = DockStyle.Top;
            txtTituloFicha.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtTituloFicha.Location = new Point(16, 16);
            txtTituloFicha.Margin = new Padding(0, 0, 0, 16);
            txtTituloFicha.Name = "txtTituloFicha";
            txtTituloFicha.ReadOnly = true;
            txtTituloFicha.Size = new Size(285, 22);
            txtTituloFicha.TabIndex = 0;
            txtTituloFicha.TabStop = false;
            txtTituloFicha.Text = "Cadastrar nova equipe";
            //
            // label4
            //
            label4.AutoSize = true;
            label4.Dock = DockStyle.Top;
            label4.Location = new Point(16, 54);
            label4.Margin = new Padding(0, 0, 0, 4);
            label4.Name = "label4";
            label4.Size = new Size(285, 15);
            label4.TabIndex = 1;
            label4.Text = "NOME DA EQUIPE";
            //
            // txtNomeEquipe
            //
            txtNomeEquipe.Dock = DockStyle.Top;
            txtNomeEquipe.Location = new Point(16, 73);
            txtNomeEquipe.Margin = new Padding(0, 0, 0, 16);
            txtNomeEquipe.MaxLength = 100;
            txtNomeEquipe.Name = "txtNomeEquipe";
            txtNomeEquipe.PlaceholderText = "Ex: Taubaté Vôlei Clube";
            txtNomeEquipe.Size = new Size(285, 23);
            txtNomeEquipe.TabIndex = 0;
            //
            // label5
            //
            label5.AutoSize = true;
            label5.Dock = DockStyle.Top;
            label5.Location = new Point(16, 112);
            label5.Margin = new Padding(0, 0, 0, 4);
            label5.Name = "label5";
            label5.Size = new Size(285, 15);
            label5.TabIndex = 2;
            label5.Text = "ESCUDO DA EQUIPE";
            //
            // imagemEscudo
            //
            imagemEscudo.Anchor = AnchorStyles.Left;
            imagemEscudo.BackColor = Color.White;
            imagemEscudo.BorderStyle = BorderStyle.FixedSingle;
            imagemEscudo.Location = new Point(16, 131);
            imagemEscudo.Margin = new Padding(0, 0, 0, 4);
            imagemEscudo.Name = "imagemEscudo";
            imagemEscudo.Size = new Size(128, 96);
            imagemEscudo.SizeMode = PictureBoxSizeMode.Zoom;
            imagemEscudo.TabIndex = 3;
            imagemEscudo.TabStop = false;
            //
            // labelEscudoEquipe
            //
            labelEscudoEquipe.AutoSize = true;
            labelEscudoEquipe.Dock = DockStyle.Top;
            labelEscudoEquipe.Font = new Font("Segoe UI", 8F);
            labelEscudoEquipe.ForeColor = Color.FromArgb(100, 116, 139);
            labelEscudoEquipe.Location = new Point(16, 231);
            labelEscudoEquipe.Margin = new Padding(0, 0, 0, 8);
            labelEscudoEquipe.Name = "labelEscudoEquipe";
            labelEscudoEquipe.Size = new Size(285, 13);
            labelEscudoEquipe.TabIndex = 4;
            labelEscudoEquipe.Text = "Sem escudo";
            //
            // layoutEscudoAcoes
            //
            layoutEscudoAcoes.AutoSize = true;
            layoutEscudoAcoes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutEscudoAcoes.Controls.Add(btnEscolherEscudo);
            layoutEscudoAcoes.Controls.Add(btnRemoverEscudo);
            layoutEscudoAcoes.Dock = DockStyle.Top;
            layoutEscudoAcoes.Location = new Point(16, 252);
            layoutEscudoAcoes.Margin = new Padding(0, 0, 0, 16);
            layoutEscudoAcoes.Name = "layoutEscudoAcoes";
            layoutEscudoAcoes.Size = new Size(285, 32);
            layoutEscudoAcoes.TabIndex = 5;
            //
            // btnEscolherEscudo
            //
            btnEscolherEscudo.Location = new Point(0, 0);
            btnEscolherEscudo.Margin = new Padding(0, 0, 8, 0);
            btnEscolherEscudo.Name = "btnEscolherEscudo";
            btnEscolherEscudo.Size = new Size(145, 32);
            btnEscolherEscudo.TabIndex = 0;
            btnEscolherEscudo.Text = "Escolher escudo";
            btnEscolherEscudo.Click += btnEscolherEscudo_Click;
            //
            // btnRemoverEscudo
            //
            btnRemoverEscudo.Location = new Point(153, 0);
            btnRemoverEscudo.Margin = new Padding(0);
            btnRemoverEscudo.Name = "btnRemoverEscudo";
            btnRemoverEscudo.Size = new Size(100, 32);
            btnRemoverEscudo.TabIndex = 1;
            btnRemoverEscudo.Text = "Remover";
            btnRemoverEscudo.Click += btnRemoverEscudo_Click;
            //
            // layoutAcoesEquipe
            //
            layoutAcoesEquipe.AutoSize = true;
            layoutAcoesEquipe.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutAcoesEquipe.Controls.Add(btnLimparFicha);
            layoutAcoesEquipe.Controls.Add(btnSalvarEquipe);
            layoutAcoesEquipe.Dock = DockStyle.Top;
            layoutAcoesEquipe.Location = new Point(16, 300);
            layoutAcoesEquipe.Margin = new Padding(0, 0, 0, 8);
            layoutAcoesEquipe.Name = "layoutAcoesEquipe";
            layoutAcoesEquipe.Size = new Size(285, 32);
            layoutAcoesEquipe.TabIndex = 6;
            //
            // btnLimparFicha
            //
            btnLimparFicha.Location = new Point(0, 0);
            btnLimparFicha.Margin = new Padding(0, 0, 8, 0);
            btnLimparFicha.Name = "btnLimparFicha";
            btnLimparFicha.Size = new Size(100, 32);
            btnLimparFicha.TabIndex = 0;
            btnLimparFicha.Text = "Limpar";
            btnLimparFicha.Click += btnNovaEquipe_Click;
            //
            // btnSalvarEquipe
            //
            btnSalvarEquipe.BackColor = Color.FromArgb(124, 58, 237);
            btnSalvarEquipe.FlatAppearance.BorderSize = 0;
            btnSalvarEquipe.FlatStyle = FlatStyle.Flat;
            btnSalvarEquipe.ForeColor = Color.White;
            btnSalvarEquipe.Location = new Point(108, 0);
            btnSalvarEquipe.Margin = new Padding(0);
            btnSalvarEquipe.Name = "btnSalvarEquipe";
            btnSalvarEquipe.Size = new Size(145, 32);
            btnSalvarEquipe.TabIndex = 1;
            btnSalvarEquipe.Text = "Salvar equipe";
            btnSalvarEquipe.UseVisualStyleBackColor = false;
            btnSalvarEquipe.Click += btnSalvarEquipe_Click;
            //
            // labelStatusEquipe
            //
            labelStatusEquipe.AutoSize = true;
            labelStatusEquipe.Dock = DockStyle.Top;
            labelStatusEquipe.ForeColor = Color.FromArgb(100, 116, 139);
            labelStatusEquipe.Location = new Point(16, 340);
            labelStatusEquipe.Margin = new Padding(0);
            labelStatusEquipe.MaximumSize = new Size(285, 0);
            labelStatusEquipe.Name = "labelStatusEquipe";
            labelStatusEquipe.Size = new Size(285, 15);
            labelStatusEquipe.TabIndex = 7;
            //
            // seletorEscudo
            //
            seletorEscudo.Filter = "Imagens|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
            seletorEscudo.RestoreDirectory = true;
            seletorEscudo.Title = "Escolher escudo da equipe";
            //
            // TelaEquipes
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.ControlLight;
            Controls.Add(panel4);
            Controls.Add(btnNovaEquipe);
            Controls.Add(txtBuscaEquipe);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(700, 420);
            Name = "TelaEquipes";
            Size = new Size(736, 610);
            Load += TelaEquipes_Load;
            panel4.ResumeLayout(false);
            painelListaEquipes.ResumeLayout(false);
            painelListaEquipes.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            layoutEditorEquipe.ResumeLayout(false);
            layoutEditorEquipe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)imagemEscudo).EndInit();
            layoutEscudoAcoes.ResumeLayout(false);
            layoutAcoesEquipe.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Label label2;
        private TextBox txtBuscaEquipe;
        private Button btnNovaEquipe;
        private Panel panel4;
        private Panel painelListaEquipes;
        private Label lblResumoEquipes;
        private TableLayoutPanel layoutCartoesEquipes;
        private Panel panel1;
        private TableLayoutPanel layoutEditorEquipe;
        private TextBox txtTituloFicha;
        private Label label4;
        private TextBox txtNomeEquipe;
        private Label label5;
        private PictureBox imagemEscudo;
        private Label labelEscudoEquipe;
        private FlowLayoutPanel layoutEscudoAcoes;
        private Button btnEscolherEscudo;
        private Button btnRemoverEscudo;
        private FlowLayoutPanel layoutAcoesEquipe;
        private Button btnLimparFicha;
        private Button btnSalvarEquipe;
        private Label labelStatusEquipe;
        private OpenFileDialog seletorEscudo;
    }
}
