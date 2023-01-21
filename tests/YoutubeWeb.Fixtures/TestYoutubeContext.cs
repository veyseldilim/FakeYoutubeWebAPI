using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Data;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Fixtures.Extensions;

namespace YoutubeWeb.Fixtures
{
    public class TestYoutubeContext : YoutubeContext
    {
        public TestYoutubeContext(DbContextOptions<YoutubeContext> options)
                : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed<User>("./Data/user.json");
            modelBuilder.Seed<Post>("./Data/post.json");
            modelBuilder.Seed<Comment>("./Data/comment.json");
            
        }




    }
}
