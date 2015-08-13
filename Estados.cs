using System;
using System.Xml;

namespace ReadXMLfromFile
{
    class Estados
    {
        string id;
        string nome;
        bool inicial;
        bool final;

        public void setID(string id) {
            this.id = id;
        }
        public void setNome(string nome) {
            this.nome = nome;
        }
        public string getID() {
            return id;
        }
        public string getNome() {
            return nome;
        }
        public void setInicial(bool inicial)
        {
            this.inicial = inicial;
        }
        public void setFinal(bool final)
        {
            this.final = final;
        }
        public bool isInicial() {
            return inicial;
        }
        public bool isFinal() {
            return final;
        }
    }
}
