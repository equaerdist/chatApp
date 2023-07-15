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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration.Get<AppOptions>(u => u.BindNonPublicProperties = true);
builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddUserRepository<SqlUserRepository>();
builder.Services.AddRegistrationService<RegistrationService>();
builder.Services.AddPasswordValidation<DefaultPasswordValidator>();
builder.Services.AddSingleton(typeof(AppOptions), config);
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(config.ConnectionString));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSignalR();
builder.Services.AddGroupRepository<SqlGroupRepository>();
builder.Services.AddMessageRepository<SqlMessageRepository>();
builder.Services.AddPasswordHandler<BcryptPasswordHandler>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false, 
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = config.Key,
    };
    options.Events = new JwtBearerEvents { OnAuthenticationFailed = (context) => { Console.WriteLine(context.Exception); return Task.CompletedTask; } };
    });
builder.Services.AddAuthorization();
var app = builder.Build();


//app.UseHttpsRedirection();
app.UseCors(opt => { opt.AllowAnyOrigin(); opt.AllowAnyHeader(); });
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();
app.MapHub<ChatHub>("chat");
app.MapControllers();

app.Run();
