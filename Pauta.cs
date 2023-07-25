using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Priestenc.Musica;

namespace Priestenc
{
    class Pauta
    {
        private Clave clave;
        public Pauta(Clave argClave = Clave.G)
        {
            clave = argClave;
        }
    }
}
