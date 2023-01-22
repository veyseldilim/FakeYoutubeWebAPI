using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using YoutubeWeb.Data.SchemaDefinitions;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Repositories;

namespace YoutubeWeb.Data
{
    public class YoutubeContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "YoutubeWebDB";

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public YoutubeContext(DbContextOptions<YoutubeContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

                if(databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) 
                    {
                        databaseCreator.Create();
                    }
                    

                    if (!databaseCreator.HasTables()) 
                    {
                        databaseCreator.CreateTables();
                        
                    }
                    
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntitySchemaDefinition());       
            modelBuilder.ApplyConfiguration(new PostEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new CommentEntitySchemaDefinition());

           

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