using Limk.VolleyBall.Backend;
using System.ComponentModel;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaInicial : UserControl, ITelaUsuario
    {
        private UsuarioAutenticado? _usuario;
        private bool _carregando;
        public event EventHandler<UserControl>? TelaSolicitada;
        public TelaInicial()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        public void DefinirUsuario(UsuarioAutenticado usuario)
        {
            _usuario = usuario;
            string nome = usuario.GetNome();
            lblBoasVindas.Text = $"Bem-vindo, {nome}!";
            btnUsuario.Text = $"Usuário: {nome} ▾";
        }

        private void TelaInicial_Load(object? sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            AtualizarInicio();
        }

        private void btnAtualizar_Click(object? sender, EventArgs e)
        {
            AtualizarInicio();
        }

        private void AtualizarInicio()
        {
            if (_carregando || _usuario == null || IsDisposed)
            {
                return;
            }

            _carregando = true;
            btnAtualizar.Enabled = false;
            lblStatusBanco.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatusBanco.Text = "Consultando o banco de dados...";
            try
            {
                ResumoInicio resumo = DadosInicio.Carregar(_usuario.GetId());
                lblTotalEquipes.Text = resumo.GetEquipes().ToString("N0");
                lblTotalAtletas.Text = resumo.GetAtletas().ToString("N0");
                lblTotalTaticas.Text = resumo.GetTaticas().ToString("N0");
                lblTotalQuadros.Text = resumo.GetQuadros().ToString("N0");
                ExibirTaticas(resumo.GetUltimasTaticas());
                ExibirEquipes(resumo.GetEquipesCadastradas());
                lblStatusBanco.Text = $"Dados das suas equipes · atualizado às {DateTime.Now:HH:mm}";
            }
            catch (Exception)
            {
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
                btnAtualizar.Enabled = true;
            }
        }

        private void ExibirTaticas(List<TaticaInicio> taticas)
        {
            layoutListaTaticas.SuspendLayout();
            LimparCartoes(layoutListaTaticas);
            lblEstadoTaticas.Text = "Nenhuma tática nas equipes às quais você tem acesso.";
            lblEstadoTaticas.Visible = taticas.Count == 0;
            foreach (TaticaInicio tatica in taticas)
            {
                CartaoTaticaInicio cartao = new CartaoTaticaInicio();
                cartao.ExibirTatica(tatica);
                cartao.AbrirTatica += SolicitarAbertura;
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
            lblEstadoEquipes.Text = "Nenhuma equipe vinculada ao seu usuário.";
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
            {
                lista.Controls[0].Dispose();
            }

            lista.RowStyles.Clear();
            lista.RowCount = 0;
        }

        private void btnUsuario_Click(object? sender, EventArgs e)
        {
            menuUsuario.Show(btnUsuario, new Point(0, btnUsuario.Height));
        }

        private void itemConfiguracoes_Click(object? sender, EventArgs e)
        {
            if (TelaSolicitada != null)
            {
                TelaSolicitada(this, new TelaConfiguracoes());
            }
        }

        private void btnPrancheta_Click(object? sender, EventArgs e)
        {
            if (TelaSolicitada != null)
            {
                TelaSolicitada(this, new TelaPranchetaTatica());
            }
        }

        private void btnTaticas_Click(object? sender, EventArgs e)
        {
            if (TelaSolicitada != null)
            {
                TelaSolicitada(this, new TelaTaticas());
            }
        }

        private void btnEquipes_Click(object? sender, EventArgs e)
        {
            if (TelaSolicitada != null)
            {
                TelaSolicitada(this, new TelaEquipes());
            }
        }

        private void btnAtletas_Click(object? sender, EventArgs e)
        {
            if (TelaSolicitada != null)
            {
                TelaSolicitada(this, new TelaAtletas());
            }
        }

        private void SolicitarAbertura(object? sender, int id)
        {
            if (TelaSolicitada != null)
            {
                TelaSolicitada(this, new TelaPranchetaTatica(id));
            }
        }
    }
}
