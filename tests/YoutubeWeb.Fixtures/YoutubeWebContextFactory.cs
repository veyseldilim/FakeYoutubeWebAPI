using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Data;
using YoutubeWeb.Domain.Mappers;

namespace YoutubeWeb.Fixtures
{
    public class YoutubeWebContextFactory
    {
        public readonly TestYoutubeContext ContextInstance;

        public readonly IMapper Mapper;
        

        public YoutubeWebContextFactory()
        {
            var contextOptions = new DbContextOptionsBuilder<YoutubeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            EnsureCreation(contextOptions);

            ContextInstance = new TestYoutubeContext(contextOptions);

            Mapper = new Mapper();
            
            
        }

        private void EnsureCreation(DbContextOptions<YoutubeContext> options)
        {
            using var context = new TestYoutubeContext(options);
            context.Database.EnsureCreated();   
        }
    }
}
