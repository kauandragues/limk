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
            if (disposing)
            {
                _carregamentoPreferencias?.Cancel();
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
            layoutCamposAtleta.SuspendLayout();
            layoutCoordenadas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericCoordenadaX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericCoordenadaY).BeginInit();
            panelFerramentas.SuspendLayout();
            panelTituloFerramentas.SuspendLayout();
            SuspendLayout();
            // 
            // panelCabecalho
            // 
            panelCabecalho.BackColor = Color.White;
            panelCabecalho.Controls.Add(btnSalvarTatica);
            panelCabecalho.Controls.Add(labelNomeTatica);
            panelCabecalho.Controls.Add(labelTitulo);
            panelCabecalho.Dock = DockStyle.Top;
            panelCabecalho.Location = new Point(0, 0);
            panelCabecalho.Name = "panelCabecalho";
            panelCabecalho.Size = new Size(842, 86);
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
            // 
            // labelNomeTatica
            // 
            labelNomeTatica.AutoSize = true;
            labelNomeTatica.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelNomeTatica.Location = new Point(194, 34);
            labelNomeTatica.Name = "labelNomeTatica";
            labelNomeTatica.Size = new Size(333, 19);
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
            labelResumoSequencia.Text = "Prévia visual • 4 fases • Fase 02 selecionada";
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
            dicasPrancheta.SetToolTip(btnExecutarSequencia, "Execução da jogada disponível em uma próxima etapa.");
            btnExecutarSequencia.UseVisualStyleBackColor = false;
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
            flowFases.AutoScroll = true;
            flowFases.Dock = DockStyle.Fill;
            flowFases.Location = new Point(0, 45);
            flowFases.Margin = new Padding(0);
            flowFases.Name = "flowFases";
            flowFases.Padding = new Padding(0, 3, 0, 0);
            flowFases.Size = new Size(818, 107);
            flowFases.TabIndex = 1;
            flowFases.WrapContents = false;
            // 
            // panelConteudo
            // 
            panelConteudo.BackColor = SystemColors.ControlLight;
            panelConteudo.Controls.Add(panelPranchetaTatica);
            panelConteudo.Controls.Add(panelPropriedades);
            panelConteudo.Controls.Add(panelFerramentas);
            panelConteudo.Dock = DockStyle.Fill;
            panelConteudo.Location = new Point(0, 86);
            panelConteudo.Name = "panelConteudo";
            panelConteudo.Size = new Size(842, 557);
            panelConteudo.TabIndex = 5;
            // 
            // panelPranchetaTatica
            // 
            panelPranchetaTatica.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelPranchetaTatica.BackColor = Color.White;
            panelPranchetaTatica.Controls.Add(prancheta);
            panelPranchetaTatica.Location = new Point(194, 23);
            panelPranchetaTatica.Name = "panelPranchetaTatica";
            panelPranchetaTatica.Size = new Size(402, 512);
            panelPranchetaTatica.TabIndex = 3;
            //
            // prancheta
            //
            prancheta.Dock = DockStyle.Fill;
            prancheta.Location = new Point(0, 0);
            prancheta.Name = "prancheta";
            prancheta.Size = new Size(402, 512);
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
            panelPropriedades.Size = new Size(203, 512);
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
            layoutPropriedades.Controls.Add(layoutCamposAtleta, 0, 2);
            layoutPropriedades.Controls.Add(labelTituloCoordenadas, 0, 3);
            layoutPropriedades.Controls.Add(layoutCoordenadas, 0, 4);
            layoutPropriedades.Controls.Add(btnAplicarPropriedades, 0, 5);
            layoutPropriedades.Controls.Add(btnRemoverElemento, 0, 6);
            layoutPropriedades.Controls.Add(labelMensagemPropriedades, 0, 7);
            layoutPropriedades.Dock = DockStyle.Top;
            layoutPropriedades.Location = new Point(12, 12);
            layoutPropriedades.Margin = new Padding(0);
            layoutPropriedades.Name = "layoutPropriedades";
            layoutPropriedades.RowCount = 8;
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.RowStyles.Add(new RowStyle());
            layoutPropriedades.Size = new Size(179, 408);
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
            labelTituloPropriedades.Size = new Size(179, 15);
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
            labelResumoElemento.Size = new Size(179, 56);
            labelResumoElemento.TabIndex = 1;
            labelResumoElemento.Text = "NENHUM ELEMENTO\nSelecione na quadra";
            labelResumoElemento.TextAlign = ContentAlignment.MiddleLeft;
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
            layoutCamposAtleta.Location = new Point(0, 89);
            layoutCamposAtleta.Margin = new Padding(0, 0, 0, 9);
            layoutCamposAtleta.Name = "layoutCamposAtleta";
            layoutCamposAtleta.RowCount = 6;
            layoutCamposAtleta.RowStyles.Add(new RowStyle());
            layoutCamposAtleta.RowStyles.Add(new RowStyle());
            layoutCamposAtleta.RowStyles.Add(new RowStyle());
            layoutCamposAtleta.RowStyles.Add(new RowStyle());
            layoutCamposAtleta.RowStyles.Add(new RowStyle());
            layoutCamposAtleta.RowStyles.Add(new RowStyle());
            layoutCamposAtleta.Size = new Size(179, 104);
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
            labelAtletaVinculado.Size = new Size(179, 12);
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
            comboAtletaVinculado.Size = new Size(179, 21);
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
            labelNumeroCamisa.Size = new Size(179, 12);
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
            textNumeroCamisa.Size = new Size(179, 23);
            textNumeroCamisa.TabIndex = 1;
            //
            // labelLadoAtleta
            //
            labelLadoAtleta.AutoSize = true;
            labelLadoAtleta.Dock = DockStyle.Top;
            labelLadoAtleta.Font = new Font("Segoe UI", 7.5F);
            labelLadoAtleta.ForeColor = Color.FromArgb(25, 35, 58);
            labelLadoAtleta.Margin = new Padding(0, 0, 0, 9);
            labelLadoAtleta.Name = "labelLadoAtleta";
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
            comboLadoAtleta.Margin = new Padding(0, 0, 0, 9);
            comboLadoAtleta.Name = "comboLadoAtleta";
            comboLadoAtleta.TabIndex = 2;
            // 
            // labelTituloCoordenadas
            // 
            labelTituloCoordenadas.AutoSize = true;
            labelTituloCoordenadas.Dock = DockStyle.Top;
            labelTituloCoordenadas.Font = new Font("Segoe UI", 7.5F);
            labelTituloCoordenadas.ForeColor = Color.FromArgb(25, 35, 58);
            labelTituloCoordenadas.Location = new Point(0, 202);
            labelTituloCoordenadas.Margin = new Padding(0, 0, 0, 9);
            labelTituloCoordenadas.Name = "labelTituloCoordenadas";
            labelTituloCoordenadas.Size = new Size(179, 12);
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
            layoutCoordenadas.Location = new Point(0, 223);
            layoutCoordenadas.Margin = new Padding(0, 0, 0, 9);
            layoutCoordenadas.Name = "layoutCoordenadas";
            layoutCoordenadas.RowCount = 2;
            layoutCoordenadas.RowStyles.Add(new RowStyle());
            layoutCoordenadas.RowStyles.Add(new RowStyle());
            layoutCoordenadas.Size = new Size(179, 44);
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
            labelCoordenadaX.Size = new Size(89, 12);
            labelCoordenadaX.TabIndex = 0;
            labelCoordenadaX.Text = "X (%)";
            // 
            // labelCoordenadaY
            // 
            labelCoordenadaY.AutoSize = true;
            labelCoordenadaY.Dock = DockStyle.Top;
            labelCoordenadaY.Font = new Font("Segoe UI", 7.5F);
            labelCoordenadaY.ForeColor = Color.FromArgb(25, 35, 58);
            labelCoordenadaY.Location = new Point(89, 0);
            labelCoordenadaY.Margin = new Padding(0, 0, 0, 9);
            labelCoordenadaY.Name = "labelCoordenadaY";
            labelCoordenadaY.Size = new Size(90, 12);
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
            numericCoordenadaX.Size = new Size(84, 23);
            numericCoordenadaX.TabIndex = 0;
            // 
            // numericCoordenadaY
            // 
            numericCoordenadaY.BackColor = Color.FromArgb(247, 248, 252);
            numericCoordenadaY.DecimalPlaces = 1;
            numericCoordenadaY.Dock = DockStyle.Fill;
            numericCoordenadaY.Font = new Font("Segoe UI", 8.5F);
            numericCoordenadaY.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numericCoordenadaY.Location = new Point(89, 21);
            numericCoordenadaY.Margin = new Padding(0, 0, 5, 0);
            numericCoordenadaY.Name = "numericCoordenadaY";
            numericCoordenadaY.Size = new Size(85, 23);
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
            btnAplicarPropriedades.Location = new Point(0, 276);
            btnAplicarPropriedades.Margin = new Padding(0, 0, 0, 9);
            btnAplicarPropriedades.Name = "btnAplicarPropriedades";
            btnAplicarPropriedades.Size = new Size(179, 33);
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
            btnRemoverElemento.Location = new Point(0, 318);
            btnRemoverElemento.Margin = new Padding(0, 0, 0, 9);
            btnRemoverElemento.Name = "btnRemoverElemento";
            btnRemoverElemento.Size = new Size(179, 33);
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
            labelMensagemPropriedades.Location = new Point(0, 360);
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
            panelFerramentas.Size = new Size(150, 512);
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
        private CartaoFaseControl cartaoFase1;
        private CartaoFaseControl cartaoFase2;
        private CartaoFaseControl cartaoFase3;
        private CartaoFaseControl cartaoFase4;
        private ToolTip dicasPrancheta;
    }
}
