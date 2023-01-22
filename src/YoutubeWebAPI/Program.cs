using Microsoft.EntityFrameworkCore;
using YoutubeWeb.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using YoutubeWebAPI.Extensions;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace YoutubeWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Get ConnectionString from appsettings.json file
           // var connectionString = builder.Configuration.GetConnectionString("DatabaseConnectionContainer");
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};TrustServerCertificate=True;User Id=SA; Password={dbPassword};";
            //Console.WriteLine($"ConnectionString: {connectionString}");

            builder.Services.AddYoutubeWebContext(connectionString);

            builder.Services
                .AddMappers()
                .AddRepositoryServices()
                .AddServices()
                .AddControllers()
                .AddValidation();

           


            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
          //  if (app.Environment.IsDevelopment())
          //  {
                app.UseSwagger();
                app.UseSwaggerUI();
          //  }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            /*

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<YoutubeContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }

            */

            app.Run();
        }
    }
}