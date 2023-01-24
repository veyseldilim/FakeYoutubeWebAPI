using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;

namespace YoutubeWeb.Data.InitializeDb
{
    public class DbInitializer
    {
        public static void Initialize(YoutubeContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            dbContext.Database.EnsureCreated();   

            if (dbContext.Users.Any() || dbContext.Comments.Any() || dbContext.Posts.Any()) return;

            var users = SeedHelper.SeedData<User>("Users.json");
            var posts = SeedHelper.SeedData<Post>("Posts.json");
            var comments = SeedHelper.SeedData<Comment>("Comments.json");

            dbContext.Users.AddRange(users);
            dbContext.Posts.AddRange(posts);
            dbContext.Comments.AddRange(comments);

            dbContext.SaveChanges();
        }
    }
}
