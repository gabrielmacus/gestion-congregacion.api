using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Identity;

namespace gestion_congregacion.api.Features.Users
{
    /// <summary>
    /// Represents a repository for managing users.
    /// </summary>
    public interface IUserRepository : IBaseDbRepository<User>
    {
        /// <summary>
        /// Registers a new user with the specified details.
        /// </summary>
        /// <param name="user">The user to register.</param>
        /// <param name="password">The password for the user.</param>
        /// <returns>The registered user.</returns>
        Task<User> Register(User user, string password);
    }
}
