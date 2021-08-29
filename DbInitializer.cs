using TestApp.Db;
using TestApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
    internal class DbInitializer
    {
        internal static void Initialize(TestDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            CrearEmpresas(dbContext);

            CrearClientes(dbContext);

            CrearDirecciones(dbContext);
        }
        private static void CrearEmpresas(TestDbContext dbContext)
        {
            var empresas = new Empresa[]
            {
                new Empresa{Nombre="Cancilleria"},
                new Empresa{Nombre="CBORD"},
                new Empresa{Nombre="NZXT"},
            };

            foreach (var empresa in empresas)
            {
                dbContext.Empresas.Add(empresa);
            }

            dbContext.SaveChanges();
        }

        private static void CrearClientes(TestDbContext dbContext)
        {
            var empresas = dbContext.Empresas.ToArray();
            var clientes = new Cliente[]
            {
                new Cliente{Nombre=Faker.Name.FullName(), EmpresaId=empresas[0].Id},
                new Cliente{Nombre=Faker.Name.FullName(), EmpresaId=empresas[1].Id},
                new Cliente{Nombre=Faker.Name.FullName(), EmpresaId=empresas[2].Id},
            };

            foreach (var cliente in clientes)
            {
                dbContext.Clientes.Add(cliente);
            }

            dbContext.SaveChanges();
        }

        private static void CrearDirecciones(TestDbContext dbContext)
        {
            var clientes = dbContext.Clientes.ToArray();
            var direcciones = new DireccionCliente[]
            {
                new DireccionCliente(Faker.Address.StreetAddress(), clientes[0].Id),
                new DireccionCliente(Faker.Address.StreetAddress(), clientes[0].Id),
                new DireccionCliente(Faker.Address.StreetAddress(), clientes[1].Id)
            };

            foreach (var direccion in direcciones)
            {
                dbContext.Direcciones.Add(direccion);
            }

            dbContext.SaveChanges();
        }        
    }
}