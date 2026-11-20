namespace Limk.VolleyBall.Backend
{
    public class QuadroTatica
    {
        public string Nome { get; set; } = "Nova fase";
        public int DuracaoMs { get; set; } = 1000;
        public int? Rotacao { get; set; }
        public List<AtletaQuadro> Atletas { get; set; } = new List<AtletaQuadro>();
        public PosicaoBola? Bola { get; set; }

        public QuadroTatica Copiar()
        {
            QuadroTatica copia = new QuadroTatica { Nome = Nome, DuracaoMs = DuracaoMs, Rotacao = Rotacao };
            foreach (AtletaQuadro atleta in Atletas)
                copia.Atletas.Add(atleta.Copiar());
            if (Bola != null)
                copia.Bola = new PosicaoBola { X = Bola.X, Y = Bola.Y };
            return copia;
        }
    }
}
