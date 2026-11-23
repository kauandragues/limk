namespace Limk.VolleyBall.Backend
{
    public class AtletaDaEquipe
    {
        private int equipeAtletaId;
        private string nome = "Sem vínculo cadastrado";
        private int numeroCamisa;

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
