using Limk.VolleyBall.Backend;
using System.Drawing;

namespace Limk.VolleyBall.User_Constrols
{
    internal sealed class MovimentoElemento
    {
        // { get; } permite ler os dados de fora, mas só o construtor pode defini-los.
        public ElementoQuadra Elemento { get; }
        public PointF Inicio { get; }
        public PointF Destino { get; }

        public MovimentoElemento(ElementoQuadra elemento, PointF destino)
        {
            Elemento = elemento;
            Inicio = new PointF(elemento.GetX(), elemento.GetY());
            Destino = destino;
        }
    }
}
