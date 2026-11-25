using dotenv.net;

namespace Limk.VolleyBall
{
    internal static class Program
    {
        // O programa começa neste método. STAThread é o modo de execução exigido pelo Windows Forms.
        [STAThread]
        static void Main()
        {
            // Um atalho pode iniciar o programa a partir de outra pasta.
            string arquivoDeConfiguracao = Path.Combine(AppContext.BaseDirectory, ".env");
            DotEnv.Load(new DotEnvOptions(envFilePaths: new string[] { arquivoDeConfiguracao }));
            ApplicationConfiguration.Initialize();
            FormPrincipal principal = new FormPrincipal();
            try
            {
                if (principal.EntrarNaConta())
                {
                    Application.Run(principal);
                }
            }
            finally
            {
                principal.Dispose();
            }
        }
    }
}
