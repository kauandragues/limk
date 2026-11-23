using Limk.VolleyBall.Backend;
using System.ComponentModel;
using System.Data;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaEquipes : UserControl, ITelaUsuario
    {
        private UsuarioAutenticado? _usuario;
        private DataTable _equipes = new DataTable();
        private int _equipeSelecionadaId;
        private string _nomeOriginal = string.Empty;
        private byte[]? _escudoOriginal;
        private byte[]? _dadosEscudo;
        private Bitmap? _imagemEscudo;
        private bool _pronto;
        private bool _operacao;
        public event EventHandler<UserControl>? TelaSolicitada;

        public TelaEquipes()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AtualizarAcoes();
        }

        public void DefinirUsuario(UsuarioAutenticado usuario)
        {
            _usuario = usuario;
        }

        private void TelaEquipes_Load(object? sender, EventArgs e)
        {
            if (_usuario == null || _operacao || _pronto || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            _operacao = true;
            AtualizarAcoes();
            lblResumoEquipes.Text = "Carregando suas equipes...";
            try
            {
                RecarregarLista();
            }
            catch (Exception)
            {
                lblResumoEquipes.Text = "Não foi possível carregar suas equipes.";
                MostrarMensagem("Verifique a conexão e abra a tela novamente.", true);
            }
            finally
            {
                _operacao = false;
                AtualizarAcoes();
            }
        }

        private void RecarregarLista(bool preservarEdicao = false)
        {
            DataTable equipes = DadosEquipe.Listar(_usuario!.GetId());
            _equipes = equipes;
            _pronto = true;
            ExibirEquipes(preservarEdicao);
        }

        private void txtBuscaEquipe_TextChanged(object? sender, EventArgs e)
        {
            if (!_operacao) ExibirEquipes(preservarEdicao: true);
        }

        private void ExibirEquipes(bool preservarEdicao = false)
        {
            if (_equipes.Columns.Count == 0) return;
            layoutCartoesEquipes.SuspendLayout();
            try
            {
                while (layoutCartoesEquipes.Controls.Count > 0) layoutCartoesEquipes.Controls[0].Dispose();
                layoutCartoesEquipes.RowStyles.Clear();
                layoutCartoesEquipes.RowCount = 0;
                string busca = txtBuscaEquipe.Text.Trim();
                foreach (DataRow equipe in _equipes.Rows)
                {
                    if (!equipe["nome"].ToString()!.Contains(busca, StringComparison.OrdinalIgnoreCase)) continue;
                    CardEquipes cartao = new CardEquipes { Dock = DockStyle.Top, Margin = new Padding(0, 0, 0, 8) };
                    cartao.ExibirEquipe(equipe);
                    cartao.Click += CartaoSelecionado;
                    cartao.ExcluirEquipe += CartaoExcluirEquipe;
                    cartao.VerElenco += CartaoVerElenco;
                    int linha = layoutCartoesEquipes.RowCount++;
                    layoutCartoesEquipes.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    layoutCartoesEquipes.Controls.Add(cartao, 0, linha);
                }
            }
            finally
            {
                layoutCartoesEquipes.ResumeLayout(true);
            }
            lblResumoEquipes.Text = layoutCartoesEquipes.Controls.Count == 0
                ? "Nenhuma equipe encontrada." : $"Suas equipes · {layoutCartoesEquipes.Controls.Count}";
            if (preservarEdicao && (txtNomeEquipe.Text != _nomeOriginal || !ReferenceEquals(_dadosEscudo, _escudoOriginal)))
            {
                DestacarSelecao();
                return;
            }

            CardEquipes? selecionado = null;
            foreach (CardEquipes cartao in layoutCartoesEquipes.Controls)
            {
                if (selecionado == null)
                    selecionado = cartao;
                if (cartao.EquipeId == _equipeSelecionadaId)
                {
                    selecionado = cartao;
                    break;
                }
            }
            if (selecionado == null) NovaEquipe();
            else SelecionarEquipe(selecionado.EquipeId);
        }

        private void CartaoSelecionado(object? sender, EventArgs e)
        {
            if (!_operacao && sender is CardEquipes cartao) SelecionarEquipe(cartao.EquipeId);
        }

        private void SelecionarEquipe(int equipeId)
        {
            DataRow? registro = LocalizarEquipe(equipeId);
            if (registro == null) return;
            _equipeSelecionadaId = equipeId;
            txtTituloFicha.Text = "Editar equipe";
            txtNomeEquipe.Text = _nomeOriginal = registro.Field<string>("nome") ?? string.Empty;
            _dadosEscudo = _escudoOriginal = registro["escudo"] as byte[];
            MostrarMensagem("");
            AtualizarEscudo();
            if (_dadosEscudo != null && _imagemEscudo == null)
                MostrarMensagem("O escudo cadastrado não pôde ser lido. Escolha outra imagem ou remova o escudo.", true);
            DestacarSelecao();
        }

        private DataRow? LocalizarEquipe(int equipeId)
        {
            foreach (DataRow equipe in _equipes.Rows)
            {
                if (equipe.Field<int>("id") == equipeId)
                    return equipe;
            }
            return null;
        }

        private void DestacarSelecao()
        {
            foreach (CardEquipes cartao in layoutCartoesEquipes.Controls)
                cartao.DefinirSelecionado(cartao.EquipeId == _equipeSelecionadaId);
        }

        private void NovaEquipe()
        {
            _equipeSelecionadaId = 0;
            txtTituloFicha.Text = "Cadastrar nova equipe";
            txtNomeEquipe.Clear();
            _nomeOriginal = string.Empty;
            _dadosEscudo = _escudoOriginal = null;
            AtualizarEscudo();
            MostrarMensagem("");
            DestacarSelecao();
        }

        private void btnNovaEquipe_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao) return;
            NovaEquipe();
            txtNomeEquipe.Focus();
        }

        private void btnEscolherEscudo_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || seletorEscudo.ShowDialog(FindForm()) != DialogResult.OK) return;
            _operacao = true;
            AtualizarAcoes();
            try
            {
                byte[] dados = ImagemEscudo.LerArquivo(seletorEscudo.FileName);
                _dadosEscudo = dados;
                AtualizarEscudo();
                MostrarMensagem("Escudo escolhido. Salve a equipe para gravar.");
            }
            catch (ValidacaoException erro)
            {
                MostrarMensagem(erro.Message, true);
            }
            catch (Exception)
            {
                MostrarMensagem("Não foi possível abrir a imagem escolhida.", true);
            }
            finally
            {
                _operacao = false;
                AtualizarAcoes();
            }
        }

        private void btnRemoverEscudo_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao) return;
            _dadosEscudo = null;
            AtualizarEscudo();
            MostrarMensagem("Escudo removido da edição. Salve a equipe para gravar.");
        }

        private void AtualizarEscudo()
        {
            imagemEscudo.Image = null;
            _imagemEscudo?.Dispose();
            _imagemEscudo = ImagemEscudo.CriarImagem(_dadosEscudo);
            imagemEscudo.Image = _imagemEscudo;
            labelEscudoEquipe.Text = _imagemEscudo == null ? "Sem escudo" : "Escudo da equipe";
            AtualizarAcoes();
        }

        private void btnSalvarEquipe_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || _usuario == null) return;
            _operacao = true;
            AtualizarAcoes();
            bool gravou = false;
            try
            {
                _equipeSelecionadaId = DadosEquipe.Salvar(_usuario.GetId(), _equipeSelecionadaId,
                    txtNomeEquipe.Text, _dadosEscudo);
                gravou = true;
                txtBuscaEquipe.Clear();
                RecarregarLista();
                MostrarMensagem("Equipe salva no banco de dados.");
            }
            catch (ValidacaoException erro)
            {
                MostrarMensagem(erro.Message, true);
            }
            catch (UnauthorizedAccessException erro)
            {
                MostrarMensagem(erro.Message, true);
            }
            catch (Exception)
            {
                MostrarMensagem(gravou
                    ? "Equipe salva. Não foi possível atualizar a lista; abra a tela novamente."
                    : "Não foi possível salvar a equipe. A edição foi mantida; tente novamente.", true);
            }
            finally
            {
                _operacao = false;
                AtualizarAcoes();
            }
        }

        private void CartaoVerElenco(object? sender, EventArgs e)
        {
            if (_pronto && !_operacao && sender is CardEquipes cartao)
                TelaSolicitada?.Invoke(this, new TelaAtletas(cartao.EquipeId));
        }

        private void CartaoExcluirEquipe(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || sender is not CardEquipes cartao) return;
            DataRow? registro = LocalizarEquipe(cartao.EquipeId);
            if (registro == null) return;
            string aviso = $"Excluir a equipe '{registro["nome"]}'?\n\nA equipe, suas táticas e os atletas exclusivos dela serão apagados para todos os usuários com acesso. Atletas de outras equipes serão preservados.";
            if (MessageBox.Show(FindForm(), aviso, "Excluir equipe", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            ExcluirEquipe(cartao.EquipeId);
        }

        private void ExcluirEquipe(int equipeId)
        {
            _operacao = true;
            AtualizarAcoes();
            bool excluiu = false;
            try
            {
                DadosEquipe.Excluir(_usuario!.GetId(), equipeId);
                excluiu = true;
                DataRow? removida = LocalizarEquipe(equipeId);
                if (removida != null) _equipes.Rows.Remove(removida);
                if (_equipeSelecionadaId == equipeId) NovaEquipe();
                ExibirEquipes(preservarEdicao: true);
                RecarregarLista(preservarEdicao: true);
                MostrarMensagem("Equipe excluída.");
            }
            catch (UnauthorizedAccessException erro)
            {
                MostrarMensagem(erro.Message, true);
            }
            catch (Exception)
            {
                MostrarMensagem(excluiu ? "Equipe excluída. Abra a tela novamente para atualizar a lista."
                    : "Não foi possível excluir a equipe. Nenhuma alteração foi gravada; tente novamente.", true);
            }
            finally
            {
                _operacao = false;
                AtualizarAcoes();
            }
        }

        private void MostrarMensagem(string texto, bool erro = false)
        {
            labelStatusEquipe.Text = texto;
            labelStatusEquipe.ForeColor = erro ? Color.FromArgb(180, 40, 60) : Color.FromArgb(100, 116, 139);
        }

        private void AtualizarAcoes()
        {
            bool editar = _pronto && !_operacao;
            painelListaEquipes.Enabled = editar;
            txtBuscaEquipe.Enabled = editar;
            btnNovaEquipe.Enabled = editar;
            txtNomeEquipe.Enabled = editar;
            btnEscolherEscudo.Enabled = editar;
            btnLimparFicha.Enabled = editar;
            btnSalvarEquipe.Enabled = editar;
            btnRemoverEscudo.Enabled = editar && _dadosEscudo != null;
        }
    }
}
