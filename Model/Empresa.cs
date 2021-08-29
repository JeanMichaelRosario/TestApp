using System.Collections.Generic;

namespace TestApp.Model
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Cliente> Clientes { get; set; }
    }
}
