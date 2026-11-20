namespace Limk.VolleyBall.Backend
{
    public class Tatica
    {
        public int Id { get; set; }
        public int EquipeId { get; set; }
        public string Nome { get; set; } = "Nova tática";
        public string Descricao { get; set; } = "";
        public int? TipoTaticaId { get; set; }
        public int? SistemaJogoId { get; set; }
        public List<QuadroTatica> Quadros { get; set; } = new List<QuadroTatica>();

        public Tatica Copiar()
        {
            Tatica copia = new Tatica
            {
                Id = Id, EquipeId = EquipeId, Nome = Nome, Descricao = Descricao,
                TipoTaticaId = TipoTaticaId, SistemaJogoId = SistemaJogoId
            };
            foreach (QuadroTatica quadro in Quadros)
                copia.Quadros.Add(quadro.Copiar());
            return copia;
        }
    }
}
