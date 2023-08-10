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

        public static string ObterAltura(int argAltura)
        {
            int alturaRelativa = argAltura % 12;
            int escala = argAltura / 12;
            string retorno = "";
            switch (alturaRelativa)
            {
                case 0: retorno = "C "; break;
                case 1: retorno = "C#/Db "; break;
                case 2: retorno = "D "; break;
                case 3: retorno = "D#/Eb "; break;
                case 4: retorno = "E "; break;
                case 5: retorno = "F "; break;
                case 6: retorno = "F#/Gb "; break;
                case 7: retorno = "G "; break;
                case 8: retorno = "G#/Ab "; break;
                case 9: retorno = "A "; break;
                case 10: retorno = "A#/Bb "; break;
                case 11: retorno = "B "; break;
                default: retorno = "? "; break;
            }
            retorno += escala.ToString();
            return retorno;
        }

        public enum Acidente { Natural, Sustenido, Bemol, DobradoSustenido, DobradoBemol};

        public enum Tom { C, G, D, A, E, B, Fs, Cs, Db, Ab, Eb, Bb, F};

        public enum Duracao { Breve, Semibreve, Minima, Seminima, Colcheia, Semicolcheia, Fusa, Semifusa, Quadrifusa};

        public enum Clave { G, F, C, C4, G8va, G8vb, F8vb, R, T};

        public static string ObterNomeClave(Clave argClave)
        {
            switch (argClave)
            {
                case Clave.G: return "Sol";
                case Clave.F: return "Fá";
                case Clave.C: return "Dó";
                case Clave.C4: return "Dó (4ª linha)";
                case Clave.G8va: return "Sol (8va)";
                case Clave.G8vb: return "Sol (8vb)";
                case Clave.F8vb: return "Fá (8vb)";
                case Clave.R: return "Ritmo/Percussão";
                case Clave.T: return "Tablatura";
                default: return "---";

            }
        }
        public enum Armadura {C, F, Bb, Eb, Ab, Db, Gb, Cb, G, D, A, E, B, Fs, Cs }

        public static string ObterNomeArmadura(Armadura argArmadura)
        {
            switch (argArmadura)
            {
                case Armadura.C: return "C";
                case Armadura.F: return "F (1 bemol)";
                case Armadura.Bb: return "Bb (2 bemois)";
                case Armadura.Eb: return "Eb (3 bemois)";
                case Armadura.Ab: return "Ab (4 bemois)";
                case Armadura.Db: return "Db (5 bemois)";
                case Armadura.Gb: return "Gb (6 bemois)";
                case Armadura.Cb: return "Db (7 bemois)";
                case Armadura.G: return "G (1 sustenido)";
                case Armadura.D: return "D (2 sustenidos)";
                case Armadura.A: return "A (3 sustenidos)";
                case Armadura.E: return "E (4 sustenidos)";
                case Armadura.B: return "B (5 sustenidos)";
                case Armadura.Fs: return "F# (6 sustenidos)";
                case Armadura.Cs: return "C# (7 sustenidos)";
                default: return "---";
            }
        }
        
        public enum Barra { Simples, Inicio, Repeticao, DuplaInicio, Volta, Fim, DuplaFim, Tracejada, Vazia}

        public static string ObterSimbolo(int argHexSimbolo)
        {
            string retorno = "";
            switch (argHexSimbolo)
            {
                case 0x1d: retorno = "dinâmica (hairpin)"; break;
                case 0x21: retorno = "ligadura"; break;
                case 0x80: retorno = "ppp"; break;
                case 0x81: retorno = "pp"; break;
                case 0x82: retorno = "p"; break;
                case 0x83: retorno = "mp"; break;
                case 0x84: retorno = "mf"; break;
                case 0x85: retorno = "f"; break;
                case 0x86: retorno = "ff"; break;
                case 0x87: retorno = "fff"; break;
                case 0x88: retorno = "sfz"; break;
                case 0x89: retorno = "sffz"; break;
                case 0x8a: retorno = "fp"; break;
                case 0xbe: retorno = "acento (>)"; break;
                case 0xcc: retorno = "fermata"; break;
                case 0xcd: retorno = "fermata (inv)"; break;
                default:
                    Console.WriteLine($"SÍMBOLO NÃO IDENTIFICADO: {Convert.ToString(argHexSimbolo, 16)}");
                    retorno = $"NÃO IDENTIFICADO {Convert.ToString(argHexSimbolo,16)}";
                    break;
            }
            return retorno;
        }
    }
}
