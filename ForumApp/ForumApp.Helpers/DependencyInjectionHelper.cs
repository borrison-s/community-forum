using ForumApp.DataAccess;
using ForumApp.DataAccess.Implementations;
using ForumApp.DataAccess.Interfaces;
using ForumApp.Services.Implementations;
using ForumApp.Services.Interfaces;
using ForumApp.Shared.TokenHelpers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ForumApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ForumAppDbContext>(x => x.UseSqlServer(connectionString));
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }

    }
}
