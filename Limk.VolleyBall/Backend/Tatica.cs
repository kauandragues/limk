using System.Text;

namespace Limk.VolleyBall.Backend
{
    public class Tatica
    {
        private int id;
        private int equipeId;
        private string nome;
        private string descricao;
        private int? tipoTaticaId;
        private int? sistemaJogoId;
        private List<QuadroTatica> quadros = new List<QuadroTatica>();

        public Tatica()
        {
            id = 0;
            equipeId = 0;
            nome = "Nova tática";
            descricao = "";
            tipoTaticaId = null;
            sistemaJogoId = null;
        }

        public Tatica(int id, int equipeId, string nome, string descricao,
            int? tipoTaticaId = null, int? sistemaJogoId = null)
        {
            this.id = id;
            this.equipeId = equipeId;
            this.nome = nome;
            this.descricao = descricao;
            this.tipoTaticaId = tipoTaticaId;
            this.sistemaJogoId = sistemaJogoId;
        }

        public int GetId()
        {
            return id;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

        public int GetEquipeId()
        {
            return equipeId;
        }

        public void SetEquipeId(int equipeId)
        {
            this.equipeId = equipeId;
        }

        public string GetNome()
        {
            return nome;
        }

        public void SetNome(string nome)
        {
            this.nome = nome;
        }

        public string GetDescricao()
        {
            return descricao;
        }

        public void SetDescricao(string descricao)
        {
            this.descricao = descricao;
        }

        public int? GetTipoTaticaId()
        {
            return tipoTaticaId;
        }

        public void SetTipoTaticaId(int? tipoTaticaId)
        {
            this.tipoTaticaId = tipoTaticaId;
        }

        public int? GetSistemaJogoId()
        {
            return sistemaJogoId;
        }

        public void SetSistemaJogoId(int? sistemaJogoId)
        {
            this.sistemaJogoId = sistemaJogoId;
        }

        public List<QuadroTatica> GetQuadros()
        {
            return quadros;
        }

        public void SetQuadros(List<QuadroTatica> quadros)
        {
            this.quadros = quadros;
        }

        public void Validar()
        {
            if (GetId() < 0 || GetEquipeId() <= 0)
            {
                throw new ValidacaoException("Selecione uma equipe para a tática.");
            }
            if (string.IsNullOrWhiteSpace(GetNome()) || Texto.ContarCaracteres(GetNome().Trim()) > 100)
            {
                throw new ValidacaoException("Informe um nome de tática com até 100 caracteres.");
            }
            // O limite do banco para a descrição é medido em bytes, não em caracteres.
            if (Encoding.UTF8.GetByteCount(GetDescricao()) > 65535)
            {
                throw new ValidacaoException("A descrição da tática está muito longa.");
            }
            if (GetTipoTaticaId() <= 0 || GetSistemaJogoId() <= 0)
            {
                throw new ValidacaoException("Selecione um tipo e um sistema válidos, ou deixe-os sem definição.");
            }
            if (GetQuadros().Count == 0)
            {
                throw new ValidacaoException("A tática precisa ter pelo menos uma fase.");
            }
            foreach (QuadroTatica quadro in GetQuadros())
            {
                ValidarQuadro(quadro);
            }
        }

        private static void ValidarQuadro(QuadroTatica quadro)
        {
            if (string.IsNullOrWhiteSpace(quadro.GetNome()) || Texto.ContarCaracteres(quadro.GetNome().Trim()) > 100)
            {
                throw new ValidacaoException("Cada fase precisa de um nome com até 100 caracteres.");
            }
            int? rotacao = quadro.GetRotacao();
            if (quadro.GetDuracaoMs() <= 0 || (rotacao.HasValue && (rotacao.Value < 1 || rotacao.Value > 6)))
            {
                throw new ValidacaoException("Informe uma duração positiva e uma rotação entre P1 e P6, ou sem rotação.");
            }

            // Cada lado pode usar a mesma camisa, mas não pode repetir um número dentro dele.
            List<int> numerosPrincipal = new List<int>();
            List<int> numerosAdversario = new List<int>();
            List<int> vinculos = new List<int>();
            foreach (AtletaQuadro atleta in quadro.GetAtletas())
            {
                int numero = atleta.GetNumeroCamisa();
                LadoAtleta lado = atleta.GetLado();
                if (numero < 1 || numero > 99 || (lado != LadoAtleta.Principal && lado != LadoAtleta.Adversario))
                {
                    throw new ValidacaoException("Os atletas precisam de números entre 1 e 99 e um lado válido.");
                }

                List<int> numerosDoLado = numerosPrincipal;
                if (lado == LadoAtleta.Adversario)
                {
                    numerosDoLado = numerosAdversario;
                }
                if (numerosDoLado.Contains(numero))
                {
                    throw new ValidacaoException("Dois atletas do mesmo lado não podem ter o mesmo número na mesma fase.");
                }
                numerosDoLado.Add(numero);

                int? vinculo = atleta.GetEquipeAtletaId();
                if (vinculo.HasValue)
                {
                    if (vinculos.Contains(vinculo.Value))
                    {
                        throw new ValidacaoException("Um atleta cadastrado só pode aparecer uma vez em cada fase.");
                    }
                    vinculos.Add(vinculo.Value);
                }
                ValidarCoordenadas(atleta.GetX(), atleta.GetY());
            }

            PosicaoBola? bola = quadro.GetBola();
            if (bola != null)
            {
                ValidarCoordenadas(bola.GetX(), bola.GetY());
            }
        }

        private static void ValidarCoordenadas(float x, float y)
        {
            // IsFinite rejeita valores especiais, como infinito, que não representam posições na quadra.
            if (!float.IsFinite(x) || !float.IsFinite(y) || x < 0 || x > 1 || y < 0 || y > 1)
            {
                throw new ValidacaoException("As posições dos elementos precisam permanecer dentro da quadra.");
            }
        }
    }
}
