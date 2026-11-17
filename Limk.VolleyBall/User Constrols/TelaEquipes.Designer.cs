namespace Limk.VolleyBall.User_Constrols
{
    partial class TelaEquipes
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaEquipes));
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            label3 = new Label();
            panel1 = new Panel();
            pictureBox4 = new PictureBox();
            panel2 = new Panel();
            button3 = new Button();
            button6 = new Button();
            button7 = new Button();
            label6 = new Label();
            pictureBox2 = new PictureBox();
            textBox4 = new TextBox();
            label7 = new Label();
            textBox5 = new TextBox();
            button5 = new Button();
            button4 = new Button();
            button2 = new Button();
            label5 = new Label();
            pictureBox1 = new PictureBox();
            textBox3 = new TextBox();
            label4 = new Label();
            textBox2 = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            panel4 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            cardEquipes2 = new CardEquipes();
            cardEquipes1 = new CardEquipes();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel4.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(19, 22);
            label1.Name = "label1";
            label1.Size = new Size(380, 33);
            label1.TabIndex = 0;
            label1.Text = "Gerenciamento de Equipes";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(28, 64);
            label2.Name = "label2";
            label2.Size = new Size(332, 16);
            label2.TabIndex = 1;
            label2.Text = "Cadastre suas equipes e defina as cores dos uniformes.";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(449, 28);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "🔍 Buscar equipe...";
            textBox1.Size = new Size(211, 27);
            textBox1.TabIndex = 2;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(124, 58, 237);
            button1.ForeColor = Color.White;
            button1.Location = new Point(666, 28);
            button1.Name = "button1";
            button1.Size = new Size(154, 28);
            button1.TabIndex = 3;
            button1.Text = "+ Cadastrar Nova Equipe";
            button1.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(3, 12);
            label3.Name = "label3";
            label3.Size = new Size(166, 17);
            label3.TabIndex = 4;
            label3.Text = "Equipes no Banco Local";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(textBox2);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(414, 0);
            panel1.MaximumSize = new Size(412, 398);
            panel1.Name = "panel1";
            panel1.Size = new Size(362, 398);
            panel1.TabIndex = 5;
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(244, 182);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(106, 84);
            pictureBox4.TabIndex = 11;
            pictureBox4.TabStop = false;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel2.BackColor = Color.White;
            panel2.Controls.Add(button3);
            panel2.Controls.Add(button6);
            panel2.Controls.Add(button7);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(textBox5);
            panel2.Location = new Point(-471, 8);
            panel2.MaximumSize = new Size(412, 398);
            panel2.Name = "panel2";
            panel2.Size = new Size(371, 63);
            panel2.TabIndex = 9;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button3.BackColor = Color.FromArgb(124, 58, 237);
            button3.ForeColor = Color.White;
            button3.Location = new Point(342, 574);
            button3.Name = "button3";
            button3.Size = new Size(140, 38);
            button3.TabIndex = 8;
            button3.Text = "✔️ Salvar Equipe";
            button3.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button6.BackColor = SystemColors.ControlLight;
            button6.Location = new Point(193, 574);
            button6.Name = "button6";
            button6.Size = new Size(140, 38);
            button6.TabIndex = 7;
            button6.Text = "Limpar";
            button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            button7.BackColor = SystemColors.ControlLight;
            button7.Location = new Point(22, 214);
            button7.Name = "button7";
            button7.Size = new Size(206, 52);
            button7.TabIndex = 5;
            button7.Text = "Adicionar";
            button7.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(21, 182);
            label6.Name = "label6";
            label6.Size = new Size(129, 17);
            label6.TabIndex = 4;
            label6.Text = "ESCUDO DA EQUIPE";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.White;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(7, 16);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(35, 33);
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // textBox4
            // 
            textBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            textBox4.BackColor = SystemColors.ControlLight;
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Location = new Point(22, 125);
            textBox4.Name = "textBox4";
            textBox4.PlaceholderText = "Ex: Taubaté Vôlei Clube";
            textBox4.Size = new Size(303, 20);
            textBox4.TabIndex = 2;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(20, 92);
            label7.Name = "label7";
            label7.Size = new Size(118, 17);
            label7.TabIndex = 1;
            label7.Text = "NOME DA EQUIPE";
            // 
            // textBox5
            // 
            textBox5.BorderStyle = BorderStyle.None;
            textBox5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox5.Location = new Point(42, 16);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(211, 27);
            textBox5.TabIndex = 0;
            textBox5.Text = "Cadastrar Nova Equipe";
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button5.BackColor = Color.FromArgb(124, 58, 237);
            button5.ForeColor = Color.White;
            button5.Location = new Point(162, 339);
            button5.Name = "button5";
            button5.Size = new Size(140, 38);
            button5.TabIndex = 8;
            button5.Text = "✔️ Salvar Equipe";
            button5.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button4.BackColor = SystemColors.ControlLight;
            button4.Location = new Point(13, 339);
            button4.Name = "button4";
            button4.Size = new Size(140, 38);
            button4.TabIndex = 7;
            button4.Text = "Limpar";
            button4.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ControlLight;
            button2.Location = new Point(22, 214);
            button2.Name = "button2";
            button2.Size = new Size(206, 52);
            button2.TabIndex = 5;
            button2.Text = "Adicionar";
            button2.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(21, 182);
            label5.Name = "label5";
            label5.Size = new Size(129, 17);
            label5.TabIndex = 4;
            label5.Text = "ESCUDO DA EQUIPE";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(7, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(35, 33);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            textBox3.BackColor = SystemColors.ControlLight;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Location = new Point(22, 125);
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Ex: Taubaté Vôlei Clube";
            textBox3.Size = new Size(303, 20);
            textBox3.TabIndex = 2;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(20, 92);
            label4.Name = "label4";
            label4.Size = new Size(118, 17);
            label4.TabIndex = 1;
            label4.Text = "NOME DA EQUIPE";
            // 
            // textBox2
            // 
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(42, 16);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(211, 27);
            textBox2.TabIndex = 0;
            textBox2.Text = "Cadastrar Nova Equipe";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel4.Controls.Add(tableLayoutPanel1);
            panel4.Controls.Add(label3);
            panel4.Controls.Add(panel1);
            panel4.Location = new Point(35, 83);
            panel4.Name = "panel4";
            panel4.Size = new Size(776, 708);
            panel4.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(cardEquipes2, 0, 0);
            tableLayoutPanel1.Controls.Add(cardEquipes1, 0, 4);
            tableLayoutPanel1.Location = new Point(0, 32);
            tableLayoutPanel1.Margin = new Padding(3, 10, 3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(408, 673);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // cardEquipes2
            // 
            cardEquipes2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cardEquipes2.Location = new Point(3, 3);
            cardEquipes2.Name = "cardEquipes2";
            cardEquipes2.Size = new Size(402, 95);
            cardEquipes2.TabIndex = 1;
            // 
            // cardEquipes1
            // 
            cardEquipes1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cardEquipes1.Location = new Point(3, 104);
            cardEquipes1.Name = "cardEquipes1";
            cardEquipes1.Size = new Size(402, 95);
            cardEquipes1.TabIndex = 0;
            // 
            // TelaEquipes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.ControlLight;
            Controls.Add(panel4);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "TelaEquipes";
            Size = new Size(841, 813);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Button button1;
        private Label label3;
        private Panel panel1;
        private TextBox textBox2;
        private PictureBox pictureBox1;
        private TextBox textBox3;
        private Label label4;
        private Label label5;
        private Button button5;
        private Button button4;
        private Button button2;
        private OpenFileDialog openFileDialog1;
        private Panel panel2;
        private Button button3;
        private Button button6;
        private Button button7;
        private Label label6;
        private PictureBox pictureBox2;
        private TextBox textBox4;
        private Label label7;
        private TextBox textBox5;
        private PictureBox pictureBox4;
        private Panel panel4;
        private TableLayoutPanel tableLayoutPanel1;
        private CardEquipes cardEquipes1;
        private CardEquipes cardEquipes2;
    }
}
