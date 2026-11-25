namespace Limk.VolleyBall.Backend
{
    // O número e as coordenadas são uma fotografia do atleta nesta fase.
    // Alterar outra fase ou o cadastro do elenco não altera essa fotografia.
    public class AtletaQuadro : ElementoQuadra
    {
        // O ? permite usar null quando o atleta não está ligado a um cadastro do elenco.
        private int? equipeAtletaId;
        private string nome;
        private int numeroCamisa;
        private LadoAtleta lado;

        public AtletaQuadro()
        {
            equipeAtletaId = null;
            nome = "";
            numeroCamisa = 0;
            lado = LadoAtleta.Principal;
            SetX(0);
            SetY(0);
        }

        public AtletaQuadro(int? equipeAtletaId, string nome, int numeroCamisa, LadoAtleta lado,
            float x, float y)
        {
            this.equipeAtletaId = equipeAtletaId;
            this.nome = nome;
            this.numeroCamisa = numeroCamisa;
            this.lado = lado;
            // As coordenadas pertencem à classe ElementoQuadra, que fornece estes métodos.
            SetX(x);
            SetY(y);
        }

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
            return new AtletaQuadro(GetEquipeAtletaId(), GetNome(), GetNumeroCamisa(), GetLado(), GetX(), GetY());
        }
    }
}
