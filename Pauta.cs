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
        public Clave clave;
        public Compasso[] compassos;
        private Instrumento instrumento;
        private int numCompassos = 0;
        public Pauta(Clave argClave)
        {
            clave = argClave;
        }
        public Pauta(Instrumento argInstrumento, Clave argClave = Clave.G)
        {
            instrumento = argInstrumento;
            clave = argClave;
            DefinirCompassos();
        }
        public int ObterNumCompassos()
        {
            return instrumento.ObterNumCompassos();
        }
        public void DefinirInstrumento(Instrumento argInstrumento)
        {
            instrumento = argInstrumento;
            DefinirCompassos();
        }
        public void DefinirCompassos()
        {
            compassos = new Compasso[ObterNumCompassos()];
            numCompassos = 0;
        }
        public Compasso AdicionarCompasso(int argBPM)
        {
            Compasso novoCompasso = new Compasso(argBPM);
            compassos[numCompassos++] = novoCompasso;
            return novoCompasso;
        }
        public void Debug(int argNivel = 0)
        {
            string prefixo = "";
            for (int i = 0; i < argNivel; i++)
            {
                prefixo += "|";
            }
            Console.WriteLine(prefixo + "==PAUTA");
            Console.WriteLine(prefixo + $"Clave: {ObterNomeClave(clave)}");
            if (numCompassos>0)
            {
                foreach (Compasso compasso in compassos)
                {
                    compasso.Debug(argNivel + 1);
                }
            }
        }
    }
}
