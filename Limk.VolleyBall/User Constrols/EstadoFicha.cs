namespace Limk.VolleyBall.User_Constrols
{
    // Guarda os valores que estavam no formulário quando a edição começou.
    internal class EstadoFicha
    {
        // readonly permite preencher estes campos no construtor e impede mudanças depois disso.
        public readonly string Nome;
        private readonly string apelido;
        private readonly string camisa;
        private readonly int equipeId;
        private readonly DateTime? nascimento;
        private readonly bool titular;
        private readonly string posicoes;

        public EstadoFicha(string nome, string apelido, string camisa, int equipeId,
            DateTime? nascimento, bool titular, string posicoes)
        {
            Nome = nome;
            this.apelido = apelido;
            this.camisa = camisa;
            this.equipeId = equipeId;
            this.nascimento = nascimento;
            this.titular = titular;
            this.posicoes = posicoes;
        }

        public bool TemMesmosValores(EstadoFicha outra)
        {
            // Comparamos os valores para avisar antes de descartar uma edição que ainda não foi salva.
            return Nome == outra.Nome && apelido == outra.apelido && camisa == outra.camisa
                && equipeId == outra.equipeId && nascimento == outra.nascimento
                && titular == outra.titular && posicoes == outra.posicoes;
        }
    }
}
