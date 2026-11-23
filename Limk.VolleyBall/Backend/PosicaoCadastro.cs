namespace Limk.VolleyBall.Backend
{
    internal class PosicaoCadastro
    {
        private readonly int id;
        private readonly string nome;

        public PosicaoCadastro(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
        }

        public int GetId()
        {
            return id;
        }

        public string GetNome()
        {
            return nome;
        }

        public override string ToString()
        {
            return nome;
        }
    }
}
