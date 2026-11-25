using Limk.VolleyBall.Backend;
using System.Drawing;

namespace Limk.VolleyBall.User_Constrols
{
    internal sealed class MovimentoElemento
    {
        private ElementoQuadra elemento;
        private PointF inicio;
        private PointF destino;

        public MovimentoElemento(ElementoQuadra elemento, PointF destino)
        {
            this.elemento = elemento;
            inicio = new PointF(elemento.GetX(), elemento.GetY());
            this.destino = destino;
        }

        public ElementoQuadra GetElemento()
        {
            return elemento;
        }

        public void SetElemento(ElementoQuadra elemento)
        {
            this.elemento = elemento;
        }

        public PointF GetInicio()
        {
            return inicio;
        }

        public void SetInicio(PointF inicio)
        {
            this.inicio = inicio;
        }

        public PointF GetDestino()
        {
            return destino;
        }

        public void SetDestino(PointF destino)
        {
            this.destino = destino;
        }
    }
}
