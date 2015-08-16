using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReadXMLfromFile
{
    class ObjAutomato
    {
        string caminho;
        string caminhotxt;
        string caminhosaida;
        List<string> saida = new List<string>();
        private List<Estados> estados = new List<Estados>();
        private List<Transicoes> transicoes = new List<Transicoes>();
        Estados inicial = new Estados();
        List<Estados> final = new List<Estados>();
        List<Transicoes> transicaovazia = new List<Transicoes>();
        // ---------- METODOS BASICOS ------------ //
        public XmlDocument abrirConexao()
        {
            XmlDocument XML = new XmlDocument();
            XML.Load(this.caminho);
            return XML;
        }
        public void lerEstados()
        {
            XmlNodeList tudoestados = abrirConexao().GetElementsByTagName("state");
            foreach (XmlNode auxiliar in tudoestados)
            {
                Estados aux = new Estados();
                aux.setID(auxiliar.Attributes.GetNamedItem("id").Value);
                aux.setNome(auxiliar.Attributes.GetNamedItem("name").Value);
                for (int i = 0; i < auxiliar.ChildNodes.Count; i++)
                {
                    if (auxiliar.ChildNodes[i] != null)
                    {
                        if (auxiliar.ChildNodes[i].Name == "initial")
                        {
                            aux.setInicial(true);
                        }
                        else if (auxiliar.ChildNodes[i].Name == "final")
                        {
                            aux.setFinal(true);
                        }
                    }
                }
                this.estados.Add(aux);

            }
        }
        public void lerTransicoes()
        {
            XmlNodeList listfrom = abrirConexao().GetElementsByTagName("transition");
            for (int i = 0; i < listfrom.Count; i++)
            {
                Transicoes auxtransicoes = new Transicoes();
                auxtransicoes.setFrom(listfrom[i]["from"].InnerText);
                auxtransicoes.setTo(listfrom[i]["to"].InnerText);
                auxtransicoes.setSimbolo(listfrom[i]["read"].InnerText);
                this.transicoes.Add(auxtransicoes);
            }

        }
        public void lerArquivoEntrada()
        {
            if (File.Exists(this.caminhotxt))
            {
                var linha = File.ReadAllLines(this.caminhotxt);
                foreach (var line in linha)
                {
                    if (testarAutomato(line) == true)
                    {
                        this.saida.Add(line + " - ACEITO");
                    }
                    else
                    {
                        this.saida.Add(line + " - REJEITADO");
                    }
                }
            }
            else
            {

                Console.WriteLine("Arquivo não encontrado!");
            }
        }
        public void pegarVazio()
        {
            foreach (Transicoes aux in this.Transicoe)
            {
                if (aux.getSimbolo() == "")
                {
                    bool contem = false;
                    foreach (Transicoes auxe in this.TransicaoVazia)
                    {
                        if (aux == auxe)
                        {
                            contem = true;
                        }
                    }

                    if (contem != true)
                    {
                        this.transicaovazia.Add(aux);
                    }
                }
            }
        }
        public void pegarInicialeFinal()
        {
            foreach (Estados aux in estados)
            {
                if (aux.isInicial() == true)
                {

                    inicial = aux;
                }
                else if (aux.isFinal() == true)
                {
                    bool contem = false;
                    for (int h = 0; h < final.Count; h++)
                    {
                        if (final[h] == aux)
                        {
                            contem = true;
                        }

                    }
                    if (contem != true)
                    {
                        final.Add(aux);
                    }

                }
            }
        }

        // ---------- METODOS BASICOS ------------ //
        public bool verificarVazio(string atual)
        {
            foreach (Transicoes aux in transicaovazia)
            {
                if (aux.getFrom() == atual)
                {
                    if (aux.getSimbolo() == "")
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public string pularVazio(string atual, string letra)
        {
            foreach (Transicoes auxV in transicaovazia)
            {

                foreach (Transicoes auxO in transicoes)
                {

                    if (auxV.getFrom() == atual)
                    {
                        if (auxV.getTo() == auxO.getFrom())
                        {
                            if (auxO.getSimbolo() == letra)
                            {
                                return auxO.getFrom();

                            }
                        }
                    }
                    return auxO.getFrom();
                }
            }
            if (TransicaoVazia.Count > 0)
            {
                return atual;
            }
            return null;
        }
        public string funcaoTransicao(string atual, string letra)
        {
            foreach (Transicoes auxiliar in this.transicoes)
            {
                if (verificarVazio(atual) == true)
                {
                    if (auxiliar.getFrom() == atual && auxiliar.getSimbolo() == letra)
                    {
                        return auxiliar.getTo();
                    }
                }
                else
                {
                    return pularVazio(atual, letra);
                }
            }
            if (TransicaoVazia.Count > 0){
                return atual;
            }
            return null;
        }
        public bool testarAutomato(string palavra)
        {
            pegarInicialeFinal();
            pegarVazio();
            char[] word = palavra.ToCharArray();
            string current_state = inicial.getID();
            for (int i = 0; i < word.Length; i++)
            {
                current_state = funcaoTransicao(current_state, word[i].ToString());
            }
            return funcaoFinal(current_state);
        }
        public bool funcaoFinal(string current_state)
        {
            for (int i = 0; i < this.final.Count; i++)
            {
                if (this.final[i].getID() == current_state)
                {
                    return true;
                }
            }
            return false;
        }
        public void gravarTXTFinal() {
            StreamWriter arqSaida = new StreamWriter(caminhosaida);
            arqSaida.WriteLine("------------------------ LISTA DE PALAVRAS ANALISADAS -------------------------");
            foreach (string aux in this.saida) {
                arqSaida.WriteLine(aux);
            }
            arqSaida.WriteLine("--------------------------------------------------------------------------------");
            arqSaida.Close();
            arqSaida.Dispose();
        }
        public ObjAutomato()
        {
            caminho = "";
        }
        public void setCaminho(string caminho)
        {
            this.caminho = caminho;
        }
        public string getCaminho()
        {
            return this.caminho;
        }
        public void setCaminhoSaida(string caminhosaida)
        {
            this.caminhosaida = caminhosaida;
        }
        public string getCaminhoSaida()
        {
            return this.caminhosaida;
        }
        public void setCaminhoTxt(string caminhotxt)
        {
            this.caminhotxt = caminhotxt;
        }
        public string getCaminhoTxt()
        {
            return this.caminhotxt;
        }
        //----------- GET E SETTERS -------------//
        internal List<Transicoes> TransicaoVazia
        {
            get
            {
                return transicaovazia;
            }

            set
            {
                transicaovazia = value;
            }
        }
        internal Estados Inicial
        {
            get
            {
                return inicial;
            }

            set
            {
                inicial = value;
            }
        }
        internal List<string> Saida
        {
            get
            {
                return saida;
            }

            set
            {
                saida = value;
            }
        }
        internal List<Estados> Final
        {
            get
            {
                return final;
            }

            set
            {
                final = value;
            }
        }
        internal List<Estados> Estado
        {
            get
            {
                return estados;
            }

            set
            {
                estados = value;
            }
        }

        internal List<Transicoes> Transicoe
        {
            get
            {
                return transicoes;
            }

            set
            {
                transicoes = value;
            }
        }
    }
}
