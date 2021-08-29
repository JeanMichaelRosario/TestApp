using TestApp.Db;
using TestApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
    {
        TestDbContext dbContext;
        ILogger<ClientesController> _logger;

        public ClientesController(TestDbContext context, ILogger<ClientesController> logger)
        {
            dbContext = context;
            _logger = logger;
        }
        // GET: ClientesController
        public ActionResult Index()
        {
            return Json(dbContext.Clientes
                .Include(x => x.Empresa)
                .Include(x => x.DireccionClientes)
                .ToList());
        }

        [HttpGet("{id}")]
        public ActionResult Details(int id)
        {
            try
            {
                var cliente = dbContext.Clientes
                .Include(x => x.Empresa)
                .Include(x => x.DireccionClientes)
                .First(x => x.Id == id);

                return Json(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Este cliente no existe");
            }
        }

        // POST: ClientesController/Create
        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                dbContext.Clientes.Add(cliente);
                dbContext.SaveChanges();
                return Ok("Cliente creado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("No se ha podido crear este cliente, revisa tu datos e intentalo nuevamente");
            }
        }

        // POST: ClientesController/Edit/5
        [HttpPut("{id}")]
        public ActionResult Edit(int id, Cliente clienteActualizado)
        {
            try
            {
                var cliente = dbContext.Clientes.Include(x => x.DireccionClientes).First(x => x.Id == id);
                cliente.Nombre = clienteActualizado.Nombre;
                foreach (var item in cliente.DireccionClientes)
                {
                    dbContext.Remove(item);
                }
                cliente.Direcciones = clienteActualizado.Direcciones;
                dbContext.SaveChanges();
                return Json(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("No se ha podido acutalizar este cliente, revisa tu datos e intentalo nuevamente");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var cliente = dbContext.Clientes.Include(x => x.DireccionClientes).First(x => x.Id == id);
                dbContext.RemoveRange(cliente.DireccionClientes);
                dbContext.Remove(cliente);
                dbContext.SaveChanges();
                return Ok("El cliente ha sido eliminado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("El cliente no se ha podido eliminar, favor intentar nuevamente");
            }
        }
    }
}
