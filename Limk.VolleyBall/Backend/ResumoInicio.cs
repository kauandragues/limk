namespace Limk.VolleyBall.Backend
{
    internal class ResumoInicio
    {
        public long Equipes { get; set; }
        public long Atletas { get; set; }
        public long Taticas { get; set; }
        public long Quadros { get; set; }
        public List<TaticaInicio> UltimasTaticas { get; set; } = new List<TaticaInicio>();
        public List<EquipeInicio> EquipesCadastradas { get; set; } = new List<EquipeInicio>();
    }
}
