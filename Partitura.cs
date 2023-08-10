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
        public Instrumento[] instrumentos = new Instrumento[64];
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
        public int ObterNumCompassos()
        {
            return numCompassos;
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
        public Compasso[] AdicionarCompasso(int argBPM)
        {
            int qtdCompassos = 0;
            foreach (Instrumento instrumento in instrumentos)
            {
                qtdCompassos += instrumento.numPautas;
            }
            Compasso[] compassosAdicionados = new Compasso[qtdCompassos];
            int numCompassos = 0;
            foreach (Instrumento instrumento in instrumentos)
            {
                Compasso[] novosCompassos = instrumento.AdicionarCompasso(argBPM);
                for (int i = 0; i < novosCompassos.Length; i++)
                {
                    compassosAdicionados[numCompassos++] = novosCompassos[i];
                }
            }
            return compassosAdicionados;
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
