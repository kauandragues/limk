namespace Limk.VolleyBall.Backend
{
    internal class BancoDeDadosException : Exception
    {
        public BancoDeDadosException(string mensagem, Exception causa) : base(mensagem, causa)
        {
        }
    }
}
