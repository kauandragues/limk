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
            TelaLogin? login = Controls[0] as TelaLogin;
            if (login != null)
            {
                login.FocarEmail();
            }
        }

        private void MostrarLogin(string emailCadastrado = "")
        {
            TelaLogin login = new TelaLogin();
            login.Dock = DockStyle.Fill;
            login.LoginConcluido += LoginConcluido;
            login.CriarContaSolicitada += CriarContaSolicitada;
            MostrarTela(login, login.BotaoEntrar, "Entrar | LIMK Volleyball");
            if (!string.IsNullOrEmpty(emailCadastrado))
            {
                login.MostrarContaCriada(emailCadastrado);
            }
            else
            {
                login.FocarEmail();
            }
        }

        private void CriarContaSolicitada(object? sender, EventArgs e)
        {
            TelaCadastroUsuario cadastro = new TelaCadastroUsuario();
            cadastro.Dock = DockStyle.Fill;
            cadastro.VoltarParaLogin += VoltarParaLogin;
            cadastro.CadastroConcluido += CadastroConcluido;
            MostrarTela(cadastro, cadastro.BotaoCadastrar, "Criar conta | LIMK Volleyball");
            cadastro.FocarNome();
        }

        private void VoltarParaLogin(object? sender, EventArgs e)
        {
            MostrarLogin();
        }

        private void CadastroConcluido(object? sender, UsuarioAutenticado usuario)
        {
            MostrarLogin(usuario.GetEmail());
        }

        private void MostrarTela(UserControl tela, IButtonControl botaoPrincipal, string titulo)
        {
            AcceptButton = null;
            while (Controls.Count > 0)
            {
                Controls[0].Dispose();
            }

            Controls.Add(tela);
            // AcceptButton define qual botão será acionado quando o usuário pressionar Enter.
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
