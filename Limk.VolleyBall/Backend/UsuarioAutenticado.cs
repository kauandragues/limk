namespace Limk.VolleyBall.Backend
{
    public class UsuarioAutenticado
    {
        private readonly int id;
        private readonly string nome;
        private readonly string email;

        public UsuarioAutenticado(int id, string nome, string email)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
        }

        public int GetId()
        {
            return id;
        }

        public string GetNome()
        {
            return nome;
        }

        public string GetEmail()
        {
            return email;
        }
    }
}
