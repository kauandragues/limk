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
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Entrar | LIMK Volleyball";

            var telaLogin = new TelaLogin { Dock = DockStyle.Fill };
            telaLogin.LoginConcluido += LoginConcluido;
            Controls.Add(telaLogin);
            AcceptButton = telaLogin.BotaoEntrar;
            Shown += (_, _) => telaLogin.FocarEmail();

            ResumeLayout();
        }

        private void LoginConcluido(object? sender, UsuarioAutenticado usuario)
        {
            Usuario = usuario;
            DialogResult = DialogResult.OK;
        }
    }
}
