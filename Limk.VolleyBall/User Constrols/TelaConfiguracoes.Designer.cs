using System.Windows.Forms.Automation;

namespace Limk.VolleyBall.User_Constrols
{
    partial class TelaConfiguracoes
    {
        private static readonly Color CorFundo = Color.FromArgb(248, 249, 254);
        private static readonly Color CorTexto = Color.FromArgb(30, 27, 45);
        private static readonly Color CorSecundaria = Color.FromArgb(100, 116, 139);
        private static readonly Color CorRoxa = Color.FromArgb(92, 48, 154);
        private static readonly Color CorAcao = Color.FromArgb(124, 58, 237);
        private static readonly Color CorBorda = Color.FromArgb(222, 215, 236);

        private Panel painelRolagem;
        private TableLayoutPanel layoutConteudo;
        private TableLayoutPanel layoutConta;
        private TableLayoutPanel layoutPreferencias;
        private Label lblTitulo;
        private Label lblDescricao;
        private Label lblNomeUsuario;
        private Label lblEmailUsuario;
        private Panel amostraCorAtletaPrincipal;
        private Panel amostraCorAtletaAdversario;
        private Panel amostraCorBola;
        private Label lblCorAtletaPrincipal;
        private Label lblCorAtletaAdversario;
        private Label lblCorBola;
        private Button btnCorAtletaPrincipal;
        private Button btnCorAtletaAdversario;
        private Button btnCorBola;
        private NumericUpDown numDuracaoQuadro;
        private Button btnSalvar;
        private Button btnRestaurarPadrao;
        private Button btnRecarregar;
        private Label lblStatus;

        private void InitializeComponent()
        {
            SuspendLayout();

            painelRolagem = new Panel { AutoScroll = true, Dock = DockStyle.Fill };
            layoutConteudo = CriarColuna();
            layoutConteudo.Padding = new Padding(16);

            lblTitulo = CriarRotulo("Configurações", new Padding(0, 0, 0, 4));
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Name = "lblTitulo";
            lblDescricao = CriarRotulo("Escolha as cores dos elementos e a duração padrão das novas fases.", new Padding(0, 0, 0, 16));
            lblDescricao.ForeColor = CorSecundaria;

            layoutConta = CriarCartao();
            lblNomeUsuario = CriarRotulo("", new Padding(0, 0, 0, 3));
            lblNomeUsuario.Name = "lblNomeUsuario";
            lblEmailUsuario = CriarRotulo("", Padding.Empty);
            lblEmailUsuario.Name = "lblEmailUsuario";
            lblEmailUsuario.ForeColor = CorSecundaria;
            AdicionarLinhas(layoutConta, CriarTituloCartao("Sua conta"), lblNomeUsuario, lblEmailUsuario);

            layoutPreferencias = CriarCartao();
            layoutPreferencias.Margin = Padding.Empty;
            amostraCorAtletaPrincipal = CriarAmostra();
            amostraCorAtletaAdversario = CriarAmostra();
            amostraCorBola = CriarAmostra();
            lblCorAtletaPrincipal = CriarHexadecimal("lblCorAtletaPrincipal");
            lblCorAtletaAdversario = CriarHexadecimal("lblCorAtletaAdversario");
            lblCorBola = CriarHexadecimal("lblCorBola");
            btnCorAtletaPrincipal = CriarBotao("btnCorAtletaPrincipal", "Alterar");
            btnCorAtletaAdversario = CriarBotao("btnCorAtletaAdversario", "Alterar");
            btnCorBola = CriarBotao("btnCorBola", "Alterar");
            btnCorAtletaPrincipal.AccessibleName = "Alterar cor do atleta principal";
            btnCorAtletaAdversario.AccessibleName = "Alterar cor do atleta adversário";
            btnCorBola.AccessibleName = "Alterar cor da bola";
            btnCorAtletaPrincipal.Click += (_, _) => AlterarCor(amostraCorAtletaPrincipal, lblCorAtletaPrincipal);
            btnCorAtletaAdversario.Click += (_, _) => AlterarCor(amostraCorAtletaAdversario, lblCorAtletaAdversario);
            btnCorBola.Click += (_, _) => AlterarCor(amostraCorBola, lblCorBola);

            numDuracaoQuadro = new NumericUpDown
            {
                Name = "numDuracaoQuadro",
                AccessibleName = "Duração padrão do quadro em milissegundos",
                Width = 144,
                Margin = new Padding(0, 0, 0, 16),
                Minimum = 1,
                Maximum = int.MaxValue,
                Increment = 100,
                Value = 1000,
                ThousandsSeparator = true
            };
            numDuracaoQuadro.ValueChanged += (_, _) =>
            {
                if (!ocupado && preferenciasCarregadas)
                    ExibirStatus(string.Empty);
            };

            btnSalvar = CriarBotao("btnSalvar", "Salvar preferências");
            btnSalvar.BackColor = CorAcao;
            btnSalvar.ForeColor = Color.White;
            btnSalvar.FlatAppearance.BorderSize = 0;
            btnSalvar.FlatAppearance.MouseOverBackColor = Color.FromArgb(109, 40, 217);
            btnSalvar.Click += btnSalvar_Click;

            btnRestaurarPadrao = CriarBotao("btnRestaurarPadrao", "Restaurar padrões");
            btnRestaurarPadrao.Click += btnRestaurarPadrao_Click;
            btnRecarregar = CriarBotao("btnRecarregar", "Tentar novamente");
            btnRecarregar.Visible = false;
            btnRecarregar.Click += btnRecarregar_Click;

            var layoutAcoes = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                Margin = Padding.Empty,
                WrapContents = true
            };
            layoutAcoes.Controls.AddRange(new Control[] { btnSalvar, btnRestaurarPadrao, btnRecarregar });

