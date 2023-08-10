using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Priestenc.Musica;

namespace Priestenc
{
    class Instrumento
    {
        public Pauta[] pautas;
        public int numPautas = 0;
        private string nome;
        private Partitura partitura;
        public Instrumento(Partitura argPartitura, string argNome = "default")
        {
            partitura = argPartitura;
            nome = argNome;
            numPautas = 1;
            pautas = new Pauta[numPautas];
            pautas[0] = new Pauta(this, Clave.G);
        }
        public Instrumento(Partitura argPartitura, string argNome, Pauta[] argPautas)
        {
            partitura = argPartitura;
            nome = argNome;
            pautas = argPautas;
            foreach (Pauta pauta in pautas)
            {
                pauta.DefinirInstrumento(this);
            }
        }
        public int ObterNumCompassos()
        {
            return partitura.ObterNumCompassos();
        }
        public Compasso[] AdicionarCompasso(int argBPM)
        {
            Compasso[] novosCompassos = new Compasso[numPautas];
            int numCompassos = 0;
            foreach (Pauta pauta in pautas)
            {
                novosCompassos[numCompassos++] = pauta.AdicionarCompasso(argBPM);
            }
            return novosCompassos;
        }
        public void Debug(int argNivel = 0)
        {
            string prefixo = "";
            for (int i = 0; i < argNivel; i++)
            {
                prefixo += "|";
            }
            Console.WriteLine(prefixo + "==INSTRUMENTO");
            Console.WriteLine(prefixo + $"Nome: {nome}");
            if (numPautas>0)
            {
                foreach (Pauta pauta in pautas)
                {
                    pauta.Debug(argNivel + 1);
                }
            }
        }
    }
}
