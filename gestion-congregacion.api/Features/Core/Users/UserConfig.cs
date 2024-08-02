using gestion_congregacion.api.Features.Operations;
using gestion_congregacion.api.Features.Users;
using gestion_congregacion.api.Features.Users.DTO;

public static partial class ServicesConfiguration
{
    public static void AddUsersFeature(this IServiceCollection services)
    {
        services.AddScoped<ICreateOperation<User>, CreateOperation<User, IUserRepository>>();
        services.AddScoped<IReadOperation<UserDTO>, ReadOperation<UserDTO, User, IUserRepository>>();
        services.AddScoped<IUpdateOperation<User>, UpdateOperation<User, IUserRepository>>();
        services.AddScoped<IDeleteOperation<User>, DeleteOperation<User, IUserRepository>>();

        services.AddScoped<IUserRepository, UserRepository>();

    }
}

