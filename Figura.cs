using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Priestenc.Musica;

namespace Priestenc
{
    class Figura
    {
        private bool pausa;
        private int oitava;
        private Nota nota;
        private Acidente acidente;
        private Duracao duracao;
        private Compasso compasso;

        public Figura(Compasso argCompasso)
        {
            compasso = argCompasso;
            pausa = false;
            oitava = 3;
            nota = Nota.C;
        }
    }
}
