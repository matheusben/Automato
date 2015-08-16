using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace ReadXMLfromFile
{

    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    class Class1
    {
        
        static void Main(string[] args)
        {
            ObjAutomato automato = new ObjAutomato();
            automato.setCaminhoTxt("entrada.txt");
            automato.setCaminho("automato.xml");
            automato.setCaminhoSaida("saida.txt");
            automato.lerEstados();
            automato.lerTransicoes();
            //automato.lerTipoAutomato();
            automato.lerArquivoEntrada();
            automato.gravarTXTFinal();
            Console.WriteLine("------------------- INFORMAÇÕES DE ARQUIVOS -------------------" );
            Console.WriteLine("ENTRADA DO AUTOMATO: " + automato.getCaminho());
            Console.WriteLine("ENTRADA DAS PALAVRAS: " + automato.getCaminhoTxt());
            Console.WriteLine("SAIDA DE RELATORIO: " + automato.getCaminhoSaida()) ;
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("-------------- SEGUE ABAIXO RELATORIO DE ANALISES --------------");
            foreach (string aux in automato.Saida) {
                Console.WriteLine(aux);
            }
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("OBS: O Relatorio está salvo com o nome de '" + automato.getCaminhoSaida() + "' na pasta do seu software.");


            Console.ReadLine();
        }
    }
}


