using gestion_congregacion.api.Features.Publishers;
using Microsoft.EntityFrameworkCore;

namespace gestion_congregacion.api.Features.Common
{
    public class AppDbContext : DbContext
    {
        public DbSet<Publisher> Publishers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
