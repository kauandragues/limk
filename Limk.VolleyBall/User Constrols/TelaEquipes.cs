using Limk.VolleyBall.Backend;
using System.ComponentModel;
using System.Data;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaEquipes : UserControl
    {
        private UsuarioAutenticado? _usuario;
        private DataTable _equipes = new DataTable();
        private int _equipeSelecionadaId;
        private string _nomeOriginal = "";
        private byte[]? _escudoOriginal;
        private byte[]? _dadosEscudo;
        private Bitmap? _imagemEscudo;
        private bool _pronto;
        private bool _operacao;
        // Avisa o FormPrincipal para trocar de tela, por exemplo, ao abrir o elenco de uma equipe.
        // EventHandler<UserControl> recebe a origem do pedido (sender) e a tela desejada.
        public event EventHandler<UserControl>? TelaSolicitada;
        public TelaEquipes()
        {
            InitializeComponent();
            // No designer do Visual Studio, o return encerra o construtor após criar os controles visuais.
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            AtualizarAcoes();
        }

        public void DefinirUsuario(UsuarioAutenticado usuario)
        {
            _usuario = usuario;
        }

        private void TelaEquipes_Load(object? sender, EventArgs e)
        {
            // Evita carregar equipes no designer do Visual Studio, sem usuário, durante uma operação ou com a tela já pronta.
            if (_usuario == null || _operacao || _pronto || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

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
            if (_usuario == null)
            {
                return;
            }

            DataTable equipes = DadosEquipe.Listar(_usuario.GetId());
            _equipes = equipes;
            _pronto = true;
            ExibirEquipes(preservarEdicao);
        }

        private void txtBuscaEquipe_TextChanged(object? sender, EventArgs e)
        {
            if (!_operacao)
            {
                ExibirEquipes(preservarEdicao: true);
            }
        }

        private void ExibirEquipes(bool preservarEdicao = false)
        {
            if (_equipes.Columns.Count == 0)
            {
                return;
            }

            layoutCartoesEquipes.SuspendLayout();
            try
            {
                while (layoutCartoesEquipes.Controls.Count > 0)
                {
                    layoutCartoesEquipes.Controls[0].Dispose();
                }

                layoutCartoesEquipes.RowStyles.Clear();
                layoutCartoesEquipes.RowCount = 0;
                string busca = txtBuscaEquipe.Text.Trim();
                foreach (DataRow equipe in _equipes.Rows)
                {
                    string? nomeEquipe = equipe["nome"].ToString();
                    if (nomeEquipe == null || !nomeEquipe.Contains(busca, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    CardEquipes cartao = new CardEquipes();
                    cartao.Dock = DockStyle.Top;
                    cartao.Margin = new Padding(0, 0, 0, 8);
                    cartao.ExibirEquipe(equipe);
                    cartao.Click += CartaoSelecionado;
                    cartao.ExcluirEquipe += CartaoExcluirEquipe;
                    cartao.VerElenco += CartaoVerElenco;
                    int linha = layoutCartoesEquipes.RowCount;
                    layoutCartoesEquipes.RowCount++;
                    layoutCartoesEquipes.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    layoutCartoesEquipes.Controls.Add(cartao, 0, linha);
                }
            }
            finally
            {
                layoutCartoesEquipes.ResumeLayout(true);
            }

            if (layoutCartoesEquipes.Controls.Count == 0)
            {
                lblResumoEquipes.Text = "Nenhuma equipe encontrada.";
            }
            else
            {
                lblResumoEquipes.Text = $"Suas equipes · {layoutCartoesEquipes.Controls.Count}";
            }

            if (preservarEdicao && (txtNomeEquipe.Text != _nomeOriginal || !ReferenceEquals(_dadosEscudo, _escudoOriginal)))
            {
                DestacarSelecao();
                return;
            }

            CardEquipes? selecionado = null;
            foreach (CardEquipes cartao in layoutCartoesEquipes.Controls)
            {
                if (selecionado == null)
                {
                    selecionado = cartao;
                }

                if (cartao.GetEquipeId() == _equipeSelecionadaId)
                {
                    selecionado = cartao;
                    break;
                }
            }

            if (selecionado == null)
            {
                NovaEquipe();
            }
            else
            {
                SelecionarEquipe(selecionado.GetEquipeId());
            }
        }

        private void CartaoSelecionado(object? sender, EventArgs e)
        {
            CardEquipes? cartao = sender as CardEquipes;
            if (!_operacao && cartao != null)
            {
                SelecionarEquipe(cartao.GetEquipeId());
            }
        }

        private void SelecionarEquipe(int equipeId)
        {
            DataRow? registro = LocalizarEquipe(equipeId);
            if (registro == null)
            {
                return;
            }

            _equipeSelecionadaId = equipeId;
            txtTituloFicha.Text = "Editar equipe";
            string? nome = registro.Field<string>("nome");
            if (nome == null)
            {
                nome = "";
            }

            _nomeOriginal = nome;
            txtNomeEquipe.Text = nome;
            _escudoOriginal = registro["escudo"] as byte[];
            _dadosEscudo = _escudoOriginal;
            MostrarMensagem("");
            AtualizarEscudo();
            if (_dadosEscudo != null && _imagemEscudo == null)
            {
                MostrarMensagem("O escudo cadastrado não pôde ser lido. Escolha outra imagem ou remova o escudo.", true);
            }

            DestacarSelecao();
        }

        private DataRow? LocalizarEquipe(int equipeId)
        {
            foreach (DataRow equipe in _equipes.Rows)
            {
                if (equipe.Field<int>("id") == equipeId)
                {
                    return equipe;
                }
            }

            return null;
        }

        private void DestacarSelecao()
        {
            foreach (CardEquipes cartao in layoutCartoesEquipes.Controls)
            {
                cartao.DefinirSelecionado(cartao.GetEquipeId() == _equipeSelecionadaId);
            }
        }

        private void NovaEquipe()
        {
            _equipeSelecionadaId = 0;
            txtTituloFicha.Text = "Cadastrar nova equipe";
            txtNomeEquipe.Clear();
            _nomeOriginal = "";
            _dadosEscudo = null;
            _escudoOriginal = null;
            AtualizarEscudo();
            MostrarMensagem("");
            DestacarSelecao();
        }

        private void btnNovaEquipe_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao)
            {
                return;
            }

            NovaEquipe();
            txtNomeEquipe.Focus();
        }

        private void btnEscolherEscudo_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || seletorEscudo.ShowDialog(FindForm()) != DialogResult.OK)
            {
                return;
            }

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
            if (!_pronto || _operacao)
            {
                return;
            }

            _dadosEscudo = null;
            AtualizarEscudo();
            MostrarMensagem("Escudo removido da edição. Salve a equipe para gravar.");
        }

        private void AtualizarEscudo()
        {
            imagemEscudo.Image = null;
            if (_imagemEscudo != null)
            {
                _imagemEscudo.Dispose();
            }

            _imagemEscudo = ImagemEscudo.CriarImagem(_dadosEscudo);
            imagemEscudo.Image = _imagemEscudo;
            if (_imagemEscudo == null)
            {
                labelEscudoEquipe.Text = "Sem escudo";
            }
            else
            {
                labelEscudoEquipe.Text = "Escudo da equipe";
            }

            AtualizarAcoes();
        }

        private void btnSalvarEquipe_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || _usuario == null)
            {
                return;
            }

            _operacao = true;
            AtualizarAcoes();
            bool gravou = false;
            try
            {
                _equipeSelecionadaId = DadosEquipe.Salvar(_usuario.GetId(), _equipeSelecionadaId, txtNomeEquipe.Text, _dadosEscudo);
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
                if (gravou)
                {
                    MostrarMensagem("Equipe salva. Não foi possível atualizar a lista; abra a tela novamente.", true);
                }
                else
                {
                    MostrarMensagem("Não foi possível salvar a equipe. A edição foi mantida; tente novamente.", true);
                }
            }
            finally
            {
                _operacao = false;
                AtualizarAcoes();
            }
        }

        private void CartaoVerElenco(object? sender, EventArgs e)
        {
            CardEquipes? cartao = sender as CardEquipes;
            if (_pronto && !_operacao && cartao != null)
            {
                if (TelaSolicitada != null)
                {
                    TelaSolicitada(this, new TelaAtletas(cartao.GetEquipeId()));
                }
            }
        }

        private void CartaoExcluirEquipe(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao)
            {
                return;
            }

            CardEquipes? cartao = sender as CardEquipes;
            if (cartao == null)
            {
                return;
            }

            DataRow? registro = LocalizarEquipe(cartao.GetEquipeId());
            if (registro == null)
            {
                return;
            }

            string aviso = $"Excluir a equipe '{registro["nome"]}'?\n\nA equipe, suas táticas e os atletas exclusivos dela serão apagados para todos os usuários com acesso. Atletas de outras equipes serão preservados.";
            if (MessageBox.Show(FindForm(), aviso, "Excluir equipe", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            ExcluirEquipe(cartao.GetEquipeId());
        }

        private void ExcluirEquipe(int equipeId)
        {
            if (_usuario == null)
            {
                return;
            }

            _operacao = true;
            AtualizarAcoes();
            bool excluiu = false;
            try
            {
                DadosEquipe.Excluir(_usuario.GetId(), equipeId);
                excluiu = true;
                DataRow? removida = LocalizarEquipe(equipeId);
                if (removida != null)
                {
                    _equipes.Rows.Remove(removida);
                }

                if (_equipeSelecionadaId == equipeId)
                {
                    NovaEquipe();
                }

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
                if (excluiu)
                {
                    MostrarMensagem("Equipe excluída. Abra a tela novamente para atualizar a lista.", true);
                }
                else
                {
                    MostrarMensagem("Não foi possível excluir a equipe. Nenhuma alteração foi gravada; tente novamente.", true);
                }
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
            if (erro)
            {
                labelStatusEquipe.ForeColor = Color.FromArgb(180, 40, 60);
            }
            else
            {
                labelStatusEquipe.ForeColor = Color.FromArgb(100, 116, 139);
            }
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
