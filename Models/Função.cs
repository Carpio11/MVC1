using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC1.Models
{
    public class Função
    {
        public int FunçãoID {get; set;}
        public string Cargo {get; set;}
        public int EmpresaID {get; set;}
        public virtual Empresa Empresa { get; set; }
    }
}
