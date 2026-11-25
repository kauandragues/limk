namespace Limk.VolleyBall.Backend
{
    public abstract class ElementoQuadra
    {
        // Coordenadas normalizadas em relação à quadra, entre 0 e 1.
        private float x;
        private float y;

        protected ElementoQuadra()
        {
            x = 0;
            y = 0;
        }

        protected ElementoQuadra(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float GetX()
        {
            return x;
        }

        public void SetX(float x)
        {
            this.x = x;
        }

        public float GetY()
        {
            return y;
        }

        public void SetY(float y)
        {
            this.y = y;
        }
    }
}
