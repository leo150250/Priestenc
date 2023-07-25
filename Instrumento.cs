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
        private Pauta[] pautas;
        private string nome;
        private Partitura partitura;
        public Instrumento(Partitura argPartitura, string argNome = "Piano")
        {
            partitura = argPartitura;
            nome = argNome;
            pautas = new Pauta[1];
            pautas[0] = new Pauta(Clave.G);
        }
        public Instrumento(Partitura argPartitura, string argNome, Pauta[] argPautas)
        {
            partitura = argPartitura;
            nome = argNome;
            pautas = argPautas;
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
        }
    }
}
