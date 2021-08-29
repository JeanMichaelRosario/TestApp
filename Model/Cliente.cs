using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace TestApp.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nombre { get; set; }
        
        [JsonIgnore]
        public ICollection<DireccionCliente> DireccionClientes { get; set; }
        
        [JsonIgnore]
        public Empresa Empresa { get; set; }

        [NotMapped]
        public ICollection<string> Direcciones 
        {
            get 
            {
                return DireccionClientes.Select(x => x.Direccion).ToList();
            }
            set
            {
                if (DireccionClientes == null || (DireccionClientes.Count == 0 && value.Count > 0) )
                {
                    DireccionClientes = new List<DireccionCliente>();
                    foreach (var direccion in value)
                    {
                        var nuevaDireccion = new DireccionCliente(direccion, Id);
                        DireccionClientes.Add(nuevaDireccion);
                    }
                }
                
            }
        }
    }
}