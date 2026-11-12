namespace Limk.VolleyBall
{
    partial class FrmInicial
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            menuLateral1 = new Limk.VolleyBall.User_Constrols.MenuLateral();
            panelTelas = new Panel();
            panel2 = new Panel();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            button1 = new Button();
            label1 = new Label();
            tableLayoutPanel1.SuspendLayout();
            panelTelas.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.Controls.Add(menuLateral1, 0, 0);
            tableLayoutPanel1.Controls.Add(panelTelas, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanel1.Size = new Size(914, 600);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // menuLateral1
            // 
            menuLateral1.BackColor = Color.FromArgb(30, 21, 42);
            menuLateral1.Dock = DockStyle.Fill;
            menuLateral1.Location = new Point(3, 5);
            menuLateral1.Margin = new Padding(3, 5, 3, 5);
            menuLateral1.Name = "menuLateral1";
            menuLateral1.Size = new Size(176, 590);
            menuLateral1.TabIndex = 0;
            // 
            // panelTelas
            // 
            panelTelas.BackColor = Color.FromArgb(248, 249, 254);
            panelTelas.Controls.Add(panel2);
            panelTelas.Controls.Add(panel1);
            panelTelas.Dock = DockStyle.Fill;
            panelTelas.Location = new Point(185, 4);
            panelTelas.Margin = new Padding(3, 4, 3, 4);
            panelTelas.Name = "panelTelas";
            panelTelas.Size = new Size(726, 592);
            panelTelas.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(18, 65);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(20, 0, 0, 20);
            panel2.Size = new Size(688, 128);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.FromArgb(16, 185, 129);
            label6.Location = new Point(153, 15);
            label6.Name = "label6";
            label6.Size = new Size(18, 20);
            label6.TabIndex = 4;
            label6.Text = "●";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 8F);
            label5.Location = new Point(167, 17);
            label5.Name = "label5";
            label5.Size = new Size(163, 19);
            label5.TabIndex = 3;
            label5.Text = "Pronto para sessão tática";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(237, 233, 254);
            label4.Font = new Font("Segoe UI", 8F);
            label4.ForeColor = Color.FromArgb(99, 14, 212);
            label4.Location = new Point(19, 17);
            label4.Name = "label4";
            label4.Size = new Size(128, 19);
            label4.TabIndex = 2;
            label4.Text = "TACTICAL DESK 2.4";
            // 
            // label3
            // 
            label3.Location = new Point(11, 77);
            label3.Name = "label3";
            label3.Size = new Size(422, 45);
            label3.TabIndex = 1;
            label3.Text = "Planeje rotações, posicione defesas de alto nível e projete táticas sequenciais com precisão em quadra.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial Rounded MT Bold", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(11, 36);
            label2.Name = "label2";
            label2.Size = new Size(347, 39);
            label2.TabIndex = 0;
            label2.Text = "LIMK Volleyball Hub";
            label2.Click += label2_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(254, 254, 255);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(726, 34);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(125, 59, 237);
            button1.ForeColor = Color.FromArgb(254, 254, 255);
            button1.Location = new Point(623, 2);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 2;
            button1.Text = "💾Salvar Tática";
            button1.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 5);
            label1.Name = "label1";
            label1.Size = new Size(127, 20);
            label1.TabIndex = 1;
            label1.Text = "LIMK Tactical Hub";
            // 
            // FrmInicial
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmInicial";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            panelTelas.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private User_Constrols.MenuLateral menuLateral1;
        private Panel panelTelas;
        private Panel panel1;
        private Button button1;
        private Label label1;
        private Panel panel2;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label4;
        private Label label6;
    }
}
