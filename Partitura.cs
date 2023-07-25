using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static Priestenc.Musica;

namespace Priestenc
{
    class Partitura
    {
        private int numCompassos;
        private int numInstrumentos = 0;
        private Instrumento[] instrumentos = new Instrumento[64];
        public Partitura()
        {
            numCompassos = 4;
            //AdicionarInstrumento("Piano");
        }
        public Partitura(int argNumInstrumentos = 1, int argNumCompassos = 8)
        {
            instrumentos = new Instrumento[argNumInstrumentos];
            numCompassos = argNumCompassos;
        }
        public void DefinirCompassos(int argNumCompassos)
        {
            numCompassos = argNumCompassos;
        }
        public Instrumento AdicionarInstrumento(string argNome)
        {
            Instrumento novoInstrumento = new Instrumento(this, argNome);
            instrumentos[numInstrumentos++] = novoInstrumento;
            return novoInstrumento;
        }
        public void Debug(int argNivel = 0)
        {
            string prefixo = "";
            for (int i = 0; i < argNivel; i++)
            {
                prefixo += "|";
            }
            Console.WriteLine(prefixo + "==PARTITURA");
            Console.WriteLine(prefixo + $"NumCompassos: {numCompassos}");
            Console.WriteLine(prefixo + $"NumInstrumentos: {numInstrumentos}");
            if (numInstrumentos>0)
            {
                foreach (Instrumento instrumento in instrumentos)
                {
                    instrumento.Debug(argNivel + 1);
                }
            }
        }
    }
}
