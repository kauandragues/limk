namespace Limk.VolleyBall.User_Constrols
{
    partial class TelaInicial
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
            components = new System.ComponentModel.Container();
            pnlCabecalho = new Panel();
            btnUsuario = new Button();
            lblInicio = new Label();
            menuUsuario = new ContextMenuStrip(components);
            itemConfiguracoes = new ToolStripMenuItem();
            pnlConteudo = new Panel();
            layoutConteudo = new TableLayoutPanel();
            lblBoasVindas = new Label();
            lblDescricao = new Label();
            lblVisaoGeral = new Label();
            layoutEstatisticas = new TableLayoutPanel();
            pnlEquipes = new Panel();
            lblTotalEquipes = new Label();
            lblEquipes = new Label();
            pnlAtletas = new Panel();
            lblTotalAtletas = new Label();
            lblAtletas = new Label();
            pnlTaticas = new Panel();
            lblTotalTaticas = new Label();
            lblTaticas = new Label();
            pnlQuadros = new Panel();
            lblTotalQuadros = new Label();
            lblQuadros = new Label();
            layoutStatus = new TableLayoutPanel();
            lblStatusBanco = new Label();
            btnAtualizar = new Button();
            lblAcessos = new Label();
            layoutAcessos = new TableLayoutPanel();
            btnPrancheta = new Button();
            btnTaticas = new Button();
            btnEquipes = new Button();
            btnAtletas = new Button();
            pnlCabecalho.SuspendLayout();
            menuUsuario.SuspendLayout();
            pnlConteudo.SuspendLayout();
            layoutConteudo.SuspendLayout();
            layoutEstatisticas.SuspendLayout();
            pnlEquipes.SuspendLayout();
            pnlAtletas.SuspendLayout();
            pnlTaticas.SuspendLayout();
            pnlQuadros.SuspendLayout();
            layoutStatus.SuspendLayout();
            layoutAcessos.SuspendLayout();
            SuspendLayout();
            //
            // pnlCabecalho
            //
            pnlCabecalho.BackColor = Color.White;
            pnlCabecalho.Controls.Add(btnUsuario);
            pnlCabecalho.Controls.Add(lblInicio);
            pnlCabecalho.Dock = DockStyle.Top;
            pnlCabecalho.Location = new Point(0, 0);
            pnlCabecalho.Name = "pnlCabecalho";
            pnlCabecalho.Size = new Size(850, 64);
            pnlCabecalho.TabIndex = 0;
            //
            // btnUsuario
            //
            btnUsuario.AutoEllipsis = true;
            btnUsuario.BackColor = Color.FromArgb(245, 240, 255);
            btnUsuario.Cursor = Cursors.Hand;
            btnUsuario.FlatAppearance.BorderSize = 0;
            btnUsuario.FlatStyle = FlatStyle.Flat;
            btnUsuario.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUsuario.ForeColor = Color.FromArgb(92, 48, 154);
            btnUsuario.Location = new Point(24, 14);
            btnUsuario.Name = "btnUsuario";
            btnUsuario.Size = new Size(280, 36);
            btnUsuario.TabIndex = 0;
            btnUsuario.Text = "Usuário: visitante ▾";
            btnUsuario.UseMnemonic = false;
            btnUsuario.UseVisualStyleBackColor = false;
            btnUsuario.Click += btnUsuario_Click;
            //
            // lblInicio
            //
            lblInicio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblInicio.ForeColor = Color.FromArgb(100, 116, 139);
            lblInicio.Location = new Point(714, 14);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(112, 36);
            lblInicio.Text = "Início";
            lblInicio.TextAlign = ContentAlignment.MiddleRight;
            //
            // menuUsuario
            //
            menuUsuario.Items.AddRange(new ToolStripItem[] { itemConfiguracoes });
            menuUsuario.Name = "menuUsuario";
            //
            // itemConfiguracoes
            //
            itemConfiguracoes.Enabled = false;
            itemConfiguracoes.Name = "itemConfiguracoes";
            itemConfiguracoes.Text = "Configurações do usuário (em breve)";
            //
            // pnlConteudo
            //
            pnlConteudo.AutoScroll = true;
            pnlConteudo.Controls.Add(layoutConteudo);
            pnlConteudo.Dock = DockStyle.Fill;
            pnlConteudo.Location = new Point(0, 64);
            pnlConteudo.Name = "pnlConteudo";
            pnlConteudo.Padding = new Padding(24);
            pnlConteudo.Size = new Size(850, 739);
            pnlConteudo.TabIndex = 1;
            //
            // layoutConteudo
            //
            layoutConteudo.AutoSize = true;
            layoutConteudo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutConteudo.ColumnCount = 1;
            layoutConteudo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutConteudo.Controls.Add(lblBoasVindas, 0, 0);
            layoutConteudo.Controls.Add(lblDescricao, 0, 1);
            layoutConteudo.Controls.Add(lblVisaoGeral, 0, 2);
            layoutConteudo.Controls.Add(layoutEstatisticas, 0, 3);
            layoutConteudo.Controls.Add(layoutStatus, 0, 4);
            layoutConteudo.Controls.Add(lblAcessos, 0, 5);
            layoutConteudo.Controls.Add(layoutAcessos, 0, 6);
            layoutConteudo.Dock = DockStyle.Top;
            layoutConteudo.Location = new Point(24, 24);
            layoutConteudo.Margin = new Padding(0);
            layoutConteudo.MinimumSize = new Size(520, 0);
            layoutConteudo.Name = "layoutConteudo";
            layoutConteudo.RowCount = 7;
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 68F));
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 136F));
            layoutConteudo.Size = new Size(802, 496);
            layoutConteudo.TabIndex = 0;
            //
            // lblBoasVindas
            //
            lblBoasVindas.AutoEllipsis = true;
            lblBoasVindas.Dock = DockStyle.Fill;
            lblBoasVindas.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblBoasVindas.Location = new Point(0, 0);
            lblBoasVindas.Margin = new Padding(0);
            lblBoasVindas.Name = "lblBoasVindas";
            lblBoasVindas.Size = new Size(802, 48);
            lblBoasVindas.Text = "Bem-vindo, visitante!";
            lblBoasVindas.TextAlign = ContentAlignment.MiddleLeft;
            lblBoasVindas.UseMnemonic = false;
            //
            // lblDescricao
            //
            lblDescricao.Dock = DockStyle.Fill;
            lblDescricao.ForeColor = Color.FromArgb(100, 116, 139);
            lblDescricao.Margin = new Padding(0, 0, 0, 12);
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Text = "Organize suas equipes, acompanhe seus cadastros e prepare as próximas jogadas.";
            //
            // lblVisaoGeral
            //
            lblVisaoGeral.Dock = DockStyle.Fill;
            lblVisaoGeral.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblVisaoGeral.Margin = new Padding(0);
            lblVisaoGeral.Name = "lblVisaoGeral";
            lblVisaoGeral.Text = "Visão geral do banco";
            lblVisaoGeral.TextAlign = ContentAlignment.MiddleLeft;
            //
            // layoutEstatisticas
            //
            layoutEstatisticas.ColumnCount = 4;
            layoutEstatisticas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            layoutEstatisticas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            layoutEstatisticas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            layoutEstatisticas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            layoutEstatisticas.Controls.Add(pnlEquipes, 0, 0);
            layoutEstatisticas.Controls.Add(pnlAtletas, 1, 0);
            layoutEstatisticas.Controls.Add(pnlTaticas, 2, 0);
            layoutEstatisticas.Controls.Add(pnlQuadros, 3, 0);
            layoutEstatisticas.Dock = DockStyle.Fill;
            layoutEstatisticas.Margin = new Padding(0);
            layoutEstatisticas.Name = "layoutEstatisticas";
            layoutEstatisticas.RowCount = 1;
            layoutEstatisticas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutEstatisticas.TabIndex = 0;
            //
            // pnlEquipes
            //
            pnlEquipes.BackColor = Color.White;
            pnlEquipes.Controls.Add(lblTotalEquipes);
            pnlEquipes.Controls.Add(lblEquipes);
            pnlEquipes.Dock = DockStyle.Fill;
            pnlEquipes.Margin = new Padding(0, 0, 8, 0);
            pnlEquipes.Name = "pnlEquipes";
            pnlEquipes.Padding = new Padding(16, 12, 16, 12);
            //
            // lblTotalEquipes
            //
            lblTotalEquipes.AutoEllipsis = true;
            lblTotalEquipes.Dock = DockStyle.Fill;
            lblTotalEquipes.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalEquipes.ForeColor = Color.FromArgb(124, 58, 237);
            lblTotalEquipes.Name = "lblTotalEquipes";
            lblTotalEquipes.Text = "—";
            lblTotalEquipes.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblEquipes
            //
            lblEquipes.Dock = DockStyle.Top;
            lblEquipes.ForeColor = Color.FromArgb(100, 116, 139);
            lblEquipes.Height = 28;
            lblEquipes.Name = "lblEquipes";
            lblEquipes.Text = "Equipes";
            //
            // pnlAtletas
            //
            pnlAtletas.BackColor = Color.White;
            pnlAtletas.Controls.Add(lblTotalAtletas);
            pnlAtletas.Controls.Add(lblAtletas);
            pnlAtletas.Dock = DockStyle.Fill;
            pnlAtletas.Margin = new Padding(4, 0, 4, 0);
            pnlAtletas.Name = "pnlAtletas";
            pnlAtletas.Padding = new Padding(16, 12, 16, 12);
            //
            // lblTotalAtletas
            //
            lblTotalAtletas.AutoEllipsis = true;
            lblTotalAtletas.Dock = DockStyle.Fill;
            lblTotalAtletas.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalAtletas.ForeColor = Color.FromArgb(124, 58, 237);
            lblTotalAtletas.Name = "lblTotalAtletas";
            lblTotalAtletas.Text = "—";
            lblTotalAtletas.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblAtletas
            //
            lblAtletas.Dock = DockStyle.Top;
            lblAtletas.ForeColor = Color.FromArgb(100, 116, 139);
            lblAtletas.Height = 28;
            lblAtletas.Name = "lblAtletas";
            lblAtletas.Text = "Atletas";
            //
            // pnlTaticas
            //
            pnlTaticas.BackColor = Color.White;
            pnlTaticas.Controls.Add(lblTotalTaticas);
            pnlTaticas.Controls.Add(lblTaticas);
            pnlTaticas.Dock = DockStyle.Fill;
            pnlTaticas.Margin = new Padding(4, 0, 4, 0);
            pnlTaticas.Name = "pnlTaticas";
            pnlTaticas.Padding = new Padding(16, 12, 16, 12);
            //
            // lblTotalTaticas
            //
            lblTotalTaticas.AutoEllipsis = true;
            lblTotalTaticas.Dock = DockStyle.Fill;
            lblTotalTaticas.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalTaticas.ForeColor = Color.FromArgb(124, 58, 237);
            lblTotalTaticas.Name = "lblTotalTaticas";
            lblTotalTaticas.Text = "—";
            lblTotalTaticas.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblTaticas
            //
            lblTaticas.Dock = DockStyle.Top;
            lblTaticas.ForeColor = Color.FromArgb(100, 116, 139);
            lblTaticas.Height = 28;
            lblTaticas.Name = "lblTaticas";
            lblTaticas.Text = "Táticas";
            //
            // pnlQuadros
            //
            pnlQuadros.BackColor = Color.White;
            pnlQuadros.Controls.Add(lblTotalQuadros);
            pnlQuadros.Controls.Add(lblQuadros);
            pnlQuadros.Dock = DockStyle.Fill;
            pnlQuadros.Margin = new Padding(8, 0, 0, 0);
            pnlQuadros.Name = "pnlQuadros";
            pnlQuadros.Padding = new Padding(16, 12, 16, 12);
            //
            // lblTotalQuadros
            //
            lblTotalQuadros.AutoEllipsis = true;
            lblTotalQuadros.Dock = DockStyle.Fill;
            lblTotalQuadros.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalQuadros.ForeColor = Color.FromArgb(124, 58, 237);
            lblTotalQuadros.Name = "lblTotalQuadros";
            lblTotalQuadros.Text = "—";
            lblTotalQuadros.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblQuadros
            //
            lblQuadros.Dock = DockStyle.Top;
            lblQuadros.ForeColor = Color.FromArgb(100, 116, 139);
            lblQuadros.Height = 28;
            lblQuadros.Name = "lblQuadros";
            lblQuadros.Text = "Quadros";
            //
            // layoutStatus
            //
            layoutStatus.ColumnCount = 2;
            layoutStatus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutStatus.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 124F));
            layoutStatus.Controls.Add(lblStatusBanco, 0, 0);
            layoutStatus.Controls.Add(btnAtualizar, 1, 0);
            layoutStatus.Dock = DockStyle.Fill;
            layoutStatus.Margin = new Padding(0, 8, 0, 8);
            layoutStatus.Name = "layoutStatus";
            layoutStatus.RowCount = 1;
            layoutStatus.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutStatus.TabIndex = 1;
            //
            // lblStatusBanco
            //
            lblStatusBanco.Dock = DockStyle.Fill;
            lblStatusBanco.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatusBanco.Margin = new Padding(0, 0, 12, 0);
            lblStatusBanco.Name = "lblStatusBanco";
            lblStatusBanco.Text = "Os totais são consultados ao abrir esta tela.";
            lblStatusBanco.TextAlign = ContentAlignment.MiddleLeft;
            //
            // btnAtualizar
            //
            btnAtualizar.Anchor = AnchorStyles.Right;
            btnAtualizar.BackColor = Color.White;
            btnAtualizar.Cursor = Cursors.Hand;
            btnAtualizar.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnAtualizar.FlatStyle = FlatStyle.Flat;
            btnAtualizar.ForeColor = Color.FromArgb(92, 48, 154);
            btnAtualizar.Margin = new Padding(0);
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.Size = new Size(124, 36);
            btnAtualizar.TabIndex = 0;
            btnAtualizar.Text = "Atualizar";
            btnAtualizar.UseVisualStyleBackColor = false;
            btnAtualizar.Click += btnAtualizar_Click;
            //
            // lblAcessos
            //
            lblAcessos.Dock = DockStyle.Fill;
            lblAcessos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblAcessos.Margin = new Padding(0);
            lblAcessos.Name = "lblAcessos";
            lblAcessos.Text = "Acesso rápido";
            lblAcessos.TextAlign = ContentAlignment.MiddleLeft;
            //
            // layoutAcessos
            //
            layoutAcessos.ColumnCount = 2;
            layoutAcessos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutAcessos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutAcessos.Controls.Add(btnPrancheta, 0, 0);
            layoutAcessos.Controls.Add(btnTaticas, 1, 0);
            layoutAcessos.Controls.Add(btnEquipes, 0, 1);
            layoutAcessos.Controls.Add(btnAtletas, 1, 1);
            layoutAcessos.Dock = DockStyle.Fill;
            layoutAcessos.Margin = new Padding(0);
            layoutAcessos.Name = "layoutAcessos";
            layoutAcessos.RowCount = 2;
            layoutAcessos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutAcessos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutAcessos.TabIndex = 2;
            //
            // btnPrancheta
            //
            btnPrancheta.BackColor = Color.FromArgb(124, 58, 237);
            btnPrancheta.Cursor = Cursors.Hand;
            btnPrancheta.Dock = DockStyle.Fill;
            btnPrancheta.FlatAppearance.BorderSize = 0;
            btnPrancheta.FlatStyle = FlatStyle.Flat;
            btnPrancheta.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPrancheta.ForeColor = Color.White;
            btnPrancheta.Margin = new Padding(0, 0, 6, 12);
            btnPrancheta.Name = "btnPrancheta";
            btnPrancheta.TabIndex = 0;
            btnPrancheta.Text = "Abrir prancheta tática";
            btnPrancheta.UseVisualStyleBackColor = false;
            btnPrancheta.Click += btnPrancheta_Click;
            //
            // btnTaticas
            //
            btnTaticas.BackColor = Color.White;
            btnTaticas.Cursor = Cursors.Hand;
            btnTaticas.Dock = DockStyle.Fill;
            btnTaticas.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnTaticas.FlatStyle = FlatStyle.Flat;
            btnTaticas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTaticas.ForeColor = Color.FromArgb(92, 48, 154);
            btnTaticas.Margin = new Padding(6, 0, 0, 12);
            btnTaticas.Name = "btnTaticas";
            btnTaticas.TabIndex = 1;
            btnTaticas.Text = "Consultar táticas";
            btnTaticas.UseVisualStyleBackColor = false;
            btnTaticas.Click += btnTaticas_Click;
            //
            // btnEquipes
            //
            btnEquipes.BackColor = Color.White;
            btnEquipes.Cursor = Cursors.Hand;
            btnEquipes.Dock = DockStyle.Fill;
            btnEquipes.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnEquipes.FlatStyle = FlatStyle.Flat;
            btnEquipes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEquipes.ForeColor = Color.FromArgb(92, 48, 154);
            btnEquipes.Margin = new Padding(0, 0, 6, 12);
            btnEquipes.Name = "btnEquipes";
            btnEquipes.TabIndex = 2;
            btnEquipes.Text = "Gerenciar equipes";
            btnEquipes.UseVisualStyleBackColor = false;
            btnEquipes.Click += btnEquipes_Click;
            //
            // btnAtletas
            //
            btnAtletas.BackColor = Color.White;
            btnAtletas.Cursor = Cursors.Hand;
            btnAtletas.Dock = DockStyle.Fill;
            btnAtletas.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnAtletas.FlatStyle = FlatStyle.Flat;
            btnAtletas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAtletas.ForeColor = Color.FromArgb(92, 48, 154);
            btnAtletas.Margin = new Padding(6, 0, 0, 12);
            btnAtletas.Name = "btnAtletas";
            btnAtletas.TabIndex = 3;
            btnAtletas.Text = "Consultar atletas";
            btnAtletas.UseVisualStyleBackColor = false;
            btnAtletas.Click += btnAtletas_Click;
            //
            // TelaInicial
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 254);
            Controls.Add(pnlConteudo);
            Controls.Add(pnlCabecalho);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(30, 27, 45);
            Name = "TelaInicial";
            Size = new Size(850, 803);
            Load += TelaInicial_Load;
            pnlCabecalho.ResumeLayout(false);
            menuUsuario.ResumeLayout(false);
            pnlConteudo.ResumeLayout(false);
            pnlConteudo.PerformLayout();
            layoutConteudo.ResumeLayout(false);
            layoutEstatisticas.ResumeLayout(false);
            pnlEquipes.ResumeLayout(false);
            pnlAtletas.ResumeLayout(false);
            pnlTaticas.ResumeLayout(false);
            pnlQuadros.ResumeLayout(false);
            layoutStatus.ResumeLayout(false);
            layoutAcessos.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCabecalho;
        private Button btnUsuario;
        private Label lblInicio;
        private ContextMenuStrip menuUsuario;
        private ToolStripMenuItem itemConfiguracoes;
        private Panel pnlConteudo;
        private TableLayoutPanel layoutConteudo;
        private Label lblBoasVindas;
        private Label lblDescricao;
        private Label lblVisaoGeral;
        private TableLayoutPanel layoutEstatisticas;
        private Panel pnlEquipes;
        private Label lblTotalEquipes;
        private Label lblEquipes;
        private Panel pnlAtletas;
        private Label lblTotalAtletas;
        private Label lblAtletas;
        private Panel pnlTaticas;
        private Label lblTotalTaticas;
        private Label lblTaticas;
        private Panel pnlQuadros;
        private Label lblTotalQuadros;
        private Label lblQuadros;
        private TableLayoutPanel layoutStatus;
        private Label lblStatusBanco;
        private Button btnAtualizar;
        private Label lblAcessos;
        private TableLayoutPanel layoutAcessos;
        private Button btnPrancheta;
        private Button btnTaticas;
        private Button btnEquipes;
        private Button btnAtletas;
    }
}
