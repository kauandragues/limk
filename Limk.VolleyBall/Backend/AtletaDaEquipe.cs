namespace Limk.VolleyBall.Backend
{
    public class AtletaDaEquipe
    {
        private int equipeAtletaId;
        private string nome;
        private int numeroCamisa;

        public AtletaDaEquipe()
        {
            equipeAtletaId = 0;
            nome = "Sem vínculo cadastrado";
            numeroCamisa = 0;
        }

        public AtletaDaEquipe(int equipeAtletaId, string nome, int numeroCamisa)
        {
            this.equipeAtletaId = equipeAtletaId;
            this.nome = nome;
            this.numeroCamisa = numeroCamisa;
        }

        public int GetEquipeAtletaId()
        {
            return equipeAtletaId;
        }

        public void SetEquipeAtletaId(int equipeAtletaId)
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

        public override string ToString()
        {
            return nome;
        }
    }
}
