using Limk.VolleyBall.Backend;
using Limk.VolleyBall.User_Constrols;

namespace Limk.VolleyBall
{
    internal sealed class FormLogin : Form
    {
        public UsuarioAutenticado? Usuario { get; private set; }

        public FormLogin()
        {
            SuspendLayout();

            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            MinimumSize = new Size(400, 480);
            ClientSize = new Size(900, 590);
            Icon = Properties.Resources.IconeLIMK;
            StartPosition = FormStartPosition.CenterScreen;
            MostrarLogin();
            Shown += FormLogin_Shown;

            ResumeLayout();
        }

        private void FormLogin_Shown(object? sender, EventArgs e)
        {
            if (Controls[0] is TelaLogin login)
                login.FocarEmail();
        }

        private void MostrarLogin(string? emailCadastrado = null)
        {
            var login = new TelaLogin { Dock = DockStyle.Fill };
            login.LoginConcluido += LoginConcluido;
            login.CriarContaSolicitada += CriarContaSolicitada;
            MostrarTela(login, login.BotaoEntrar, "Entrar | LIMK Volleyball");
            if (emailCadastrado != null)
                login.MostrarContaCriada(emailCadastrado);
            else
                login.FocarEmail();
        }

        private void CriarContaSolicitada(object? sender, EventArgs e)
        {
            var cadastro = new TelaCadastroUsuario { Dock = DockStyle.Fill };
            cadastro.VoltarParaLogin += VoltarParaLogin;
            cadastro.CadastroConcluido += CadastroConcluido;
            MostrarTela(cadastro, cadastro.BotaoCadastrar, "Criar conta | LIMK Volleyball");
            cadastro.FocarNome();
        }

        private void VoltarParaLogin(object? sender, EventArgs e) => MostrarLogin();

        private void CadastroConcluido(object? sender, UsuarioAutenticado usuario) => MostrarLogin(usuario.Email);

        private void MostrarTela(UserControl tela, IButtonControl botaoPrincipal, string titulo)
        {
            AcceptButton = null;
            while (Controls.Count > 0)
                Controls[0].Dispose();
            Controls.Add(tela);
            AcceptButton = botaoPrincipal;
            Text = titulo;
        }

        private void LoginConcluido(object? sender, UsuarioAutenticado usuario)
        {
            Usuario = usuario;
            DialogResult = DialogResult.OK;
        }
    }
}
