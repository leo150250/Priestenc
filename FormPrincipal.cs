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
                //Ler numero de compassos
                int numCompassos = bytes[0x34];
                //Ler instrumentos
                int numInstrumentos = bytes[0x37];
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
                    novaPartitura.AdicionarInstrumento(nomeInstrumento);
                    //Não sei por quê tive que somar esses números, mas pelo menos faz bater os ponteiros certinho
                    ponteiroInstrumentos += (tamanhoBloco * 2) + 0x12;
                   // Debug($"Próximo ponteiro: {ponteiroInstrumentos.ToString("x4")}");
                }
                //Debug(bytes[1].ToString());
                //Debug(bytes[2].ToString());
                //Debug(bytes[3].ToString());
                //Debug(bytes[0].ToString());
                //Debug(bytes[4].ToString());
                novaPartitura.Debug();
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
    }
}
