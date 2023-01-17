using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Data;
using YoutubeWebAPI;

namespace YoutubeWeb.Fixtures
{
    public class InMemoryApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
            .UseEnvironment("Testing")
            .ConfigureTestServices(services =>
            {
                var options = new
                             DbContextOptionsBuilder<YoutubeContext>()
                             .UseInMemoryDatabase(Guid.NewGuid().ToString())
                             .Options;


                services.AddScoped<YoutubeContext>(serviceProvider =>
                                                  new TestYoutubeContext(options));

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<YoutubeContext>();
                db.Database.EnsureCreated();
            });
        }


    }
}
