using System.Text;

namespace Limk.VolleyBall.Backend
{
    public class Tatica
    {
        private int id;
        private int equipeId;
        private string nome = "Nova tática";
        private string descricao = "";
        private int? tipoTaticaId;
        private int? sistemaJogoId;
        private List<QuadroTatica> quadros = new List<QuadroTatica>();

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
                throw new ValidacaoException("Selecione uma equipe para a tática.");
            if (string.IsNullOrWhiteSpace(GetNome()) || GetNome().Trim().EnumerateRunes().Count() > 100)
                throw new ValidacaoException("Informe um nome de tática com até 100 caracteres.");
            if (Encoding.UTF8.GetByteCount(GetDescricao()) > 65535)
                throw new ValidacaoException("A descrição da tática está muito longa.");
            if (GetTipoTaticaId() <= 0 || GetSistemaJogoId() <= 0)
                throw new ValidacaoException("Selecione um tipo e um sistema válidos, ou deixe-os sem definição.");
            if (GetQuadros().Count == 0)
                throw new ValidacaoException("A tática precisa ter pelo menos uma fase.");
            foreach (QuadroTatica quadro in GetQuadros())
            {
                if (string.IsNullOrWhiteSpace(quadro.GetNome()) || quadro.GetNome().Trim().EnumerateRunes().Count() > 100)
                    throw new ValidacaoException("Cada fase precisa de um nome com até 100 caracteres.");
                if (quadro.GetDuracaoMs() <= 0 || (quadro.GetRotacao().HasValue && (quadro.GetRotacao() < 1 || quadro.GetRotacao() > 6)))
                    throw new ValidacaoException("Informe uma duração positiva e uma rotação entre P1 e P6, ou sem rotação.");
                HashSet<(LadoAtleta, int)> numeros = new HashSet<(LadoAtleta, int)>();
                HashSet<int> vinculos = new HashSet<int>();
                foreach (AtletaQuadro atleta in quadro.GetAtletas())
                {
                    if (atleta.GetNumeroCamisa() < 1 || atleta.GetNumeroCamisa() > 99 ||
                        (atleta.GetLado() != LadoAtleta.Principal && atleta.GetLado() != LadoAtleta.Adversario))
                        throw new ValidacaoException("Os atletas precisam de números entre 1 e 99 e um lado válido.");
                    if (!numeros.Add((atleta.GetLado(), atleta.GetNumeroCamisa())))
                        throw new ValidacaoException("Dois atletas do mesmo lado não podem ter o mesmo número na mesma fase.");
                    int? vinculo = atleta.GetEquipeAtletaId();
                    if (vinculo.HasValue && !vinculos.Add(vinculo.Value))
                        throw new ValidacaoException("Um atleta cadastrado só pode aparecer uma vez em cada fase.");
                    ValidarCoordenadas(atleta.GetX(), atleta.GetY());
                }
                PosicaoBola? bola = quadro.GetBola();
                if (bola != null)
                    ValidarCoordenadas(bola.GetX(), bola.GetY());
            }
        }

        private static void ValidarCoordenadas(float x, float y)
        {
            if (!float.IsFinite(x) || !float.IsFinite(y) || x < 0 || x > 1 || y < 0 || y > 1)
                throw new ValidacaoException("As posições dos elementos precisam permanecer dentro da quadra.");
        }
    }
}
