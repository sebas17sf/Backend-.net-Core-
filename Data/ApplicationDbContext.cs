using Microsoft.EntityFrameworkCore;
using WebApplication2.Model;


namespace WebApplication2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RolesModel> RolesModels { get; set; }
        public DbSet<UsuariosModel> UsuariosModels { get; set; }
        public DbSet<PropiedadesModel> PropiedadesModels { get; set; }
        public DbSet<PagosModel> PagosModels { get; set; }
        public DbSet<MantenimientoModel> MantenimientoModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<RolesModel>().ToTable("roles");
            modelBuilder.Entity<UsuariosModel>().ToTable("usuarios");
            modelBuilder.Entity<PropiedadesModel>().ToTable("propiedades");
            modelBuilder.Entity<PagosModel>().ToTable("pagos");
            modelBuilder.Entity<MantenimientoModel>().ToTable("mantenimiento");
        }
    }
    }