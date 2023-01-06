using Microsoft.EntityFrameworkCore;
using YoutubeWeb.Data.SchemaDefinitions;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Repositories;

namespace YoutubeWeb.Data
{
    public class YoutubeContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "YoutubeWeb";

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public YoutubeContext(DbContextOptions<YoutubeContext> options) : base(options)
        {

        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new CommentEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new PostEntitySchemaDefinition());

            base.OnModelCreating(modelBuilder);
        }

        

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken 
            = default(CancellationToken))
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}