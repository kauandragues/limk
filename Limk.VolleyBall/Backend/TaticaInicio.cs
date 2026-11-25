namespace Limk.VolleyBall.Backend
{
    internal class TaticaInicio
    {
        private int id;
        private string nome;
        private string descricao;
        private string equipe;
        private long quadros;

        public TaticaInicio()
        {
            id = 0;
            nome = "";
            descricao = "";
            equipe = "";
            quadros = 0;
        }

        public TaticaInicio(int id, string nome, string descricao, string equipe, long quadros)
        {
            this.id = id;
            this.nome = nome;
            this.descricao = descricao;
            this.equipe = equipe;
            this.quadros = quadros;
        }

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
