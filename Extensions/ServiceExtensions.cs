using WebApplication5.Services.Repository;
using WebApplication5.Services.Registration;
using WebApplication5.Services.Passwords;
namespace WebApplication5.Extensions 
{
    public static class ServiceExtensions 
    {
        public static IServiceCollection AddUserRepository<T>(this IServiceCollection services) 
        where T : class, IUserRepository
        {
            services.AddScoped<IUserRepository, T>();
            return services;
        }
        public static IServiceCollection AddRegistrationService<T>(this IServiceCollection services) 
        where T : class, IRegistrationService
        {
            services.AddScoped<IRegistrationService, T>();
            return services;
        }
        public static IServiceCollection AddPasswordValidation<T>(this IServiceCollection services) 
        where T : class, IPasswordValidator
        {
            services.AddSingleton<IPasswordValidator, T>();
            return services;
        }
        public static IServiceCollection AddPasswordHandler<T> (this IServiceCollection services) where T : class, IPasswordHandler
        {
            services.AddSingleton<IPasswordHandler, T>();
            return services;
        }
        public static IServiceCollection AddGroupRepository<T>(this IServiceCollection services) where T: class, IGroupRepository
        {
            services.AddScoped<IGroupRepository, T>();
            return services;
        }
        public static IServiceCollection AddMessageRepository<T>(this IServiceCollection services) where T: class, IMessageRepository
        {
            services.AddScoped<IMessageRepository, T>();
            return services;
        }

    }
}