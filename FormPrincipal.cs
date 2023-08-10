using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static Priestenc.Musica;

namespace Priestenc
{
    public partial class FormPrincipal : Form
    {
        private int numLinhasLog = 0;
        private int maxLinhasLog = 5;
        private string[] linhasLog;
        private FileStream leitorArquivo;
        public FormPrincipal()
        {
            InitializeComponent();
            textBoxLog.Text = string.Empty;
        }

        private void telaPrincipal_Load(object sender, EventArgs e)
        {
            linhasLog = new string[maxLinhasLog];
            CarregarArquivo("039hcgr.enc");
        }

        public void CarregarArquivo(string argArquivo)
        {
            Debug($"Carregando arquivo \"{argArquivo}\"...");
            try
            {
                leitorArquivo = new FileStream(argArquivo, FileMode.Open);
                Debug($"Arquivo encontrado!");
                int tamanho = (int)leitorArquivo.Length;
                byte[] bytes = new byte[tamanho];
                leitorArquivo.Read(bytes, 0, tamanho);
                //Ler número de sistemas
                int numSistemas = bytes[0x2e];
                //Ler número de páginas
                int numPaginas = bytes[0x30];
                //Ler número de pautas por sistema
                int numPautasSistema = bytes[0x32];
                Debug($"Pautas por sistema: {numPautasSistema}");
                //Ler numero de compassos
                int numCompassos = bytes[0x34];
                Debug($"Compassos: {numCompassos}");
                //Ler instrumentos
                int numInstrumentos = bytes[0x37];
                Debug($"Instrumentos: {numInstrumentos}");
                int ponteiroInstrumentos = 0xc2;
                Partitura novaPartitura = new Partitura(numInstrumentos,numCompassos);
                for (int i = 0; i < numInstrumentos; i++)
                {
                    int tamanhoBloco = BitConverter.ToInt16(bytes, ponteiroInstrumentos + 0x4);
                    //Debug(tamanhoBloco.ToString());
                    string nomeInstrumento = "";
                    int nomeInicio = ponteiroInstrumentos + 0x8;
                    while (bytes[nomeInicio]!=0)
                    {
                        nomeInstrumento += (char)bytes[nomeInicio];
                        nomeInicio++;
                    }
                    //Debug(nomeInstrumento);
                    Instrumento novoInstrumento = novaPartitura.AdicionarInstrumento(nomeInstrumento);
                    //Não sei por quê tive que somar esses números, mas pelo menos faz bater os ponteiros certinho
                    ponteiroInstrumentos += (tamanhoBloco * 2) + 0x12;
                   // Debug($"Próximo ponteiro: {ponteiroInstrumentos.ToString("x4")}");
                }

                //Ler estruturas LINE (Sistemas)
                int ponteiroPaginas = ponteiroInstrumentos;
                string textoPaginas = ObterStringBytes(bytes, ponteiroPaginas, 4);
                //Debug(textoPaginas);
                int ponteiroSistemas = ponteiroPaginas + (34 * numPaginas);
                for (int i = 0; i < numSistemas; i++)
                {
                    //22 bytes no cabeçalho dos sistemas + 30 bytes por bloco.
                    string textoSistemas = ObterStringBytes(bytes, ponteiroSistemas, 4);
                    //Debug(textoSistemas);
                    ponteiroSistemas += 4;
                    //Debug($"Num compassos: {bytes[ponteiroSistemas + 0x10]}");
                    ponteiroSistemas += 16;
                    for (int j = 0; j < numPautasSistema; j++)
                    {
                        //Debug(ObterHexBytes(bytes, ponteiroSistemas, 30));

                        //Obter clave e armadura de cada instrumento aqui? Ou definir para cada compasso?

                        //Debug($"Clave: {ObterNomeClave((Clave)bytes[ponteiroSistemas + 0xf])}");
                        //Debug($"Armadura: {ObterNomeArmadura((Armadura)bytes[ponteiroSistemas + 0x10])}");
                        novaPartitura.instrumentos[j].pautas[0].clave = (Clave)bytes[ponteiroSistemas + 0xf];
                        ponteiroSistemas += 30;
                    }
                    //Por quê 14?
                    ponteiroSistemas += 14;
                    //Debug(ObterHexBytes(bytes, ponteiroSistemas, 32));
                }

                //Ler estruturas MEAS (Compassos)
                //int ponteiroCompassos = ponteiroSistemas + ((394 * numSistemas));
                int ponteiroCompassos = ponteiroSistemas;
                for (int i = 0; i < numCompassos; i++)
                {
                    string textoCompassos = ObterStringBytes(bytes, ponteiroCompassos, 4);
                    //Debug(textoCompassos);
                    ponteiroCompassos += 4;
                    int bytesAposCabecalho = BitConverter.ToInt16(bytes, ponteiroCompassos);
                    //Debug(ObterHexBytes(bytes, ponteiroCompassos - 4, bytesAposCabecalho + 4));
                    
                    //Debug($"Bytes após cabecalho: {bytesAposCabecalho}");
                    //Debug(ObterHexBytes(bytes, ponteiroCompassos, bytesAposCabecalho + 0x3a));
                    Compasso[] novosCompassos = novaPartitura.AdicionarCompasso(bytes[ponteiroCompassos + 0x04]);
                    foreach (Compasso novoCompasso in novosCompassos)
                    {
                        novoCompasso.numerador = bytes[ponteiroCompassos + 0x0c];
                        novoCompasso.denominador = bytes[ponteiroCompassos + 0x0d];
                        novoCompasso.barraInicio = (Barra)bytes[ponteiroCompassos + 0x10];
                        novoCompasso.barraFim = (Barra)bytes[ponteiroCompassos + 0x11];
                    }
                    //Debug(novosCompassos.Length.ToString());
                    ponteiroCompassos += 0x3a;
                    //Debug(ObterHexBytes(bytes, ponteiroCompassos, 32));
                    if ((bytes[ponteiroCompassos]==0x00) && (bytes[ponteiroCompassos+1]==0x00)) {
                        bool continuarLeitura = true;
                        int ponteiroObjeto = ponteiroCompassos + 2;
                        int duracaoObjeto;
                        int numObjetos = 0;
                        Debug($"Compasso #{i}");
                        while (continuarLeitura) {
                            duracaoObjeto = 0x0;
                            continuarLeitura = false;
                            int tipoObjeto = bytes[ponteiroObjeto];
                            switch (tipoObjeto)
                            {
                                case 0x10:
                                    //Debug("----- Clave");
                                    //Debug($"Pauta: {bytes[ponteiroObjeto + 0x02]}");
                                    ponteiroObjeto += 0x03;
                                    //Debug($"Clave: {ObterNomeClave((Clave)bytes[ponteiroObjeto])}");
                                    duracaoObjeto = 0x1a;
                                    //Debug(ObterHexBytes(bytes, ponteiroObjeto, duracaoObjeto));
                                    continuarLeitura = true;
                                    break;
                                case 0x20:
                                    //Debug("Armadura");
                                    break;
                                case 0x30:
                                    //Debug("----- Ligadura de duração");
                                    //Aparentemente, a ligadura de duração possui 18 bytes, incluindo cabeçalho
                                    ponteiroObjeto += 0x03;
                                    duracaoObjeto = 0x0f;
                                    //Debug(ObterHexBytes(bytes, ponteiroObjeto, duracaoObjeto));
                                    continuarLeitura = true;
                                    break;
                                case 0x40:
                                    //Debug("----- Conector");
                                    //Debug($"Pauta: {bytes[ponteiroObjeto + 0x02]}");
                                    ponteiroObjeto += 0x03;
                                    //duracaoObjeto = 0x0d;
                                    //Debug($"Num. Conexoes: {bytes[ponteiroObjeto]}");
                                    //Debug(ObterHexBytes(bytes, ponteiroObjeto, 4));
                                    for (int j = 0; j < bytes[ponteiroObjeto]; j++)
                                    {
                                        //Debug(ObterHexBytes(bytes, ponteiroObjeto + 4 + (16 * j), 16));
                                        //duracaoObjeto += 0x0f;
                                    }
                                    duracaoObjeto = 4 + (bytes[ponteiroObjeto] * 16) + 7;
                                    //Debug(ObterHexBytes(bytes, ponteiroObjeto + 4 + (bytes[ponteiroObjeto] * 16), 7));
                                    //Debug(ObterHexBytes(bytes, ponteiroObjeto, duracaoObjeto));
                                    continuarLeitura = true;
                                    break;
                                case 0x50:
                                    //Debug("----- Símbolo flutuante");
                                    //Debug($"Pauta: {bytes[ponteiroObjeto+0x02]}");
                                    ponteiroObjeto += 0x03;
                                    ObterSimbolo(bytes[ponteiroObjeto]);
                                    //Debug($"Símbolo: {ObterSimbolo(bytes[ponteiroObjeto])}");
                                    switch (bytes[ponteiroObjeto])
                                    {
                                        case 0x21:
                                        case 0x1d:
                                            duracaoObjeto = 0x19;
                                            break;
                                        default: duracaoObjeto = 0x0d; break;
                                    }
                                    //Debug(ObterHexBytes(bytes, ponteiroObjeto, duracaoObjeto));
                                    continuarLeitura = true;
                                    break;
                                case 0x60:
                                    Debug("Canto");
                                    break;
                                case 0x70:
                                    Debug("Cifra");
                                    ponteiroObjeto += 0x09;
                                    duracaoObjeto = 0x0d;
                                    continuarLeitura = true;
                                    break;
                                case 0x80:
                                    //Debug("----- Pausa");
                                    //Debug($"Pauta: {bytes[ponteiroObjeto + 0x02]}");
                                    ponteiroObjeto += 0x03;
                                    duracaoObjeto = 0x0f;
                                    //Debug(ObterHexBytes(bytes, ponteiroObjeto, duracaoObjeto));
                                    continuarLeitura = true;
                                    break;
                                case 0x90:
                                case 0x91:
                                case 0x92:
                                case 0x93:
                                    //Debug("----- Nota");
                                    //Debug($"Pauta: {bytes[ponteiroObjeto + 0x02]}");
                                    ponteiroObjeto += 0x03;
                                    //Debug($"Duração: {(Duracao)bytes[ponteiroObjeto]}");
                                    //Debug($"Altura: {ObterAltura(bytes[ponteiroObjeto + 0x0a])}");
                                    duracaoObjeto = 0x19;
                                    //Debug(ObterHexBytes(bytes, ponteiroObjeto, duracaoObjeto));
                                    continuarLeitura = true;
                                    break;
                                default:
                                    Debug($"NÃO IDENTIFICADO: {ObterHexBytes(bytes, ponteiroObjeto, 1)}");
                                    Debug(ObterHexBytes(bytes, ponteiroObjeto, 16));
                                    continuarLeitura = false;
                                    break;
                            }
                            //ponteiroObjeto += 1;
                            if (!continuarLeitura)
                            {
                                Debug("!! FIM INESPERADO DA LEITURA !!");
                                Debug(ObterHexBytes(bytes, ponteiroObjeto - 8, 8));
                                Debug(ObterHexBytes(bytes, ponteiroObjeto, 32));
                            }
                            ponteiroObjeto += duracaoObjeto;
                            numObjetos++;
                            if ((bytes[ponteiroObjeto-2]==0xff) && (bytes[ponteiroObjeto-1]==0xff)) {
                                Debug($"Objetos lidos: {numObjetos}");
                                continuarLeitura = false;
                            }
                        }
                    }
                    //Debug(ObterHexBytes(bytes, ponteiroCompassos, 16));
                    ponteiroCompassos += bytesAposCabecalho;
                }
                Debug(ObterStringBytes(bytes, ponteiroCompassos, 16));
                //novaPartitura.Debug();
                Debug("PRONTO!");
            } catch
            {
                Debug($"Erro: O arquivo \"{argArquivo}\" não existe!");
            }
        }

        public void Debug(string argTexto)
        {
            Console.WriteLine(argTexto);
            textBoxLog.Text += $"{argTexto}\r\n";
        }
        public void Debug(char argTexto)
        {
            Console.WriteLine(argTexto);
            textBoxLog.Text += $"{argTexto}\r\n";
        }
        public string ObterStringBytes(byte[] argBytes, int argStart, int argLength)
        {
            string retorno = "";
            for (int i = 0; i < argLength; i++)
            {
                retorno += (char)argBytes[argStart + i];
            }
            return retorno;
        }
        public string ObterHexBytes(byte[] argBytes, int argStart, int argLength)
        {
            string retorno = "";
            for (int i = 0; i < argLength; i++)
            {
                retorno += Convert.ToString(argBytes[argStart + i],16).ToUpper().PadLeft(2,'0') + " ";
            }
            return retorno;
        }
    }
}
