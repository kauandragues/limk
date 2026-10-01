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
            tableLayoutPanel1 = new TableLayoutPanel();
            btnAtletas = new Button();
            pictureBox1 = new PictureBox();
            btnPranchetaTatica = new Button();
            btnInicio = new Button();
            btnEquipes = new Button();
            btnTaticas = new Button();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(30, 21, 42);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(162, 517);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(btnAtletas, 0, 4);
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(btnPranchetaTatica, 0, 2);
            tableLayoutPanel1.Controls.Add(btnInicio, 0, 1);
            tableLayoutPanel1.Controls.Add(btnEquipes, 0, 5);
            tableLayoutPanel1.Controls.Add(btnTaticas, 0, 3);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 21.7519112F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.935333F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.935333F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.935333F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.935333F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.935333F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 28.5714264F));
            tableLayoutPanel1.Size = new Size(158, 517);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // btnAtletas
            // 
            btnAtletas.Dock = DockStyle.Top;
            btnAtletas.FlatAppearance.BorderSize = 0;
            btnAtletas.FlatStyle = FlatStyle.Flat;
            btnAtletas.Location = new Point(3, 268);
            btnAtletas.Name = "btnAtletas";
            btnAtletas.Size = new Size(152, 43);
            btnAtletas.TabIndex = 4;
            btnAtletas.Text = "ATLETAS";
            btnAtletas.UseVisualStyleBackColor = true;
            btnAtletas.Click += btnAtletas_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = Properties.Resources.teste;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(152, 106);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnPranchetaTatica
            // 
            btnPranchetaTatica.BackColor = Color.FromArgb(30, 21, 42);
            btnPranchetaTatica.Dock = DockStyle.Top;
            btnPranchetaTatica.FlatAppearance.BorderSize = 0;
            btnPranchetaTatica.FlatStyle = FlatStyle.Flat;
            btnPranchetaTatica.Location = new Point(3, 166);
            btnPranchetaTatica.Name = "btnPranchetaTatica";
            btnPranchetaTatica.Size = new Size(152, 43);
            btnPranchetaTatica.TabIndex = 1;
            btnPranchetaTatica.Text = "PRANCHETA TÁTICA";
            btnPranchetaTatica.UseVisualStyleBackColor = false;
            btnPranchetaTatica.Click += btnPranchetaTatica_Click;
            // 
            // btnInicio
            // 
            btnInicio.BackColor = Color.FromArgb(30, 21, 42);
            btnInicio.Dock = DockStyle.Top;
            btnInicio.FlatAppearance.BorderSize = 0;
            btnInicio.FlatStyle = FlatStyle.Flat;
            btnInicio.ForeColor = Color.Black;
            btnInicio.Location = new Point(3, 115);
            btnInicio.Name = "btnInicio";
            btnInicio.Size = new Size(152, 43);
            btnInicio.TabIndex = 0;
            btnInicio.TabStop = false;
            btnInicio.Text = "INÍCIO";
            btnInicio.UseVisualStyleBackColor = false;
            btnInicio.Click += btnInicio_Click_1;
            // 
            // btnEquipes
            // 
            btnEquipes.Dock = DockStyle.Top;
            btnEquipes.FlatAppearance.BorderSize = 0;
            btnEquipes.FlatStyle = FlatStyle.Flat;
            btnEquipes.Location = new Point(3, 319);
            btnEquipes.Name = "btnEquipes";
            btnEquipes.Size = new Size(152, 43);
            btnEquipes.TabIndex = 3;
            btnEquipes.Text = "EQUIPES";
            btnEquipes.UseVisualStyleBackColor = true;
            btnEquipes.Click += btnEquipes_Click;
            // 
            // btnTaticas
            // 
            btnTaticas.Dock = DockStyle.Top;
            btnTaticas.FlatAppearance.BorderSize = 0;
            btnTaticas.FlatStyle = FlatStyle.Flat;
            btnTaticas.Location = new Point(3, 217);
            btnTaticas.Name = "btnTaticas";
            btnTaticas.Size = new Size(152, 43);
            btnTaticas.TabIndex = 2;
            btnTaticas.Text = "TÁTICAS";
            btnTaticas.UseVisualStyleBackColor = true;
            btnTaticas.Click += btnTaticas_Click;
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
            tableLayoutPanel1.ResumeLayout(false);
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
        private TableLayoutPanel tableLayoutPanel1;
    }
}
