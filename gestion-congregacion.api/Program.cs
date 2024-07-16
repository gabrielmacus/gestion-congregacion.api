using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.Publishers;
using gestion_congregacion.api.Features.Stream;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using StackExchange.Redis;
using gestion_congregacion.api.Features.Events;
using Microsoft.AspNetCore.Identity;
using gestion_congregacion.api.Features.Users;
using Microsoft.Extensions.DependencyInjection.Extensions;
using gestion_congregacion.api.Features.Roles;
using Microsoft.AspNetCore.Mvc;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Auth
builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme, options => {
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});
builder.Services
    .AddIdentityCore<User>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager<SignInManager<User>>()
    .AddApiEndpoints();
#endregion

#region CORS
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
#endregion

#region ODATA
builder.Services.AddControllers().AddOData(
    options => options
        .EnableQueryFeatures(builder.Configuration.GetValue<int>("MaxTop"))
        .AddRouteComponents("odata", AppModelBuilder.GetEdmModel())
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
#endregion

#region Repositories
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
#endregion

#region Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion


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

app.MapGroup($"{pathPrefix}/User").MapIdentityApi<User>().AddEndpointFilter(async (invocationContext, next) =>
{
    var disallowedEndpoints = new List<string> { "/User/register" };
    var path = invocationContext.HttpContext.Request.Path;
    if(disallowedEndpoints.Any(p => p == path.Value))
    {
        invocationContext.HttpContext.Response.StatusCode = 403;
        return null;
    }
    return await next(invocationContext);
}); ;

app.Run();

