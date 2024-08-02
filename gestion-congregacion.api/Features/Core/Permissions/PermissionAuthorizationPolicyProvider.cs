using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace gestion_congregacion.api.Features.Permissions
{
    public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public PermissionAuthorizationPolicyProvider(
            IOptions<AuthorizationOptions> options) 
            : base(options)
        {
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            //Creates dynamically the policy based on the permission name
            AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);
            if(policy is not null) return policy;
            return new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(policyName))
                .Build();
        }
    }
}
