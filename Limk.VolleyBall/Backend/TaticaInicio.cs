namespace Limk.VolleyBall.Backend
{
    internal class TaticaInicio
    {
        private int id;
        private string nome = "";
        private string descricao = "";
        private string equipe = "";
        private long quadros;

        public int GetId()
        {
            return id;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public string GetNome()
        {
            return nome;
        }

        public void SetNome(string nome)
        {
            this.nome = nome;
        }

        public string GetDescricao()
        {
            return descricao;
        }

        public void SetDescricao(string descricao)
        {
            this.descricao = descricao;
        }

        public string GetEquipe()
        {
            return equipe;
        }

        public void SetEquipe(string equipe)
        {
            this.equipe = equipe;
        }

        public long GetQuadros()
        {
            return quadros;
        }

        public void SetQuadros(long quadros)
        {
            this.quadros = quadros;
        }
    }
}
