using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Data.Tests.Extensions;
using YoutubeWeb.Domain.Entities;

namespace YoutubeWeb.Data.Tests
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
