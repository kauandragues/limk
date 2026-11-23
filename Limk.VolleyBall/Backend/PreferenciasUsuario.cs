namespace Limk.VolleyBall.Backend
{
    internal class PreferenciasUsuario
    {
        private readonly string corAtletaPrincipal;
        private readonly string corAtletaAdversario;
        private readonly string corBola;
        private readonly int duracaoQuadroPadraoMs;

        public PreferenciasUsuario(string corAtletaPrincipal, string corAtletaAdversario, string corBola, int duracaoQuadroPadraoMs)
        {
            this.corAtletaPrincipal = corAtletaPrincipal;
            this.corAtletaAdversario = corAtletaAdversario;
            this.corBola = corBola;
            this.duracaoQuadroPadraoMs = duracaoQuadroPadraoMs;
        }

        public string GetCorAtletaPrincipal()
        {
            return corAtletaPrincipal;
        }

        public string GetCorAtletaAdversario()
        {
            return corAtletaAdversario;
        }

        public string GetCorBola()
        {
            return corBola;
        }

        public int GetDuracaoQuadroPadraoMs()
        {
            return duracaoQuadroPadraoMs;
        }

        public static PreferenciasUsuario CriarPadrao()
        {
            return new PreferenciasUsuario("#6935D3", "#E53935", "#FFD700", 1000);
        }
    }
}
