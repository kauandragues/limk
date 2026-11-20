using dotenv.net;

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

            string nomeUsuario;
            using (var login = new FormLogin())
            {
                if (login.ShowDialog() != DialogResult.OK || login.Usuario is not { } usuario)
                    return;

                nomeUsuario = usuario.Nome;
            }

            using var principal = new Form1();
            principal.DefinirUsuario(nomeUsuario);
            Application.Run(principal);
        }
    }
}
