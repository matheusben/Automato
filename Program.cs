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
        public static XmlDocument abrirConexao(string caminho) {
            XmlDocument XML = new XmlDocument();
            XML.Load(caminho);
            return XML;
        }
        public static string lerTipoAutomato(string caminho) {
            XmlNodeList tudo = abrirConexao(caminho).GetElementsByTagName("type");
            string tipoautomato = tudo[0].InnerText;
            return tipoautomato;
        }
        public static List<Estados> lerEstados(string caminho) {
            List<Estados> estados = new List<Estados>();
            XmlNodeList tudoestados = abrirConexao(caminho).GetElementsByTagName("state");
            int i = 0;
            foreach (XmlNode teste in tudoestados) {
                Estados aux = new Estados();
                aux.setID(teste.Attributes.GetNamedItem("id").Value);
                aux.setNome(teste.Attributes.GetNamedItem("name").Value);

                if (tudoestados[i].ChildNodes[5] != null)
                {
                    if (tudoestados[i].ChildNodes[5].Name == "initial")
                    {
                        aux.setInicial(true);
                    }
                    else if (tudoestados[i].ChildNodes[5].Name == "final")
                    {
                        aux.setFinal(true);
                    }
                }
                estados.Add(aux);
                i++;
                
            }
            return estados;
        }
        public static List<Transicoes> lerTransicoes(string caminho) {
            List<Transicoes> transicoes = new List<Transicoes>();
            XmlNodeList listfrom = abrirConexao(caminho).GetElementsByTagName("transition");
            for (int i = 0; i < listfrom.Count; i++)
            {
                Transicoes auxtransicoes = new Transicoes();
                auxtransicoes.setFrom(listfrom[i]["from"].InnerText);
                auxtransicoes.setTo(listfrom[i]["to"].InnerText);
                auxtransicoes.setSimbolo(listfrom[i]["read"].InnerText);
                transicoes.Add(auxtransicoes);
            }
            return transicoes;
        }
        public static List<string> lerArquivoEntrada(string caminho) {
            List<string> entrada = new List<string>();
            try
            {
                if (File.Exists(caminho))
                {
                    var linha = File.ReadAllLines(caminho);
                    foreach (var line in linha) {
                        Console.WriteLine(line);
                    }
                }
                else
                {

                    Console.WriteLine("Arquivo não encontrado!");
                }
            }
            catch (Exception e) {
                Console.WriteLine("NADA");
            }
            return entrada;
        }
        static void Main(string[] args){
            string caminho = "Teste.xml";
            //-------------- VARIAVEIS ----------//
            List<Estados> estados = lerEstados(caminho);
            List<Transicoes> transicoes = lerTransicoes(caminho);
            string tipoautomato = lerTipoAutomato(caminho);
            int i = 0;
            //-------------- FIM VARIAVEIS ----------//
            //---------------------- VERIFICACAO ----------------------//
            Console.WriteLine("ARQUIVO COM AS ENTRADA -----------------------\n");
            Console.WriteLine("Digite o Caminho: \n");
            string caminhotxt = "teste.txt";
            lerArquivoEntrada(caminhotxt);
            Console.WriteLine("FIM ARQUIVO COM AS ENTRADA -----------------------\n");

            //---------------------- FIM VERIFICACAO ----------------------//

            Console.WriteLine("TIPO AUTOMATO --> " + tipoautomato);
            for (i = 0; i < estados.Count; i++)
            {
                Console.WriteLine("\n" + "ID: " + estados[i].getID() + " - " + estados[i].getNome() + " FINAL: '" + estados[i].isFinal() + "' INICIAL: '" +estados[i].isInicial() + "'");
            }
            for (i = 0; i < transicoes.Count; i++)
            {
                Console.WriteLine("\n" + "DE: " + transicoes[i].getFrom() + " PARA: " + transicoes[i].getTo() + " SIMBOLO: " + transicoes[i].getSimbolo());
            }
            
            Console.ReadLine();
        }
    }
}

