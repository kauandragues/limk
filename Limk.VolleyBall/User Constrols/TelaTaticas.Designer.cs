namespace Limk.VolleyBall.User_Constrols
{
    partial class TelaTaticas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaTaticas));
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            panel1 = new Panel();
            button2 = new Button();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            cardTática1 = new CardTática();
            panel2 = new Panel();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 22);
            label1.Name = "label1";
            label1.Size = new Size(336, 35);
            label1.TabIndex = 0;
            label1.Text = "Repositório de Táticas";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 57);
            label2.Name = "label2";
            label2.Size = new Size(478, 20);
            label2.TabIndex = 1;
            label2.Text = "Organize, edite e apresente todas as jogadas e formações do seu time.";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(124, 58, 237);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(662, 22);
            button1.Name = "button1";
            button1.Size = new Size(140, 35);
            button1.TabIndex = 2;
            button1.Text = "+ Nova Tática";
            button1.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.White;
            panel1.Controls.Add(button2);
            panel1.Controls.Add(comboBox2);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(textBox1);
            panel1.Location = new Point(23, 91);
            panel1.Name = "panel1";
            panel1.Size = new Size(779, 51);
            panel1.TabIndex = 3;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.BackColor = Color.Transparent;
            button2.FlatAppearance.BorderColor = Color.White;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.Location = new Point(736, 11);
            button2.Name = "button2";
            button2.Size = new Size(40, 29);
            button2.TabIndex = 4;
            button2.UseVisualStyleBackColor = false;
            // 
            // comboBox2
            // 
            comboBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBox2.BackColor = SystemColors.ControlLight;
            comboBox2.ForeColor = Color.DimGray;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(579, 12);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 6;
            comboBox2.Text = "Todas as Fases";
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBox1.BackColor = SystemColors.ControlLight;
            comboBox1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.ForeColor = Color.DimGray;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(415, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 5;
            comboBox1.Text = "Todas as Equipes";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BackColor = SystemColors.ControlLight;
            textBox1.ForeColor = Color.Black;
            textBox1.Location = new Point(19, 12);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "🔍 Buscar por nome de jogada ou rotação...";
            textBox1.Size = new Size(378, 27);
            textBox1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(cardTática1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 43.5897446F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 56.4102554F));
            tableLayoutPanel1.Size = new Size(779, 617);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // cardTática1
            // 
            cardTática1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cardTática1.BackColor = Color.White;
            cardTática1.Location = new Point(3, 3);
            cardTática1.Name = "cardTática1";
            cardTática1.Size = new Size(383, 262);
            cardTática1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(tableLayoutPanel1);
            panel2.Location = new Point(23, 183);
            panel2.Name = "panel2";
            panel2.Size = new Size(779, 617);
            panel2.TabIndex = 5;
            // 
            // TelaTaticas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = SystemColors.ControlLight;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "TelaTaticas";
            Size = new Size(840, 803);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button button1;
        private Panel panel1;
        private TextBox textBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Button button2;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel2;
        private CardTática cardTática1;
    }
}
