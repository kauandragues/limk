namespace Limk.VolleyBall.Backend
{
    internal class EquipeInicio
    {
        private string nome = "";
        private long atletas;
        private long taticas;

        public string GetNome()
        {
            return nome;
        }

        public void SetNome(string nome)
        {
            this.nome = nome;
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
    }
}
