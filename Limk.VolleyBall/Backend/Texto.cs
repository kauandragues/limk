using System.Text;

namespace Limk.VolleyBall.Backend
{
    internal static class Texto
    {
        public static int ContarCaracteres(string texto)
        {
            int quantidade = 0;

            // Alguns símbolos, como emojis, ocupam duas posições em uma string.
            // EnumerateRunes percorre cada símbolo inteiro para contar corretamente.
            foreach (Rune caractere in texto.EnumerateRunes())
            {
                quantidade++;
            }

            return quantidade;
        }

        public static string LimitarCaracteres(string texto, int limite)
        {
            // StringBuilder permite montar um texto acrescentando um caractere por vez.
            StringBuilder resultado = new StringBuilder();
            int quantidade = 0;

            foreach (Rune caractere in texto.EnumerateRunes())
            {
                if (quantidade >= limite)
                {
                    break;
                }

                resultado.Append(caractere.ToString());
                quantidade++;
            }

            return resultado.ToString();
        }
    }
}
