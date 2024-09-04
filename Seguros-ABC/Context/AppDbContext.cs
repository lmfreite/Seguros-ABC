using Microsoft.EntityFrameworkCore;
using Seguros_ABC.Models;

namespace Seguros_ABC.Context
{//inicializa la conexion a la BD
    public class AppDbContext : DbContext //orm
    {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Asegurado> Asegurados { get; set; }

    }
}
