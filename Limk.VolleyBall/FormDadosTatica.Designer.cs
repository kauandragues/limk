namespace Limk.VolleyBall
{
    partial class FormDadosTatica
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            layoutDados = new TableLayoutPanel();
            lblNome = new Label();
            lblEquipe = new Label();
            lblTipo = new Label();
            lblSistema = new Label();
            lblDescricao = new Label();
            lblFase = new Label();
            lblNomeFase = new Label();
            lblDuracao = new Label();
            lblRotacao = new Label();
            txtNome = new TextBox();
            cmbEquipe = new ComboBox();
            cmbTipo = new ComboBox();
            cmbSistema = new ComboBox();
            txtDescricao = new TextBox();
            txtFase = new TextBox();
            numDuracao = new NumericUpDown();
            cmbRotacao = new ComboBox();
            lblMensagem = new Label();
            pnlBotoes = new FlowLayoutPanel();
            btnConfirmar = new Button();
            btnCancelar = new Button();
            layoutDados.SuspendLayout();
            pnlBotoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDuracao).BeginInit();
            SuspendLayout();
            // layoutDados
            layoutDados.Name = "layoutDados";
            layoutDados.ColumnCount = 2;
            layoutDados.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
            layoutDados.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDados.Dock = DockStyle.Fill;
            layoutDados.Padding = new Padding(20);
            layoutDados.RowCount = 12;
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            layoutDados.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // lblNome
            lblNome.Name = "lblNome";
            lblNome.Text = "Nome da tática";
            lblNome.Dock = DockStyle.Fill;
            lblNome.TextAlign = ContentAlignment.MiddleLeft;
            lblNome.Margin = new Padding(0, 0, 8, 6);
            // lblEquipe
            lblEquipe.Name = "lblEquipe";
            lblEquipe.Text = "Equipe";
            lblEquipe.Dock = DockStyle.Fill;
            lblEquipe.TextAlign = ContentAlignment.MiddleLeft;
            lblEquipe.Margin = new Padding(0, 0, 8, 6);
            // lblTipo
            lblTipo.Name = "lblTipo";
            lblTipo.Text = "Tipo de tática";
            lblTipo.Dock = DockStyle.Fill;
            lblTipo.TextAlign = ContentAlignment.MiddleLeft;
            lblTipo.Margin = new Padding(0, 0, 8, 6);
            // lblSistema
            lblSistema.Name = "lblSistema";
            lblSistema.Text = "Sistema de jogo";
            lblSistema.Dock = DockStyle.Fill;
            lblSistema.TextAlign = ContentAlignment.MiddleLeft;
            lblSistema.Margin = new Padding(0, 0, 8, 6);
            // lblDescricao
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Text = "Descrição";
            lblDescricao.Dock = DockStyle.Fill;
            lblDescricao.TextAlign = ContentAlignment.MiddleLeft;
            lblDescricao.Margin = new Padding(0, 0, 8, 6);
            // lblFase
            lblFase.Name = "lblFase";
            lblFase.Text = "Fase selecionada";
            lblFase.Dock = DockStyle.Fill;
            lblFase.TextAlign = ContentAlignment.MiddleLeft;
            lblFase.Margin = new Padding(0, 0, 8, 6);
            // lblNomeFase
            lblNomeFase.Name = "lblNomeFase";
            lblNomeFase.Text = "Nome da fase";
            lblNomeFase.Dock = DockStyle.Fill;
            lblNomeFase.TextAlign = ContentAlignment.MiddleLeft;
            lblNomeFase.Margin = new Padding(0, 0, 8, 6);
            // lblDuracao
            lblDuracao.Name = "lblDuracao";
            lblDuracao.Text = "Duração (segundos)";
            lblDuracao.Dock = DockStyle.Fill;
            lblDuracao.TextAlign = ContentAlignment.MiddleLeft;
            lblDuracao.Margin = new Padding(0, 0, 8, 6);
            // lblRotacao
            lblRotacao.Name = "lblRotacao";
            lblRotacao.Text = "Rotação";
            lblRotacao.Dock = DockStyle.Fill;
            lblRotacao.TextAlign = ContentAlignment.MiddleLeft;
            lblRotacao.Margin = new Padding(0, 0, 8, 6);
            // txtNome
            txtNome.Name = "txtNome";
            txtNome.Dock = DockStyle.Fill;
            txtNome.MaxLength = 100;
            txtNome.Margin = new Padding(0, 3, 0, 6);
            // cmbEquipe
            cmbEquipe.Name = "cmbEquipe";
            cmbEquipe.Dock = DockStyle.Fill;
            cmbEquipe.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEquipe.Margin = new Padding(0, 3, 0, 6);
            // cmbTipo
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Dock = DockStyle.Fill;
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Margin = new Padding(0, 3, 0, 6);
            // cmbSistema
            cmbSistema.Name = "cmbSistema";
            cmbSistema.Dock = DockStyle.Fill;
            cmbSistema.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSistema.Margin = new Padding(0, 3, 0, 6);
            // txtDescricao
            txtDescricao.Name = "txtDescricao";
            txtDescricao.Dock = DockStyle.Fill;
            txtDescricao.Multiline = true;
            txtDescricao.ScrollBars = ScrollBars.Vertical;
            txtDescricao.MaxLength = 65535;
            txtDescricao.Margin = new Padding(0, 3, 0, 6);
            // txtFase
            txtFase.Name = "txtFase";
            txtFase.Dock = DockStyle.Fill;
            txtFase.MaxLength = 100;
            txtFase.Margin = new Padding(0, 3, 0, 6);
            // numDuracao
            numDuracao.Name = "numDuracao";
            numDuracao.Dock = DockStyle.Fill;
            numDuracao.DecimalPlaces = 3;
            numDuracao.Minimum = 0.001M;
            numDuracao.Maximum = 2147483.647M;
            numDuracao.Increment = 0.1M;
            numDuracao.Value = 1M;
            numDuracao.Margin = new Padding(0, 3, 0, 6);
            // cmbRotacao
            cmbRotacao.Name = "cmbRotacao";
            cmbRotacao.Dock = DockStyle.Fill;
            cmbRotacao.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRotacao.Margin = new Padding(0, 3, 0, 6);
            // lblMensagem
            lblMensagem.Name = "lblMensagem";
            lblMensagem.Dock = DockStyle.Fill;
            lblMensagem.ForeColor = Color.FromArgb(180, 40, 60);
            lblMensagem.Margin = new Padding(0);
            // pnlBotoes
            pnlBotoes.Name = "pnlBotoes";
            pnlBotoes.Dock = DockStyle.Fill;
            pnlBotoes.FlowDirection = FlowDirection.RightToLeft;
            pnlBotoes.Margin = new Padding(0);
            // btnConfirmar
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Text = "Aplicar";
            btnConfirmar.BackColor = Color.FromArgb(124, 58, 237);
            btnConfirmar.ForeColor = Color.White;
            btnConfirmar.FlatStyle = FlatStyle.Flat;
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.Size = new Size(120, 36);
            btnConfirmar.TabIndex = 0;
            btnConfirmar.Click += btnConfirmar_Click;
            // btnCancelar
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Text = "Cancelar";
            btnCancelar.Size = new Size(120, 36);
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.TabIndex = 1;
            layoutDados.Controls.Add(lblNome, 0, 0);
            layoutDados.Controls.Add(txtNome, 1, 0);
            layoutDados.Controls.Add(lblEquipe, 0, 1);
            layoutDados.Controls.Add(cmbEquipe, 1, 1);
            layoutDados.Controls.Add(lblTipo, 0, 2);
            layoutDados.Controls.Add(cmbTipo, 1, 2);
            layoutDados.Controls.Add(lblSistema, 0, 3);
            layoutDados.Controls.Add(cmbSistema, 1, 3);
            layoutDados.Controls.Add(lblDescricao, 0, 4);
            layoutDados.Controls.Add(txtDescricao, 1, 4);
            layoutDados.Controls.Add(lblFase, 0, 5);
            layoutDados.Controls.Add(lblNomeFase, 0, 6);
            layoutDados.Controls.Add(txtFase, 1, 6);
            layoutDados.Controls.Add(lblDuracao, 0, 7);
            layoutDados.Controls.Add(numDuracao, 1, 7);
            layoutDados.Controls.Add(lblRotacao, 0, 8);
            layoutDados.Controls.Add(cmbRotacao, 1, 8);
            layoutDados.Controls.Add(lblMensagem, 0, 9);
            layoutDados.Controls.Add(pnlBotoes, 0, 10);
            layoutDados.SetColumnSpan(lblFase, 2);
            layoutDados.SetColumnSpan(lblMensagem, 2);
            layoutDados.SetColumnSpan(pnlBotoes, 2);
            pnlBotoes.Controls.Add(btnConfirmar);
            pnlBotoes.Controls.Add(btnCancelar);
            lblFase.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            AcceptButton = btnConfirmar;
            CancelButton = btnCancelar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(620, 620);
            Controls.Add(layoutDados);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormDadosTatica";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Dados da tática e da fase";
            layoutDados.ResumeLayout(false);
            layoutDados.PerformLayout();
            pnlBotoes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numDuracao).EndInit();
            ResumeLayout(false);
        }
        private TableLayoutPanel layoutDados;
        private Label lblNome;
        private Label lblEquipe;
        private Label lblTipo;
        private Label lblSistema;
        private Label lblDescricao;
        private Label lblFase;
        private Label lblNomeFase;
        private Label lblDuracao;
        private Label lblRotacao;
        private TextBox txtNome;
        private ComboBox cmbEquipe;
        private ComboBox cmbTipo;
        private ComboBox cmbSistema;
        private TextBox txtDescricao;
        private TextBox txtFase;
        private NumericUpDown numDuracao;
        private ComboBox cmbRotacao;
        private Label lblMensagem;
        private FlowLayoutPanel pnlBotoes;
        private Button btnConfirmar;
        private Button btnCancelar;
    }
}
