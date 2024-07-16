using gestion_congregacion.api.Features.Common;
using Microsoft.AspNetCore.Mvc;

namespace gestion_congregacion.api.Features.Users
{
    public class UserController : CRUDController<User, IUserRepository>
    {
        public UserController(UserRepository repository) : base(repository)
        {
        }

        public override  async Task<IActionResult> Post([FromBody] User model)
        {
            //_repository.Register(model, model.);
            return await Task.FromResult(NotFound());
        }
    }
}
