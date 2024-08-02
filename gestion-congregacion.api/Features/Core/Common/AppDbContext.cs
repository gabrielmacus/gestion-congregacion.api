using gestion_congregacion.api.Features.Users;
using gestion_congregacion.api.Features.Publishers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using gestion_congregacion.api.Features.Roles;
using gestion_congregacion.api.Features.Events;
using gestion_congregacion.api.Features.EventTypes;

namespace gestion_congregacion.api.Features.Common
{
    public class AppDbContext : IdentityDbContext<User, Role, long>
    {
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

    }
}
