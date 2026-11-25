namespace Limk.VolleyBall.Backend
{
    public class PosicaoBola : ElementoQuadra
    {
        public PosicaoBola()
        {
            SetX(0);
            SetY(0);
        }

        public PosicaoBola(float x, float y)
        {
            SetX(x);
            SetY(y);
        }

        public PosicaoBola Copiar()
        {
            return new PosicaoBola(GetX(), GetY());
        }
    }
}
