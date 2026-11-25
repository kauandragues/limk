namespace Limk.VolleyBall.Backend
{
    internal class ValidacaoException : Exception
    {
        private readonly string campo;

        // O campo ajuda a tela a indicar onde corrigir os dados. Vazio significa uma regra geral.
        // base guarda a mensagem em Message e a causa em InnerException.
        public ValidacaoException(string mensagem, Exception? causa = null, string campo = "") : base(mensagem, causa)
        {
            this.campo = campo;
        }

        public string GetCampo()
        {
            return campo;
        }
    }
}
