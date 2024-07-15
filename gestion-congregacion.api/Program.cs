using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.Publishers;
using gestion_congregacion.api.Features.Stream;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using StackExchange.Redis;
using gestion_congregacion.api.Features.Events;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins") ?? "";
builder.Services.AddCors(options =>
{
    
    options.AddPolicy("CorsPolicy", policy => {
        policy
            .WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("Content-Disposition");
        ;
    });
    
});

builder.Services.AddControllers().AddOData(
    options => options
        .EnableQueryFeatures(builder.Configuration.GetValue<int>("MaxTop"))
        .AddRouteComponents("odata", AppModelBuilder.GetEdmModel())
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

#region Repositories
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
#endregion

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var dbConnString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySql(dbConnString, ServerVersion.AutoDetect(dbConnString))
);

if (!EF.IsDesignTime)
{
    var multiplexer = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis") ?? "");
    builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
    multiplexer.GetServer(multiplexer.GetEndPoints().First()).FlushDatabase();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy");

var pathPrefix = builder.Configuration.GetValue<string>("PathPrefix") ?? "";
app.MapHub<StreamHub>("{pathPrefix}/hubs/stream/{Name}/{Participants:int}");

app.Run();
