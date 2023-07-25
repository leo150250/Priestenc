using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Priestenc.Musica;

namespace Priestenc
{
    class Compasso
    {
        private Figura[] figuras;
        private Tom tom;
        private int tempos;
        private int divisao;
        private Clave clave;
        public Compasso()
        {
            tempos = 4;
            divisao = 4;
            tom = Tom.C;
            clave = Clave.G;
            InserirFigura("-4");
        }
        
        public Figura InserirFigura(string argCodFigura)
        {
            Figura novaFigura = new Figura(this);
            
            return novaFigura;
        }
    }
}
