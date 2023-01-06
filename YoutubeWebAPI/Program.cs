using Microsoft.EntityFrameworkCore;
using YoutubeWeb.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using YoutubeWebAPI.Extensions;

namespace YoutubeWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Get ConnectionString from appsettings.json file
            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
            //Console.WriteLine($"ConnectionString: {connectionString}");

            builder.Services.AddYoutubeWebContext(connectionString);
           

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.Run();
        }
    }
}