using Newtonsoft.Json;

namespace TestApp.Model
{
    public class DireccionCliente
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int ClienteId { get; set; }
        public string Direccion { get; set; }

        public DireccionCliente(string direccion, int clienteId)
        {
            ClienteId = clienteId;
            Direccion = direccion;
        }
    }
}