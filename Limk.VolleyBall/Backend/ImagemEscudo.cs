using System.Runtime.InteropServices;

namespace Limk.VolleyBall.Backend
{
    internal static class ImagemEscudo
    {
        public const int TamanhoMaximo = 4 * 1024 * 1024;
        public static Bitmap? CriarImagem(byte[]? dados)
        {
            if (dados == null || dados.Length == 0 || dados.Length > TamanhoMaximo)
            {
                return null;
            }

            try
            {
                MemoryStream arquivo = new MemoryStream(dados);
                try
                {
                    Image original = Image.FromStream(arquivo);
                    try
                    {
                        if (original.Width > 4096 || original.Height > 4096)
                        {
                            return null;
                        }

                        // A cópia continua válida depois que o arquivo e a imagem original são fechados.
                        return new Bitmap(original);
                    }
                    finally
                    {
                        original.Dispose();
                    }
                }
                finally
                {
                    arquivo.Dispose();
                }
            }
            catch (Exception erro) when (erro is ArgumentException || erro is OutOfMemoryException || erro is ExternalException)
            {
                return null;
            }
        }

        public static byte[] LerArquivo(string caminho)
        {
            FileStream arquivo = new FileStream(caminho, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan);
            try
            {
                long tamanho = arquivo.Length;
                if (tamanho > TamanhoMaximo)
                {
                    throw new ValidacaoException("Escolha um escudo de até 4 MB.");
                }

                byte[] dados = new byte[(int)tamanho];
                arquivo.ReadExactly(dados);
                Bitmap? imagem = CriarImagem(dados);
                try
                {
                    if (imagem == null)
                    {
                        throw new ValidacaoException("Escolha uma imagem válida, com até 4096 pixels de largura e altura.");
                    }

                    return dados;
                }
                finally
                {
                    if (imagem != null)
                    {
                        imagem.Dispose();
                    }
                }
            }
            finally
            {
                arquivo.Dispose();
            }
        }
    }
}
