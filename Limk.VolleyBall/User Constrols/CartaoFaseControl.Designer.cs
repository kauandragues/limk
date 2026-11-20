namespace Limk.VolleyBall.User_Constrols
{
    partial class CartaoFaseControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        private void InitializeComponent()
        {
            btnSelecionarFase = new Button();
            SuspendLayout();
            //
            // btnSelecionarFase
            //
            btnSelecionarFase.AutoEllipsis = true;
            btnSelecionarFase.BackColor = Color.White;
            btnSelecionarFase.Cursor = Cursors.Hand;
            btnSelecionarFase.Dock = DockStyle.Fill;
            btnSelecionarFase.FlatAppearance.BorderColor = Color.Gainsboro;
            btnSelecionarFase.FlatStyle = FlatStyle.Flat;
            btnSelecionarFase.Font = new Font("Segoe UI", 8F);
            btnSelecionarFase.ForeColor = Color.FromArgb(25, 35, 58);
            btnSelecionarFase.Location = new Point(0, 0);
            btnSelecionarFase.Name = "btnSelecionarFase";
            btnSelecionarFase.Padding = new Padding(8);
            btnSelecionarFase.Size = new Size(188, 78);
            btnSelecionarFase.TabIndex = 0;
            btnSelecionarFase.Text = "FASE 00\nNome da fase\nTipo • 00:00.0";
            btnSelecionarFase.TextAlign = ContentAlignment.MiddleLeft;
            btnSelecionarFase.UseMnemonic = false;
            btnSelecionarFase.UseVisualStyleBackColor = false;
            btnSelecionarFase.Click += btnSelecionarFase_Click;
            //
            // CartaoFaseControl
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnSelecionarFase);
            Name = "CartaoFaseControl";
            Size = new Size(188, 78);
            TabStop = false;
            ResumeLayout(false);
        }

        #endregion

        private Button btnSelecionarFase;
    }
}
