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
            // Um atalho pode iniciar o programa a partir de outra pasta.
            string arquivoDeConfiguracao = Path.Combine(AppContext.BaseDirectory, ".env");
            DotEnv.Load(new DotEnvOptions(envFilePaths: new string[] { arquivoDeConfiguracao }));

            ApplicationConfiguration.Initialize();

            using FormPrincipal principal = new FormPrincipal();
            if (principal.EntrarNaConta())
                Application.Run(principal);
        }
    }
}
