namespace Limk.VolleyBall.Backend
{
    internal class ResumoInicio
    {
        private long equipes;
        private long atletas;
        private long taticas;
        private long quadros;
        private List<TaticaInicio> ultimasTaticas = new List<TaticaInicio>();
        private List<EquipeInicio> equipesCadastradas = new List<EquipeInicio>();

        public long GetEquipes()
        {
            return equipes;
        }

        public void SetEquipes(long equipes)
        {
            this.equipes = equipes;
        }

        public long GetAtletas()
        {
            return atletas;
        }

        public void SetAtletas(long atletas)
        {
            this.atletas = atletas;
        }

        public long GetTaticas()
        {
            return taticas;
        }

        public void SetTaticas(long taticas)
        {
            this.taticas = taticas;
        }

        public long GetQuadros()
        {
            return quadros;
        }

        public void SetQuadros(long quadros)
        {
            this.quadros = quadros;
        }

        public List<TaticaInicio> GetUltimasTaticas()
        {
            return ultimasTaticas;
        }

        public void SetUltimasTaticas(List<TaticaInicio> ultimasTaticas)
        {
            this.ultimasTaticas = ultimasTaticas;
        }

        public List<EquipeInicio> GetEquipesCadastradas()
        {
            return equipesCadastradas;
        }

        public void SetEquipesCadastradas(List<EquipeInicio> equipesCadastradas)
        {
            this.equipesCadastradas = equipesCadastradas;
        }
    }
}
