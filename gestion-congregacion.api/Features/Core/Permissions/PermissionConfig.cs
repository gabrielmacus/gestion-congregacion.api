

using gestion_congregacion.api.Features.Permissions;
using Microsoft.AspNetCore.Authorization;

public static partial class ServicesConfiguration
{
    public static void AddPermissionsPolicy(this IServiceCollection services)
    { 

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

    }
}

