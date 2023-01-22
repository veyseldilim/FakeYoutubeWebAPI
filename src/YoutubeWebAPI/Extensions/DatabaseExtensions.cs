using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YoutubeWeb.Data;

namespace YoutubeWebAPI.Extensions
{
    public static class DatabaseExtensions
    {

        public static IServiceCollection AddYoutubeWebContext
            (this IServiceCollection services, string connectionString)
        {


            return services
                 .AddEntityFrameworkSqlServer()
                 .AddDbContext<YoutubeContext>(contextOptions =>
                 {
                     contextOptions.UseSqlServer(connectionString,

                     serverOptions => {
                         serverOptions.MigrationsAssembly
                         (typeof(Program).Assembly.FullName);
                     });

                     
                });

        }


    }
}
