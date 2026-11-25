namespace Limk.VolleyBall.Backend
{
    public class Atleta
    {
        private int id;
        private string nome;
        private string apelido;
        private int numeroCamisa;
        // O ? permite deixar a data como null quando ela não foi informada.
        private DateTime? dataNascimento;
        private int idEquipe;
        private bool titular;

        public Atleta()
        {
            id = 0;
            nome = "";
            apelido = "";
            numeroCamisa = 0;
            dataNascimento = null;
            idEquipe = 0;
            titular = false;
        }

        public Atleta(int id, string nome, string apelido, int numeroCamisa, DateTime? dataNascimento,
            int idEquipe, bool titular)
        {
            this.id = id;
            this.nome = nome;
            this.apelido = apelido;
            this.numeroCamisa = numeroCamisa;
            this.dataNascimento = dataNascimento;
            this.idEquipe = idEquipe;
            this.titular = titular;
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
