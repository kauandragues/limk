namespace Limk.VolleyBall.User_Constrols
{
    partial class MenuLateral
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
            panel1 = new Panel();
            btnAtletas = new Button();
            btnEquipes = new Button();
            btnTaticas = new Button();
            btnPranchetaTatica = new Button();
            btnInicio = new Button();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(30, 21, 42);
            panel1.Controls.Add(btnAtletas);
            panel1.Controls.Add(btnEquipes);
            panel1.Controls.Add(btnTaticas);
            panel1.Controls.Add(btnPranchetaTatica);
            panel1.Controls.Add(btnInicio);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(160, 517);
            panel1.TabIndex = 0;
            // 
            // btnAtletas
            // 
            btnAtletas.FlatStyle = FlatStyle.Flat;
            btnAtletas.Location = new Point(2, 262);
            btnAtletas.Name = "btnAtletas";
            btnAtletas.Size = new Size(155, 43);
            btnAtletas.TabIndex = 4;
            btnAtletas.Text = "ATLETAS";
            btnAtletas.UseVisualStyleBackColor = true;
            btnAtletas.Click += btnAtletas_Click;
            // 
            // btnEquipes
            // 
            btnEquipes.FlatStyle = FlatStyle.Flat;
            btnEquipes.Location = new Point(2, 213);
            btnEquipes.Name = "btnEquipes";
            btnEquipes.Size = new Size(155, 43);
            btnEquipes.TabIndex = 3;
            btnEquipes.Text = "EQUIPES";
            btnEquipes.UseVisualStyleBackColor = true;
            btnEquipes.Click += btnEquipes_Click;
            // 
            // btnTaticas
            // 
            btnTaticas.FlatStyle = FlatStyle.Flat;
            btnTaticas.Location = new Point(2, 164);
            btnTaticas.Name = "btnTaticas";
            btnTaticas.Size = new Size(155, 43);
            btnTaticas.TabIndex = 2;
            btnTaticas.Text = "TÁTICAS";
            btnTaticas.UseVisualStyleBackColor = true;
            btnTaticas.Click += btnTaticas_Click;
            // 
            // btnPranchetaTatica
            // 
            btnPranchetaTatica.BackColor = Color.FromArgb(30, 21, 42);
            btnPranchetaTatica.FlatStyle = FlatStyle.Flat;
            btnPranchetaTatica.Location = new Point(3, 118);
            btnPranchetaTatica.Name = "btnPranchetaTatica";
            btnPranchetaTatica.Size = new Size(155, 43);
            btnPranchetaTatica.TabIndex = 1;
            btnPranchetaTatica.Text = "PRANCHETA TÁTICA";
            btnPranchetaTatica.UseVisualStyleBackColor = false;
            btnPranchetaTatica.Click += btnPranchetaTatica_Click;
            // 
            // btnInicio
            // 
            btnInicio.BackColor = Color.FromArgb(30, 21, 42);
            btnInicio.FlatStyle = FlatStyle.Flat;
            btnInicio.Location = new Point(3, 74);
            btnInicio.Name = "btnInicio";
            btnInicio.Size = new Size(155, 43);
            btnInicio.TabIndex = 0;
            btnInicio.TabStop = false;
            btnInicio.Text = "INÍCIO";
            btnInicio.UseVisualStyleBackColor = false;
            btnInicio.Click += btnInicio_Click_1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.teste;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(155, 65);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // MenuLateral
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 21, 42);
            Controls.Add(panel1);
            Name = "MenuLateral";
            Size = new Size(162, 517);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Button btnInicio;
        private Button btnPranchetaTatica;
        private Button btnTaticas;
        private Button btnAtletas;
        private Button btnEquipes;
    }
}
