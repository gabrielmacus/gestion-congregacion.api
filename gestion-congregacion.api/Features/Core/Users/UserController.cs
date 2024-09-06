using AutoMapper;
using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.Operations;
using gestion_congregacion.api.Features.Permissions;
using gestion_congregacion.api.Features.Users.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace gestion_congregacion.api.Features.Users
{

    public class UserController : ODataController, IUserController
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IReadOperation<User> _readOperation;
        private readonly IDeleteOperation<User> _deleteOperation;

        public UserController(
            UserManager<User> userManager,
            IMapper mapper,
            IReadOperation<User> readOperation,
            IDeleteOperation<User> deleteOperation)
        {
            _userManager = userManager;
            _mapper = mapper;
            _readOperation = readOperation;
            _deleteOperation = deleteOperation;
        }

        [HttpGet]
        [HasPermission(Permission.UsersRead)]
        public IQueryable<User> Get()
        {
            return _readOperation.Get();
        }

        [HttpGet]
        public async Task<IActionResult> Get(long key)
        {
            return await _readOperation.Get(key);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _userManager.CreateAsync(_mapper.Map<User>(data), data.Password);
            result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Unlock(long key)
        {
            var user = await _userManager.FindByIdAsync(key.ToString());
            if (user == null) return NotFound();

            var result = await _userManager.SetLockoutEndDateAsync(user, null);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            return await _deleteOperation.Delete(key);
        }

        private bool _disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                // Dispose managed resources here
                _userManager.Dispose();
                _readOperation.Dispose();
            }
            // Dispose unmanaged resources here
            _disposed = true;
        }
    }
}
