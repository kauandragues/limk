namespace Limk.VolleyBall.Backend
{
    public class Atleta
    {
        private int id;
        private string nome = string.Empty;
        private string apelido = string.Empty;
        private int numeroCamisa;
        private DateTime? dataNascimento;
        private int idEquipe;
        private bool titular;

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

        public string GetApelido()
        {
            return apelido;
        }

        public void SetApelido(string apelido)
        {
            this.apelido = apelido;
        }

        public int GetNumeroCamisa()
        {
            return numeroCamisa;
        }

        public void SetNumeroCamisa(int numeroCamisa)
        {
            this.numeroCamisa = numeroCamisa;
        }

        public DateTime? GetDataNascimento()
        {
            return dataNascimento;
        }

        public void SetDataNascimento(DateTime? dataNascimento)
        {
            this.dataNascimento = dataNascimento;
        }

        public int GetIdEquipe()
        {
            return idEquipe;
        }

        public void SetIdEquipe(int idEquipe)
        {
            this.idEquipe = idEquipe;
        }

        public bool GetTitular()
        {
            return titular;
        }

        public void SetTitular(bool titular)
        {
            this.titular = titular;
        }
    }
}
