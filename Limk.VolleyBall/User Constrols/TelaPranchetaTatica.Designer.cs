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
            if (disposing && !IsDisposed)
            {
                _encerramento.Cancel();
                _encerramento.Dispose();
                _tempoTransicao.Stop();
                _movimentos.Clear();
                components?.Dispose();
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaPranchetaTatica));
            labelStatusPrancheta = new Label();
            timerSequencia = new System.Windows.Forms.Timer(components);
            panelCabecalho = new Panel();
            btnSalvarTatica = new Button();
            labelNomeTatica = new Label();
            labelTitulo = new Label();
            panelSequencia = new Panel();
            layoutSequencia = new TableLayoutPanel();
            cabecalhoSequencia = new TableLayoutPanel();
            titulosSequencia = new Panel();
            labelTituloSequencia = new Label();
            labelResumoSequencia = new Label();
            acoesSequencia = new FlowLayoutPanel();
            btnNovaFase = new Button();
            btnFaseAnterior = new Button();
            btnExecutarSequencia = new Button();
            btnProximaFase = new Button();
            btnDuplicarFase = new Button();
            btnExcluirFase = new Button();
            flowFases = new FlowLayoutPanel();
            panelConteudo = new Panel();
            panelPranchetaTatica = new Panel();
            prancheta = new PranchetaControl();
            panelPropriedades = new Panel();
            layoutPropriedades = new TableLayoutPanel();
            labelTituloPropriedades = new Label();
            labelResumoElemento = new Label();
            layoutDadosTatica = new TableLayoutPanel();
            labelNomeTaticaPropriedade = new Label();
            textNomeTatica = new TextBox();
            labelEquipeTatica = new Label();
            comboEquipeTatica = new ComboBox();
            labelTipoTatica = new Label();
            comboTipoTatica = new ComboBox();
            labelSistemaTatica = new Label();
            comboSistemaTatica = new ComboBox();
            labelDescricaoTatica = new Label();
            textDescricaoTatica = new TextBox();
            labelDadosFase = new Label();
            labelNomeFase = new Label();
            textNomeFase = new TextBox();
            layoutTempoFase = new TableLayoutPanel();
            labelDuracaoFase = new Label();
            labelRotacaoFase = new Label();
            numericDuracaoFase = new NumericUpDown();
            comboRotacaoFase = new ComboBox();
            layoutCamposAtleta = new TableLayoutPanel();
            labelAtletaVinculado = new Label();
            comboAtletaVinculado = new ComboBox();
            labelNumeroCamisa = new Label();
            textNumeroCamisa = new TextBox();
            labelLadoAtleta = new Label();
            comboLadoAtleta = new ComboBox();
            labelTituloCoordenadas = new Label();
            layoutCoordenadas = new TableLayoutPanel();
            labelCoordenadaX = new Label();
            labelCoordenadaY = new Label();
            numericCoordenadaX = new NumericUpDown();
            numericCoordenadaY = new NumericUpDown();
            btnAplicarPropriedades = new Button();
            btnRemoverElemento = new Button();
            labelMensagemPropriedades = new Label();
            panelFerramentas = new Panel();
            btnAdicionarAtleta = new Button();
            btnAdicionarBola = new Button();
            panelTituloFerramentas = new Panel();
            labelTituloFerramentas = new Label();
            dicasPrancheta = new ToolTip(components);
            panelCabecalho.SuspendLayout();
            panelSequencia.SuspendLayout();
            layoutSequencia.SuspendLayout();
            cabecalhoSequencia.SuspendLayout();
            titulosSequencia.SuspendLayout();
            acoesSequencia.SuspendLayout();
            panelConteudo.SuspendLayout();
            panelPranchetaTatica.SuspendLayout();
            panelPropriedades.SuspendLayout();
            layoutPropriedades.SuspendLayout();
            layoutDadosTatica.SuspendLayout();
            layoutTempoFase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericDuracaoFase).BeginInit();
            layoutCamposAtleta.SuspendLayout();
            layoutCoordenadas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericCoordenadaX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericCoordenadaY).BeginInit();
            panelFerramentas.SuspendLayout();
            panelTituloFerramentas.SuspendLayout();
            SuspendLayout();
            // 
            // labelStatusPrancheta
            // 
            labelStatusPrancheta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelStatusPrancheta.AutoEllipsis = true;
            labelStatusPrancheta.Font = new Font("Segoe UI", 9F);
            labelStatusPrancheta.ForeColor = Color.FromArgb(100, 116, 139);
            labelStatusPrancheta.Location = new Point(20, 76);
            labelStatusPrancheta.Name = "labelStatusPrancheta";
            labelStatusPrancheta.Size = new Size(800, 28);
            labelStatusPrancheta.TabIndex = 0;
            labelStatusPrancheta.Text = "Salve a tática para guardar suas fases no banco de dados.";
            // 
            // timerSequencia
            // 
            timerSequencia.Interval = 16;
            timerSequencia.Tick += timerSequencia_Tick;
            // 
            // panelCabecalho
            // 
            panelCabecalho.BackColor = Color.White;
            panelCabecalho.Controls.Add(labelStatusPrancheta);
            panelCabecalho.Controls.Add(btnSalvarTatica);
            panelCabecalho.Controls.Add(labelNomeTatica);
            panelCabecalho.Controls.Add(labelTitulo);
            panelCabecalho.Dock = DockStyle.Top;
            panelCabecalho.Location = new Point(0, 0);
            panelCabecalho.Name = "panelCabecalho";
            panelCabecalho.Size = new Size(842, 112);
            panelCabecalho.TabIndex = 1;
            // 
            // btnSalvarTatica
            // 
            btnSalvarTatica.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSalvarTatica.BackColor = Color.FromArgb(124, 58, 237);
            btnSalvarTatica.FlatStyle = FlatStyle.Flat;
            btnSalvarTatica.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalvarTatica.ForeColor = SystemColors.Window;
            btnSalvarTatica.Location = new Point(665, 26);
            btnSalvarTatica.Name = "btnSalvarTatica";
            btnSalvarTatica.Size = new Size(154, 37);
            btnSalvarTatica.TabIndex = 1;
            btnSalvarTatica.Text = "💾 Salvar Tática";
            btnSalvarTatica.UseVisualStyleBackColor = false;
            btnSalvarTatica.Click += btnSalvarTatica_Click;
            // 
            // labelNomeTatica
            // 
            labelNomeTatica.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelNomeTatica.AutoEllipsis = true;
            labelNomeTatica.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNomeTatica.Location = new Point(194, 34);
            labelNomeTatica.Name = "labelNomeTatica";
            labelNomeTatica.Size = new Size(450, 28);
            labelNomeTatica.TabIndex = 0;
            labelNomeTatica.Text = "Nova tática";
            labelNomeTatica.UseMnemonic = false;
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
            // panelSequencia
            // 
            panelSequencia.BackColor = Color.White;
            panelSequencia.Controls.Add(layoutSequencia);
            panelSequencia.Dock = DockStyle.Bottom;
            panelSequencia.Location = new Point(0, 643);
            panelSequencia.Name = "panelSequencia";
            panelSequencia.Padding = new Padding(12, 9, 12, 9);
            panelSequencia.Size = new Size(842, 170);
            panelSequencia.TabIndex = 4;
            // 
            // layoutSequencia
            // 
            layoutSequencia.ColumnCount = 1;
            layoutSequencia.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutSequencia.Controls.Add(cabecalhoSequencia, 0, 0);
            layoutSequencia.Controls.Add(flowFases, 0, 1);
            layoutSequencia.Dock = DockStyle.Fill;
            layoutSequencia.Location = new Point(12, 9);
            layoutSequencia.Margin = new Padding(0);
            layoutSequencia.Name = "layoutSequencia";
            layoutSequencia.RowCount = 2;
            layoutSequencia.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            layoutSequencia.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutSequencia.Size = new Size(818, 152);
            layoutSequencia.TabIndex = 0;
            // 
            // cabecalhoSequencia
            // 
            cabecalhoSequencia.ColumnCount = 2;
            cabecalhoSequencia.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            cabecalhoSequencia.ColumnStyles.Add(new ColumnStyle());
            cabecalhoSequencia.Controls.Add(titulosSequencia, 0, 0);
            cabecalhoSequencia.Controls.Add(acoesSequencia, 1, 0);
            cabecalhoSequencia.Dock = DockStyle.Fill;
            cabecalhoSequencia.Location = new Point(0, 0);
            cabecalhoSequencia.Margin = new Padding(0);
            cabecalhoSequencia.Name = "cabecalhoSequencia";
            cabecalhoSequencia.RowCount = 1;
            cabecalhoSequencia.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            cabecalhoSequencia.Size = new Size(818, 45);
            cabecalhoSequencia.TabIndex = 0;
            // 
            // titulosSequencia
            // 
            titulosSequencia.Controls.Add(labelTituloSequencia);
            titulosSequencia.Controls.Add(labelResumoSequencia);
            titulosSequencia.Dock = DockStyle.Fill;
            titulosSequencia.Location = new Point(0, 0);
            titulosSequencia.Margin = new Padding(0);
            titulosSequencia.Name = "titulosSequencia";
            titulosSequencia.Size = new Size(430, 45);
            titulosSequencia.TabIndex = 0;
            // 
            // labelTituloSequencia
            // 
            labelTituloSequencia.Dock = DockStyle.Top;
            labelTituloSequencia.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            labelTituloSequencia.ForeColor = Color.FromArgb(25, 35, 58);
            labelTituloSequencia.Location = new Point(0, 0);
            labelTituloSequencia.Name = "labelTituloSequencia";
            labelTituloSequencia.Size = new Size(430, 20);
            labelTituloSequencia.TabIndex = 0;
            labelTituloSequencia.Text = "SEQUÊNCIA TÁTICA DO RALLY";
            // 
            // labelResumoSequencia
            // 
            labelResumoSequencia.AutoEllipsis = true;
            labelResumoSequencia.Dock = DockStyle.Bottom;
            labelResumoSequencia.Font = new Font("Segoe UI", 7.5F);
            labelResumoSequencia.ForeColor = Color.FromArgb(126, 144, 171);
            labelResumoSequencia.Location = new Point(0, 25);
            labelResumoSequencia.Name = "labelResumoSequencia";
            labelResumoSequencia.Size = new Size(430, 20);
            labelResumoSequencia.TabIndex = 1;
            labelResumoSequencia.Text = "1 fase · Fase 01 selecionada";
            // 
            // acoesSequencia
            // 
            acoesSequencia.AutoSize = true;
            acoesSequencia.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            acoesSequencia.Controls.Add(btnNovaFase);
            acoesSequencia.Controls.Add(btnFaseAnterior);
            acoesSequencia.Controls.Add(btnExecutarSequencia);
            acoesSequencia.Controls.Add(btnProximaFase);
            acoesSequencia.Controls.Add(btnDuplicarFase);
            acoesSequencia.Controls.Add(btnExcluirFase);
            acoesSequencia.Dock = DockStyle.Fill;
            acoesSequencia.Location = new Point(430, 0);
            acoesSequencia.Margin = new Padding(0);
            acoesSequencia.Name = "acoesSequencia";
            acoesSequencia.Size = new Size(388, 45);
            acoesSequencia.TabIndex = 1;
            acoesSequencia.WrapContents = false;
            // 
            // btnNovaFase
            // 
            btnNovaFase.BackColor = Color.White;
            btnNovaFase.Cursor = Cursors.Hand;
            btnNovaFase.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 243);
            btnNovaFase.FlatStyle = FlatStyle.Flat;
            btnNovaFase.Font = new Font("Segoe UI", 8F);
            btnNovaFase.ForeColor = Color.FromArgb(124, 58, 237);
            btnNovaFase.Location = new Point(5, 0);
            btnNovaFase.Margin = new Padding(5, 0, 0, 0);
            btnNovaFase.Name = "btnNovaFase";
            btnNovaFase.Size = new Size(94, 29);
            btnNovaFase.TabIndex = 0;
            btnNovaFase.Text = "+ Nova fase";
            btnNovaFase.UseVisualStyleBackColor = false;
            btnNovaFase.Click += btnNovaFase_Click;
            // 
            // btnFaseAnterior
            // 
            btnFaseAnterior.BackColor = Color.White;
            btnFaseAnterior.Cursor = Cursors.Hand;
            btnFaseAnterior.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 243);
            btnFaseAnterior.FlatStyle = FlatStyle.Flat;
            btnFaseAnterior.Font = new Font("Segoe UI", 8F);
            btnFaseAnterior.ForeColor = Color.FromArgb(124, 58, 237);
            btnFaseAnterior.Location = new Point(104, 0);
            btnFaseAnterior.Margin = new Padding(5, 0, 0, 0);
            btnFaseAnterior.Name = "btnFaseAnterior";
            btnFaseAnterior.Size = new Size(29, 29);
            btnFaseAnterior.TabIndex = 1;
            btnFaseAnterior.Text = "‹";
            dicasPrancheta.SetToolTip(btnFaseAnterior, "Fase anterior");
            btnFaseAnterior.UseVisualStyleBackColor = false;
            btnFaseAnterior.Click += btnFaseAnterior_Click;
            // 
            // btnExecutarSequencia
            // 
            btnExecutarSequencia.BackColor = Color.FromArgb(124, 58, 237);
            btnExecutarSequencia.Cursor = Cursors.Hand;
            btnExecutarSequencia.Enabled = false;
            btnExecutarSequencia.FlatAppearance.BorderSize = 0;
            btnExecutarSequencia.FlatStyle = FlatStyle.Flat;
            btnExecutarSequencia.Font = new Font("Segoe UI", 8F);
            btnExecutarSequencia.ForeColor = Color.White;
            btnExecutarSequencia.Location = new Point(138, 0);
            btnExecutarSequencia.Margin = new Padding(5, 0, 0, 0);
            btnExecutarSequencia.Name = "btnExecutarSequencia";
            btnExecutarSequencia.Size = new Size(83, 29);
            btnExecutarSequencia.TabIndex = 2;
            btnExecutarSequencia.Text = "▶ Executar";
            dicasPrancheta.SetToolTip(btnExecutarSequencia, "Anima a sequência usando a duração em milissegundos de cada quadro. Clique novamente para parar.");
            btnExecutarSequencia.UseVisualStyleBackColor = false;
            btnExecutarSequencia.Click += btnExecutarSequencia_Click;
            // 
            // btnProximaFase
            // 
            btnProximaFase.BackColor = Color.White;
            btnProximaFase.Cursor = Cursors.Hand;
            btnProximaFase.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 243);
            btnProximaFase.FlatStyle = FlatStyle.Flat;
            btnProximaFase.Font = new Font("Segoe UI", 8F);
            btnProximaFase.ForeColor = Color.FromArgb(124, 58, 237);
            btnProximaFase.Location = new Point(226, 0);
            btnProximaFase.Margin = new Padding(5, 0, 0, 0);
            btnProximaFase.Name = "btnProximaFase";
            btnProximaFase.Size = new Size(29, 29);
            btnProximaFase.TabIndex = 3;
            btnProximaFase.Text = "›";
            dicasPrancheta.SetToolTip(btnProximaFase, "Próxima fase");
            btnProximaFase.UseVisualStyleBackColor = false;
            btnProximaFase.Click += btnProximaFase_Click;
            // 
            // btnDuplicarFase
            // 
            btnDuplicarFase.BackColor = Color.White;
            btnDuplicarFase.Cursor = Cursors.Hand;
            btnDuplicarFase.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 243);
            btnDuplicarFase.FlatStyle = FlatStyle.Flat;
            btnDuplicarFase.Font = new Font("Segoe UI", 8F);
            btnDuplicarFase.ForeColor = Color.FromArgb(124, 58, 237);
            btnDuplicarFase.Location = new Point(260, 0);
            btnDuplicarFase.Margin = new Padding(5, 0, 0, 0);
            btnDuplicarFase.Name = "btnDuplicarFase";
            btnDuplicarFase.Size = new Size(65, 29);
            btnDuplicarFase.TabIndex = 4;
            btnDuplicarFase.Text = "Duplicar";
            btnDuplicarFase.UseVisualStyleBackColor = false;
            btnDuplicarFase.Click += btnDuplicarFase_Click;
            // 
            // btnExcluirFase
            // 
            btnExcluirFase.BackColor = Color.White;
            btnExcluirFase.Cursor = Cursors.Hand;
            btnExcluirFase.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 243);
            btnExcluirFase.FlatStyle = FlatStyle.Flat;
            btnExcluirFase.Font = new Font("Segoe UI", 8F);
            btnExcluirFase.ForeColor = Color.FromArgb(124, 58, 237);
            btnExcluirFase.Location = new Point(330, 0);
            btnExcluirFase.Margin = new Padding(5, 0, 0, 0);
            btnExcluirFase.Name = "btnExcluirFase";
            btnExcluirFase.Size = new Size(58, 29);
            btnExcluirFase.TabIndex = 5;
            btnExcluirFase.Text = "Excluir";
            btnExcluirFase.UseVisualStyleBackColor = false;
            btnExcluirFase.Click += btnExcluirFase_Click;
            // 
            // flowFases
            // 
            flowFases.AllowDrop = true;
            flowFases.AutoScroll = true;
            flowFases.Dock = DockStyle.Fill;
            flowFases.Location = new Point(0, 45);
            flowFases.Margin = new Padding(0);
            flowFases.Name = "flowFases";
            flowFases.Padding = new Padding(0, 3, 0, 0);
            flowFases.Size = new Size(818, 107);
            flowFases.TabIndex = 1;
            dicasPrancheta.SetToolTip(flowFases, "Arraste os cartões para alterar a ordem dos quadros. Salve a tática para manter a nova sequência.");
            flowFases.WrapContents = false;
            flowFases.DragDrop += flowFases_DragDrop;
            flowFases.DragEnter += flowFases_DragOver;
            flowFases.DragOver += flowFases_DragOver;
            // 
            // panelConteudo
            // 
            panelConteudo.BackColor = SystemColors.ControlLight;
            panelConteudo.Controls.Add(panelPranchetaTatica);
            panelConteudo.Controls.Add(panelPropriedades);
            panelConteudo.Controls.Add(panelFerramentas);
            panelConteudo.Dock = DockStyle.Fill;
            panelConteudo.Location = new Point(0, 112);
            panelConteudo.Name = "panelConteudo";
            panelConteudo.Size = new Size(842, 531);
            panelConteudo.TabIndex = 5;
            // 
            // panelPranchetaTatica
            // 
            panelPranchetaTatica.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelPranchetaTatica.BackColor = Color.White;
            panelPranchetaTatica.Controls.Add(prancheta);
            panelPranchetaTatica.Location = new Point(194, 23);
            panelPranchetaTatica.Name = "panelPranchetaTatica";
            panelPranchetaTatica.Size = new Size(402, 486);
            panelPranchetaTatica.TabIndex = 3;
            // 
            // prancheta
            // 
            prancheta.Dock = DockStyle.Fill;
            prancheta.Location = new Point(0, 0);
            prancheta.Name = "prancheta";
            prancheta.Size = new Size(402, 486);
            prancheta.TabIndex = 0;
            prancheta.SelecaoAlterada += prancheta_SelecaoAlterada;
            prancheta.PosicaoAlterada += prancheta_PosicaoAlterada;
            // 
            // panelPropriedades
            // 
            panelPropriedades.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelPropriedades.AutoScroll = true;
            panelPropriedades.BackColor = Color.White;
            panelPropriedades.Controls.Add(layoutPropriedades);
            panelPropriedades.Location = new Point(616, 23);
            panelPropriedades.Name = "panelPropriedades";
            panelPropriedades.Padding = new Padding(12);
            panelPropriedades.Size = new Size(203, 486);
            panelPropriedades.TabIndex = 4;
            // 
            // layoutPropriedades
            // 
            layoutPropriedades.AutoSize = true;
            layoutPropriedades.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutPropriedades.ColumnCount = 1;
            layoutPropriedades.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPropriedades.Controls.Add(labelTituloPropriedades, 0, 0);
            layoutPropriedades.Controls.Add(labelResumoElemento, 0, 1);
            layoutPropriedades.Controls.Add(layoutDadosTatica, 0, 2);
            layoutPropriedades.Controls.Add(layoutCamposAtleta, 0, 3);
            layoutPropriedades.Controls.Add(labelTituloCoordenadas, 0, 4);
            layoutPropriedades.Controls.Add(layoutCoordenadas, 0, 5);
            layoutPropriedades.Controls.Add(btnAplicarPropriedades, 0, 6);
            layoutPropriedades.Controls.Add(btnRemoverElemento, 0, 7);
            layoutPropriedades.Controls.Add(labelMensagemPropriedades, 0, 8);
            layoutPropriedades.Dock = DockStyle.Top;
            layoutPropriedades.Location = new Point(12, 12);
            layoutPropriedades.Margin = new Padding(0);
            layoutPropriedades.Name = "layoutPropriedades";
            layoutPropriedades.RowCount = 9;
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.Size = new Size(162, 869);
            layoutPropriedades.TabIndex = 0;
            // 
            // labelTituloPropriedades
            // 
            labelTituloPropriedades.AutoSize = true;
            labelTituloPropriedades.Dock = DockStyle.Top;
            labelTituloPropriedades.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelTituloPropriedades.ForeColor = Color.FromArgb(25, 35, 58);
            labelTituloPropriedades.Location = new Point(0, 0);
            labelTituloPropriedades.Margin = new Padding(0, 0, 0, 9);
            labelTituloPropriedades.Name = "labelTituloPropriedades";
            labelTituloPropriedades.Size = new Size(162, 15);
            labelTituloPropriedades.TabIndex = 0;
            labelTituloPropriedades.Text = "PROPRIEDADES";
            // 
            // labelResumoElemento
            // 
            labelResumoElemento.Dock = DockStyle.Top;
            labelResumoElemento.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelResumoElemento.ForeColor = Color.FromArgb(124, 58, 237);
            labelResumoElemento.Location = new Point(0, 24);
            labelResumoElemento.Margin = new Padding(0, 0, 0, 9);
            labelResumoElemento.Name = "labelResumoElemento";
            labelResumoElemento.Size = new Size(162, 56);
            labelResumoElemento.TabIndex = 1;
            labelResumoElemento.Text = "TÁTICA E FASE\nFase 01";
            labelResumoElemento.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // layoutDadosTatica
            // 
            layoutDadosTatica.AutoSize = true;
            layoutDadosTatica.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutDadosTatica.ColumnCount = 1;
            layoutDadosTatica.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutDadosTatica.Controls.Add(labelNomeTaticaPropriedade, 0, 0);
            layoutDadosTatica.Controls.Add(textNomeTatica, 0, 1);
            layoutDadosTatica.Controls.Add(labelEquipeTatica, 0, 2);
            layoutDadosTatica.Controls.Add(comboEquipeTatica, 0, 3);
            layoutDadosTatica.Controls.Add(labelTipoTatica, 0, 4);
            layoutDadosTatica.Controls.Add(comboTipoTatica, 0, 5);
            layoutDadosTatica.Controls.Add(labelSistemaTatica, 0, 6);
            layoutDadosTatica.Controls.Add(comboSistemaTatica, 0, 7);
            layoutDadosTatica.Controls.Add(labelDescricaoTatica, 0, 8);
            layoutDadosTatica.Controls.Add(textDescricaoTatica, 0, 9);
            layoutDadosTatica.Controls.Add(labelDadosFase, 0, 10);
            layoutDadosTatica.Controls.Add(labelNomeFase, 0, 11);
            layoutDadosTatica.Controls.Add(textNomeFase, 0, 12);
            layoutDadosTatica.Controls.Add(layoutTempoFase, 0, 13);
            layoutDadosTatica.Dock = DockStyle.Top;
            layoutDadosTatica.Font = new Font("Segoe UI", 8.5F);
            layoutDadosTatica.Location = new Point(0, 89);
            layoutDadosTatica.Margin = new Padding(0, 0, 0, 9);
            layoutDadosTatica.Name = "layoutDadosTatica";
            layoutDadosTatica.RowCount = 14;
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.RowStyles.Add(new RowStyle());
            layoutDadosTatica.Size = new Size(162, 401);
            layoutDadosTatica.TabIndex = 2;
            // 
            // labelNomeTaticaPropriedade
            // 
            labelNomeTaticaPropriedade.AutoSize = true;
            labelNomeTaticaPropriedade.Dock = DockStyle.Top;
            labelNomeTaticaPropriedade.Location = new Point(0, 0);
            labelNomeTaticaPropriedade.Margin = new Padding(0, 0, 0, 3);
            labelNomeTaticaPropriedade.Name = "labelNomeTaticaPropriedade";
            labelNomeTaticaPropriedade.Size = new Size(162, 15);
            labelNomeTaticaPropriedade.TabIndex = 0;
            labelNomeTaticaPropriedade.Text = "Nome da tática";
            // 
            // textNomeTatica
            // 
            textNomeTatica.Dock = DockStyle.Top;
            textNomeTatica.Location = new Point(0, 18);
            textNomeTatica.Margin = new Padding(0, 0, 0, 8);
            textNomeTatica.MaxLength = 100;
            textNomeTatica.Name = "textNomeTatica";
            textNomeTatica.Size = new Size(162, 23);
            textNomeTatica.TabIndex = 0;
            // 
            // labelEquipeTatica
            // 
            labelEquipeTatica.AutoSize = true;
            labelEquipeTatica.Dock = DockStyle.Top;
            labelEquipeTatica.Location = new Point(0, 49);
            labelEquipeTatica.Margin = new Padding(0, 0, 0, 3);
            labelEquipeTatica.Name = "labelEquipeTatica";
            labelEquipeTatica.Size = new Size(162, 15);
            labelEquipeTatica.TabIndex = 1;
            labelEquipeTatica.Text = "Equipe";
            // 
            // comboEquipeTatica
            // 
            comboEquipeTatica.Dock = DockStyle.Top;
            comboEquipeTatica.DropDownStyle = ComboBoxStyle.DropDownList;
            comboEquipeTatica.Location = new Point(0, 67);
            comboEquipeTatica.Margin = new Padding(0, 0, 0, 8);
            comboEquipeTatica.Name = "comboEquipeTatica";
            comboEquipeTatica.Size = new Size(162, 21);
            comboEquipeTatica.TabIndex = 1;
            comboEquipeTatica.SelectedIndexChanged += comboEquipeTatica_SelectedIndexChanged;
            // 
            // labelTipoTatica
            // 
            labelTipoTatica.AutoSize = true;
            labelTipoTatica.Dock = DockStyle.Top;
            labelTipoTatica.Location = new Point(0, 96);
            labelTipoTatica.Margin = new Padding(0, 0, 0, 3);
            labelTipoTatica.Name = "labelTipoTatica";
            labelTipoTatica.Size = new Size(162, 15);
            labelTipoTatica.TabIndex = 2;
            labelTipoTatica.Text = "Tipo de tática";
            // 
            // comboTipoTatica
            // 
            comboTipoTatica.Dock = DockStyle.Top;
            comboTipoTatica.DropDownStyle = ComboBoxStyle.DropDownList;
            comboTipoTatica.Location = new Point(0, 114);
            comboTipoTatica.Margin = new Padding(0, 0, 0, 8);
            comboTipoTatica.Name = "comboTipoTatica";
            comboTipoTatica.Size = new Size(162, 21);
            comboTipoTatica.TabIndex = 2;
            // 
            // labelSistemaTatica
            // 
            labelSistemaTatica.AutoSize = true;
            labelSistemaTatica.Dock = DockStyle.Top;
            labelSistemaTatica.Location = new Point(0, 143);
            labelSistemaTatica.Margin = new Padding(0, 0, 0, 3);
            labelSistemaTatica.Name = "labelSistemaTatica";
            labelSistemaTatica.Size = new Size(162, 15);
            labelSistemaTatica.TabIndex = 3;
            labelSistemaTatica.Text = "Sistema de jogo";
            // 
            // comboSistemaTatica
            // 
            comboSistemaTatica.Dock = DockStyle.Top;
            comboSistemaTatica.DropDownStyle = ComboBoxStyle.DropDownList;
            comboSistemaTatica.Location = new Point(0, 161);
            comboSistemaTatica.Margin = new Padding(0, 0, 0, 8);
            comboSistemaTatica.Name = "comboSistemaTatica";
            comboSistemaTatica.Size = new Size(162, 21);
            comboSistemaTatica.TabIndex = 3;
            // 
            // labelDescricaoTatica
            // 
            labelDescricaoTatica.AutoSize = true;
            labelDescricaoTatica.Dock = DockStyle.Top;
            labelDescricaoTatica.Location = new Point(0, 190);
            labelDescricaoTatica.Margin = new Padding(0, 0, 0, 3);
            labelDescricaoTatica.Name = "labelDescricaoTatica";
            labelDescricaoTatica.Size = new Size(162, 15);
            labelDescricaoTatica.TabIndex = 4;
            labelDescricaoTatica.Text = "Descrição";
            // 
            // textDescricaoTatica
            // 
            textDescricaoTatica.Dock = DockStyle.Top;
            textDescricaoTatica.Location = new Point(0, 208);
            textDescricaoTatica.Margin = new Padding(0, 0, 0, 10);
            textDescricaoTatica.MaxLength = 65535;
            textDescricaoTatica.Multiline = true;
            textDescricaoTatica.Name = "textDescricaoTatica";
            textDescricaoTatica.ScrollBars = ScrollBars.Vertical;
            textDescricaoTatica.Size = new Size(162, 55);
            textDescricaoTatica.TabIndex = 4;
            // 
            // labelDadosFase
            // 
            labelDadosFase.AutoSize = true;
            labelDadosFase.Dock = DockStyle.Top;
            labelDadosFase.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            labelDadosFase.ForeColor = Color.FromArgb(124, 58, 237);
            labelDadosFase.Location = new Point(0, 273);
            labelDadosFase.Margin = new Padding(0, 0, 0, 8);
            labelDadosFase.Name = "labelDadosFase";
            labelDadosFase.Size = new Size(162, 15);
            labelDadosFase.TabIndex = 5;
            labelDadosFase.Text = "FASE SELECIONADA";
            // 
            // labelNomeFase
            // 
            labelNomeFase.AutoSize = true;
            labelNomeFase.Dock = DockStyle.Top;
            labelNomeFase.Location = new Point(0, 296);
            labelNomeFase.Margin = new Padding(0, 0, 0, 3);
            labelNomeFase.Name = "labelNomeFase";
            labelNomeFase.Size = new Size(162, 15);
            labelNomeFase.TabIndex = 6;
            labelNomeFase.Text = "Nome da fase";
            // 
            // textNomeFase
            // 
            textNomeFase.Dock = DockStyle.Top;
            textNomeFase.Location = new Point(0, 314);
            textNomeFase.Margin = new Padding(0, 0, 0, 8);
            textNomeFase.MaxLength = 100;
            textNomeFase.Name = "textNomeFase";
            textNomeFase.Size = new Size(162, 23);
            textNomeFase.TabIndex = 5;
            // 
            // layoutTempoFase
            // 
            layoutTempoFase.AutoSize = true;
            layoutTempoFase.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutTempoFase.ColumnCount = 2;
            layoutTempoFase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutTempoFase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutTempoFase.Controls.Add(labelDuracaoFase, 0, 0);
            layoutTempoFase.Controls.Add(labelRotacaoFase, 1, 0);
            layoutTempoFase.Controls.Add(numericDuracaoFase, 0, 1);
            layoutTempoFase.Controls.Add(comboRotacaoFase, 1, 1);
            layoutTempoFase.Dock = DockStyle.Top;
            layoutTempoFase.Location = new Point(0, 345);
            layoutTempoFase.Margin = new Padding(0);
            layoutTempoFase.Name = "layoutTempoFase";
            layoutTempoFase.RowCount = 2;
            layoutTempoFase.RowStyles.Add(new RowStyle());
            layoutTempoFase.RowStyles.Add(new RowStyle());
            layoutTempoFase.Size = new Size(162, 56);
            layoutTempoFase.TabIndex = 6;
            // 
            // labelDuracaoFase
            // 
            labelDuracaoFase.AutoSize = true;
            labelDuracaoFase.Dock = DockStyle.Top;
            labelDuracaoFase.Location = new Point(0, 0);
            labelDuracaoFase.Margin = new Padding(0, 0, 5, 3);
            labelDuracaoFase.Name = "labelDuracaoFase";
            labelDuracaoFase.Size = new Size(76, 30);
            labelDuracaoFase.TabIndex = 0;
            labelDuracaoFase.Text = "Duração (ms)";
            // 
            // labelRotacaoFase
            // 
            labelRotacaoFase.AutoSize = true;
            labelRotacaoFase.Dock = DockStyle.Top;
            labelRotacaoFase.Location = new Point(81, 0);
            labelRotacaoFase.Margin = new Padding(0, 0, 0, 3);
            labelRotacaoFase.Name = "labelRotacaoFase";
            labelRotacaoFase.Size = new Size(81, 15);
            labelRotacaoFase.TabIndex = 1;
            labelRotacaoFase.Text = "Rotação";
            // 
            // numericDuracaoFase
            // 
            numericDuracaoFase.Dock = DockStyle.Top;
            numericDuracaoFase.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numericDuracaoFase.Location = new Point(0, 33);
            numericDuracaoFase.Margin = new Padding(0, 0, 5, 0);
            numericDuracaoFase.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numericDuracaoFase.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericDuracaoFase.Name = "numericDuracaoFase";
            numericDuracaoFase.Size = new Size(76, 23);
            numericDuracaoFase.TabIndex = 0;
            dicasPrancheta.SetToolTip(numericDuracaoFase, "Tempo para ir deste quadro ao próximo. 1000 ms = 1 segundo.");
            numericDuracaoFase.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // comboRotacaoFase
            // 
            comboRotacaoFase.Dock = DockStyle.Top;
            comboRotacaoFase.DropDownStyle = ComboBoxStyle.DropDownList;
            comboRotacaoFase.Items.AddRange(new object[] { "Nenhuma", "P1", "P2", "P3", "P4", "P5", "P6" });
            comboRotacaoFase.Location = new Point(81, 33);
            comboRotacaoFase.Margin = new Padding(0);
            comboRotacaoFase.Name = "comboRotacaoFase";
            comboRotacaoFase.Size = new Size(81, 21);
            comboRotacaoFase.TabIndex = 1;
            // 
            // layoutCamposAtleta
            // 
            layoutCamposAtleta.AutoSize = true;
            layoutCamposAtleta.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutCamposAtleta.ColumnCount = 1;
            layoutCamposAtleta.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutCamposAtleta.Controls.Add(labelAtletaVinculado, 0, 0);
            layoutCamposAtleta.Controls.Add(comboAtletaVinculado, 0, 1);
            layoutCamposAtleta.Controls.Add(labelNumeroCamisa, 0, 2);
            layoutCamposAtleta.Controls.Add(textNumeroCamisa, 0, 3);
            layoutCamposAtleta.Controls.Add(labelLadoAtleta, 0, 4);
            layoutCamposAtleta.Controls.Add(comboLadoAtleta, 0, 5);
            layoutCamposAtleta.Dock = DockStyle.Top;
            layoutCamposAtleta.Location = new Point(0, 499);
            layoutCamposAtleta.Margin = new Padding(0, 0, 0, 9);
            layoutCamposAtleta.Name = "layoutCamposAtleta";
            layoutCamposAtleta.RowCount = 6;
            layoutCamposAtleta.RowStyles.Add(new RowStyle());
            layoutCamposAtleta.RowStyles.Add(new RowStyle());
            layoutCamposAtleta.RowStyles.Add(new RowStyle());
            layoutCamposAtleta.RowStyles.Add(new RowStyle());
            layoutCamposAtleta.RowStyles.Add(new RowStyle());
            layoutCamposAtleta.RowStyles.Add(new RowStyle());
            layoutCamposAtleta.Size = new Size(162, 155);
            layoutCamposAtleta.TabIndex = 2;
            // 
            // labelAtletaVinculado
            // 
            labelAtletaVinculado.AutoSize = true;
            labelAtletaVinculado.Dock = DockStyle.Top;
            labelAtletaVinculado.Font = new Font("Segoe UI", 7.5F);
            labelAtletaVinculado.ForeColor = Color.FromArgb(25, 35, 58);
            labelAtletaVinculado.Location = new Point(0, 0);
            labelAtletaVinculado.Margin = new Padding(0, 0, 0, 9);
            labelAtletaVinculado.Name = "labelAtletaVinculado";
            labelAtletaVinculado.Size = new Size(162, 12);
            labelAtletaVinculado.TabIndex = 0;
            labelAtletaVinculado.Text = "ATLETA VINCULADO";
            // 
            // comboAtletaVinculado
            // 
            comboAtletaVinculado.BackColor = Color.FromArgb(247, 248, 252);
            comboAtletaVinculado.DisplayMember = "Nome";
            comboAtletaVinculado.Dock = DockStyle.Top;
            comboAtletaVinculado.DropDownStyle = ComboBoxStyle.DropDownList;
            comboAtletaVinculado.Font = new Font("Segoe UI", 8.5F);
            comboAtletaVinculado.Location = new Point(0, 21);
            comboAtletaVinculado.Margin = new Padding(0, 0, 0, 9);
            comboAtletaVinculado.Name = "comboAtletaVinculado";
            comboAtletaVinculado.Size = new Size(162, 21);
            comboAtletaVinculado.TabIndex = 0;
            comboAtletaVinculado.SelectedIndexChanged += comboAtletaVinculado_SelectedIndexChanged;
            // 
            // labelNumeroCamisa
            // 
            labelNumeroCamisa.AutoSize = true;
            labelNumeroCamisa.Dock = DockStyle.Top;
            labelNumeroCamisa.Font = new Font("Segoe UI", 7.5F);
            labelNumeroCamisa.ForeColor = Color.FromArgb(25, 35, 58);
            labelNumeroCamisa.Location = new Point(0, 51);
            labelNumeroCamisa.Margin = new Padding(0, 0, 0, 9);
            labelNumeroCamisa.Name = "labelNumeroCamisa";
            labelNumeroCamisa.Size = new Size(162, 12);
            labelNumeroCamisa.TabIndex = 1;
            labelNumeroCamisa.Text = "NÚMERO DA CAMISA";
            // 
            // textNumeroCamisa
            // 
            textNumeroCamisa.BackColor = Color.FromArgb(247, 248, 252);
            textNumeroCamisa.Dock = DockStyle.Top;
            textNumeroCamisa.Font = new Font("Segoe UI", 9F);
            textNumeroCamisa.Location = new Point(0, 72);
            textNumeroCamisa.Margin = new Padding(0, 0, 0, 9);
            textNumeroCamisa.Name = "textNumeroCamisa";
            textNumeroCamisa.Size = new Size(162, 23);
            textNumeroCamisa.TabIndex = 1;
            // 
            // labelLadoAtleta
            // 
            labelLadoAtleta.AutoSize = true;
            labelLadoAtleta.Dock = DockStyle.Top;
            labelLadoAtleta.Font = new Font("Segoe UI", 7.5F);
            labelLadoAtleta.ForeColor = Color.FromArgb(25, 35, 58);
            labelLadoAtleta.Location = new Point(0, 104);
            labelLadoAtleta.Margin = new Padding(0, 0, 0, 9);
            labelLadoAtleta.Name = "labelLadoAtleta";
            labelLadoAtleta.Size = new Size(162, 12);
            labelLadoAtleta.TabIndex = 2;
            labelLadoAtleta.Text = "LADO DO ATLETA";
            // 
            // comboLadoAtleta
            // 
            comboLadoAtleta.AccessibleName = "Lado do atleta";
            comboLadoAtleta.BackColor = Color.FromArgb(247, 248, 252);
            comboLadoAtleta.Dock = DockStyle.Top;
            comboLadoAtleta.DropDownStyle = ComboBoxStyle.DropDownList;
            comboLadoAtleta.Font = new Font("Segoe UI", 8.5F);
            comboLadoAtleta.Items.AddRange(new object[] { "Principal", "Adversário" });
            comboLadoAtleta.Location = new Point(0, 125);
            comboLadoAtleta.Margin = new Padding(0, 0, 0, 9);
            comboLadoAtleta.Name = "comboLadoAtleta";
            comboLadoAtleta.Size = new Size(162, 21);
            comboLadoAtleta.TabIndex = 2;
            // 
            // labelTituloCoordenadas
            // 
            labelTituloCoordenadas.AutoSize = true;
            labelTituloCoordenadas.Dock = DockStyle.Top;
            labelTituloCoordenadas.Font = new Font("Segoe UI", 7.5F);
            labelTituloCoordenadas.ForeColor = Color.FromArgb(25, 35, 58);
            labelTituloCoordenadas.Location = new Point(0, 663);
            labelTituloCoordenadas.Margin = new Padding(0, 0, 0, 9);
            labelTituloCoordenadas.Name = "labelTituloCoordenadas";
            labelTituloCoordenadas.Size = new Size(162, 12);
            labelTituloCoordenadas.TabIndex = 3;
            labelTituloCoordenadas.Text = "COORDENADAS NA QUADRA";
            // 
            // layoutCoordenadas
            // 
            layoutCoordenadas.AutoSize = true;
            layoutCoordenadas.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            layoutCoordenadas.ColumnCount = 2;
            layoutCoordenadas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutCoordenadas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutCoordenadas.Controls.Add(labelCoordenadaX, 0, 0);
            layoutCoordenadas.Controls.Add(labelCoordenadaY, 1, 0);
            layoutCoordenadas.Controls.Add(numericCoordenadaX, 0, 1);
            layoutCoordenadas.Controls.Add(numericCoordenadaY, 1, 1);
            layoutCoordenadas.Dock = DockStyle.Top;
            layoutCoordenadas.Enabled = false;
            layoutCoordenadas.Location = new Point(0, 684);
            layoutCoordenadas.Margin = new Padding(0, 0, 0, 9);
            layoutCoordenadas.Name = "layoutCoordenadas";
            layoutCoordenadas.RowCount = 2;
            layoutCoordenadas.RowStyles.Add(new RowStyle());
            layoutCoordenadas.RowStyles.Add(new RowStyle());
            layoutCoordenadas.Size = new Size(162, 44);
            layoutCoordenadas.TabIndex = 4;
            // 
            // labelCoordenadaX
            // 
            labelCoordenadaX.AutoSize = true;
            labelCoordenadaX.Dock = DockStyle.Top;
            labelCoordenadaX.Font = new Font("Segoe UI", 7.5F);
            labelCoordenadaX.ForeColor = Color.FromArgb(25, 35, 58);
            labelCoordenadaX.Location = new Point(0, 0);
            labelCoordenadaX.Margin = new Padding(0, 0, 0, 9);
            labelCoordenadaX.Name = "labelCoordenadaX";
            labelCoordenadaX.Size = new Size(81, 12);
            labelCoordenadaX.TabIndex = 0;
            labelCoordenadaX.Text = "X (%)";
            // 
            // labelCoordenadaY
            // 
            labelCoordenadaY.AutoSize = true;
            labelCoordenadaY.Dock = DockStyle.Top;
            labelCoordenadaY.Font = new Font("Segoe UI", 7.5F);
            labelCoordenadaY.ForeColor = Color.FromArgb(25, 35, 58);
            labelCoordenadaY.Location = new Point(81, 0);
            labelCoordenadaY.Margin = new Padding(0, 0, 0, 9);
            labelCoordenadaY.Name = "labelCoordenadaY";
            labelCoordenadaY.Size = new Size(81, 12);
            labelCoordenadaY.TabIndex = 1;
            labelCoordenadaY.Text = "Y (%)";
            // 
            // numericCoordenadaX
            // 
            numericCoordenadaX.BackColor = Color.FromArgb(247, 248, 252);
            numericCoordenadaX.DecimalPlaces = 1;
            numericCoordenadaX.Dock = DockStyle.Fill;
            numericCoordenadaX.Font = new Font("Segoe UI", 8.5F);
            numericCoordenadaX.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numericCoordenadaX.Location = new Point(0, 21);
            numericCoordenadaX.Margin = new Padding(0, 0, 5, 0);
            numericCoordenadaX.Name = "numericCoordenadaX";
            numericCoordenadaX.Size = new Size(76, 23);
            numericCoordenadaX.TabIndex = 0;
            // 
            // numericCoordenadaY
            // 
            numericCoordenadaY.BackColor = Color.FromArgb(247, 248, 252);
            numericCoordenadaY.DecimalPlaces = 1;
            numericCoordenadaY.Dock = DockStyle.Fill;
            numericCoordenadaY.Font = new Font("Segoe UI", 8.5F);
            numericCoordenadaY.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numericCoordenadaY.Location = new Point(81, 21);
            numericCoordenadaY.Margin = new Padding(0, 0, 5, 0);
            numericCoordenadaY.Name = "numericCoordenadaY";
            numericCoordenadaY.Size = new Size(76, 23);
            numericCoordenadaY.TabIndex = 1;
            // 
            // btnAplicarPropriedades
            // 
            btnAplicarPropriedades.BackColor = Color.FromArgb(124, 58, 237);
            btnAplicarPropriedades.Cursor = Cursors.Hand;
            btnAplicarPropriedades.Dock = DockStyle.Top;
            btnAplicarPropriedades.Enabled = false;
            btnAplicarPropriedades.FlatAppearance.BorderSize = 0;
            btnAplicarPropriedades.FlatStyle = FlatStyle.Flat;
            btnAplicarPropriedades.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnAplicarPropriedades.ForeColor = Color.White;
            btnAplicarPropriedades.Location = new Point(0, 737);
            btnAplicarPropriedades.Margin = new Padding(0, 0, 0, 9);
            btnAplicarPropriedades.Name = "btnAplicarPropriedades";
            btnAplicarPropriedades.Size = new Size(162, 33);
            btnAplicarPropriedades.TabIndex = 5;
            btnAplicarPropriedades.Text = "Aplicar alterações";
            btnAplicarPropriedades.UseVisualStyleBackColor = false;
            btnAplicarPropriedades.Click += btnAplicarPropriedades_Click;
            // 
            // btnRemoverElemento
            // 
            btnRemoverElemento.BackColor = Color.White;
            btnRemoverElemento.Cursor = Cursors.Hand;
            btnRemoverElemento.Dock = DockStyle.Top;
            btnRemoverElemento.Enabled = false;
            btnRemoverElemento.FlatAppearance.BorderSize = 0;
            btnRemoverElemento.FlatStyle = FlatStyle.Flat;
            btnRemoverElemento.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnRemoverElemento.ForeColor = Color.FromArgb(231, 46, 89);
            btnRemoverElemento.Location = new Point(0, 779);
            btnRemoverElemento.Margin = new Padding(0, 0, 0, 9);
            btnRemoverElemento.Name = "btnRemoverElemento";
            btnRemoverElemento.Size = new Size(162, 33);
            btnRemoverElemento.TabIndex = 6;
            btnRemoverElemento.Text = "Remover da quadra";
            btnRemoverElemento.UseVisualStyleBackColor = false;
            btnRemoverElemento.Click += btnRemoverElemento_Click;
            // 
            // labelMensagemPropriedades
            // 
            labelMensagemPropriedades.AutoSize = true;
            labelMensagemPropriedades.Dock = DockStyle.Top;
            labelMensagemPropriedades.Font = new Font("Segoe UI", 8F);
            labelMensagemPropriedades.ForeColor = Color.FromArgb(126, 144, 171);
            labelMensagemPropriedades.Location = new Point(0, 821);
            labelMensagemPropriedades.Margin = new Padding(0, 0, 0, 9);
            labelMensagemPropriedades.MaximumSize = new Size(160, 0);
            labelMensagemPropriedades.Name = "labelMensagemPropriedades";
            labelMensagemPropriedades.Size = new Size(160, 39);
            labelMensagemPropriedades.TabIndex = 7;
            labelMensagemPropriedades.Text = "Clique em um atleta ou em uma bola para ver suas propriedades.";
            // 
            // panelFerramentas
            // 
            panelFerramentas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panelFerramentas.BackColor = Color.White;
            panelFerramentas.Controls.Add(btnAdicionarAtleta);
            panelFerramentas.Controls.Add(btnAdicionarBola);
            panelFerramentas.Controls.Add(panelTituloFerramentas);
            panelFerramentas.Location = new Point(21, 23);
            panelFerramentas.Name = "panelFerramentas";
            panelFerramentas.Size = new Size(150, 486);
            panelFerramentas.TabIndex = 2;
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
            // panelTituloFerramentas
            // 
            panelTituloFerramentas.BackColor = Color.White;
            panelTituloFerramentas.Controls.Add(labelTituloFerramentas);
            panelTituloFerramentas.Dock = DockStyle.Top;
            panelTituloFerramentas.Location = new Point(0, 0);
            panelTituloFerramentas.Name = "panelTituloFerramentas";
            panelTituloFerramentas.Size = new Size(150, 53);
            panelTituloFerramentas.TabIndex = 3;
            // 
            // labelTituloFerramentas
            // 
            labelTituloFerramentas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelTituloFerramentas.AutoSize = true;
            labelTituloFerramentas.BackColor = Color.White;
            labelTituloFerramentas.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTituloFerramentas.ForeColor = Color.FromArgb(124, 58, 237);
            labelTituloFerramentas.Location = new Point(23, 18);
            labelTituloFerramentas.Name = "labelTituloFerramentas";
            labelTituloFerramentas.Size = new Size(105, 19);
            labelTituloFerramentas.TabIndex = 1;
            labelTituloFerramentas.Text = "Ferramentas";
            // 
            // TelaPranchetaTatica
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Red;
            Controls.Add(panelConteudo);
            Controls.Add(panelSequencia);
            Controls.Add(panelCabecalho);
            Name = "TelaPranchetaTatica";
            Size = new Size(842, 813);
            Load += TelaPranchetaTatica_Load;
            panelCabecalho.ResumeLayout(false);
            panelCabecalho.PerformLayout();
            panelSequencia.ResumeLayout(false);
            layoutSequencia.ResumeLayout(false);
            cabecalhoSequencia.ResumeLayout(false);
            cabecalhoSequencia.PerformLayout();
            titulosSequencia.ResumeLayout(false);
            acoesSequencia.ResumeLayout(false);
            panelConteudo.ResumeLayout(false);
            panelPranchetaTatica.ResumeLayout(false);
            panelPropriedades.ResumeLayout(false);
            panelPropriedades.PerformLayout();
            layoutPropriedades.ResumeLayout(false);
            layoutPropriedades.PerformLayout();
            layoutDadosTatica.ResumeLayout(false);
            layoutDadosTatica.PerformLayout();
            layoutTempoFase.ResumeLayout(false);
            layoutTempoFase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericDuracaoFase).EndInit();
            layoutCamposAtleta.ResumeLayout(false);
            layoutCamposAtleta.PerformLayout();
            layoutCoordenadas.ResumeLayout(false);
            layoutCoordenadas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericCoordenadaX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericCoordenadaY).EndInit();
            panelFerramentas.ResumeLayout(false);
            panelTituloFerramentas.ResumeLayout(false);
            panelTituloFerramentas.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelCabecalho;
        private Panel panelSequencia;
        private Panel panelConteudo;
        private Panel panelPropriedades;
        private Panel panelFerramentas;
        private Panel panelPranchetaTatica;
        private Label labelStatusPrancheta;
        private System.Windows.Forms.Timer timerSequencia;
        private Label labelTitulo;
        private Button btnSalvarTatica;
        private Label labelNomeTatica;
        private Panel panelTituloFerramentas;
        private Button btnAdicionarAtleta;
        private Button btnAdicionarBola;
        private Label labelTituloFerramentas;
        private Label labelTituloPropriedades;
        private TextBox textNumeroCamisa;
        private Label labelNumeroCamisa;
        private ComboBox comboLadoAtleta;
        private Label labelLadoAtleta;
        private ComboBox comboAtletaVinculado;
        private Label labelAtletaVinculado;
        private Label labelTituloCoordenadas;
        public PranchetaControl prancheta;
        private TableLayoutPanel layoutPropriedades;
        private Label labelResumoElemento;
        private TableLayoutPanel layoutCamposAtleta;
        private TableLayoutPanel layoutDadosTatica;
        private Label labelNomeTaticaPropriedade;
        private TextBox textNomeTatica;
        private Label labelEquipeTatica;
        private ComboBox comboEquipeTatica;
        private Label labelTipoTatica;
        private ComboBox comboTipoTatica;
        private Label labelSistemaTatica;
        private ComboBox comboSistemaTatica;
        private Label labelDescricaoTatica;
        private TextBox textDescricaoTatica;
        private Label labelDadosFase;
        private Label labelNomeFase;
        private TextBox textNomeFase;
        private TableLayoutPanel layoutTempoFase;
        private Label labelDuracaoFase;
        private NumericUpDown numericDuracaoFase;
        private Label labelRotacaoFase;
        private ComboBox comboRotacaoFase;
        private TableLayoutPanel layoutCoordenadas;
        private Label labelCoordenadaX;
        private Label labelCoordenadaY;
        private NumericUpDown numericCoordenadaX;
        private NumericUpDown numericCoordenadaY;
        private Button btnAplicarPropriedades;
        private Button btnRemoverElemento;
        private Label labelMensagemPropriedades;
        private TableLayoutPanel layoutSequencia;
        private TableLayoutPanel cabecalhoSequencia;
        private Panel titulosSequencia;
        private Label labelTituloSequencia;
        private Label labelResumoSequencia;
        private FlowLayoutPanel acoesSequencia;
        private Button btnNovaFase;
        private Button btnFaseAnterior;
        private Button btnExecutarSequencia;
        private Button btnProximaFase;
        private Button btnDuplicarFase;
        private Button btnExcluirFase;
        private FlowLayoutPanel flowFases;
        private ToolTip dicasPrancheta;
    }
}
