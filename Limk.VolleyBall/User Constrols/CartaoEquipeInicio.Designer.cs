namespace Limk.VolleyBall.User_Constrols
{
    partial class CartaoEquipeInicio
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
            layoutCartao = new TableLayoutPanel();
            lblInicial = new Label();
            lblNome = new Label();
            lblResumo = new Label();
            layoutCartao.SuspendLayout();
            SuspendLayout();
            //
            // layoutCartao
            //
            layoutCartao.ColumnCount = 2;
            layoutCartao.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            layoutCartao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCartao.Controls.Add(lblInicial, 0, 0);
            layoutCartao.Controls.Add(lblNome, 1, 0);
            layoutCartao.Controls.Add(lblResumo, 1, 1);
            layoutCartao.Dock = DockStyle.Fill;
            layoutCartao.Name = "layoutCartao";
            layoutCartao.Padding = new Padding(10);
            layoutCartao.RowCount = 2;
            layoutCartao.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutCartao.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            layoutCartao.SetRowSpan(lblInicial, 2);
            layoutCartao.TabIndex = 0;
            //
            // lblInicial
            //
            lblInicial.Anchor = AnchorStyles.Left;
            lblInicial.BackColor = Color.FromArgb(237, 228, 255);
            lblInicial.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblInicial.ForeColor = Color.FromArgb(92, 48, 154);
            lblInicial.Margin = new Padding(0, 0, 8, 0);
            lblInicial.Name = "lblInicial";
            lblInicial.Size = new Size(32, 36);
            lblInicial.Text = "E";
            lblInicial.TextAlign = ContentAlignment.MiddleCenter;
            lblInicial.UseMnemonic = false;
            //
            // lblNome
            //
            lblNome.AutoEllipsis = true;
            lblNome.Dock = DockStyle.Fill;
            lblNome.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNome.Margin = new Padding(0);
            lblNome.Name = "lblNome";
            lblNome.Text = "Nome da equipe";
            lblNome.TextAlign = ContentAlignment.MiddleLeft;
            lblNome.UseMnemonic = false;
            //
            // lblResumo
            //
            lblResumo.AutoEllipsis = true;
            lblResumo.Dock = DockStyle.Fill;
            lblResumo.Font = new Font("Segoe UI", 8F);
            lblResumo.ForeColor = Color.FromArgb(100, 116, 139);
            lblResumo.Margin = new Padding(0);
            lblResumo.Name = "lblResumo";
            lblResumo.Text = "Atletas e táticas";
            lblResumo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // CartaoEquipeInicio
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 254);
            Controls.Add(layoutCartao);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(30, 27, 45);
            Margin = new Padding(0, 0, 0, 10);
            Name = "CartaoEquipeInicio";
            Size = new Size(270, 84);
            layoutCartao.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel layoutCartao;
        private Label lblInicial;
        private Label lblNome;
        private Label lblResumo;
    }
}
