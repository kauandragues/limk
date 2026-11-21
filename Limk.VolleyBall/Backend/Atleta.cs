namespace Limk.VolleyBall.Backend
{
    public class Atleta
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Apelido { get; set; } = string.Empty;
        public int NumeroCamisa { get; set; }
        public DateTime? DataNascimento { get; set; }
        public int IdEquipe { get; set; }
        public bool Titular { get; set; }
    }
}
