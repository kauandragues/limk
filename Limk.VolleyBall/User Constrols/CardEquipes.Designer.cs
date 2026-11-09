namespace Limk.VolleyBall.User_Constrols
{
    partial class CardEquipes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardEquipes));
            panel3 = new Panel();
            button8 = new Button();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            pictureBox3 = new PictureBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(pictureBox2);
            panel3.Controls.Add(pictureBox1);
            panel3.Controls.Add(button8);
            panel3.Controls.Add(label10);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(pictureBox3);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(396, 95);
            panel3.TabIndex = 7;
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(124, 58, 237);
            button8.ForeColor = Color.White;
            button8.Location = new Point(86, 61);
            button8.Name = "button8";
            button8.Size = new Size(145, 25);
            button8.TabIndex = 10;
            button8.Text = "Ver Elenco";
            button8.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(224, 38);
            label10.Name = "label10";
            label10.Size = new Size(137, 16);
            label10.TabIndex = 9;
            label10.Text = "0 Táticas Cadastradas";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(86, 38);
            label9.Name = "label9";
            label9.Size = new Size(135, 16);
            label9.TabIndex = 7;
            label9.Text = "0 Atletas Cadastrados";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(84, 6);
            label8.Name = "label8";
            label8.Size = new Size(134, 28);
            label8.TabIndex = 8;
            label8.Text = "Vôlei Taubaté";
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.MediumSlateBlue;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(12, 12);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(61, 53);
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(314, 57);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(47, 29);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(267, 63);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(47, 29);
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            // 
            // CardEquipes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel3);
            Name = "CardEquipes";
            Size = new Size(396, 95);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel3;
        private PictureBox pictureBox1;
        private Button button8;
        private Label label10;
        private Label label9;
        private Label label8;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
    }
}
