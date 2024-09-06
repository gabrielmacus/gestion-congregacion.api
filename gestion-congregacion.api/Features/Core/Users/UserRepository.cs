using AutoMapper;
using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Identity;

namespace gestion_congregacion.api.Features.Users
{
    public class UserRepository : BaseRepository<User, AppDbContext>, IUserRepository, IDisposable
    {
        private readonly UserManager<User> _userManager;
        private bool _disposed = false;

        public UserRepository(
            AppDbContext context,
            UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public async Task<User> Register(User entity, string password)
        {
            var result = await _userManager.CreateAsync(entity, password);
            if (result.Succeeded) return entity;
            throw new Exception("Failed to register user");
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
