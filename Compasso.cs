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
        public int numerador;
        public int denominador;
        public Barra barraInicio = Barra.Simples;
        public Barra barraFim = Barra.Simples;
        private int bpm = 120;
        public Clave clave;
        public Compasso()
        {
            numerador = 4;
            denominador = 4;
            tom = Tom.C;
            clave = Clave.G;
            InserirFigura("-4");
        }
        public Compasso(int argBPM) : base()
        {
            bpm = argBPM;
        }
        
        public Figura InserirFigura(string argCodFigura)
        {
            Figura novaFigura = new Figura(this);
            return novaFigura;
        }
        public void Debug(int argNivel = 0)
        {
            string prefixo = "";
            for (int i = 0; i < argNivel; i++)
            {
                prefixo += "|";
            }
            Console.WriteLine(prefixo + "==COMPASSO");
            //Console.WriteLine(prefixo + $"BPM: {bpm}");
        }
    }
}
