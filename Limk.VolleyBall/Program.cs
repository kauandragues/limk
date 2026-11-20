using dotenv.net;
using Limk.VolleyBall.Backend;

namespace Limk.VolleyBall
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            DotEnv.Load();

            ApplicationConfiguration.Initialize();

            UsuarioAutenticado usuarioConectado;
            using (var login = new FormLogin())
            {
                if (login.ShowDialog() != DialogResult.OK || login.Usuario is not { } usuario)
                    return;

                usuarioConectado = usuario;
            }

            using var principal = new Form1();
            principal.DefinirUsuario(usuarioConectado);
            Application.Run(principal);
        }
    }
}
