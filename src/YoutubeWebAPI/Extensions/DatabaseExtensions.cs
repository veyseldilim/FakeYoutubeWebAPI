using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YoutubeWeb.Data;
using YoutubeWeb.Data.InitializeDb;

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

        public static IApplicationBuilder UseItToSeedSqlServer(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                
                var context = services.GetRequiredService<YoutubeContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }

            return app;
        }


    }
}
