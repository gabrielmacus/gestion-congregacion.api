using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace gestion_congregacion.api.Features.Users
{
    public class UserRepository : BaseDbRepository<User, AppDbContext>, IUserRepository
    { 
        private readonly UserManager<User> _userManager;

        public UserRepository(AppDbContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }


        public async Task<User> Register(User entity, string password)
        {

            var result = await _userManager.CreateAsync(entity, password);
            if (result.Succeeded)
            {
                return entity;
            }
            else
            {
                throw new Exception("Failed to register user");
            }
        }
         


        /*
        private bool disposedValue;
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public  User Add(User entity)
        {
        }

        public async Task<bool> Delete(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task<IEnumerable<User>> Find(ODataQueryOptions<User> options, bool includeDeleted = false)
        {
            // Implement the logic to find users based on the provided options
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAll(bool includeDeleted = false)
        {
            // Implement the logic to get all users
            throw new NotImplementedException();
        }

        public async Task<User?> GetById(long id, bool includeDeleted = false)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user;
        }

        public async Task SaveChanges()
        {
            // Implement the logic to save changes
            throw new NotImplementedException();
        }

        public async Task Update(User entity)
        {
            var result = await _userManager.UpdateAsync(entity);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to update user");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar el estado administrado (objetos administrados)
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~UserRepository()
        // {
        //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }*/
    }
}