            lblStatus = CriarRotulo("", new Padding(0, 10, 0, 0));
            lblStatus.Name = "lblStatus";
            lblStatus.LiveSetting = AutomationLiveSetting.Polite;
            lblStatus.Visible = false;

            AdicionarLinhas(layoutPreferencias, CriarTituloCartao("Preferências da prancheta"),
                CriarEditorCor("Atleta principal", amostraCorAtletaPrincipal, lblCorAtletaPrincipal, btnCorAtletaPrincipal),
                CriarEditorCor("Atleta adversário", amostraCorAtletaAdversario, lblCorAtletaAdversario, btnCorAtletaAdversario),
                CriarEditorCor("Bola", amostraCorBola, lblCorBola, btnCorBola),
                CriarRotuloCampo("Duração padrão do quadro (ms)"), numDuracaoQuadro, layoutAcoes, lblStatus);

            AdicionarLinhas(layoutConteudo, lblTitulo, lblDescricao, layoutConta, layoutPreferencias);
            layoutConteudo.SizeChanged += AjustarLayout;
            btnRecarregar.VisibleChanged += AjustarLayout;
            painelRolagem.Controls.Add(layoutConteudo);
            Controls.Add(painelRolagem);

            AccessibleName = "Configurações de usuário";
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = CorFundo;
            Font = new Font("Segoe UI", 9F);
            ForeColor = CorTexto;
            Name = "TelaConfiguracoes";
            Size = new Size(848, 740);
            Load += TelaConfiguracoes_Load;

            ResumeLayout();
        }

        private static TableLayoutPanel CriarColuna()
        {
            var layout = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                Dock = DockStyle.Top,
                Margin = Padding.Empty
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            return layout;
        }

        private static TableLayoutPanel CriarCartao()
        {
            TableLayoutPanel cartao = CriarColuna();
            cartao.BackColor = Color.White;
            cartao.Padding = new Padding(16);
            cartao.Margin = new Padding(0, 0, 0, 12);
            return cartao;
        }

        private static void AdicionarLinhas(TableLayoutPanel layout, params Control[] controles)
        {
            layout.RowCount = controles.Length;
            for (int i = 0; i < controles.Length; i++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                controles[i].TabIndex = i;
                layout.Controls.Add(controles[i], 0, i);
            }
        }

        private static Label CriarRotulo(string texto, Padding margem) => new Label
        {
            AutoSize = true,
            Dock = DockStyle.Top,
            Margin = margem,
            Text = texto,
            UseMnemonic = false
        };

        private static Label CriarTituloCartao(string texto)
        {
            Label rotulo = CriarRotulo(texto, new Padding(0, 0, 0, 9));
            rotulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            return rotulo;
        }

        private static Label CriarRotuloCampo(string texto)
        {
            Label rotulo = CriarRotulo(texto, new Padding(0, 0, 0, 5));
            rotulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            return rotulo;
        }

        private static Label CriarHexadecimal(string nome)
        {
            Label rotulo = CriarRotulo("—", new Padding(0, 0, 8, 0));
            rotulo.Name = nome;
            rotulo.Anchor = AnchorStyles.Left;
            return rotulo;
        }

        private static Panel CriarAmostra() => new Panel
        {
            Anchor = AnchorStyles.Left,
            BorderStyle = BorderStyle.FixedSingle,
            Height = 22,
            Margin = new Padding(0, 0, 8, 0),
            Width = 22
        };

        private static FlowLayoutPanel CriarEditorCor(string titulo, Panel amostra, Label hex, Button botao)
        {
            var campo = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 10)
            };

            Label rotulo = CriarRotuloCampo(titulo);
            rotulo.AutoSize = false;
            rotulo.Dock = DockStyle.None;
            rotulo.Margin = Padding.Empty;
            rotulo.Size = new Size(144, 32);
            rotulo.TextAlign = ContentAlignment.MiddleLeft;

            var escolha = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 3,
                Margin = Padding.Empty,
                RowCount = 1
            };
            escolha.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            escolha.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            escolha.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            escolha.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            escolha.Controls.Add(amostra, 0, 0);
            escolha.Controls.Add(hex, 1, 0);
            botao.Anchor = AnchorStyles.Left;
            botao.Margin = Padding.Empty;
            escolha.Controls.Add(botao, 2, 0);
            campo.Controls.Add(rotulo);
            campo.Controls.Add(escolha);
            return campo;
        }

        private static Button CriarBotao(string nome, string texto)
        {
            var botao = new Button
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = CorRoxa,
                Margin = new Padding(0, 0, 8, 4),
                MinimumSize = new Size(0, 32),
                Name = nome,
                Padding = new Padding(10, 0, 10, 0),
                Text = texto,
                UseVisualStyleBackColor = false
            };
            botao.FlatAppearance.BorderColor = CorBorda;
            botao.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 240, 255);
            return botao;
        }
    }
}
