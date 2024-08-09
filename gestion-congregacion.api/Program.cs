using gestion_congregacion.api.Features.Common;
using gestion_congregacion.api.Features.Publishers;
using gestion_congregacion.api.Features.Stream;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using StackExchange.Redis;
using Microsoft.AspNetCore.Identity;
using gestion_congregacion.api.Features.Users;
using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddAutoMapper(typeof(MappingProfile));

var modelBuilder = new ODataConventionModelBuilder();

builder.Services.AddHangfire(builder.Configuration);
builder.Services.AddPuppeteer();

builder.Services.AddUsersFeature();
builder.Services.AddPermissionsPolicy();

builder.Services.AddMeetingsFeature(modelBuilder);
builder.Services.AddPublishersFeature(modelBuilder);
builder.Services.AddMeetingEventsFeature(modelBuilder);


builder.Services.AddFetchMeetingEventsFeature();
    
#region Auth
builder.Services.AddAuthentication(IdentityConstants.BearerScheme)
    
    .AddBearerToken(IdentityConstants.BearerScheme, options =>
    {
    })
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        // Cookie settings
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        // https://stackoverflow.com/questions/38800919/how-to-return-401-instead-of-302-in-asp-net-core
        // https://www.permit.io/blog/401-vs-403-error-whats-the-difference TODO: Read error encapsulation

        //options.Events.OnRedirectToLogin = context =>
        //{
        //    context.Response.StatusCode = context.HttpContext.User.Identity!.IsAuthenticated ? 403 : 401;
        //    return Task.CompletedTask;
        //};
        options.SlidingExpiration = true;
    })
    /*
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    })*/;

builder.Services.AddAuthorization();

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

#region Filters
builder.Services.AddScoped<ValidateModelFilter>();
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
        .AddRouteComponents("odata", modelBuilder.GetEdmModel() /*AppModelBuilder.GetEdmModel()*/)
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
#endregion


#region Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion


var dbConnString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => { 
        options.UseMySql(dbConnString, ServerVersion.AutoDetect(dbConnString));
        #if DEBUG
        //options.EnableSensitiveDataLogging(true);
        //options.LogTo(Console.WriteLine);
        #endif
    }
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

app.UseHangfire();

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
});

app.UseFetchMeetingEventsFeature();

app.Run();

