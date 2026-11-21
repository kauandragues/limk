using Limk.VolleyBall.Backend;
using System.ComponentModel;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaConfiguracoes : UserControl, ITelaUsuario
    {
        private readonly CancellationTokenSource encerramento = new();
        private UsuarioAutenticado? usuario;
        private bool ocupado;
        private bool preferenciasCarregadas;

        public TelaConfiguracoes()
        {
            InitializeComponent();
            DefinirOcupado(false);
        }

        public void DefinirUsuario(UsuarioAutenticado usuarioAutenticado)
        {
            usuario = usuarioAutenticado;
            lblNomeUsuario.Text = usuarioAutenticado.Nome;
            lblEmailUsuario.Text = usuarioAutenticado.Email;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !Disposing && !IsDisposed)
            {
                encerramento.Cancel();
                encerramento.Dispose();
            }

            base.Dispose(disposing);
        }

        private async void TelaConfiguracoes_Load(object? sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            await CarregarAsync();
        }

        private async void btnRecarregar_Click(object? sender, EventArgs e) => await CarregarAsync();

        private async Task CarregarAsync()
        {
            if (usuario is null || ocupado)
                return;

            CancellationToken token = encerramento.Token;
            preferenciasCarregadas = false;
            btnRecarregar.Visible = false;
            DefinirOcupado(true);
            ExibirStatus("Carregando suas preferências…");

            try
            {
                PreferenciasUsuario preferencias = await DadosPreferenciasUsuario.CarregarAsync(usuario.Id, token);
                if (token.IsCancellationRequested)
                    return;

                ExibirPreferencias(preferencias);
                preferenciasCarregadas = true;
                ExibirStatus(string.Empty);
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested)
            {
            }
            catch (FalhaPreferenciasUsuarioException)
            {
                if (!token.IsCancellationRequested)
                {
                    ExibirStatus("Não foi possível carregar suas preferências. Verifique a conexão e tente novamente.", erro: true);
                    btnRecarregar.Visible = true;
                }
            }
            finally
            {
                if (!token.IsCancellationRequested)
                    DefinirOcupado(false);
            }
        }

        private async void btnSalvar_Click(object? sender, EventArgs e)
        {
            if (usuario is null || ocupado || !preferenciasCarregadas)
                return;

            var preferencias = new PreferenciasUsuario(lblCorAtletaPrincipal.Text, lblCorAtletaAdversario.Text,
                lblCorBola.Text, decimal.ToInt32(numDuracaoQuadro.Value));

            CancellationToken token = encerramento.Token;
            DefinirOcupado(true);
            ExibirStatus("Salvando suas preferências…");

            try
            {
                await DadosPreferenciasUsuario.SalvarAsync(usuario.Id, preferencias, token);
                if (!token.IsCancellationRequested)
                    ExibirStatus("Preferências salvas.", sucesso: true);
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested)
            {
            }
            catch (FalhaPreferenciasUsuarioException)
            {
                if (!token.IsCancellationRequested)
                    ExibirStatus("Não foi possível salvar suas preferências. Verifique a conexão e tente novamente.", erro: true);
            }
            finally
            {
                if (!token.IsCancellationRequested)
                {
                    DefinirOcupado(false);
                    painelRolagem.ScrollControlIntoView(lblStatus);
                }
            }
        }

        private void btnRestaurarPadrao_Click(object? sender, EventArgs e)
        {
            ExibirPreferencias(PreferenciasUsuario.Padrao);
            ExibirStatus("Valores originais restaurados no formulário. Salve para aplicar.");
        }

        private void ExibirPreferencias(PreferenciasUsuario preferencias)
        {
            ExibirCor(amostraCorAtletaPrincipal, lblCorAtletaPrincipal, preferencias.CorAtletaPrincipal);
            ExibirCor(amostraCorAtletaAdversario, lblCorAtletaAdversario, preferencias.CorAtletaAdversario);
            ExibirCor(amostraCorBola, lblCorBola, preferencias.CorBola);
            numDuracaoQuadro.Value = preferencias.DuracaoQuadroPadraoMs;
        }

        private void AlterarCor(Panel amostra, Label rotulo)
        {
            using var dialogo = new ColorDialog { Color = amostra.BackColor, FullOpen = true };
            if (dialogo.ShowDialog(this) != DialogResult.OK)
                return;

            Color cor = dialogo.Color;
            ExibirCor(amostra, rotulo, $"#{cor.R:X2}{cor.G:X2}{cor.B:X2}");
            ExibirStatus(string.Empty);
        }

        private static void ExibirCor(Panel amostra, Label rotulo, string cor)
        {
            amostra.BackColor = ColorTranslator.FromHtml(cor);
            rotulo.Text = cor;
        }

        private void DefinirOcupado(bool valor)
        {
            ocupado = valor;
            bool editar = preferenciasCarregadas && !ocupado;
            btnCorAtletaPrincipal.Enabled = editar;
            btnCorAtletaAdversario.Enabled = editar;
            btnCorBola.Enabled = editar;
            numDuracaoQuadro.Enabled = editar;
            btnRestaurarPadrao.Enabled = editar;
            btnSalvar.Enabled = editar;
            btnRecarregar.Enabled = !ocupado;
            UseWaitCursor = ocupado;
        }

        private void ExibirStatus(string texto, bool erro = false, bool sucesso = false)
        {
            lblStatus.ForeColor = erro ? Color.FromArgb(155, 39, 64)
                : sucesso ? Color.FromArgb(26, 103, 67) : CorSecundaria;
            lblStatus.Text = texto;
            lblStatus.Visible = texto.Length > 0;
        }

      

        private void AjustarLayout(object? sender, EventArgs e)
        {
            int largura = Math.Max(1, layoutConteudo.ClientSize.Width - layoutConteudo.Padding.Horizontal);
            lblTitulo.MaximumSize = new Size(largura, 0);
            lblDescricao.MaximumSize = new Size(largura, 0);

            int larguraConta = Math.Max(1, largura - layoutConta.Padding.Horizontal);
            lblNomeUsuario.MaximumSize = new Size(larguraConta, 0);
            lblEmailUsuario.MaximumSize = new Size(larguraConta, 0);

            int larguraPreferencias = Math.Max(1, largura - layoutPreferencias.Padding.Horizontal);
            lblStatus.MaximumSize = new Size(larguraPreferencias, 0);
            foreach (FlowLayoutPanel linha in layoutPreferencias.Controls.OfType<FlowLayoutPanel>())
            {
                int larguraNecessaria = linha.Controls.Cast<Control>().Where(controle => controle.Visible)
                    .Sum(controle => (controle.AutoSize ? controle.PreferredSize.Width : controle.Width) + controle.Margin.Horizontal);
                linha.WrapContents = larguraNecessaria > larguraPreferencias;
            }
        }
    }
}
