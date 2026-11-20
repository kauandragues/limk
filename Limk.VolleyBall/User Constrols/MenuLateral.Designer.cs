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
            lblMarca = new Label();
            btnPranchetaTatica = new Button();
            btnInicio = new Button();
            btnEquipes = new Button();
            btnTaticas = new Button();
            panelRodape = new Panel();
            btnConfiguracoes = new Button();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panelRodape.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 449);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Padding = new Padding(12, 12, 12, 12);
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 56F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(btnAtletas, 0, 4);
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(lblMarca, 1, 0);
            tableLayoutPanel1.SetColumnSpan(btnInicio, 2);
            tableLayoutPanel1.SetColumnSpan(btnPranchetaTatica, 2);
            tableLayoutPanel1.SetColumnSpan(btnTaticas, 2);
            tableLayoutPanel1.SetColumnSpan(btnAtletas, 2);
            tableLayoutPanel1.SetColumnSpan(btnEquipes, 2);
            tableLayoutPanel1.Controls.Add(btnPranchetaTatica, 0, 2);
            tableLayoutPanel1.Controls.Add(btnInicio, 0, 1);
            tableLayoutPanel1.Controls.Add(btnEquipes, 0, 5);
            tableLayoutPanel1.Controls.Add(btnTaticas, 0, 3);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tableLayoutPanel1.Size = new Size(200, 376);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // btnAtletas
            // 
            btnAtletas.Dock = DockStyle.Fill;
            btnAtletas.FlatAppearance.BorderSize = 0;
            btnAtletas.FlatStyle = FlatStyle.Flat;
            btnAtletas.Location = new Point(12, 271);
            btnAtletas.Margin = new Padding(0, 3, 0, 3);
            btnAtletas.Name = "btnAtletas";
            btnAtletas.Size = new Size(176, 42);
            btnAtletas.TabIndex = 3;
            btnAtletas.Text = "Atletas";
            btnAtletas.UseVisualStyleBackColor = false;
            btnAtletas.Click += btnAtletas_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = Properties.Resources.LogoMenuLateral;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Margin = new Padding(4, 16, 2, 20);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 76);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            //
            // lblMarca
            //
            lblMarca.Anchor = AnchorStyles.Left;
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblMarca.ForeColor = Color.White;
            lblMarca.Margin = new Padding(6, 16, 0, 20);
            lblMarca.Name = "lblMarca";
            lblMarca.Text = "LIMK";
            // 
            // btnPranchetaTatica
            // 
            btnPranchetaTatica.Dock = DockStyle.Fill;
            btnPranchetaTatica.FlatAppearance.BorderSize = 0;
            btnPranchetaTatica.FlatStyle = FlatStyle.Flat;
            btnPranchetaTatica.Location = new Point(12, 175);
            btnPranchetaTatica.Margin = new Padding(0, 3, 0, 3);
            btnPranchetaTatica.Name = "btnPranchetaTatica";
            btnPranchetaTatica.Size = new Size(176, 42);
            btnPranchetaTatica.TabIndex = 1;
            btnPranchetaTatica.Text = "Prancheta tática";
            btnPranchetaTatica.UseVisualStyleBackColor = false;
            btnPranchetaTatica.Click += btnPranchetaTatica_Click;
            // 
            // btnInicio
            // 
            btnInicio.Dock = DockStyle.Fill;
            btnInicio.FlatAppearance.BorderSize = 0;
            btnInicio.FlatStyle = FlatStyle.Flat;
            btnInicio.Location = new Point(12, 127);
            btnInicio.Margin = new Padding(0, 3, 0, 3);
            btnInicio.Name = "btnInicio";
            btnInicio.Size = new Size(176, 42);
            btnInicio.TabIndex = 0;
            btnInicio.Text = "Início";
            btnInicio.UseVisualStyleBackColor = false;
            btnInicio.Click += btnInicio_Click_1;
            // 
            // btnEquipes
            // 
            btnEquipes.Dock = DockStyle.Fill;
            btnEquipes.FlatAppearance.BorderSize = 0;
            btnEquipes.FlatStyle = FlatStyle.Flat;
            btnEquipes.Location = new Point(12, 319);
            btnEquipes.Margin = new Padding(0, 3, 0, 3);
            btnEquipes.Name = "btnEquipes";
            btnEquipes.Size = new Size(176, 42);
            btnEquipes.TabIndex = 4;
            btnEquipes.Text = "Equipes";
            btnEquipes.UseVisualStyleBackColor = false;
            btnEquipes.Click += btnEquipes_Click;
            // 
            // btnTaticas
            // 
            btnTaticas.Dock = DockStyle.Fill;
            btnTaticas.FlatAppearance.BorderSize = 0;
            btnTaticas.FlatStyle = FlatStyle.Flat;
            btnTaticas.Location = new Point(12, 223);
            btnTaticas.Margin = new Padding(0, 3, 0, 3);
            btnTaticas.Name = "btnTaticas";
            btnTaticas.Size = new Size(176, 42);
            btnTaticas.TabIndex = 2;
            btnTaticas.Text = "Táticas";
            btnTaticas.UseVisualStyleBackColor = false;
            btnTaticas.Click += btnTaticas_Click;
            //
            // panelRodape
            //
            panelRodape.Controls.Add(btnConfiguracoes);
            panelRodape.Dock = DockStyle.Bottom;
            panelRodape.Location = new Point(0, 449);
            panelRodape.Name = "panelRodape";
            panelRodape.Padding = new Padding(12, 12, 12, 12);
            panelRodape.Size = new Size(200, 68);
            panelRodape.TabIndex = 1;
            //
            // btnConfiguracoes
            //
            btnConfiguracoes.AccessibleName = "Configurações";
            btnConfiguracoes.Dock = DockStyle.Fill;
            btnConfiguracoes.FlatAppearance.BorderSize = 0;
            btnConfiguracoes.FlatStyle = FlatStyle.Flat;
            btnConfiguracoes.Location = new Point(12, 12);
            btnConfiguracoes.Name = "btnConfiguracoes";
            btnConfiguracoes.Size = new Size(176, 44);
            btnConfiguracoes.TabIndex = 0;
            btnConfiguracoes.Text = "⚙️ Configurações";
            btnConfiguracoes.UseVisualStyleBackColor = false;
            btnConfiguracoes.Click += btnConfiguracoes_Click;
            //
            // MenuLateral
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 21, 42);
            Controls.Add(panel1);
            Controls.Add(panelRodape);
            Name = "MenuLateral";
            Size = new Size(200, 517);
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panelRodape.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Label lblMarca;
        private Button btnInicio;
        private Button btnPranchetaTatica;
        private Button btnTaticas;
        private Button btnAtletas;
        private Button btnEquipes;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panelRodape;
        private Button btnConfiguracoes;
    }
}
