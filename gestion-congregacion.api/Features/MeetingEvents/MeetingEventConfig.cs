
using gestion_congregacion.api.Features.MeetingEvents;
using gestion_congregacion.api.Features.Operations;
using Microsoft.OData.ModelBuilder;

public static partial class ServicesConfiguration
{
    public static void AddMeetingEventsFeature(this IServiceCollection services, ODataConventionModelBuilder modelBuilder)
    {
        services.AddScoped<ICreateOperation<MeetingEvent>, CreateOperation<MeetingEvent, IMeetingEventRepository>>();
        services.AddScoped<IReadOperation<MeetingEvent>, ReadOperation<MeetingEvent, IMeetingEventRepository>>();
        services.AddScoped<IUpdateOperation<MeetingEvent>, UpdateOperation<MeetingEvent, IMeetingEventRepository>>();
        services.AddScoped<IDeleteOperation<MeetingEvent>, DeleteOperation<MeetingEvent, IMeetingEventRepository>>();

        services.AddScoped<IMeetingEventRepository, MeetingEventRepository>();

        services.AddScoped<UniqueOrderFilter>();

        modelBuilder.EntitySet<MeetingEvent>("MeetingEvent");
    }

}