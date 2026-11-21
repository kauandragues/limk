using System.Runtime.InteropServices;

namespace Limk.VolleyBall.Backend
{
    internal static class ImagemEscudo
    {
        public const int TamanhoMaximo = 4 * 1024 * 1024;

        public static Bitmap? CriarImagem(byte[]? dados)
        {
            if (dados == null || dados.Length == 0 || dados.Length > TamanhoMaximo)
                return null;
            try
            {
                using var arquivo = new MemoryStream(dados);
                using Image original = Image.FromStream(arquivo);
                if (original.Width > 4096 || original.Height > 4096)
                    return null;
                // A cópia continua válida depois que o arquivo e a imagem original são fechados.
                return new Bitmap(original);
            }
            catch (Exception erro) when (erro is ArgumentException or OutOfMemoryException or ExternalException)
            {
                return null;
            }
        }

        public static async Task<byte[]> LerArquivoAsync(string caminho, CancellationToken token)
        {
            using var arquivo = new FileStream(caminho, FileMode.Open, FileAccess.Read, FileShare.Read, 4096,
                FileOptions.Asynchronous | FileOptions.SequentialScan);
            long tamanho = arquivo.Length;
            if (tamanho > TamanhoMaximo)
                throw new ArgumentException("Escolha um escudo de até 4 MB.");

            byte[] dados = new byte[(int)tamanho];
            await arquivo.ReadExactlyAsync(dados, token).ConfigureAwait(false);
            using Bitmap? imagem = CriarImagem(dados);
            if (imagem == null)
                throw new ArgumentException("Escolha uma imagem válida, com até 4096 pixels de largura e altura.");
            return dados;
        }
    }
}
