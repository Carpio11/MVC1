namespace MVC1.Models
{
    public class FunçãoBase
    {
        public string Cargo { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}