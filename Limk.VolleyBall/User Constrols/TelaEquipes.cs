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
        private readonly CancellationTokenSource _encerramento = new CancellationTokenSource();
        public event EventHandler<UserControl>? TelaSolicitada;

        public TelaEquipes()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            AtualizarAcoes();
        }

        public void DefinirUsuario(UsuarioAutenticado usuario) => _usuario = usuario;

        private async void TelaEquipes_Load(object? sender, EventArgs e)
        {
            if (_usuario == null || _operacao || _pronto || DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            _operacao = true;
            AtualizarAcoes();
            label3.Text = "Carregando suas equipes...";
            try { await RecarregarListaAsync(); }
            catch (Exception)
            {
                if (!IsDisposed)
                {
                    label3.Text = "Não foi possível carregar suas equipes.";
                    MostrarMensagem("Verifique a conexão e abra a tela novamente.", true);
                }
            }
            finally { _operacao = false; if (!IsDisposed) AtualizarAcoes(); }
        }

        private async Task RecarregarListaAsync(bool preservarEdicao = false)
        {
            CancellationToken token = _encerramento.Token;
            DataTable equipes = await DadosUsuario.CarregarEquipesAsync(_usuario!.Id, token);
            if (token.IsCancellationRequested) return;
            _equipes = equipes;
            _pronto = true;
            ExibirEquipes(preservarEdicao);
        }

        private void textBox1_TextChanged(object? sender, EventArgs e)
        {
            if (!_operacao) ExibirEquipes(preservarEdicao: true);
        }

        private void ExibirEquipes(bool preservarEdicao = false)
        {
            if (_equipes.Columns.Count == 0) return;
            tableLayoutPanel1.SuspendLayout();
            try
            {
                while (tableLayoutPanel1.Controls.Count > 0) tableLayoutPanel1.Controls[0].Dispose();
                tableLayoutPanel1.RowStyles.Clear();
                tableLayoutPanel1.RowCount = 0;
                string busca = textBox1.Text.Trim();
                foreach (DataRow equipe in _equipes.Rows)
                {
                    if (!equipe["nome"].ToString()!.Contains(busca, StringComparison.OrdinalIgnoreCase)) continue;
                    var cartao = new CardEquipes { Dock = DockStyle.Top, Margin = new Padding(0, 0, 0, 8) };
                    cartao.ExibirEquipe(equipe);
                    cartao.Click += CartaoSelecionado;
                    cartao.ExcluirEquipe += CartaoExcluirEquipe;
                    cartao.VerElenco += CartaoVerElenco;
                    int linha = tableLayoutPanel1.RowCount++;
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    tableLayoutPanel1.Controls.Add(cartao, 0, linha);
                }
            }
            finally
            {
                tableLayoutPanel1.ResumeLayout(true);
            }
            label3.Text = tableLayoutPanel1.Controls.Count == 0
                ? "Nenhuma equipe encontrada." : $"Suas equipes · {tableLayoutPanel1.Controls.Count}";
            if (preservarEdicao && (textBox3.Text != _nomeOriginal || !ReferenceEquals(_dadosEscudo, _escudoOriginal)))
            {
                DestacarSelecao();
                return;
            }

            var selecionado = tableLayoutPanel1.Controls.OfType<CardEquipes>().FirstOrDefault(c => c.EquipeId == _equipeSelecionadaId)
                ?? tableLayoutPanel1.Controls.OfType<CardEquipes>().FirstOrDefault();
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
            textBox2.Text = "Editar equipe";
            textBox3.Text = _nomeOriginal = registro.Field<string>("nome") ?? string.Empty;
            _dadosEscudo = _escudoOriginal = registro["escudo"] as byte[];
            MostrarMensagem("");
            AtualizarEscudo();
            if (_dadosEscudo != null && _imagemEscudo == null)
                MostrarMensagem("O escudo cadastrado não pôde ser lido. Escolha outra imagem ou remova o escudo.", true);
            DestacarSelecao();
        }

        private DataRow? LocalizarEquipe(int equipeId) =>
            _equipes.AsEnumerable().FirstOrDefault(registro => registro.Field<int>("id") == equipeId);

        private void DestacarSelecao()
        {
            foreach (CardEquipes cartao in tableLayoutPanel1.Controls)
                cartao.DefinirSelecionado(cartao.EquipeId == _equipeSelecionadaId);
        }

        private void NovaEquipe()
        {
            _equipeSelecionadaId = 0;
            textBox2.Text = "Cadastrar nova equipe";
            textBox3.Clear();
            _nomeOriginal = string.Empty;
            _dadosEscudo = _escudoOriginal = null;
            AtualizarEscudo();
            MostrarMensagem("");
            DestacarSelecao();
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao) return;
            NovaEquipe();
            textBox3.Focus();
        }

        private void button4_Click(object? sender, EventArgs e) => button1_Click(sender, e);

        private async void button2_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || openFileDialog1.ShowDialog(FindForm()) != DialogResult.OK) return;
            _operacao = true;
            AtualizarAcoes();
            try
            {
                byte[] dados = await ImagemEscudo.LerArquivoAsync(openFileDialog1.FileName, _encerramento.Token);
                if (IsDisposed) return;
                _dadosEscudo = dados;
                AtualizarEscudo();
                MostrarMensagem("Escudo escolhido. Salve a equipe para gravar.");
            }
            catch (OperationCanceledException) { }
            catch (ArgumentException erro) { if (!IsDisposed) MostrarMensagem(erro.Message, true); }
            catch (Exception) { if (!IsDisposed) MostrarMensagem("Não foi possível abrir a imagem escolhida.", true); }
            finally { _operacao = false; if (!IsDisposed) AtualizarAcoes(); }
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
            pictureBox4.Image = null;
            _imagemEscudo?.Dispose();
            _imagemEscudo = ImagemEscudo.CriarImagem(_dadosEscudo);
            pictureBox4.Image = _imagemEscudo;
            labelEscudoEquipe.Text = _imagemEscudo == null ? "Sem escudo" : "Escudo da equipe";
            AtualizarAcoes();
        }

        private async void button5_Click(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || _usuario == null) return;
            _operacao = true;
            AtualizarAcoes();
            bool gravou = false;
            try
            {
                _equipeSelecionadaId = await DadosEquipe.SalvarAsync(_usuario.Id, _equipeSelecionadaId,
                    textBox3.Text, _dadosEscudo, _encerramento.Token);
                gravou = true;
                if (IsDisposed) return;
                textBox1.Clear();
                await RecarregarListaAsync();
                if (!IsDisposed) MostrarMensagem("Equipe salva no banco de dados.");
            }
            catch (OperationCanceledException) { }
            catch (ArgumentException erro) { if (!IsDisposed) MostrarMensagem(erro.Message, true); }
            catch (UnauthorizedAccessException erro) { if (!IsDisposed) MostrarMensagem(erro.Message, true); }
            catch (Exception)
            {
                if (!IsDisposed) MostrarMensagem(gravou
                    ? "Equipe salva. Não foi possível atualizar a lista; abra a tela novamente."
                    : "Não foi possível salvar a equipe. A edição foi mantida; tente novamente.", true);
            }
            finally { _operacao = false; if (!IsDisposed) AtualizarAcoes(); }
        }

        private void CartaoVerElenco(object? sender, EventArgs e)
        {
            if (_pronto && !_operacao && sender is CardEquipes cartao)
                TelaSolicitada?.Invoke(this, new TelaAtletas(cartao.EquipeId));
        }

        private async void CartaoExcluirEquipe(object? sender, EventArgs e)
        {
            if (!_pronto || _operacao || sender is not CardEquipes cartao || LocalizarEquipe(cartao.EquipeId) is not { } registro) return;
            string aviso = $"Excluir a equipe '{registro["nome"]}'?\n\nA equipe, suas táticas e os atletas exclusivos dela serão apagados para todos os usuários com acesso. Atletas de outras equipes serão preservados.";
            if (MessageBox.Show(FindForm(), aviso, "Excluir equipe", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
            await ExcluirEquipeAsync(cartao.EquipeId);
        }

        private async Task ExcluirEquipeAsync(int equipeId)
        {
            _operacao = true;
            AtualizarAcoes();
            bool excluiu = false;
            try
            {
                await DadosEquipe.ExcluirAsync(_usuario!.Id, equipeId, _encerramento.Token);
                excluiu = true;
                if (IsDisposed) return;
                DataRow? removida = LocalizarEquipe(equipeId);
                if (removida != null) _equipes.Rows.Remove(removida);
                if (_equipeSelecionadaId == equipeId) NovaEquipe();
                ExibirEquipes(preservarEdicao: true);
                await RecarregarListaAsync(preservarEdicao: true);
                if (!IsDisposed) MostrarMensagem("Equipe excluída.");
            }
            catch (OperationCanceledException) { }
            catch (UnauthorizedAccessException erro) { if (!IsDisposed) MostrarMensagem(erro.Message, true); }
            catch (Exception)
            {
                if (!IsDisposed) MostrarMensagem(excluiu ? "Equipe excluída. Abra a tela novamente para atualizar a lista."
                    : "Não foi possível excluir a equipe. Nenhuma alteração foi gravada; tente novamente.", true);
            }
            finally { _operacao = false; if (!IsDisposed) AtualizarAcoes(); }
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
            textBox1.Enabled = editar;
            button1.Enabled = editar;
            textBox3.Enabled = editar;
            button2.Enabled = editar;
            button4.Enabled = editar;
            button5.Enabled = editar;
            btnRemoverEscudo.Enabled = editar && _dadosEscudo != null;
        }
    }
}
