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
    public class EmpresasController : Controller
    {
        TestDbContext dbContext;
        ILogger<EmpresasController> _logger;

        public EmpresasController(TestDbContext context, ILogger<EmpresasController> logger)
        {
            dbContext = context;
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<Empresa> Index()
        {
            return dbContext.Empresas
                .Include(x => x.Clientes)
                .ThenInclude(x =>x.DireccionClientes)
                .ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult Details(int id)
        {
            try
            {
                var empresa = dbContext.Empresas
                    .Include(x => x.Clientes)
                    .ThenInclude(x => x.DireccionClientes)
                    .First(x => x.Id == id);

                return Json(empresa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Esta empresa no existe");
            }
        }

        // POST: ClientesController/Create
        [HttpPost]
        public ActionResult Create(Empresa empresa)
        {
            try
            {
                dbContext.Empresas.Add(empresa);
                dbContext.SaveChanges();
                return Json(empresa);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("No se ha podido crear esta empresa, revisa tu datos e intentalo nuevamente");
            }
        }

        // POST: ClientesController/Edit/5
        [HttpPut("{id}")]
        public ActionResult Edit(int id, Empresa edirEmpresa)
        {
            try
            {
                var empresa = dbContext.Empresas.Include(x => x.Clientes)
                    .ThenInclude(x => x.DireccionClientes).First(x => x.Id == id);
                empresa.Nombre = edirEmpresa.Nombre;
                dbContext.SaveChanges();
                return Json(empresa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("No se ha podido acutalizar esta empresa, revisa tu datos e intentalo nuevamente");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var empresa = dbContext.Empresas.Include(x => x.Clientes)
                    .ThenInclude(x => x.DireccionClientes).First(x => x.Id == id);
                foreach (var cliente in empresa.Clientes)
                {
                    dbContext.RemoveRange(cliente.DireccionClientes);
                }
                dbContext.RemoveRange(empresa.Clientes);
                dbContext.Remove(empresa);
                dbContext.SaveChanges();
                return Ok("La empresa ha sido eliminada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("La empresa no se ha podido eliminar, favor intentar nuevamente");
            }
        }
    }
}
