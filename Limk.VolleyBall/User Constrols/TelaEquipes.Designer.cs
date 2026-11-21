namespace Limk.VolleyBall.User_Constrols
{
    partial class TelaEquipes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                _encerramento.Cancel();
                _encerramento.Dispose();
                pictureBox4.Image = null;
                _imagemEscudo?.Dispose();
                openFileDialog1.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            panel4 = new Panel();
            painelListaEquipes = new Panel();
            label3 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            layoutEditorEquipe = new TableLayoutPanel();
            textBox2 = new TextBox();
            label4 = new Label();
            textBox3 = new TextBox();
            label5 = new Label();
            pictureBox4 = new PictureBox();
            labelEscudoEquipe = new Label();
            layoutEscudoAcoes = new FlowLayoutPanel();
            button2 = new Button();
            btnRemoverEscudo = new Button();
            layoutAcoesEquipe = new FlowLayoutPanel();
            button4 = new Button();
            button5 = new Button();
            labelStatusEquipe = new Label();
            openFileDialog1 = new OpenFileDialog();
            panel4.SuspendLayout();
            painelListaEquipes.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            layoutEditorEquipe.SuspendLayout();
            layoutEscudoAcoes.SuspendLayout();
            layoutAcoesEquipe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            //
            // label1
            //
            label1.Name = "label1";
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 16.2F, FontStyle.Bold);
            label1.Location = new Point(17, 16);
            label1.Text = "Gerenciamento de Equipes";
            //
            // label2
            //
            label2.Name = "label2";
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 7.8F);
            label2.Location = new Point(24, 48);
            label2.Text = "Cadastre suas equipes e atualize o nome e o escudo.";
            //
            // textBox1
            //
            textBox1.Name = "textBox1";
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(360, 20);
            textBox1.Size = new Size(185, 23);
            textBox1.PlaceholderText = "Buscar equipe...";
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            //
            // button1
            //
            button1.Name = "button1";
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(557, 16);
            button1.Size = new Size(160, 32);
            button1.BackColor = Color.FromArgb(124, 58, 237);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Text = "+ Cadastrar nova equipe";
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            //
            // panel4
            //
            panel4.Name = "panel4";
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel4.Location = new Point(20, 84);
            panel4.Size = new Size(696, 506);
            panel4.Controls.Add(painelListaEquipes);
            panel4.Controls.Add(panel1);
            panel4.TabIndex = 2;
            //
            // painelListaEquipes
            //
            painelListaEquipes.Name = "painelListaEquipes";
            painelListaEquipes.AutoScroll = true;
            painelListaEquipes.Dock = DockStyle.Fill;
            painelListaEquipes.Padding = new Padding(0, 0, 12, 0);
            painelListaEquipes.Controls.Add(tableLayoutPanel1);
            painelListaEquipes.Controls.Add(label3);
            //
            // label3
            //
            label3.Name = "label3";
            label3.AutoSize = true;
            label3.Dock = DockStyle.Top;
            label3.Padding = new Padding(0, 0, 0, 8);
            label3.Text = "Suas equipes";
            //
            // tableLayoutPanel1
            //
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Margin = Padding.Empty;
            tableLayoutPanel1.RowCount = 0;
            //
            // panel1
            //
            panel1.Name = "panel1";
            panel1.AutoScroll = true;
            panel1.BackColor = Color.White;
            panel1.Dock = DockStyle.Right;
            panel1.Size = new Size(317, 506);
            panel1.Controls.Add(layoutEditorEquipe);
            //
            // layoutEditorEquipe
            //
            layoutEditorEquipe.Name = "layoutEditorEquipe";
            layoutEditorEquipe.AutoSize = true;
            layoutEditorEquipe.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutEditorEquipe.ColumnCount = 1;
            layoutEditorEquipe.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutEditorEquipe.Dock = DockStyle.Top;
            layoutEditorEquipe.Padding = new Padding(16);
            layoutEditorEquipe.RowCount = 9;
            layoutEditorEquipe.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutEditorEquipe.Controls.Add(textBox2, 0, 0);
            layoutEditorEquipe.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutEditorEquipe.Controls.Add(label4, 0, 1);
            layoutEditorEquipe.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutEditorEquipe.Controls.Add(textBox3, 0, 2);
            layoutEditorEquipe.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutEditorEquipe.Controls.Add(label5, 0, 3);
            layoutEditorEquipe.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutEditorEquipe.Controls.Add(pictureBox4, 0, 4);
            layoutEditorEquipe.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutEditorEquipe.Controls.Add(labelEscudoEquipe, 0, 5);
            layoutEditorEquipe.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutEditorEquipe.Controls.Add(layoutEscudoAcoes, 0, 6);
            layoutEditorEquipe.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutEditorEquipe.Controls.Add(layoutAcoesEquipe, 0, 7);
            layoutEditorEquipe.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutEditorEquipe.Controls.Add(labelStatusEquipe, 0, 8);
            //
            // textBox2
            //
            textBox2.Name = "textBox2";
            textBox2.BackColor = Color.White;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Dock = DockStyle.Top;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            textBox2.Margin = new Padding(0, 0, 0, 16);
            textBox2.ReadOnly = true;
            textBox2.TabStop = false;
            textBox2.Text = "Cadastrar nova equipe";
            //
            // label4
            //
            label4.Name = "label4";
            label4.AutoSize = true;
            label4.Dock = DockStyle.Top;
            label4.Margin = new Padding(0, 0, 0, 4);
            label4.Text = "NOME DA EQUIPE";
            //
            // textBox3
            //
            textBox3.Name = "textBox3";
            textBox3.Dock = DockStyle.Top;
            textBox3.Margin = new Padding(0, 0, 0, 16);
            textBox3.MaxLength = 100;
            textBox3.PlaceholderText = "Ex: Taubaté Vôlei Clube";
            textBox3.TabIndex = 0;
            //
            // label5
            //
            label5.Name = "label5";
            label5.AutoSize = true;
            label5.Dock = DockStyle.Top;
            label5.Margin = new Padding(0, 0, 0, 4);
            label5.Text = "ESCUDO DA EQUIPE";
            //
            // pictureBox4
            //
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Anchor = AnchorStyles.Left;
            pictureBox4.BackColor = Color.White;
            pictureBox4.BorderStyle = BorderStyle.FixedSingle;
            pictureBox4.Margin = new Padding(0, 0, 0, 4);
            pictureBox4.Size = new Size(128, 96);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabStop = false;
            //
            // labelEscudoEquipe
            //
            labelEscudoEquipe.Name = "labelEscudoEquipe";
            labelEscudoEquipe.AutoSize = true;
            labelEscudoEquipe.Dock = DockStyle.Top;
            labelEscudoEquipe.Font = new Font("Segoe UI", 8F);
            labelEscudoEquipe.ForeColor = Color.FromArgb(100, 116, 139);
            labelEscudoEquipe.Margin = new Padding(0, 0, 0, 8);
            labelEscudoEquipe.Text = "Sem escudo";
            //
            // layoutEscudoAcoes
            //
            layoutEscudoAcoes.Name = "layoutEscudoAcoes";
            layoutEscudoAcoes.AutoSize = true;
            layoutEscudoAcoes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutEscudoAcoes.Dock = DockStyle.Top;
            layoutEscudoAcoes.Margin = new Padding(0, 0, 0, 16);
            layoutEscudoAcoes.Controls.Add(button2);
            layoutEscudoAcoes.Controls.Add(btnRemoverEscudo);
            //
            // button2
            //
            button2.Name = "button2";
            button2.Size = new Size(145, 32);
            button2.Margin = new Padding(0, 0, 8, 0);
            button2.Text = "Escolher escudo";
            button2.TabIndex = 0;
            button2.Click += button2_Click;
            //
            // btnRemoverEscudo
            //
            btnRemoverEscudo.Name = "btnRemoverEscudo";
            btnRemoverEscudo.Size = new Size(100, 32);
            btnRemoverEscudo.Margin = Padding.Empty;
            btnRemoverEscudo.Text = "Remover";
            btnRemoverEscudo.TabIndex = 1;
            btnRemoverEscudo.Click += btnRemoverEscudo_Click;
            //
            // layoutAcoesEquipe
            //
            layoutAcoesEquipe.Name = "layoutAcoesEquipe";
            layoutAcoesEquipe.AutoSize = true;
            layoutAcoesEquipe.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutAcoesEquipe.Dock = DockStyle.Top;
            layoutAcoesEquipe.Margin = new Padding(0, 0, 0, 8);
            layoutAcoesEquipe.Controls.Add(button4);
            layoutAcoesEquipe.Controls.Add(button5);
            //
            // button4
            //
            button4.Name = "button4";
            button4.Size = new Size(100, 32);
            button4.Margin = new Padding(0, 0, 8, 0);
            button4.Text = "Limpar";
            button4.TabIndex = 0;
            button4.Click += button4_Click;
            //
            // button5
            //
            button5.Name = "button5";
            button5.Size = new Size(145, 32);
            button5.Margin = Padding.Empty;
            button5.BackColor = Color.FromArgb(124, 58, 237);
            button5.ForeColor = Color.White;
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;
            button5.Text = "Salvar equipe";
            button5.TabIndex = 1;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            //
            // labelStatusEquipe
            //
            labelStatusEquipe.Name = "labelStatusEquipe";
            labelStatusEquipe.AutoSize = true;
            labelStatusEquipe.Dock = DockStyle.Top;
            labelStatusEquipe.ForeColor = Color.FromArgb(100, 116, 139);
            labelStatusEquipe.Margin = Padding.Empty;
            labelStatusEquipe.MaximumSize = new Size(285, 0);
            //
            // openFileDialog1
            //
            openFileDialog1.Title = "Escolher escudo da equipe";
            openFileDialog1.Filter = "Imagens|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            openFileDialog1.RestoreDirectory = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.ControlLight;
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(700, 420);
            Name = "TelaEquipes";
            Size = new Size(736, 610);
            Controls.Add(panel4);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Load += TelaEquipes_Load;
            layoutAcoesEquipe.ResumeLayout(false);
            layoutAcoesEquipe.PerformLayout();
            layoutEscudoAcoes.ResumeLayout(false);
            layoutEscudoAcoes.PerformLayout();
            layoutEditorEquipe.ResumeLayout(false);
            layoutEditorEquipe.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            painelListaEquipes.ResumeLayout(false);
            painelListaEquipes.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Button button1;
        private Panel panel4;
        private Panel painelListaEquipes;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private TableLayoutPanel layoutEditorEquipe;
        private TextBox textBox2;
        private Label label4;
        private TextBox textBox3;
        private Label label5;
        private PictureBox pictureBox4;
        private Label labelEscudoEquipe;
        private FlowLayoutPanel layoutEscudoAcoes;
        private Button button2;
        private Button btnRemoverEscudo;
        private FlowLayoutPanel layoutAcoesEquipe;
        private Button button4;
        private Button button5;
        private Label labelStatusEquipe;
        private OpenFileDialog openFileDialog1;
    }
}
