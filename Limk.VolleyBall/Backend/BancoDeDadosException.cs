namespace Limk.VolleyBall.Backend
{
    internal class BancoDeDadosException : Exception
    {
        private readonly string operacao;

        // A operação completa a mensagem padrão. base também guarda a causa em InnerException.
        public BancoDeDadosException(string operacao, Exception causa)
            : base("Não foi possível " + operacao + ". Verifique a conexão e tente novamente.", causa)
        {
            this.operacao = operacao;
        }

        public string GetOperacao()
        {
            return operacao;
        }
    }
}
