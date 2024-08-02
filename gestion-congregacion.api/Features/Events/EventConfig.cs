
using gestion_congregacion.api.Features.Events;
using gestion_congregacion.api.Features.Operations;
using Microsoft.OData.ModelBuilder;

public static partial class ServicesConfiguration
{
    public static void AddEventsFeature(this IServiceCollection services, ODataConventionModelBuilder modelBuilder)
    {
        services.AddScoped<ICreateOperation<Event>, CreateOperation<Event, IEventRepository>>();
        services.AddScoped<IReadOperation<Event>, ReadOperation<Event, Event, IEventRepository>>();
        services.AddScoped<IUpdateOperation<Event>, UpdateOperation<Event, IEventRepository>>();
        services.AddScoped<IDeleteOperation<Event>, DeleteOperation<Event, IEventRepository>>();

        services.AddScoped<IEventRepository, EventRepository>();

        modelBuilder.EntitySet<Event>("Event");
    }

}