namespace Limk.VolleyBall.User_Constrols
{
    partial class TelaConfiguracoes
    {
        private void InitializeComponent()
        {
            painelRolagem = new Panel();
            layoutConteudo = new TableLayoutPanel();
            lblTitulo = new Label();
            lblDescricao = new Label();
            layoutConta = new TableLayoutPanel();
            lblTituloConta = new Label();
            lblNomeUsuario = new Label();
            lblEmailUsuario = new Label();
            layoutPreferencias = new TableLayoutPanel();
            lblTituloPreferencias = new Label();
            layoutCorAtletaPrincipal = new FlowLayoutPanel();
            lblTituloCorAtletaPrincipal = new Label();
            layoutEscolhaCorAtletaPrincipal = new TableLayoutPanel();
            amostraCorAtletaPrincipal = new Panel();
            lblCorAtletaPrincipal = new Label();
            btnCorAtletaPrincipal = new Button();
            layoutCorAtletaAdversario = new FlowLayoutPanel();
            lblTituloCorAtletaAdversario = new Label();
            layoutEscolhaCorAtletaAdversario = new TableLayoutPanel();
            amostraCorAtletaAdversario = new Panel();
            lblCorAtletaAdversario = new Label();
            btnCorAtletaAdversario = new Button();
            layoutCorBola = new FlowLayoutPanel();
            lblTituloCorBola = new Label();
            layoutEscolhaCorBola = new TableLayoutPanel();
            amostraCorBola = new Panel();
            lblCorBola = new Label();
            btnCorBola = new Button();
            lblDuracaoQuadro = new Label();
            numDuracaoQuadro = new NumericUpDown();
            layoutAcoes = new FlowLayoutPanel();
            btnSalvar = new Button();
            btnRestaurarPadrao = new Button();
            btnRecarregar = new Button();
            lblStatus = new Label();
            painelRolagem.SuspendLayout();
            layoutConteudo.SuspendLayout();
            layoutConta.SuspendLayout();
            layoutPreferencias.SuspendLayout();
            layoutCorAtletaPrincipal.SuspendLayout();
            layoutEscolhaCorAtletaPrincipal.SuspendLayout();
            amostraCorAtletaPrincipal.SuspendLayout();
            layoutCorAtletaAdversario.SuspendLayout();
            layoutEscolhaCorAtletaAdversario.SuspendLayout();
            amostraCorAtletaAdversario.SuspendLayout();
            layoutCorBola.SuspendLayout();
            layoutEscolhaCorBola.SuspendLayout();
            amostraCorBola.SuspendLayout();
            layoutAcoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDuracaoQuadro).BeginInit();
            SuspendLayout();
            //
            // painelRolagem
            //
            painelRolagem.Name = "painelRolagem";
            painelRolagem.AutoScroll = true;
            painelRolagem.Dock = DockStyle.Fill;
            //
            // layoutConteudo
            //
            layoutConteudo.Name = "layoutConteudo";
            layoutConteudo.AutoSize = true;
            layoutConteudo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutConteudo.ColumnCount = 1;
            layoutConteudo.Dock = DockStyle.Top;
            layoutConteudo.Margin = Padding.Empty;
            layoutConteudo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutConteudo.Padding = new Padding(16);
            layoutConteudo.RowCount = 4;
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblTitulo.TabIndex = 0;
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblDescricao.TabIndex = 1;
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutConta.TabIndex = 2;
            layoutConteudo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutPreferencias.TabIndex = 3;
            //
            // lblTitulo
            //
            lblTitulo.Name = "lblTitulo";
            lblTitulo.AutoSize = true;
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Margin = new Padding(0, 0, 0, 4);
            lblTitulo.Text = "Configurações";
            lblTitulo.UseMnemonic = false;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            //
            // lblDescricao
            //
            lblDescricao.Name = "lblDescricao";
            lblDescricao.AutoSize = true;
            lblDescricao.Dock = DockStyle.Top;
            lblDescricao.Margin = new Padding(0, 0, 0, 16);
            lblDescricao.Text = "Escolha as cores dos elementos e a duração padrão das novas fases.";
            lblDescricao.UseMnemonic = false;
            lblDescricao.ForeColor = Color.FromArgb(100, 116, 139);
            //
            // layoutConta
            //
            layoutConta.Name = "layoutConta";
            layoutConta.AutoSize = true;
            layoutConta.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutConta.ColumnCount = 1;
            layoutConta.Dock = DockStyle.Top;
            layoutConta.Margin = Padding.Empty;
            layoutConta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutConta.BackColor = Color.White;
            layoutConta.Padding = new Padding(16);
            layoutConta.Margin = new Padding(0, 0, 0, 12);
            layoutConta.RowCount = 3;
            layoutConta.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblTituloConta.TabIndex = 0;
            layoutConta.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblNomeUsuario.TabIndex = 1;
            layoutConta.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblEmailUsuario.TabIndex = 2;
            //
            // lblTituloConta
            //
            lblTituloConta.Name = "lblTituloConta";
            lblTituloConta.AutoSize = true;
            lblTituloConta.Dock = DockStyle.Top;
            lblTituloConta.Margin = new Padding(0, 0, 0, 9);
            lblTituloConta.Text = "Sua conta";
            lblTituloConta.UseMnemonic = false;
            lblTituloConta.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            //
            // lblNomeUsuario
            //
            lblNomeUsuario.Name = "lblNomeUsuario";
            lblNomeUsuario.AutoSize = true;
            lblNomeUsuario.Dock = DockStyle.Top;
            lblNomeUsuario.Margin = new Padding(0, 0, 0, 3);
            lblNomeUsuario.Text = "Nome do usuário";
            lblNomeUsuario.UseMnemonic = false;
            //
            // lblEmailUsuario
            //
            lblEmailUsuario.Name = "lblEmailUsuario";
            lblEmailUsuario.AutoSize = true;
            lblEmailUsuario.Dock = DockStyle.Top;
            lblEmailUsuario.Margin = Padding.Empty;
            lblEmailUsuario.Text = "usuario@exemplo.com";
            lblEmailUsuario.UseMnemonic = false;
            lblEmailUsuario.ForeColor = Color.FromArgb(100, 116, 139);
            //
            // layoutPreferencias
            //
            layoutPreferencias.Name = "layoutPreferencias";
            layoutPreferencias.AutoSize = true;
            layoutPreferencias.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutPreferencias.ColumnCount = 1;
            layoutPreferencias.Dock = DockStyle.Top;
            layoutPreferencias.Margin = Padding.Empty;
            layoutPreferencias.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPreferencias.BackColor = Color.White;
            layoutPreferencias.Padding = new Padding(16);
            layoutPreferencias.Margin = new Padding(0, 0, 0, 12);
            layoutPreferencias.Margin = Padding.Empty;
            layoutPreferencias.RowCount = 8;
            layoutPreferencias.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblTituloPreferencias.TabIndex = 0;
            layoutPreferencias.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutCorAtletaPrincipal.TabIndex = 1;
            layoutPreferencias.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutCorAtletaAdversario.TabIndex = 2;
            layoutPreferencias.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutCorBola.TabIndex = 3;
            layoutPreferencias.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblDuracaoQuadro.TabIndex = 4;
            layoutPreferencias.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            numDuracaoQuadro.TabIndex = 5;
            layoutPreferencias.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutAcoes.TabIndex = 6;
            layoutPreferencias.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblStatus.TabIndex = 7;
            //
            // lblTituloPreferencias
            //
            lblTituloPreferencias.Name = "lblTituloPreferencias";
            lblTituloPreferencias.AutoSize = true;
            lblTituloPreferencias.Dock = DockStyle.Top;
            lblTituloPreferencias.Margin = new Padding(0, 0, 0, 9);
            lblTituloPreferencias.Text = "Preferências da prancheta";
            lblTituloPreferencias.UseMnemonic = false;
            lblTituloPreferencias.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            //
            // layoutCorAtletaPrincipal
            //
            layoutCorAtletaPrincipal.Name = "layoutCorAtletaPrincipal";
            layoutCorAtletaPrincipal.AutoSize = true;
            layoutCorAtletaPrincipal.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutCorAtletaPrincipal.Dock = DockStyle.Top;
            layoutCorAtletaPrincipal.Margin = new Padding(0, 0, 0, 10);
            //
            // lblTituloCorAtletaPrincipal
            //
            lblTituloCorAtletaPrincipal.Name = "lblTituloCorAtletaPrincipal";
            lblTituloCorAtletaPrincipal.AutoSize = true;
            lblTituloCorAtletaPrincipal.Dock = DockStyle.Top;
            lblTituloCorAtletaPrincipal.Margin = Padding.Empty;
            lblTituloCorAtletaPrincipal.Text = "Atleta principal";
            lblTituloCorAtletaPrincipal.UseMnemonic = false;
            lblTituloCorAtletaPrincipal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTituloCorAtletaPrincipal.AutoSize = false;
            lblTituloCorAtletaPrincipal.Dock = DockStyle.None;
            lblTituloCorAtletaPrincipal.Size = new Size(144, 32);
            lblTituloCorAtletaPrincipal.TextAlign = ContentAlignment.MiddleLeft;
            //
            // layoutEscolhaCorAtletaPrincipal
            //
            layoutEscolhaCorAtletaPrincipal.Name = "layoutEscolhaCorAtletaPrincipal";
            layoutEscolhaCorAtletaPrincipal.AutoSize = true;
            layoutEscolhaCorAtletaPrincipal.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutEscolhaCorAtletaPrincipal.ColumnCount = 3;
            layoutEscolhaCorAtletaPrincipal.RowCount = 1;
            layoutEscolhaCorAtletaPrincipal.Margin = Padding.Empty;
            layoutEscolhaCorAtletaPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutEscolhaCorAtletaPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutEscolhaCorAtletaPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutEscolhaCorAtletaPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //
            // amostraCorAtletaPrincipal
            //
            amostraCorAtletaPrincipal.Name = "amostraCorAtletaPrincipal";
            amostraCorAtletaPrincipal.Anchor = AnchorStyles.Left;
            amostraCorAtletaPrincipal.BorderStyle = BorderStyle.FixedSingle;
            amostraCorAtletaPrincipal.Size = new Size(22, 22);
            amostraCorAtletaPrincipal.Margin = new Padding(0, 0, 8, 0);
            //
            // lblCorAtletaPrincipal
            //
            lblCorAtletaPrincipal.Name = "lblCorAtletaPrincipal";
            lblCorAtletaPrincipal.AutoSize = true;
            lblCorAtletaPrincipal.Anchor = AnchorStyles.Left;
            lblCorAtletaPrincipal.Margin = new Padding(0, 0, 8, 0);
            lblCorAtletaPrincipal.Text = "—";
            lblCorAtletaPrincipal.UseMnemonic = false;
            //
            // btnCorAtletaPrincipal
            //
            btnCorAtletaPrincipal.Name = "btnCorAtletaPrincipal";
            btnCorAtletaPrincipal.AutoSize = true;
            btnCorAtletaPrincipal.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCorAtletaPrincipal.BackColor = Color.White;
            btnCorAtletaPrincipal.Cursor = Cursors.Hand;
            btnCorAtletaPrincipal.FlatStyle = FlatStyle.Flat;
            btnCorAtletaPrincipal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCorAtletaPrincipal.ForeColor = Color.FromArgb(92, 48, 154);
            btnCorAtletaPrincipal.Margin = new Padding(0, 0, 8, 4);
            btnCorAtletaPrincipal.MinimumSize = new Size(0, 32);
            btnCorAtletaPrincipal.Padding = new Padding(10, 0, 10, 0);
            btnCorAtletaPrincipal.Text = "Alterar";
            btnCorAtletaPrincipal.UseVisualStyleBackColor = false;
            btnCorAtletaPrincipal.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnCorAtletaPrincipal.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 240, 255);
            btnCorAtletaPrincipal.AccessibleName = "Alterar cor do atleta principal";
            btnCorAtletaPrincipal.Anchor = AnchorStyles.Left;
            btnCorAtletaPrincipal.Margin = Padding.Empty;
            //
            // layoutCorAtletaAdversario
            //
            layoutCorAtletaAdversario.Name = "layoutCorAtletaAdversario";
            layoutCorAtletaAdversario.AutoSize = true;
            layoutCorAtletaAdversario.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutCorAtletaAdversario.Dock = DockStyle.Top;
            layoutCorAtletaAdversario.Margin = new Padding(0, 0, 0, 10);
            //
            // lblTituloCorAtletaAdversario
            //
            lblTituloCorAtletaAdversario.Name = "lblTituloCorAtletaAdversario";
            lblTituloCorAtletaAdversario.AutoSize = true;
            lblTituloCorAtletaAdversario.Dock = DockStyle.Top;
            lblTituloCorAtletaAdversario.Margin = Padding.Empty;
            lblTituloCorAtletaAdversario.Text = "Atleta adversário";
            lblTituloCorAtletaAdversario.UseMnemonic = false;
            lblTituloCorAtletaAdversario.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTituloCorAtletaAdversario.AutoSize = false;
            lblTituloCorAtletaAdversario.Dock = DockStyle.None;
            lblTituloCorAtletaAdversario.Size = new Size(144, 32);
            lblTituloCorAtletaAdversario.TextAlign = ContentAlignment.MiddleLeft;
            //
            // layoutEscolhaCorAtletaAdversario
            //
            layoutEscolhaCorAtletaAdversario.Name = "layoutEscolhaCorAtletaAdversario";
            layoutEscolhaCorAtletaAdversario.AutoSize = true;
            layoutEscolhaCorAtletaAdversario.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutEscolhaCorAtletaAdversario.ColumnCount = 3;
            layoutEscolhaCorAtletaAdversario.RowCount = 1;
            layoutEscolhaCorAtletaAdversario.Margin = Padding.Empty;
            layoutEscolhaCorAtletaAdversario.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutEscolhaCorAtletaAdversario.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutEscolhaCorAtletaAdversario.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutEscolhaCorAtletaAdversario.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //
            // amostraCorAtletaAdversario
            //
            amostraCorAtletaAdversario.Name = "amostraCorAtletaAdversario";
            amostraCorAtletaAdversario.Anchor = AnchorStyles.Left;
            amostraCorAtletaAdversario.BorderStyle = BorderStyle.FixedSingle;
            amostraCorAtletaAdversario.Size = new Size(22, 22);
            amostraCorAtletaAdversario.Margin = new Padding(0, 0, 8, 0);
            //
            // lblCorAtletaAdversario
            //
            lblCorAtletaAdversario.Name = "lblCorAtletaAdversario";
            lblCorAtletaAdversario.AutoSize = true;
            lblCorAtletaAdversario.Anchor = AnchorStyles.Left;
            lblCorAtletaAdversario.Margin = new Padding(0, 0, 8, 0);
            lblCorAtletaAdversario.Text = "—";
            lblCorAtletaAdversario.UseMnemonic = false;
            //
            // btnCorAtletaAdversario
            //
            btnCorAtletaAdversario.Name = "btnCorAtletaAdversario";
            btnCorAtletaAdversario.AutoSize = true;
            btnCorAtletaAdversario.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCorAtletaAdversario.BackColor = Color.White;
            btnCorAtletaAdversario.Cursor = Cursors.Hand;
            btnCorAtletaAdversario.FlatStyle = FlatStyle.Flat;
            btnCorAtletaAdversario.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCorAtletaAdversario.ForeColor = Color.FromArgb(92, 48, 154);
            btnCorAtletaAdversario.Margin = new Padding(0, 0, 8, 4);
            btnCorAtletaAdversario.MinimumSize = new Size(0, 32);
            btnCorAtletaAdversario.Padding = new Padding(10, 0, 10, 0);
            btnCorAtletaAdversario.Text = "Alterar";
            btnCorAtletaAdversario.UseVisualStyleBackColor = false;
            btnCorAtletaAdversario.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnCorAtletaAdversario.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 240, 255);
            btnCorAtletaAdversario.AccessibleName = "Alterar cor do atleta adversário";
            btnCorAtletaAdversario.Anchor = AnchorStyles.Left;
            btnCorAtletaAdversario.Margin = Padding.Empty;
            //
            // layoutCorBola
            //
            layoutCorBola.Name = "layoutCorBola";
            layoutCorBola.AutoSize = true;
            layoutCorBola.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutCorBola.Dock = DockStyle.Top;
            layoutCorBola.Margin = new Padding(0, 0, 0, 10);
            //
            // lblTituloCorBola
            //
            lblTituloCorBola.Name = "lblTituloCorBola";
            lblTituloCorBola.AutoSize = true;
            lblTituloCorBola.Dock = DockStyle.Top;
            lblTituloCorBola.Margin = Padding.Empty;
            lblTituloCorBola.Text = "Bola";
            lblTituloCorBola.UseMnemonic = false;
            lblTituloCorBola.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTituloCorBola.AutoSize = false;
            lblTituloCorBola.Dock = DockStyle.None;
            lblTituloCorBola.Size = new Size(144, 32);
            lblTituloCorBola.TextAlign = ContentAlignment.MiddleLeft;
            //
            // layoutEscolhaCorBola
            //
            layoutEscolhaCorBola.Name = "layoutEscolhaCorBola";
            layoutEscolhaCorBola.AutoSize = true;
            layoutEscolhaCorBola.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutEscolhaCorBola.ColumnCount = 3;
            layoutEscolhaCorBola.RowCount = 1;
            layoutEscolhaCorBola.Margin = Padding.Empty;
            layoutEscolhaCorBola.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutEscolhaCorBola.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutEscolhaCorBola.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layoutEscolhaCorBola.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            //
            // amostraCorBola
            //
            amostraCorBola.Name = "amostraCorBola";
            amostraCorBola.Anchor = AnchorStyles.Left;
            amostraCorBola.BorderStyle = BorderStyle.FixedSingle;
            amostraCorBola.Size = new Size(22, 22);
            amostraCorBola.Margin = new Padding(0, 0, 8, 0);
            //
            // lblCorBola
            //
            lblCorBola.Name = "lblCorBola";
            lblCorBola.AutoSize = true;
            lblCorBola.Anchor = AnchorStyles.Left;
            lblCorBola.Margin = new Padding(0, 0, 8, 0);
            lblCorBola.Text = "—";
            lblCorBola.UseMnemonic = false;
            //
            // btnCorBola
            //
            btnCorBola.Name = "btnCorBola";
            btnCorBola.AutoSize = true;
            btnCorBola.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCorBola.BackColor = Color.White;
            btnCorBola.Cursor = Cursors.Hand;
            btnCorBola.FlatStyle = FlatStyle.Flat;
            btnCorBola.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCorBola.ForeColor = Color.FromArgb(92, 48, 154);
            btnCorBola.Margin = new Padding(0, 0, 8, 4);
            btnCorBola.MinimumSize = new Size(0, 32);
            btnCorBola.Padding = new Padding(10, 0, 10, 0);
            btnCorBola.Text = "Alterar";
            btnCorBola.UseVisualStyleBackColor = false;
            btnCorBola.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnCorBola.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 240, 255);
            btnCorBola.AccessibleName = "Alterar cor da bola";
            btnCorBola.Anchor = AnchorStyles.Left;
            btnCorBola.Margin = Padding.Empty;
            //
            // lblDuracaoQuadro
            //
            lblDuracaoQuadro.Name = "lblDuracaoQuadro";
            lblDuracaoQuadro.AutoSize = true;
            lblDuracaoQuadro.Dock = DockStyle.Top;
            lblDuracaoQuadro.Margin = new Padding(0, 0, 0, 5);
            lblDuracaoQuadro.Text = "Duração padrão do quadro (ms)";
            lblDuracaoQuadro.UseMnemonic = false;
            lblDuracaoQuadro.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            //
            // numDuracaoQuadro
            //
            numDuracaoQuadro.Name = "numDuracaoQuadro";
            numDuracaoQuadro.AccessibleName = "Duração padrão do quadro em milissegundos";
            numDuracaoQuadro.Size = new Size(144, 23);
            numDuracaoQuadro.Margin = new Padding(0, 0, 0, 16);
            numDuracaoQuadro.Minimum = 1M;
            numDuracaoQuadro.Maximum = 2147483647M;
            numDuracaoQuadro.Increment = 100M;
            numDuracaoQuadro.Value = 1000M;
            numDuracaoQuadro.ThousandsSeparator = true;
            //
            // layoutAcoes
            //
            layoutAcoes.Name = "layoutAcoes";
            layoutAcoes.AutoSize = true;
            layoutAcoes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutAcoes.Dock = DockStyle.Top;
            layoutAcoes.Margin = Padding.Empty;
            layoutAcoes.WrapContents = true;
            //
            // btnSalvar
            //
            btnSalvar.Name = "btnSalvar";
            btnSalvar.AutoSize = true;
            btnSalvar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSalvar.BackColor = Color.White;
            btnSalvar.Cursor = Cursors.Hand;
            btnSalvar.FlatStyle = FlatStyle.Flat;
            btnSalvar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSalvar.ForeColor = Color.FromArgb(92, 48, 154);
            btnSalvar.Margin = new Padding(0, 0, 8, 4);
            btnSalvar.MinimumSize = new Size(0, 32);
            btnSalvar.Padding = new Padding(10, 0, 10, 0);
            btnSalvar.Text = "Salvar preferências";
            btnSalvar.UseVisualStyleBackColor = false;
            btnSalvar.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnSalvar.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 240, 255);
            btnSalvar.BackColor = Color.FromArgb(124, 58, 237);
            btnSalvar.ForeColor = Color.White;
            btnSalvar.FlatAppearance.BorderSize = 0;
            btnSalvar.FlatAppearance.MouseOverBackColor = Color.FromArgb(109, 40, 217);
            //
            // btnRestaurarPadrao
            //
            btnRestaurarPadrao.Name = "btnRestaurarPadrao";
            btnRestaurarPadrao.AutoSize = true;
            btnRestaurarPadrao.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRestaurarPadrao.BackColor = Color.White;
            btnRestaurarPadrao.Cursor = Cursors.Hand;
            btnRestaurarPadrao.FlatStyle = FlatStyle.Flat;
            btnRestaurarPadrao.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRestaurarPadrao.ForeColor = Color.FromArgb(92, 48, 154);
            btnRestaurarPadrao.Margin = new Padding(0, 0, 8, 4);
            btnRestaurarPadrao.MinimumSize = new Size(0, 32);
            btnRestaurarPadrao.Padding = new Padding(10, 0, 10, 0);
            btnRestaurarPadrao.Text = "Restaurar padrões";
            btnRestaurarPadrao.UseVisualStyleBackColor = false;
            btnRestaurarPadrao.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnRestaurarPadrao.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 240, 255);
            //
            // btnRecarregar
            //
            btnRecarregar.Name = "btnRecarregar";
            btnRecarregar.AutoSize = true;
            btnRecarregar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRecarregar.BackColor = Color.White;
            btnRecarregar.Cursor = Cursors.Hand;
            btnRecarregar.FlatStyle = FlatStyle.Flat;
            btnRecarregar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRecarregar.ForeColor = Color.FromArgb(92, 48, 154);
            btnRecarregar.Margin = new Padding(0, 0, 8, 4);
            btnRecarregar.MinimumSize = new Size(0, 32);
            btnRecarregar.Padding = new Padding(10, 0, 10, 0);
            btnRecarregar.Text = "Tentar novamente";
            btnRecarregar.UseVisualStyleBackColor = false;
            btnRecarregar.FlatAppearance.BorderColor = Color.FromArgb(222, 215, 236);
            btnRecarregar.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 240, 255);
            btnRecarregar.Visible = false;
            //
            // lblStatus
            //
            lblStatus.Name = "lblStatus";
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Top;
            lblStatus.Margin = new Padding(0, 10, 0, 0);
            lblStatus.Text = "";
            lblStatus.UseMnemonic = false;
            lblStatus.LiveSetting = System.Windows.Forms.Automation.AutomationLiveSetting.Polite;
            lblStatus.Visible = false;
            painelRolagem.Controls.Add(layoutConteudo);
            layoutConteudo.Controls.Add(lblTitulo, 0, 0);
            layoutConteudo.Controls.Add(lblDescricao, 0, 1);
            layoutConteudo.Controls.Add(layoutConta, 0, 2);
            layoutConteudo.Controls.Add(layoutPreferencias, 0, 3);
            layoutConteudo.SizeChanged += AjustarLayout;
            layoutConta.Controls.Add(lblTituloConta, 0, 0);
            layoutConta.Controls.Add(lblNomeUsuario, 0, 1);
            layoutConta.Controls.Add(lblEmailUsuario, 0, 2);
            layoutPreferencias.Controls.Add(lblTituloPreferencias, 0, 0);
            layoutPreferencias.Controls.Add(layoutCorAtletaPrincipal, 0, 1);
            layoutPreferencias.Controls.Add(layoutCorAtletaAdversario, 0, 2);
            layoutPreferencias.Controls.Add(layoutCorBola, 0, 3);
            layoutPreferencias.Controls.Add(lblDuracaoQuadro, 0, 4);
            layoutPreferencias.Controls.Add(numDuracaoQuadro, 0, 5);
            layoutPreferencias.Controls.Add(layoutAcoes, 0, 6);
            layoutPreferencias.Controls.Add(lblStatus, 0, 7);
            layoutCorAtletaPrincipal.Controls.Add(lblTituloCorAtletaPrincipal);
            layoutCorAtletaPrincipal.Controls.Add(layoutEscolhaCorAtletaPrincipal);
            layoutEscolhaCorAtletaPrincipal.Controls.Add(amostraCorAtletaPrincipal, 0, 0);
            layoutEscolhaCorAtletaPrincipal.Controls.Add(lblCorAtletaPrincipal, 1, 0);
            layoutEscolhaCorAtletaPrincipal.Controls.Add(btnCorAtletaPrincipal, 2, 0);
            btnCorAtletaPrincipal.Click += btnCorAtletaPrincipal_Click;
            layoutCorAtletaAdversario.Controls.Add(lblTituloCorAtletaAdversario);
            layoutCorAtletaAdversario.Controls.Add(layoutEscolhaCorAtletaAdversario);
            layoutEscolhaCorAtletaAdversario.Controls.Add(amostraCorAtletaAdversario, 0, 0);
            layoutEscolhaCorAtletaAdversario.Controls.Add(lblCorAtletaAdversario, 1, 0);
            layoutEscolhaCorAtletaAdversario.Controls.Add(btnCorAtletaAdversario, 2, 0);
            btnCorAtletaAdversario.Click += btnCorAtletaAdversario_Click;
            layoutCorBola.Controls.Add(lblTituloCorBola);
            layoutCorBola.Controls.Add(layoutEscolhaCorBola);
            layoutEscolhaCorBola.Controls.Add(amostraCorBola, 0, 0);
            layoutEscolhaCorBola.Controls.Add(lblCorBola, 1, 0);
            layoutEscolhaCorBola.Controls.Add(btnCorBola, 2, 0);
            btnCorBola.Click += btnCorBola_Click;
            numDuracaoQuadro.ValueChanged += numDuracaoQuadro_ValueChanged;
            layoutAcoes.Controls.Add(btnSalvar);
            layoutAcoes.Controls.Add(btnRestaurarPadrao);
            layoutAcoes.Controls.Add(btnRecarregar);
            btnSalvar.Click += btnSalvar_Click;
            btnRestaurarPadrao.Click += btnRestaurarPadrao_Click;
            btnRecarregar.Click += btnRecarregar_Click;
            btnRecarregar.VisibleChanged += AjustarLayout;
            AccessibleName = "Configurações de usuário";
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 254);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(30, 27, 45);
            Name = "TelaConfiguracoes";
            Size = new Size(848, 740);
            Controls.Add(painelRolagem);
            Load += TelaConfiguracoes_Load;
            layoutAcoes.ResumeLayout(false);
            layoutAcoes.PerformLayout();
            amostraCorBola.ResumeLayout(false);
            amostraCorBola.PerformLayout();
            layoutEscolhaCorBola.ResumeLayout(false);
            layoutEscolhaCorBola.PerformLayout();
            layoutCorBola.ResumeLayout(false);
            layoutCorBola.PerformLayout();
            amostraCorAtletaAdversario.ResumeLayout(false);
            amostraCorAtletaAdversario.PerformLayout();
            layoutEscolhaCorAtletaAdversario.ResumeLayout(false);
            layoutEscolhaCorAtletaAdversario.PerformLayout();
            layoutCorAtletaAdversario.ResumeLayout(false);
            layoutCorAtletaAdversario.PerformLayout();
            amostraCorAtletaPrincipal.ResumeLayout(false);
            amostraCorAtletaPrincipal.PerformLayout();
            layoutEscolhaCorAtletaPrincipal.ResumeLayout(false);
            layoutEscolhaCorAtletaPrincipal.PerformLayout();
            layoutCorAtletaPrincipal.ResumeLayout(false);
            layoutCorAtletaPrincipal.PerformLayout();
            layoutPreferencias.ResumeLayout(false);
            layoutPreferencias.PerformLayout();
            layoutConta.ResumeLayout(false);
            layoutConta.PerformLayout();
            layoutConteudo.ResumeLayout(false);
            layoutConteudo.PerformLayout();
            painelRolagem.ResumeLayout(false);
            painelRolagem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDuracaoQuadro).EndInit();
            ResumeLayout(false);
        }

        private Panel painelRolagem;
        private TableLayoutPanel layoutConteudo;
        private Label lblTitulo;
        private Label lblDescricao;
        private TableLayoutPanel layoutConta;
        private Label lblTituloConta;
        private Label lblNomeUsuario;
        private Label lblEmailUsuario;
        private TableLayoutPanel layoutPreferencias;
        private Label lblTituloPreferencias;
        private FlowLayoutPanel layoutCorAtletaPrincipal;
        private Label lblTituloCorAtletaPrincipal;
        private TableLayoutPanel layoutEscolhaCorAtletaPrincipal;
        private Panel amostraCorAtletaPrincipal;
        private Label lblCorAtletaPrincipal;
        private Button btnCorAtletaPrincipal;
        private FlowLayoutPanel layoutCorAtletaAdversario;
        private Label lblTituloCorAtletaAdversario;
        private TableLayoutPanel layoutEscolhaCorAtletaAdversario;
        private Panel amostraCorAtletaAdversario;
        private Label lblCorAtletaAdversario;
        private Button btnCorAtletaAdversario;
        private FlowLayoutPanel layoutCorBola;
        private Label lblTituloCorBola;
        private TableLayoutPanel layoutEscolhaCorBola;
        private Panel amostraCorBola;
        private Label lblCorBola;
        private Button btnCorBola;
        private Label lblDuracaoQuadro;
        private NumericUpDown numDuracaoQuadro;
        private FlowLayoutPanel layoutAcoes;
        private Button btnSalvar;
        private Button btnRestaurarPadrao;
        private Button btnRecarregar;
        private Label lblStatus;
    }
}
