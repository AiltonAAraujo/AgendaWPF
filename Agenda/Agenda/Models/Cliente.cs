using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agenda.Models
{
    public class Cliente
    {
        public int id { get; set; }
        public string nome { get; set; }        
        public string sobrenome { get; set; }
        public int telefone { get; set; }
    }
}