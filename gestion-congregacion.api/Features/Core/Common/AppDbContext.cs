using gestion_congregacion.api.Features.Users;
using gestion_congregacion.api.Features.Publishers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using gestion_congregacion.api.Features.Roles;
using gestion_congregacion.api.Features.MeetingEvents;
using gestion_congregacion.api.Features.Meetings;

namespace gestion_congregacion.api.Features.Common
{
    public class AppDbContext : IdentityDbContext<User, Role, long>
    {
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<MeetingEvent> Events { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

    }
}
