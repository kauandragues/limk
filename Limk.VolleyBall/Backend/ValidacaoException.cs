namespace Limk.VolleyBall.Backend
{
    internal class ValidacaoException : Exception
    {
        public ValidacaoException(string mensagem, Exception? causa = null) : base(mensagem, causa)
        {
        }
    }
}
