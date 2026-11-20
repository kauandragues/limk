namespace Limk.VolleyBall.User_Constrols
{
    partial class CartaoTaticaInicio
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
            pnlDestaque = new Panel();
            layoutCartao = new TableLayoutPanel();
            lblEquipe = new Label();
            lblNome = new Label();
            lblDescricao = new Label();
            lblQuadros = new Label();
            layoutRodape = new TableLayoutPanel();
            btnAbrirTatica = new Button();
            layoutCartao.SuspendLayout();
            SuspendLayout();
            //
            // pnlDestaque
            //
            pnlDestaque.BackColor = Color.FromArgb(124, 58, 237);
            pnlDestaque.Dock = DockStyle.Left;
            pnlDestaque.Name = "pnlDestaque";
            pnlDestaque.Size = new Size(3, 160);
            //
            // layoutCartao
            //
            layoutCartao.ColumnCount = 1;
            layoutCartao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCartao.Controls.Add(lblEquipe, 0, 0);
            layoutCartao.Controls.Add(lblNome, 0, 1);
            layoutCartao.Controls.Add(lblDescricao, 0, 2);
            layoutCartao.Controls.Add(layoutRodape, 0, 3);
            layoutCartao.Dock = DockStyle.Fill;
            layoutCartao.Name = "layoutCartao";
            layoutCartao.Padding = new Padding(16, 12, 16, 12);
            layoutCartao.RowCount = 4;
            layoutCartao.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            layoutCartao.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutCartao.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutCartao.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutCartao.TabIndex = 0;
            //
            // layoutRodape
            //
            layoutRodape.ColumnCount = 2;
            layoutRodape.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutRodape.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 84F));
            layoutRodape.RowCount = 1;
            layoutRodape.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutRodape.Controls.Add(lblQuadros, 0, 0);
            layoutRodape.Controls.Add(btnAbrirTatica, 1, 0);
            layoutRodape.Dock = DockStyle.Fill;
            layoutRodape.Margin = new Padding(0);
            layoutRodape.Name = "layoutRodape";
            //
            // btnAbrirTatica
            //
            btnAbrirTatica.BackColor = Color.FromArgb(124, 58, 237);
            btnAbrirTatica.Dock = DockStyle.Fill;
            btnAbrirTatica.FlatStyle = FlatStyle.Flat;
            btnAbrirTatica.FlatAppearance.BorderSize = 0;
            btnAbrirTatica.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnAbrirTatica.ForeColor = Color.White;
            btnAbrirTatica.Margin = new Padding(0);
            btnAbrirTatica.Name = "btnAbrirTatica";
            btnAbrirTatica.Text = "Abrir";
            btnAbrirTatica.UseVisualStyleBackColor = false;
            //
            // lblEquipe
            //
            lblEquipe.AutoEllipsis = true;
            lblEquipe.Dock = DockStyle.Fill;
            lblEquipe.Font = new Font("Segoe UI", 8.5F);
            lblEquipe.ForeColor = Color.FromArgb(92, 48, 154);
            lblEquipe.Margin = new Padding(0);
            lblEquipe.Name = "lblEquipe";
            lblEquipe.Text = "Equipe";
            lblEquipe.UseMnemonic = false;
            //
            // lblNome
            //
            lblNome.AutoEllipsis = true;
            lblNome.Dock = DockStyle.Fill;
            lblNome.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblNome.Margin = new Padding(0);
            lblNome.Name = "lblNome";
            lblNome.Text = "Nome da tática";
            lblNome.UseMnemonic = false;
            //
            // lblDescricao
            //
            lblDescricao.AutoEllipsis = true;
            lblDescricao.Dock = DockStyle.Fill;
            lblDescricao.ForeColor = Color.FromArgb(100, 116, 139);
            lblDescricao.Margin = new Padding(0, 0, 0, 6);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Text = "Descrição da tática.";
            lblDescricao.UseMnemonic = false;
            //
            // lblQuadros
            //
            lblQuadros.Dock = DockStyle.Fill;
            lblQuadros.Font = new Font("Segoe UI", 8.5F);
            lblQuadros.ForeColor = Color.FromArgb(92, 48, 154);
            lblQuadros.Margin = new Padding(0);
            lblQuadros.Name = "lblQuadros";
            lblQuadros.Text = "Quadros da tática";
            lblQuadros.TextAlign = ContentAlignment.MiddleLeft;
            //
            // CartaoTaticaInicio
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(layoutCartao);
            Controls.Add(pnlDestaque);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(30, 27, 45);
            Margin = new Padding(0, 0, 0, 12);
            Name = "CartaoTaticaInicio";
            Size = new Size(500, 160);
            layoutCartao.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlDestaque;
        private TableLayoutPanel layoutRodape;
        private Button btnAbrirTatica;
        private TableLayoutPanel layoutCartao;
        private Label lblEquipe;
        private Label lblNome;
        private Label lblDescricao;
        private Label lblQuadros;
    }
}
