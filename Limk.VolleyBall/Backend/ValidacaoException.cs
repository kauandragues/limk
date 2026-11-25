namespace Limk.VolleyBall.Backend
{
    internal class ValidacaoException : Exception
    {
        // base guarda a mensagem e a causa do erro na classe Exception, da qual esta classe herda.
        public ValidacaoException(string mensagem, Exception? causa = null) : base(mensagem, causa)
        {
        }
    }
}
