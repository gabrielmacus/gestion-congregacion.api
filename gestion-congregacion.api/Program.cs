using gestion_congregacion.api.Features.Stream;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins") ?? "";
Console.WriteLine($"Allowed origins: {allowedOrigins}");
// Add services to the container.
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();


var multiplexer = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis") ?? "");
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
multiplexer.GetServer(multiplexer.GetEndPoints().First()).FlushDatabase();


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
