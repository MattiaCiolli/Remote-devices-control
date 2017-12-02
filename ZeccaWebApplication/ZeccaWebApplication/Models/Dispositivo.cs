using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeccaWebApplication.Models
{
    public class Dispositivo
    {
        public string ID { get; set; }
        public string Descrizione { get; set; }
        public int Tipo { get; set; }
        public string Indirizzo { get; set; }
    }
}