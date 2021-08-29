using TestApp.Model;
using Microsoft.EntityFrameworkCore;

namespace TestApp.Db
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> contextOptions): base(contextOptions)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<DireccionCliente> Direcciones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cliente>().ToTable("Cliente");
            builder.Entity<Empresa>().ToTable("Empresa");
            builder.Entity<DireccionCliente>().ToTable("DireccionCliente");
        }
    }
}
