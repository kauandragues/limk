namespace Limk.VolleyBall.User_Constrols
{
    partial class CardTática
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardTática));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            btnExcluirTatica = new Button();
            pictureBox1 = new PictureBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoEllipsis = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(27, 20);
            label1.Name = "label1";
            label1.Size = new Size(359, 18);
            label1.TabIndex = 0;
            label1.Text = "Nome da Equipe";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.UseMnemonic = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoEllipsis = true;
            label2.Font = new Font("Arial", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(27, 53);
            label2.Name = "label2";
            label2.Size = new Size(480, 20);
            label2.TabIndex = 1;
            label2.Text = "Nome da tática";
            label2.UseMnemonic = false;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoEllipsis = true;
            label3.ForeColor = Color.DimGray;
            label3.Location = new Point(26, 74);
            label3.Name = "label3";
            label3.Size = new Size(438, 51);
            label3.TabIndex = 2;
            label3.Text = resources.GetString("label3.Text");
            label3.UseMnemonic = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.BackColor = SystemColors.ControlLight;
            button1.Location = new Point(28, 156);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(254, 29);
            button1.TabIndex = 4;
            button1.Text = "🖊️ Abrir na Prancheta";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.BackColor = Color.FromArgb(124, 58, 237);
            button2.ForeColor = Color.White;
            button2.Location = new Point(296, 156);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(168, 29);
            button2.TabIndex = 5;
            button2.Text = "▶️ Apresentar";
            button2.UseVisualStyleBackColor = false;
            // 
            // btnExcluirTatica
            //
            btnExcluirTatica.AccessibleName = "Excluir tática";
            btnExcluirTatica.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExcluirTatica.AutoSize = true;
            btnExcluirTatica.Cursor = Cursors.Hand;
            btnExcluirTatica.FlatStyle = FlatStyle.Flat;
            btnExcluirTatica.ForeColor = Color.FromArgb(148, 39, 52);
            btnExcluirTatica.Location = new Point(407, 14);
            btnExcluirTatica.Margin = new Padding(3, 2, 3, 2);
            btnExcluirTatica.Name = "btnExcluirTatica";
            btnExcluirTatica.Size = new Size(98, 27);
            btnExcluirTatica.TabIndex = 6;
            btnExcluirTatica.Text = "Excluir tática";
            btnExcluirTatica.UseVisualStyleBackColor = true;
            //
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox1.BackColor = SystemColors.Window;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(28, 124);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(30, 16);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(74, 124);
            label4.Name = "label4";
            label4.Size = new Size(226, 15);
            label4.TabIndex = 7;
            label4.Text = "Quantidade de momentos configurados";
            // 
            // CardTática
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pictureBox1);
            Controls.Add(label4);
            Controls.Add(button2);
            Controls.Add(btnExcluirTatica);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "CardTática";
            Size = new Size(534, 206);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
        private Button button2;
        private Button btnExcluirTatica;
        private PictureBox pictureBox1;
        private Label label4;
    }
}
