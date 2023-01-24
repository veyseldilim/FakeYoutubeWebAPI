using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;

namespace YoutubeWeb.Data.SchemaDefinitions
{
    public class PostEntitySchemaDefinition : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts", YoutubeContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Body).HasMaxLength(100);

            builder.HasOne(e => e.User)
                   .WithMany(c => c.Posts)
                   .HasForeignKey(k => k.UserId)
                   .OnDelete(DeleteBehavior.SetNull);


            /*
            builder.HasData(

                
                 new Post
                 {
                     Body = "PostBody1",
                     Title = "PostTitle1",
                     // First Post 
                     Id = new Guid("5c31703a-7608-4ea7-8176-d89666e2e04a"),
                     // Second User Id
                     UserId = new Guid("ba2687bc-b317-4279-beef-f67b051e7825"),
                     PostComments = null,
                     User = null

                 },

                 new Post
                 {
                     Body = "PostBody2",
                     Title = "PostTitle2",
                     // Second Post
                     Id = new Guid("6fe717f4-bdbf-4e3d-8226-056681bb5799"),
                     // Second User Id
                     UserId = new Guid("ba2687bc-b317-4279-beef-f67b051e7825"),
                     PostComments = null,
                     User = null

                 },

                 new Post
                 {
                     Body = "PostBody3",
                     Title = "PostTitle3",
                     // Third Post
                     Id = new Guid("52570987-e4e4-4fb4-a261-4c67d0b64c6d"),
                     // Second User Id
                     UserId = new Guid("ba2687bc-b317-4279-beef-f67b051e7825"),
                     PostComments = null,
                     User = null


                 },

                 new Post
                 {
                     Body = "PostBody4",
                     Title = "PostTitle4",
                     // Fourth Post
                     Id = new Guid("557b0023-d8f3-499e-b5af-03dce7628de3"),
                     // First User Id
                     UserId = new Guid("ca5e34ff-fc44-4c2f-9522-04ab57f76682"),
                     PostComments = null,
                     User = null

                 },

                 new Post
                 {
                     // Fifth Post
                     Body = "PostBody5",
                     Title = "PostTitle5",
                     Id = Guid.NewGuid(),
                     // Fourth User Id
                     UserId = new Guid("00e72632-00ce-49bc-ad9f-10ba207a41fb"),
                     PostComments = null,
                     User = null


                 },

                 new Post
                 {
                     // Sixth Post
                     Body = "PostBody6",
                     Title = "PostTitle6",
                     Id = Guid.NewGuid(),
                     // Second User Id
                     UserId = new Guid("ba2687bc-b317-4279-beef-f67b051e7825"),
                     PostComments = null,
                     User = null
                 }

              );


            */

        }
    }
}
