namespace Limk.VolleyBall.Backend
{
    public class QuadroTatica
    {
        private string nome;
        private int duracaoMs;
        private int? rotacao;
        private List<AtletaQuadro> atletas = new List<AtletaQuadro>();
        private PosicaoBola? bola;

        public QuadroTatica()
        {
            nome = "Nova fase";
            duracaoMs = 1000;
            rotacao = null;
            bola = null;
        }

        public QuadroTatica(string nome, int duracaoMs = 1000, int? rotacao = null)
        {
            this.nome = nome;
            this.duracaoMs = duracaoMs;
            this.rotacao = rotacao;
        }

        public string GetNome()
        {
            return nome;
        }

        public void SetNome(string nome)
        {
            this.nome = nome;
        }

        public int GetDuracaoMs()
        {
            return duracaoMs;
        }

        public void SetDuracaoMs(int duracaoMs)
        {
            this.duracaoMs = duracaoMs;
        }

        public int? GetRotacao()
        {
            return rotacao;
        }

        public void SetRotacao(int? rotacao)
        {
            this.rotacao = rotacao;
        }

        public List<AtletaQuadro> GetAtletas()
        {
            return atletas;
        }

        public void SetAtletas(List<AtletaQuadro> atletas)
        {
            this.atletas = atletas;
        }

        public PosicaoBola? GetBola()
        {
            return bola;
        }

        public void SetBola(PosicaoBola? bola)
        {
            this.bola = bola;
        }

        public QuadroTatica Copiar()
        {
            // Copiamos também os atletas e a bola para que editar uma fase não altere a outra.
            QuadroTatica copia = new QuadroTatica(GetNome(), GetDuracaoMs(), GetRotacao());
            foreach (AtletaQuadro atleta in GetAtletas())
            {
                copia.GetAtletas().Add(atleta.Copiar());
            }
            if (bola != null)
            {
                copia.SetBola(bola.Copiar());
            }
            return copia;
        }
    }
}
