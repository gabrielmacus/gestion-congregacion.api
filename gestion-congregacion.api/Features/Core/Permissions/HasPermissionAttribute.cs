using Microsoft.AspNetCore.Authorization;

namespace gestion_congregacion.api.Features.Permissions
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission) : base(policy: permission)
        {
            Permission = permission;
        }

        public string Permission { get; }
    }
}
