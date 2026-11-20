namespace Limk.VolleyBall.Backend
{
    // O número e as coordenadas são uma fotografia do atleta nesta fase.
    // Alterar outra fase ou o cadastro do elenco não altera essa fotografia.
    public class AtletaQuadro
    {
        public int? EquipeAtletaId { get; set; }
        public string Nome { get; set; } = "";
        public int NumeroCamisa { get; set; }
        public LadoAtleta Lado { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public AtletaQuadro Copiar()
        {
            return new AtletaQuadro
            {
                EquipeAtletaId = EquipeAtletaId, Nome = Nome, NumeroCamisa = NumeroCamisa,
                Lado = Lado, X = X, Y = Y
            };
        }
    }
}
