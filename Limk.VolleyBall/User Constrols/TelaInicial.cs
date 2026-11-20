using Limk.VolleyBall.Backend;
using System.ComponentModel;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaInicial : UserControl
    {
        private bool _carregando;

        public event EventHandler<UserControl>? TelaSolicitada;

        public TelaInicial()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        // A futura tela de login poderá informar o nome sem alterar o layout.
        public void DefinirUsuario(string? nomeUsuario)
        {
            string nome = string.IsNullOrWhiteSpace(nomeUsuario) ? "visitante" : nomeUsuario.Trim();
            lblBoasVindas.Text = $"Bem-vindo, {nome}!";
            btnUsuario.Text = $"Usuário: {nome} ▾";
        }

        private async void TelaInicial_Load(object? sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            await AtualizarEstatisticasAsync();
        }

        private async void btnAtualizar_Click(object? sender, EventArgs e)
        {
            await AtualizarEstatisticasAsync();
        }

        private async Task AtualizarEstatisticasAsync()
        {
            if (_carregando)
                return;

            _carregando = true;
            btnAtualizar.Enabled = false;
            lblStatusBanco.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatusBanco.Text = "Consultando o banco de dados...";

            try
            {
                ResumoInicio resumo = await DadosInicio.CarregarAsync();

                // O usuário pode abrir outra tela enquanto a consulta está em andamento.
                if (IsDisposed)
                    return;

                lblTotalEquipes.Text = resumo.Equipes.ToString("N0");
                lblTotalAtletas.Text = resumo.Atletas.ToString("N0");
                lblTotalTaticas.Text = resumo.Taticas.ToString("N0");
                lblTotalQuadros.Text = resumo.Quadros.ToString("N0");
                lblStatusBanco.Text = $"Dados do banco conectado · atualizado às {DateTime.Now:HH:mm}";
            }
            catch (Exception)
            {
                if (IsDisposed)
                    return;

                lblTotalEquipes.Text = "—";
                lblTotalAtletas.Text = "—";
                lblTotalTaticas.Text = "—";
                lblTotalQuadros.Text = "—";
                lblStatusBanco.ForeColor = Color.FromArgb(160, 70, 35);
                lblStatusBanco.Text = "Não foi possível consultar o banco. Verifique a conexão e clique em Atualizar.";
            }
            finally
            {
                _carregando = false;
                if (!IsDisposed)
                    btnAtualizar.Enabled = true;
            }
        }

        private void btnUsuario_Click(object? sender, EventArgs e)
        {
            menuUsuario.Show(btnUsuario, new Point(0, btnUsuario.Height));
        }

        private void btnPrancheta_Click(object? sender, EventArgs e)
        {
            TelaSolicitada?.Invoke(this, new TelaPranchetaTatica());
        }

        private void btnTaticas_Click(object? sender, EventArgs e)
        {
            TelaSolicitada?.Invoke(this, new TelaTaticas());
        }

        private void btnEquipes_Click(object? sender, EventArgs e)
        {
            TelaSolicitada?.Invoke(this, new TelaEquipes());
        }

        private void btnAtletas_Click(object? sender, EventArgs e)
        {
            TelaSolicitada?.Invoke(this, new TelaAtletas());
        }
    }
}
