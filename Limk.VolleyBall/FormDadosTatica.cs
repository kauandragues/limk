using Limk.VolleyBall.Backend;
using System.Data;

namespace Limk.VolleyBall
{
    public partial class FormDadosTatica : Form
    {
        private readonly Tatica _dados = new Tatica();
        private readonly QuadroTatica _fase = new QuadroTatica();
        internal Tatica Dados => _dados;

        public FormDadosTatica()
        {
            InitializeComponent();
        }

        internal FormDadosTatica(Tatica dados, int indiceFase, DataTable equipes, DataTable tipos, DataTable sistemas) : this()
        {
            _dados = dados;
            _fase = dados.Quadros[indiceFase];
            txtNome.Text = dados.Nome;
            txtDescricao.Text = dados.Descricao;
            cmbEquipe.DisplayMember = "nome";
            cmbEquipe.ValueMember = "id";
            cmbEquipe.DataSource = equipes.Copy();
            cmbEquipe.SelectedValue = dados.EquipeId;
            cmbEquipe.Enabled = dados.Id == 0;
            PreencherOpcional(cmbTipo, tipos, dados.TipoTaticaId);
            PreencherOpcional(cmbSistema, sistemas, dados.SistemaJogoId);
            txtFase.Text = _fase.Nome;
            numDuracao.Value = Math.Clamp(_fase.DuracaoMs / 1000m, numDuracao.Minimum, numDuracao.Maximum);
            cmbRotacao.Items.AddRange(new object[] { "Sem rotação", "P1", "P2", "P3", "P4", "P5", "P6" });
            cmbRotacao.SelectedIndex = _fase.Rotacao ?? 0;
        }

        private static void PreencherOpcional(ComboBox combo, DataTable origem, int? selecionado)
        {
            DataTable opcoes = origem.Copy();
            DataRow vazio = opcoes.NewRow();
            vazio["id"] = 0;
            vazio["nome"] = "Sem definição";
            opcoes.Rows.InsertAt(vazio, 0);
            combo.DisplayMember = "nome";
            combo.ValueMember = "id";
            combo.DataSource = opcoes;
            combo.SelectedValue = selecionado ?? 0;
        }

        private void btnConfirmar_Click(object? sender, EventArgs e)
        {
            _dados.Nome = txtNome.Text.Trim();
            _dados.Descricao = txtDescricao.Text.Trim();
            _dados.EquipeId = cmbEquipe.SelectedValue is int equipe ? equipe : 0;
            _dados.TipoTaticaId = cmbTipo.SelectedValue is int tipo && tipo > 0 ? tipo : null;
            _dados.SistemaJogoId = cmbSistema.SelectedValue is int sistema && sistema > 0 ? sistema : null;
            _fase.Nome = txtFase.Text.Trim();
            _fase.DuracaoMs = (int)(numDuracao.Value * 1000);
            _fase.Rotacao = cmbRotacao.SelectedIndex > 0 ? cmbRotacao.SelectedIndex : null;
            try
            {
                DadosPrancheta.Validar(_dados);
                DialogResult = DialogResult.OK;
            }
            catch (ArgumentException erro)
            {
                lblMensagem.Text = erro.Message;
            }
        }
    }
}
