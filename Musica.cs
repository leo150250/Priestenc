using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priestenc
{
    public static class Musica
    {
        public enum Nota { C, D, E, F, G, A, B};

        public enum Acidente { Natural, Sustenido, Bemol, DobradoSustenido, DobradoBemol};

        public enum Tom { C, G, D, A, E, B, Fs, Cs, Db, Ab, Eb, Bb, F};

        public enum Duracao { Breve, Semibreve, Minima, Seminima, Colcheia, Semicolcheia, Fusa, Semifusa};

        public enum Clave { G, F, C, R};
    }
}
