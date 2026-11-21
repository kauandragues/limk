namespace Limk.VolleyBall
{
    partial class FormPrincipal
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
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            //
            // tableLayoutPanel1
            //
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(menuLateral1, 0, 0);
            tableLayoutPanel1.Controls.Add(panelTelas, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanel1.Size = new Size(1057, 811);
            tableLayoutPanel1.TabIndex = 0;
            //
            // menuLateral1
            //
            menuLateral1.Dock = DockStyle.Fill;
            menuLateral1.Location = new Point(0, 0);
            menuLateral1.Margin = new Padding(0);
            menuLateral1.Name = "menuLateral1";
            menuLateral1.Size = new Size(200, 811);
            menuLateral1.TabIndex = 0;
            //
            // panelTelas
            //
            panelTelas.Dock = DockStyle.Fill;
            panelTelas.Location = new Point(203, 4);
            panelTelas.Margin = new Padding(3, 4, 3, 4);
            panelTelas.Name = "panelTelas";
            panelTelas.Size = new Size(851, 803);
            panelTelas.TabIndex = 1;
            //
            // FormPrincipal
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1057, 811);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(3, 4, 3, 4);
            Icon = Properties.Resources.IconeLIMK;
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LIMK Volleyball";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private User_Constrols.MenuLateral menuLateral1;
        private Panel panelTelas;
    }
}
