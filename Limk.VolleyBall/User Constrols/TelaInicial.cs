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

            await AtualizarInicioAsync();
        }

        private async void btnAtualizar_Click(object? sender, EventArgs e)
        {
            await AtualizarInicioAsync();
        }

        private async Task AtualizarInicioAsync()
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
                ExibirTaticas(resumo.UltimasTaticas);
                ExibirEquipes(resumo.EquipesCadastradas);
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
                LimparCartoes(layoutListaTaticas);
                LimparCartoes(layoutListaEquipes);
                lblEstadoTaticas.Text = "Não foi possível carregar as táticas.";
                lblEstadoEquipes.Text = "Não foi possível carregar as equipes.";
                lblEstadoTaticas.Visible = true;
                lblEstadoEquipes.Visible = true;
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

        private void ExibirTaticas(List<TaticaInicio> taticas)
        {
            layoutListaTaticas.SuspendLayout();
            LimparCartoes(layoutListaTaticas);
            lblEstadoTaticas.Text = "Nenhuma tática cadastrada. Use Nova tática para abrir a prancheta.";
            lblEstadoTaticas.Visible = taticas.Count == 0;

            foreach (TaticaInicio tatica in taticas)
            {
                CartaoTaticaInicio cartao = new CartaoTaticaInicio();
                cartao.ExibirTatica(tatica);
                cartao.Dock = DockStyle.Top;
                int linha = layoutListaTaticas.RowCount;
                layoutListaTaticas.RowCount++;
                layoutListaTaticas.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                layoutListaTaticas.Controls.Add(cartao, 0, linha);
            }
            layoutListaTaticas.ResumeLayout(true);
        }

        private void ExibirEquipes(List<EquipeInicio> equipes)
        {
            layoutListaEquipes.SuspendLayout();
            LimparCartoes(layoutListaEquipes);
            lblEstadoEquipes.Text = "Nenhuma equipe cadastrada.";
            lblEstadoEquipes.Visible = equipes.Count == 0;

            foreach (EquipeInicio equipe in equipes)
            {
                CartaoEquipeInicio cartao = new CartaoEquipeInicio();
                cartao.ExibirEquipe(equipe);
                cartao.Dock = DockStyle.Top;
                int linha = layoutListaEquipes.RowCount;
                layoutListaEquipes.RowCount++;
                layoutListaEquipes.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                layoutListaEquipes.Controls.Add(cartao, 0, linha);
            }
            layoutListaEquipes.ResumeLayout(true);
        }

        private void LimparCartoes(TableLayoutPanel lista)
        {
            // Remove os cartões anteriores antes de atualizar os dados.
            while (lista.Controls.Count > 0)
                lista.Controls[0].Dispose();
            lista.RowStyles.Clear();
            lista.RowCount = 0;
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
