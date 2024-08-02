using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.Operations;
using gestion_congregacion.api.Features.Users.DTO;
using Microsoft.AspNetCore.Mvc;

namespace gestion_congregacion.api.Features.Users
{
    public interface IUserController : IReadOperation<UserDTO>
    {
        Task<IActionResult> Register([FromBody] RegisterRequestDTO data);
        Task<IActionResult> Unlock(long key);
    }
}
