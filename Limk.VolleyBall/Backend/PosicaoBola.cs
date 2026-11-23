namespace Limk.VolleyBall.Backend
{
    public class PosicaoBola : ElementoQuadra
    {
        public PosicaoBola Copiar()
        {
            PosicaoBola copia = new PosicaoBola();
            copia.SetX(GetX());
            copia.SetY(GetY());
            return copia;
        }
    }
}
