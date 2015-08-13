using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadXMLfromFile
{
    class Transicoes
    {
        string from;
        string to;
        string simbolo;

        public void setFrom(string from)
        {
            this.from = from;
        }
        public void setTo(string to)
        {
            this.to = to;
        }
        public void setSimbolo(string simbolo)
        {
            this.simbolo = simbolo;
        }
        public string getFrom()
        {
            return from;
        }
        public string getTo()
        {
            return to;
        }
        public string getSimbolo()
        {
            return simbolo;
        }
    }
}
