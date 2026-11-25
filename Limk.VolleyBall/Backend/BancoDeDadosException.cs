namespace Limk.VolleyBall.Backend
{
    internal class BancoDeDadosException : Exception
    {
        // base repassa a mensagem e o erro original para o construtor da classe Exception.
        public BancoDeDadosException(string mensagem, Exception causa) : base(mensagem, causa)
        {
        }
    }
}
