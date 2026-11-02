namespace Limk.VolleyBall.User_Constrols
{
    partial class TelaPranchetaTatica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaPranchetaTatica));
            panelCabecalho = new Panel();
            button1 = new Button();
            labelNomeTatica = new Label();
            labelTitulo = new Label();
            panel5 = new Panel();
            panel2 = new Panel();
            panelPranchetaTatica = new Panel();
            panel6 = new Panel();
            panel3 = new Panel();
            btnAdicionarAtleta = new Button();
            btnAdicionarBola = new Button();
            panel1 = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            panelCabecalho.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panelCabecalho
            // 
            panelCabecalho.BackColor = Color.White;
            panelCabecalho.Controls.Add(button1);
            panelCabecalho.Controls.Add(labelNomeTatica);
            panelCabecalho.Controls.Add(labelTitulo);
            panelCabecalho.Dock = DockStyle.Top;
            panelCabecalho.Location = new Point(0, 0);
            panelCabecalho.Name = "panelCabecalho";
            panelCabecalho.Size = new Size(842, 86);
            panelCabecalho.TabIndex = 1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(124, 58, 237);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.Window;
            button1.Location = new Point(665, 26);
            button1.Name = "button1";
            button1.Size = new Size(154, 37);
            button1.TabIndex = 1;
            button1.Text = "💾 Salvar Tática";
            button1.UseVisualStyleBackColor = false;
            // 
            // labelNomeTatica
            // 
            labelNomeTatica.AutoSize = true;
            labelNomeTatica.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNomeTatica.Location = new Point(194, 34);
            labelNomeTatica.Name = "labelNomeTatica";
            labelNomeTatica.Size = new Size(333, 19);
            labelNomeTatica.TabIndex = 0;
            labelNomeTatica.Text = "Rotação 1 - Saída Rápida Com Central (P1)";
            // 
            // labelTitulo
            // 
            labelTitulo.AutoSize = true;
            labelTitulo.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitulo.Location = new Point(34, 34);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(137, 19);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "Prancheta Tática";
            // 
            // panel5
            // 
            panel5.BackColor = Color.White;
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(0, 680);
            panel5.Name = "panel5";
            panel5.Size = new Size(842, 133);
            panel5.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = SystemColors.ControlLight;
            panel2.Controls.Add(panelPranchetaTatica);
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(panel3);
            panel2.Location = new Point(0, 85);
            panel2.Name = "panel2";
            panel2.Size = new Size(842, 597);
            panel2.TabIndex = 5;
            // 
            // panelPranchetaTatica
            // 
            panelPranchetaTatica.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelPranchetaTatica.BackColor = Color.White;
            panelPranchetaTatica.Location = new Point(194, 23);
            panelPranchetaTatica.Name = "panelPranchetaTatica";
            panelPranchetaTatica.Size = new Size(402, 552);
            panelPranchetaTatica.TabIndex = 3;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel6.BackColor = Color.White;
            panel6.Location = new Point(616, 23);
            panel6.Name = "panel6";
            panel6.Size = new Size(203, 552);
            panel6.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel3.BackColor = Color.White;
            panel3.Controls.Add(btnAdicionarAtleta);
            panel3.Controls.Add(btnAdicionarBola);
            panel3.Controls.Add(panel1);
            panel3.Location = new Point(21, 23);
            panel3.Name = "panel3";
            panel3.Size = new Size(150, 552);
            panel3.TabIndex = 2;
            // 
            // btnAdicionarAtleta
            // 
            btnAdicionarAtleta.Dock = DockStyle.Top;
            btnAdicionarAtleta.FlatAppearance.BorderSize = 0;
            btnAdicionarAtleta.FlatAppearance.MouseDownBackColor = Color.FromArgb(133, 62, 255);
            btnAdicionarAtleta.FlatAppearance.MouseOverBackColor = Color.FromArgb(124, 58, 237);
            btnAdicionarAtleta.FlatStyle = FlatStyle.Flat;
            btnAdicionarAtleta.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdicionarAtleta.Image = (Image)resources.GetObject("btnAdicionarAtleta.Image");
            btnAdicionarAtleta.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdicionarAtleta.Location = new Point(0, 101);
            btnAdicionarAtleta.Name = "btnAdicionarAtleta";
            btnAdicionarAtleta.Size = new Size(150, 48);
            btnAdicionarAtleta.TabIndex = 6;
            btnAdicionarAtleta.Text = "   Atleta";
            btnAdicionarAtleta.UseVisualStyleBackColor = true;
            btnAdicionarAtleta.Click += btnAdicionarAtleta_Click;
            // 
            // btnAdicionarBola
            // 
            btnAdicionarBola.Dock = DockStyle.Top;
            btnAdicionarBola.FlatAppearance.BorderSize = 0;
            btnAdicionarBola.FlatAppearance.MouseDownBackColor = Color.FromArgb(133, 62, 255);
            btnAdicionarBola.FlatAppearance.MouseOverBackColor = Color.FromArgb(124, 58, 237);
            btnAdicionarBola.FlatStyle = FlatStyle.Flat;
            btnAdicionarBola.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdicionarBola.Image = (Image)resources.GetObject("btnAdicionarBola.Image");
            btnAdicionarBola.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdicionarBola.Location = new Point(0, 53);
            btnAdicionarBola.Name = "btnAdicionarBola";
            btnAdicionarBola.Size = new Size(150, 48);
            btnAdicionarBola.TabIndex = 5;
            btnAdicionarBola.Text = "  Bola";
            btnAdicionarBola.UseVisualStyleBackColor = true;
            btnAdicionarBola.Click += btnAdicionarBola_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(150, 53);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLight;
            label1.Location = new Point(42, 17);
            label1.Name = "label1";
            label1.Size = new Size(105, 19);
            label1.TabIndex = 1;
            label1.Text = "Ferramentas";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(5, 8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(40, 36);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // TelaPranchetaTatica
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Red;
            Controls.Add(panel2);
            Controls.Add(panel5);
            Controls.Add(panelCabecalho);
            Name = "TelaPranchetaTatica";
            Size = new Size(842, 813);
            panelCabecalho.ResumeLayout(false);
            panelCabecalho.PerformLayout();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelCabecalho;
        private Panel panel5;
        private Panel panel2;
        private Panel panel6;
        private Panel panel3;
        private Panel panelPranchetaTatica;
        private Label labelTitulo;
        private Button button1;
        private Label labelNomeTatica;
        private Panel panel1;
        private Button btnAdicionarAtleta;
        private Button btnAdicionarBola;
        private PictureBox pictureBox1;
        private Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
