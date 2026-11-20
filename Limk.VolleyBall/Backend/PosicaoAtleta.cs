using System;
using System.Collections.Generic;
using System.Text;

namespace Limk.VolleyBall.Backend
{
    public enum LadoAtleta
    {
        Principal,
        Adversario
    }

    public class PosicaoAtleta : PosicaoElementos
    {
        public int AtletaId { get; set; }
        public int? EquipeAtletaId { get; set; }
        public LadoAtleta Lado { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
