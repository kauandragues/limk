using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Limk.VolleyBall.User_Constrols
{
    public partial class TelaInicial : UserControl
    {
        private readonly Dictionary<Control, (Rectangle Limites, Font Fonte)> _original = new();
        private readonly Dictionary<Control, Font> _fontesCriadas = new();
        private readonly Size _tamanhoDesign = new Size(726, 592);

        public TelaInicial()
        {
            InitializeComponent();
            DoubleBuffered = true;

            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right; // Salvar (cabeçalho)
            GuardarOriginais(panelTelas);
            Resize += (s, e) => EscalarTela();
        }

        private void GuardarOriginais(Control pai)
        {
            foreach (Control c in pai.Controls)
            {
                if (c == panel1) continue; // cabeçalho (Dock Top) fica como está

                if (c is Label l) l.AutoSize = false;
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                _original[c] = (c.Bounds, (Font)c.Font.Clone());
                GuardarOriginais(c);
            }
        }

        private void EscalarTela()
        {
            if (_original.Count == 0 || ClientSize.Width == 0 || ClientSize.Height == 0) return;

            float sx = Math.Max(1f, ClientSize.Width / (float)_tamanhoDesign.Width);
            float sy = Math.Max(1f, ClientSize.Height / (float)_tamanhoDesign.Height);
            float sf = Math.Min(sx, sy) / (DeviceDpi / 96f);

            SuspendLayout();
            foreach (var (c, o) in _original)
            {
                c.Bounds = new Rectangle(
                    (int)(o.Limites.X * sx), (int)(o.Limites.Y * sy),
                    (int)(o.Limites.Width * sx), (int)(o.Limites.Height * sy));

                var nova = new Font(o.Fonte.FontFamily, o.Fonte.Size * sf, o.Fonte.Style);
                c.Font = nova;
                if (_fontesCriadas.TryGetValue(c, out var velha)) velha.Dispose();
                _fontesCriadas[c] = nova;
            }
            ResumeLayout(true);
        }
    }
}
