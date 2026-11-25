namespace Limk.VolleyBall.Backend
{
    internal class EquipeInicio
    {
        private string nome;
        private long atletas;
        private long taticas;

        public EquipeInicio()
        {
            nome = "";
            atletas = 0;
            taticas = 0;
        }

        public EquipeInicio(string nome, long atletas, long taticas)
        {
            this.nome = nome;
            this.atletas = atletas;
            this.taticas = taticas;
        }

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
