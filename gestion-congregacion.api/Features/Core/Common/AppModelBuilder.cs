using gestion_congregacion.api.Features.MeetingEvents;
using gestion_congregacion.api.Features.Publishers;
using gestion_congregacion.api.Features.Users;
using gestion_congregacion.api.Features.Users.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace gestion_congregacion.api.Features.Common
{
    public class AppModelBuilder
    {
        public static IEdmModel GetEdmModel()
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Publisher>("Publisher");
            modelBuilder.EntitySet<MeetingEvent>("Event");

            modelBuilder.EntitySet<User>("User");
            modelBuilder.EntitySet<UserDTO>("UserDTO");
            modelBuilder.EntityType<User>()
                .Collection
                .Action("Register");
            modelBuilder.EntityType<User>()
                .Action("Unlock");

            var model = modelBuilder.GetEdmModel();
            return model;

        }
    }
}
