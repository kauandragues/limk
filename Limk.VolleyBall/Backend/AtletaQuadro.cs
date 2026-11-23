namespace Limk.VolleyBall.Backend
{
    // O número e as coordenadas são uma fotografia do atleta nesta fase.
    // Alterar outra fase ou o cadastro do elenco não altera essa fotografia.
    public class AtletaQuadro : ElementoQuadra
    {
        private int? equipeAtletaId;
        private string nome = "";
        private int numeroCamisa;
        private LadoAtleta lado;

        public int? GetEquipeAtletaId()
        {
            return equipeAtletaId;
        }

        public void SetEquipeAtletaId(int? equipeAtletaId)
        {
            this.equipeAtletaId = equipeAtletaId;
        }

        public string GetNome()
        {
            return nome;
        }

        public void SetNome(string nome)
        {
            this.nome = nome;
        }

        public int GetNumeroCamisa()
        {
            return numeroCamisa;
        }

        public void SetNumeroCamisa(int numeroCamisa)
        {
            this.numeroCamisa = numeroCamisa;
        }

        public LadoAtleta GetLado()
        {
            return lado;
        }

        public void SetLado(LadoAtleta lado)
        {
            this.lado = lado;
        }

        public AtletaQuadro Copiar()
        {
            AtletaQuadro atletaDaFase = new AtletaQuadro();
            atletaDaFase.SetEquipeAtletaId(GetEquipeAtletaId());
            atletaDaFase.SetNome(GetNome());
            atletaDaFase.SetNumeroCamisa(GetNumeroCamisa());
            atletaDaFase.SetLado(GetLado());
            atletaDaFase.SetX(GetX());
            atletaDaFase.SetY(GetY());
            return atletaDaFase;
        }
    }
}
