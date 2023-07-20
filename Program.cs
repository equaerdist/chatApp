using WebApplication5.Services;
using Npgsql.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Extensions;
using WebApplication5;
using WebApplication5.Services.Repository;
using WebApplication5.Services.Registration;
using WebApplication5.Services.Passwords;
using WebApplication5.AutoMapperProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApplication5.Services.Hubs;
using WebApplication5.Services.Middlewares;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Serialization;
using WebApplication5.Services.Repository.UserGroupsRepository;
using WebApplication5.Services.GroupManager;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration.Get<AppOptions>(u => u.BindNonPublicProperties = true);
builder.Services.AddLogging();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddUserRepository<SqlUserRepository>();
builder.Services.AddRegistrationService<RegistrationService>();
builder.Services.AddPasswordValidation<DefaultPasswordValidator>();
builder.Services.AddSingleton(typeof(AppOptions), config);
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(config.ConnectionString));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSignalR();
builder.Services.AddUserGroupsRepository<SqlUserGroupsRepository>();
builder.Services.AddGroupRepository<SqlGroupRepository>();
builder.Services.AddGroupManager<GroupManager>();
builder.Services.AddMessageRepository<SqlMessageRepository>();
builder.Services.AddPasswordHandler<BcryptPasswordHandler>();
builder.Services.AddSingleton<IUserIdProvider, CustomIdProvider>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false, 
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = config.Key,
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
            {
                var path = context.HttpContext.Request.Path;
                var accessToken = context.HttpContext.Request.Query["access_token"];
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            },
        OnAuthenticationFailed = context =>
        {
            throw new Exception(context.Exception.Message);
        }
    };
    });
builder.Services.AddAuthorization();
var app = builder.Build();


//app.UseHttpsRedirection();
app.UseCors(opt => { opt.AllowAnyOrigin(); opt.AllowAnyHeader(); });
app.UseMiddleware<ClientErrorHandler>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();
app.MapHub<UserHub>("/chat");
app.MapControllers();

app.Run();
