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
            layoutHero = new TableLayoutPanel();
            layoutApresentacao = new TableLayoutPanel();
            lblBoasVindas = new Label();
            lblTituloProjeto = new Label();
            lblDescricao = new Label();
            layoutAcessos = new TableLayoutPanel();
            btnPrancheta = new Button();
            btnTaticas = new Button();
            btnEquipes = new Button();
            btnAtletas = new Button();
            layoutStatus = new TableLayoutPanel();
            lblStatusBanco = new Label();
            btnAtualizar = new Button();
            layoutPainel = new TableLayoutPanel();
            layoutSecaoTaticas = new TableLayoutPanel();
            layoutCabecalhoTaticas = new TableLayoutPanel();
            lblTituloTaticas = new Label();
            btnVerTaticas = new Button();
            lblEstadoTaticas = new Label();
            layoutListaTaticas = new TableLayoutPanel();
            layoutLateral = new TableLayoutPanel();
            layoutSecaoEquipes = new TableLayoutPanel();
            lblTituloEquipes = new Label();
            lblEstadoEquipes = new Label();
            layoutListaEquipes = new TableLayoutPanel();
            btnVerEquipes = new Button();
            layoutResumo = new TableLayoutPanel();
            lblVisaoGeral = new Label();
            layoutEstatisticas = new TableLayoutPanel();
            pnlTaticas = new Panel();
            lblTotalTaticas = new Label();
            lblTaticas = new Label();
            pnlAtletas = new Panel();
            lblTotalAtletas = new Label();
            lblAtletas = new Label();
            pnlEquipes = new Panel();
            lblTotalEquipes = new Label();
            lblEquipes = new Label();
            pnlQuadros = new Panel();
            lblTotalQuadros = new Label();
            lblQuadros = new Label();
            pnlCabecalho.SuspendLayout();
            menuUsuario.SuspendLayout();
            pnlConteudo.SuspendLayout();
            layoutConteudo.SuspendLayout();
            layoutHero.SuspendLayout();
            layoutApresentacao.SuspendLayout();
            layoutAcessos.SuspendLayout();
            layoutStatus.SuspendLayout();
            layoutPainel.SuspendLayout();
            layoutSecaoTaticas.SuspendLayout();
            layoutCabecalhoTaticas.SuspendLayout();
            layoutListaTaticas.SuspendLayout();
            layoutLateral.SuspendLayout();
            layoutSecaoEquipes.SuspendLayout();
            layoutListaEquipes.SuspendLayout();
            layoutResumo.SuspendLayout();
            layoutEstatisticas.SuspendLayout();
            pnlTaticas.SuspendLayout();
            pnlAtletas.SuspendLayout();
            pnlEquipes.SuspendLayout();
            pnlQuadros.SuspendLayout();
            SuspendLayout();
            //
            // pnlCabecalho
            //
            pnlCabecalho.Name = "pnlCabecalho";
            pnlCabecalho.BackColor = Color.White;
            pnlCabecalho.Controls.Add(btnUsuario);
            pnlCabecalho.Controls.Add(lblInicio);
            pnlCabecalho.Dock = DockStyle.Top;
            pnlCabecalho.Size = new Size(850, 64);
            pnlCabecalho.TabIndex = 0;
            //
            // btnUsuario
            //
            btnUsuario.Name = "btnUsuario";
            btnUsuario.Text = "Usuário: visitante ▾";
            btnUsuario.BackColor = Color.White;
            btnUsuario.ForeColor = Color.FromArgb(92, 48, 154);
            btnUsuario.Cursor = Cursors.Hand;
            btnUsuario.FlatStyle = FlatStyle.Flat;
            btnUsuario.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnUsuario.UseVisualStyleBackColor = false;
            btnUsuario.AutoEllipsis = true;
            btnUsuario.BackColor = Color.FromArgb(245, 240, 255);
            btnUsuario.FlatAppearance.BorderSize = 0;
            btnUsuario.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUsuario.Location = new Point(24, 14);
            btnUsuario.Size = new Size(280, 36);
            btnUsuario.TabIndex = 0;
            btnUsuario.UseMnemonic = false;
            btnUsuario.Click += btnUsuario_Click;
            //
            // lblInicio
            //
            lblInicio.Name = "lblInicio";
            lblInicio.Text = "Início";
            lblInicio.Dock = DockStyle.Fill;
            lblInicio.Margin = new Padding(0);
            lblInicio.Dock = DockStyle.None;
            lblInicio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblInicio.ForeColor = Color.FromArgb(100, 116, 139);
            lblInicio.Location = new Point(714, 14);
            lblInicio.Size = new Size(112, 36);
            lblInicio.TextAlign = ContentAlignment.MiddleRight;
            //
            // menuUsuario
            //
            menuUsuario.Name = "menuUsuario";
            menuUsuario.Items.AddRange(new ToolStripItem[] { itemConfiguracoes });
            //
            // itemConfiguracoes
            //
            itemConfiguracoes.Name = "itemConfiguracoes";
            itemConfiguracoes.Enabled = false;
            itemConfiguracoes.Text = "Configurações do usuário (em breve)";
            //
            // pnlConteudo
            //
            pnlConteudo.Name = "pnlConteudo";
            pnlConteudo.AutoScroll = true;
            pnlConteudo.Controls.Add(layoutConteudo);
            pnlConteudo.Dock = DockStyle.Fill;
            pnlConteudo.Location = new Point(0, 64);
            pnlConteudo.Padding = new Padding(24);
            pnlConteudo.Size = new Size(850, 739);
            pnlConteudo.TabIndex = 1;
            //
            // layoutConteudo
            //
            layoutConteudo.Name = "layoutConteudo";
            layoutConteudo.ColumnCount = 1;
            layoutConteudo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutConteudo.Controls.Add(layoutHero, 0, 0);
            layoutConteudo.Controls.Add(layoutStatus, 0, 1);
            layoutConteudo.Controls.Add(layoutPainel, 0, 2);
            layoutConteudo.RowCount = 3;
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0F));
            layoutConteudo.AutoSize = true;
            layoutConteudo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutConteudo.Dock = DockStyle.Top;
            layoutConteudo.Location = new Point(24, 24);
            layoutConteudo.Margin = new Padding(0);
            layoutConteudo.MinimumSize = new Size(660, 0);
            layoutConteudo.Size = new Size(802, 800);
            layoutConteudo.TabIndex = 0;
            //
            // layoutHero
            //
            layoutHero.Name = "layoutHero";
            layoutHero.ColumnCount = 2;
            layoutHero.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            layoutHero.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            layoutHero.Controls.Add(layoutApresentacao, 0, 0);
            layoutHero.Controls.Add(layoutAcessos, 1, 0);
            layoutHero.RowCount = 1;
            layoutHero.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutHero.BackColor = Color.White;
            layoutHero.Dock = DockStyle.Fill;
            layoutHero.Margin = new Padding(0);
            layoutHero.Padding = new Padding(20);
            layoutHero.TabIndex = 0;
            //
            // layoutApresentacao
            //
            layoutApresentacao.Name = "layoutApresentacao";
            layoutApresentacao.ColumnCount = 1;
            layoutApresentacao.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutApresentacao.Controls.Add(lblBoasVindas, 0, 0);
            layoutApresentacao.Controls.Add(lblTituloProjeto, 0, 1);
            layoutApresentacao.Controls.Add(lblDescricao, 0, 2);
            layoutApresentacao.RowCount = 3;
            layoutApresentacao.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            layoutApresentacao.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            layoutApresentacao.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutApresentacao.Dock = DockStyle.Fill;
            layoutApresentacao.Margin = new Padding(0);
            //
            // lblBoasVindas
            //
            lblBoasVindas.Name = "lblBoasVindas";
            lblBoasVindas.Text = "Bem-vindo, visitante!";
            lblBoasVindas.Dock = DockStyle.Fill;
            lblBoasVindas.Margin = new Padding(0);
            lblBoasVindas.AutoEllipsis = true;
            lblBoasVindas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBoasVindas.ForeColor = Color.FromArgb(92, 48, 154);
            lblBoasVindas.TextAlign = ContentAlignment.MiddleLeft;
            lblBoasVindas.UseMnemonic = false;
            //
            // lblTituloProjeto
            //
            lblTituloProjeto.Name = "lblTituloProjeto";
            lblTituloProjeto.Text = "LIMK Volleyball Hub";
            lblTituloProjeto.Dock = DockStyle.Fill;
            lblTituloProjeto.Margin = new Padding(0);
            lblTituloProjeto.AutoEllipsis = true;
            lblTituloProjeto.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTituloProjeto.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblDescricao
            //
            lblDescricao.Name = "lblDescricao";
            lblDescricao.Text = "Crie táticas de vôlei, organize suas equipes e planeje cada momento da jogada.";
            lblDescricao.Dock = DockStyle.Fill;
            lblDescricao.Margin = new Padding(0);
            lblDescricao.ForeColor = Color.FromArgb(100, 116, 139);
            lblDescricao.Margin = new Padding(0, 8, 8, 0);
            //
            // layoutAcessos
            //
            layoutAcessos.Name = "layoutAcessos";
            layoutAcessos.ColumnCount = 2;
            layoutAcessos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutAcessos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutAcessos.Controls.Add(btnPrancheta, 0, 0);
            layoutAcessos.Controls.Add(btnTaticas, 1, 0);
            layoutAcessos.Controls.Add(btnEquipes, 0, 1);
            layoutAcessos.Controls.Add(btnAtletas, 1, 1);
            layoutAcessos.RowCount = 2;
            layoutAcessos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutAcessos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutAcessos.Dock = DockStyle.Fill;
            layoutAcessos.Margin = new Padding(20, 14, 0, 14);
            layoutAcessos.TabIndex = 0;
            //
            // btnPrancheta
            //
            btnPrancheta.Name = "btnPrancheta";
            btnPrancheta.Text = "+ Nova tática";
            btnPrancheta.BackColor = Color.FromArgb(124, 58, 237);
            btnPrancheta.ForeColor = Color.White;
            btnPrancheta.Cursor = Cursors.Hand;
            btnPrancheta.FlatStyle = FlatStyle.Flat;
            btnPrancheta.FlatAppearance.BorderSize = 0;
            btnPrancheta.UseVisualStyleBackColor = false;
            btnPrancheta.Dock = DockStyle.Fill;
            btnPrancheta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrancheta.Margin = new Padding(0, 0, 4, 4);
            btnPrancheta.TabIndex = 0;
            btnPrancheta.Click += btnPrancheta_Click;
            //
            // btnTaticas
            //
            btnTaticas.Name = "btnTaticas";
            btnTaticas.Text = "Abrir táticas";
            btnTaticas.BackColor = Color.White;
            btnTaticas.ForeColor = Color.FromArgb(92, 48, 154);
            btnTaticas.Cursor = Cursors.Hand;
            btnTaticas.FlatStyle = FlatStyle.Flat;
            btnTaticas.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnTaticas.UseVisualStyleBackColor = false;
            btnTaticas.Dock = DockStyle.Fill;
            btnTaticas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTaticas.Margin = new Padding(4, 0, 0, 4);
            btnTaticas.TabIndex = 1;
            btnTaticas.Click += btnTaticas_Click;
            //
            // btnEquipes
            //
            btnEquipes.Name = "btnEquipes";
            btnEquipes.Text = "Equipes";
            btnEquipes.BackColor = Color.White;
            btnEquipes.ForeColor = Color.FromArgb(92, 48, 154);
            btnEquipes.Cursor = Cursors.Hand;
            btnEquipes.FlatStyle = FlatStyle.Flat;
            btnEquipes.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnEquipes.UseVisualStyleBackColor = false;
            btnEquipes.Dock = DockStyle.Fill;
            btnEquipes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEquipes.Margin = new Padding(0, 4, 4, 0);
            btnEquipes.TabIndex = 2;
            btnEquipes.Click += btnEquipes_Click;
            //
            // btnAtletas
            //
            btnAtletas.Name = "btnAtletas";
            btnAtletas.Text = "Atletas";
            btnAtletas.BackColor = Color.White;
            btnAtletas.ForeColor = Color.FromArgb(92, 48, 154);
            btnAtletas.Cursor = Cursors.Hand;
            btnAtletas.FlatStyle = FlatStyle.Flat;
            btnAtletas.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnAtletas.UseVisualStyleBackColor = false;
            btnAtletas.Dock = DockStyle.Fill;
            btnAtletas.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAtletas.Margin = new Padding(4, 4, 0, 0);
            btnAtletas.TabIndex = 3;
            btnAtletas.Click += btnAtletas_Click;
            //
            // layoutStatus
            //
            layoutStatus.Name = "layoutStatus";
            layoutStatus.ColumnCount = 2;
            layoutStatus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutStatus.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            layoutStatus.Controls.Add(lblStatusBanco, 0, 0);
            layoutStatus.Controls.Add(btnAtualizar, 1, 0);
            layoutStatus.RowCount = 1;
            layoutStatus.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutStatus.Dock = DockStyle.Fill;
            layoutStatus.Margin = new Padding(0, 6, 0, 6);
            layoutStatus.TabIndex = 1;
            //
            // lblStatusBanco
            //
            lblStatusBanco.Name = "lblStatusBanco";
            lblStatusBanco.Text = "Os dados são consultados ao abrir esta tela.";
            lblStatusBanco.Dock = DockStyle.Fill;
            lblStatusBanco.Margin = new Padding(0);
            lblStatusBanco.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatusBanco.Margin = new Padding(0, 0, 12, 0);
            lblStatusBanco.TextAlign = ContentAlignment.MiddleLeft;
            //
            // btnAtualizar
            //
            btnAtualizar.Name = "btnAtualizar";
            btnAtualizar.Text = "Atualizar";
            btnAtualizar.BackColor = Color.White;
            btnAtualizar.ForeColor = Color.FromArgb(92, 48, 154);
            btnAtualizar.Cursor = Cursors.Hand;
            btnAtualizar.FlatStyle = FlatStyle.Flat;
            btnAtualizar.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnAtualizar.UseVisualStyleBackColor = false;
            btnAtualizar.Anchor = AnchorStyles.Right;
            btnAtualizar.Margin = new Padding(0);
            btnAtualizar.Size = new Size(110, 32);
            btnAtualizar.TabIndex = 0;
            btnAtualizar.Click += btnAtualizar_Click;
            //
            // layoutPainel
            //
            layoutPainel.Name = "layoutPainel";
            layoutPainel.ColumnCount = 2;
            layoutPainel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64F));
            layoutPainel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            layoutPainel.Controls.Add(layoutSecaoTaticas, 0, 0);
            layoutPainel.Controls.Add(layoutLateral, 1, 0);
            layoutPainel.RowCount = 1;
            layoutPainel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0F));
            layoutPainel.AutoSize = true;
            layoutPainel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutPainel.Dock = DockStyle.Top;
            layoutPainel.Margin = new Padding(0);
            layoutPainel.TabIndex = 2;
            //
            // layoutSecaoTaticas
            //
            layoutSecaoTaticas.Name = "layoutSecaoTaticas";
            layoutSecaoTaticas.ColumnCount = 1;
            layoutSecaoTaticas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutSecaoTaticas.Controls.Add(layoutCabecalhoTaticas, 0, 0);
            layoutSecaoTaticas.Controls.Add(lblEstadoTaticas, 0, 1);
            layoutSecaoTaticas.Controls.Add(layoutListaTaticas, 0, 2);
            layoutSecaoTaticas.RowCount = 3;
            layoutSecaoTaticas.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layoutSecaoTaticas.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0F));
            layoutSecaoTaticas.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0F));
            layoutSecaoTaticas.AutoSize = true;
            layoutSecaoTaticas.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutSecaoTaticas.Dock = DockStyle.Top;
            layoutSecaoTaticas.Margin = new Padding(0, 0, 20, 0);
            //
            // layoutCabecalhoTaticas
            //
            layoutCabecalhoTaticas.Name = "layoutCabecalhoTaticas";
            layoutCabecalhoTaticas.ColumnCount = 2;
            layoutCabecalhoTaticas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCabecalhoTaticas.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 76F));
            layoutCabecalhoTaticas.Controls.Add(lblTituloTaticas, 0, 0);
            layoutCabecalhoTaticas.Controls.Add(btnVerTaticas, 1, 0);
            layoutCabecalhoTaticas.RowCount = 1;
            layoutCabecalhoTaticas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutCabecalhoTaticas.Dock = DockStyle.Fill;
            layoutCabecalhoTaticas.Margin = new Padding(0);
            layoutCabecalhoTaticas.TabIndex = 0;
            //
            // lblTituloTaticas
            //
            lblTituloTaticas.Name = "lblTituloTaticas";
            lblTituloTaticas.Text = "Últimas táticas cadastradas";
            lblTituloTaticas.Dock = DockStyle.Fill;
            lblTituloTaticas.Margin = new Padding(0);
            lblTituloTaticas.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTituloTaticas.TextAlign = ContentAlignment.MiddleLeft;
            //
            // btnVerTaticas
            //
            btnVerTaticas.Name = "btnVerTaticas";
            btnVerTaticas.Text = "Ver todas";
            btnVerTaticas.BackColor = Color.White;
            btnVerTaticas.ForeColor = Color.FromArgb(92, 48, 154);
            btnVerTaticas.Cursor = Cursors.Hand;
            btnVerTaticas.FlatStyle = FlatStyle.Flat;
            btnVerTaticas.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnVerTaticas.UseVisualStyleBackColor = false;
            btnVerTaticas.Anchor = AnchorStyles.Right;
            btnVerTaticas.BackColor = Color.FromArgb(248, 249, 254);
            btnVerTaticas.FlatAppearance.BorderSize = 0;
            btnVerTaticas.Margin = new Padding(0);
            btnVerTaticas.Size = new Size(76, 32);
            btnVerTaticas.TabIndex = 0;
            btnVerTaticas.Click += btnTaticas_Click;
            //
            // lblEstadoTaticas
            //
            lblEstadoTaticas.Name = "lblEstadoTaticas";
            lblEstadoTaticas.Text = "As últimas táticas cadastradas aparecerão aqui.";
            lblEstadoTaticas.Dock = DockStyle.Fill;
            lblEstadoTaticas.Margin = new Padding(0);
            lblEstadoTaticas.AutoSize = true;
            lblEstadoTaticas.ForeColor = Color.FromArgb(100, 116, 139);
            lblEstadoTaticas.MinimumSize = new Size(0, 72);
            lblEstadoTaticas.Padding = new Padding(12);
            lblEstadoTaticas.BackColor = Color.White;
            //
            // layoutListaTaticas
            //
            layoutListaTaticas.Name = "layoutListaTaticas";
            layoutListaTaticas.ColumnCount = 1;
            layoutListaTaticas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutListaTaticas.RowCount = 0;
            layoutListaTaticas.AutoSize = true;
            layoutListaTaticas.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutListaTaticas.Dock = DockStyle.Top;
            layoutListaTaticas.Margin = new Padding(0);
            //
            // layoutLateral
            //
            layoutLateral.Name = "layoutLateral";
            layoutLateral.ColumnCount = 1;
            layoutLateral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutLateral.Controls.Add(layoutSecaoEquipes, 0, 0);
            layoutLateral.Controls.Add(layoutResumo, 0, 1);
            layoutLateral.RowCount = 2;
            layoutLateral.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0F));
            layoutLateral.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0F));
            layoutLateral.AutoSize = true;
            layoutLateral.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutLateral.Dock = DockStyle.Top;
            layoutLateral.Margin = new Padding(0);
            layoutLateral.TabIndex = 1;
            //
            // layoutSecaoEquipes
            //
            layoutSecaoEquipes.Name = "layoutSecaoEquipes";
            layoutSecaoEquipes.ColumnCount = 1;
            layoutSecaoEquipes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutSecaoEquipes.Controls.Add(lblTituloEquipes, 0, 0);
            layoutSecaoEquipes.Controls.Add(lblEstadoEquipes, 0, 1);
            layoutSecaoEquipes.Controls.Add(layoutListaEquipes, 0, 2);
            layoutSecaoEquipes.Controls.Add(btnVerEquipes, 0, 3);
            layoutSecaoEquipes.RowCount = 4;
            layoutSecaoEquipes.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutSecaoEquipes.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0F));
            layoutSecaoEquipes.RowStyles.Add(new RowStyle(SizeType.AutoSize, 0F));
            layoutSecaoEquipes.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            layoutSecaoEquipes.AutoSize = true;
            layoutSecaoEquipes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutSecaoEquipes.BackColor = Color.White;
            layoutSecaoEquipes.Dock = DockStyle.Top;
            layoutSecaoEquipes.Margin = new Padding(0);
            layoutSecaoEquipes.Padding = new Padding(16);
            layoutSecaoEquipes.TabIndex = 0;
            //
            // lblTituloEquipes
            //
            lblTituloEquipes.Name = "lblTituloEquipes";
            lblTituloEquipes.Text = "Equipes cadastradas";
            lblTituloEquipes.Dock = DockStyle.Fill;
            lblTituloEquipes.Margin = new Padding(0);
            lblTituloEquipes.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTituloEquipes.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblEstadoEquipes
            //
            lblEstadoEquipes.Name = "lblEstadoEquipes";
            lblEstadoEquipes.Text = "As equipes cadastradas aparecerão aqui.";
            lblEstadoEquipes.Dock = DockStyle.Fill;
            lblEstadoEquipes.Margin = new Padding(0);
            lblEstadoEquipes.AutoSize = true;
            lblEstadoEquipes.ForeColor = Color.FromArgb(100, 116, 139);
            lblEstadoEquipes.MinimumSize = new Size(0, 68);
            lblEstadoEquipes.Padding = new Padding(0, 8, 0, 8);
            //
            // layoutListaEquipes
            //
            layoutListaEquipes.Name = "layoutListaEquipes";
            layoutListaEquipes.ColumnCount = 1;
            layoutListaEquipes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutListaEquipes.RowCount = 0;
            layoutListaEquipes.AutoSize = true;
            layoutListaEquipes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutListaEquipes.Dock = DockStyle.Top;
            layoutListaEquipes.Margin = new Padding(0);
            //
            // btnVerEquipes
            //
            btnVerEquipes.Name = "btnVerEquipes";
            btnVerEquipes.Text = "Ver todas as equipes";
            btnVerEquipes.BackColor = Color.White;
            btnVerEquipes.ForeColor = Color.FromArgb(92, 48, 154);
            btnVerEquipes.Cursor = Cursors.Hand;
            btnVerEquipes.FlatStyle = FlatStyle.Flat;
            btnVerEquipes.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnVerEquipes.UseVisualStyleBackColor = false;
            btnVerEquipes.Dock = DockStyle.Fill;
            btnVerEquipes.Margin = new Padding(0, 10, 0, 0);
            btnVerEquipes.TabIndex = 0;
            btnVerEquipes.Click += btnEquipes_Click;
            //
            // layoutResumo
            //
            layoutResumo.Name = "layoutResumo";
            layoutResumo.ColumnCount = 1;
            layoutResumo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutResumo.Controls.Add(lblVisaoGeral, 0, 0);
            layoutResumo.Controls.Add(layoutEstatisticas, 0, 1);
            layoutResumo.RowCount = 2;
            layoutResumo.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            layoutResumo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutResumo.BackColor = Color.White;
            layoutResumo.Dock = DockStyle.Top;
            layoutResumo.Margin = new Padding(0, 20, 0, 0);
            layoutResumo.Padding = new Padding(16);
            layoutResumo.Size = new Size(288, 246);
            layoutResumo.TabIndex = 1;
            //
            // lblVisaoGeral
            //
            lblVisaoGeral.Name = "lblVisaoGeral";
            lblVisaoGeral.Text = "Visão geral";
            lblVisaoGeral.Dock = DockStyle.Fill;
            lblVisaoGeral.Margin = new Padding(0);
            lblVisaoGeral.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblVisaoGeral.TextAlign = ContentAlignment.MiddleLeft;
            //
            // layoutEstatisticas
            //
            layoutEstatisticas.Name = "layoutEstatisticas";
            layoutEstatisticas.ColumnCount = 2;
            layoutEstatisticas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutEstatisticas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutEstatisticas.Controls.Add(pnlTaticas, 0, 0);
            layoutEstatisticas.Controls.Add(pnlAtletas, 1, 0);
            layoutEstatisticas.Controls.Add(pnlEquipes, 0, 1);
            layoutEstatisticas.Controls.Add(pnlQuadros, 1, 1);
            layoutEstatisticas.RowCount = 2;
            layoutEstatisticas.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutEstatisticas.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutEstatisticas.Dock = DockStyle.Fill;
            layoutEstatisticas.Margin = new Padding(0);
            //
            // pnlTaticas
            //
            pnlTaticas.Name = "pnlTaticas";
            pnlTaticas.BackColor = Color.FromArgb(248, 249, 254);
            pnlTaticas.Controls.Add(lblTotalTaticas);
            pnlTaticas.Controls.Add(lblTaticas);
            pnlTaticas.Dock = DockStyle.Fill;
            pnlTaticas.Margin = new Padding(0, 0, 6, 6);
            pnlTaticas.Padding = new Padding(10, 6, 10, 6);
            //
            // lblTotalTaticas
            //
            lblTotalTaticas.Name = "lblTotalTaticas";
            lblTotalTaticas.Text = "—";
            lblTotalTaticas.Dock = DockStyle.Fill;
            lblTotalTaticas.Margin = new Padding(0);
            lblTotalTaticas.AutoEllipsis = true;
            lblTotalTaticas.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalTaticas.ForeColor = Color.FromArgb(124, 58, 237);
            lblTotalTaticas.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblTaticas
            //
            lblTaticas.Name = "lblTaticas";
            lblTaticas.Text = "Táticas";
            lblTaticas.Dock = DockStyle.Fill;
            lblTaticas.Margin = new Padding(0);
            lblTaticas.Dock = DockStyle.Top;
            lblTaticas.Font = new Font("Segoe UI", 8.5F);
            lblTaticas.ForeColor = Color.FromArgb(100, 116, 139);
            lblTaticas.Height = 22;
            //
            // pnlAtletas
            //
            pnlAtletas.Name = "pnlAtletas";
            pnlAtletas.BackColor = Color.FromArgb(248, 249, 254);
            pnlAtletas.Controls.Add(lblTotalAtletas);
            pnlAtletas.Controls.Add(lblAtletas);
            pnlAtletas.Dock = DockStyle.Fill;
            pnlAtletas.Margin = new Padding(0, 0, 6, 6);
            pnlAtletas.Padding = new Padding(10, 6, 10, 6);
            //
            // lblTotalAtletas
            //
            lblTotalAtletas.Name = "lblTotalAtletas";
            lblTotalAtletas.Text = "—";
            lblTotalAtletas.Dock = DockStyle.Fill;
            lblTotalAtletas.Margin = new Padding(0);
            lblTotalAtletas.AutoEllipsis = true;
            lblTotalAtletas.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalAtletas.ForeColor = Color.FromArgb(124, 58, 237);
            lblTotalAtletas.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblAtletas
            //
            lblAtletas.Name = "lblAtletas";
            lblAtletas.Text = "Atletas";
            lblAtletas.Dock = DockStyle.Fill;
            lblAtletas.Margin = new Padding(0);
            lblAtletas.Dock = DockStyle.Top;
            lblAtletas.Font = new Font("Segoe UI", 8.5F);
            lblAtletas.ForeColor = Color.FromArgb(100, 116, 139);
            lblAtletas.Height = 22;
            //
            // pnlEquipes
            //
            pnlEquipes.Name = "pnlEquipes";
            pnlEquipes.BackColor = Color.FromArgb(248, 249, 254);
            pnlEquipes.Controls.Add(lblTotalEquipes);
            pnlEquipes.Controls.Add(lblEquipes);
            pnlEquipes.Dock = DockStyle.Fill;
            pnlEquipes.Margin = new Padding(0, 0, 6, 6);
            pnlEquipes.Padding = new Padding(10, 6, 10, 6);
            //
            // lblTotalEquipes
            //
            lblTotalEquipes.Name = "lblTotalEquipes";
            lblTotalEquipes.Text = "—";
            lblTotalEquipes.Dock = DockStyle.Fill;
            lblTotalEquipes.Margin = new Padding(0);
            lblTotalEquipes.AutoEllipsis = true;
            lblTotalEquipes.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalEquipes.ForeColor = Color.FromArgb(124, 58, 237);
            lblTotalEquipes.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblEquipes
            //
            lblEquipes.Name = "lblEquipes";
            lblEquipes.Text = "Equipes";
            lblEquipes.Dock = DockStyle.Fill;
            lblEquipes.Margin = new Padding(0);
            lblEquipes.Dock = DockStyle.Top;
            lblEquipes.Font = new Font("Segoe UI", 8.5F);
            lblEquipes.ForeColor = Color.FromArgb(100, 116, 139);
            lblEquipes.Height = 22;
            //
            // pnlQuadros
            //
            pnlQuadros.Name = "pnlQuadros";
            pnlQuadros.BackColor = Color.FromArgb(248, 249, 254);
            pnlQuadros.Controls.Add(lblTotalQuadros);
            pnlQuadros.Controls.Add(lblQuadros);
            pnlQuadros.Dock = DockStyle.Fill;
            pnlQuadros.Margin = new Padding(0, 0, 6, 6);
            pnlQuadros.Padding = new Padding(10, 6, 10, 6);
            //
            // lblTotalQuadros
            //
            lblTotalQuadros.Name = "lblTotalQuadros";
            lblTotalQuadros.Text = "—";
            lblTotalQuadros.Dock = DockStyle.Fill;
            lblTotalQuadros.Margin = new Padding(0);
            lblTotalQuadros.AutoEllipsis = true;
            lblTotalQuadros.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalQuadros.ForeColor = Color.FromArgb(124, 58, 237);
            lblTotalQuadros.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblQuadros
            //
            lblQuadros.Name = "lblQuadros";
            lblQuadros.Text = "Quadros";
            lblQuadros.Dock = DockStyle.Fill;
            lblQuadros.Margin = new Padding(0);
            lblQuadros.Dock = DockStyle.Top;
            lblQuadros.Font = new Font("Segoe UI", 8.5F);
            lblQuadros.ForeColor = Color.FromArgb(100, 116, 139);
            lblQuadros.Height = 22;
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
            pnlQuadros.ResumeLayout(false);
            pnlEquipes.ResumeLayout(false);
            pnlAtletas.ResumeLayout(false);
            pnlTaticas.ResumeLayout(false);
            layoutEstatisticas.ResumeLayout(false);
            layoutEstatisticas.PerformLayout();
            layoutResumo.ResumeLayout(false);
            layoutResumo.PerformLayout();
            layoutListaEquipes.ResumeLayout(false);
            layoutListaEquipes.PerformLayout();
            layoutSecaoEquipes.ResumeLayout(false);
            layoutSecaoEquipes.PerformLayout();
            layoutLateral.ResumeLayout(false);
            layoutLateral.PerformLayout();
            layoutListaTaticas.ResumeLayout(false);
            layoutListaTaticas.PerformLayout();
            layoutCabecalhoTaticas.ResumeLayout(false);
            layoutCabecalhoTaticas.PerformLayout();
            layoutSecaoTaticas.ResumeLayout(false);
            layoutSecaoTaticas.PerformLayout();
            layoutPainel.ResumeLayout(false);
            layoutPainel.PerformLayout();
            layoutStatus.ResumeLayout(false);
            layoutStatus.PerformLayout();
            layoutAcessos.ResumeLayout(false);
            layoutAcessos.PerformLayout();
            layoutApresentacao.ResumeLayout(false);
            layoutApresentacao.PerformLayout();
            layoutHero.ResumeLayout(false);
            layoutHero.PerformLayout();
            layoutConteudo.ResumeLayout(false);
            layoutConteudo.PerformLayout();
            pnlConteudo.ResumeLayout(false);
            pnlConteudo.PerformLayout();
            menuUsuario.ResumeLayout(false);
            pnlCabecalho.ResumeLayout(false);
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
        private TableLayoutPanel layoutHero;
        private TableLayoutPanel layoutApresentacao;
        private Label lblBoasVindas;
        private Label lblTituloProjeto;
        private Label lblDescricao;
        private TableLayoutPanel layoutAcessos;
        private Button btnPrancheta;
        private Button btnTaticas;
        private Button btnEquipes;
        private Button btnAtletas;
        private TableLayoutPanel layoutStatus;
        private Label lblStatusBanco;
        private Button btnAtualizar;
        private TableLayoutPanel layoutPainel;
        private TableLayoutPanel layoutSecaoTaticas;
        private TableLayoutPanel layoutCabecalhoTaticas;
        private Label lblTituloTaticas;
        private Button btnVerTaticas;
        private Label lblEstadoTaticas;
        private TableLayoutPanel layoutListaTaticas;
        private TableLayoutPanel layoutLateral;
        private TableLayoutPanel layoutSecaoEquipes;
        private Label lblTituloEquipes;
        private Label lblEstadoEquipes;
        private TableLayoutPanel layoutListaEquipes;
        private Button btnVerEquipes;
        private TableLayoutPanel layoutResumo;
        private Label lblVisaoGeral;
        private TableLayoutPanel layoutEstatisticas;
        private Panel pnlTaticas;
        private Label lblTotalTaticas;
        private Label lblTaticas;
        private Panel pnlAtletas;
        private Label lblTotalAtletas;
        private Label lblAtletas;
        private Panel pnlEquipes;
        private Label lblTotalEquipes;
        private Label lblEquipes;
        private Panel pnlQuadros;
        private Label lblTotalQuadros;
        private Label lblQuadros;
    }
}
