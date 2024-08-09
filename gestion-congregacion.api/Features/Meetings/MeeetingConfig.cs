
using gestion_congregacion.api.Features.Meetings;
using gestion_congregacion.api.Features.Operations;
using Microsoft.OData.ModelBuilder;

public static partial class ServicesConfiguration
{
    public static void AddMeetingsFeature(this IServiceCollection services, ODataConventionModelBuilder modelBuilder)
    {
        services.AddScoped<ICreateOperation<Meeting>, CreateOperation<Meeting, IMeetingRepository>>();
        services.AddScoped<IReadOperation<Meeting>, ReadOperation<Meeting, Meeting, IMeetingRepository>>();
        services.AddScoped<IUpdateOperation<Meeting>, UpdateOperation<Meeting, IMeetingRepository>>();
        services.AddScoped<IDeleteOperation<Meeting>, DeleteOperation<Meeting, IMeetingRepository>>();

        services.AddScoped<IMeetingRepository, MeetingRepository>();

        modelBuilder.EntitySet<Meeting>("Meeting");
    }

}