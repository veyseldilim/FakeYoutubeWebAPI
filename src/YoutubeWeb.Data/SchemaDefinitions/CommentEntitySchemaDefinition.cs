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
    public class CommentEntitySchemaDefinition : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments", YoutubeContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Body).IsRequired().HasMaxLength(140);

            builder.HasOne(u => u.User)
                .WithMany(u => u.UserComments)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.HasOne(u => u.Post)
                .WithMany(u => u.PostComments)
                .HasForeignKey(u => u.PostId)
                .OnDelete(DeleteBehavior.Cascade);


            

            builder.HasData( 

                new Comment { 
                    Id = Guid.NewGuid(),
                    Body = "FirstCommentBody",
                    // First Post Id
                    PostId = new Guid("5c31703a-7608-4ea7-8176-d89666e2e04a"),
                    // First User Id
                    UserId = new Guid("ca5e34ff-fc44-4c2f-9522-04ab57f76682"),
                    Post = null,
                    User = null
                    
                    

                }, 
                new Comment {
                    Id = Guid.NewGuid(),
                    Body = "SecondCommentBody",
                    // Second Post Id
                    PostId = new Guid("6fe717f4-bdbf-4e3d-8226-056681bb5799"),
                    // Second User Id
                    UserId = new Guid("ba2687bc-b317-4279-beef-f67b051e7825"),
                    Post = null,
                    User = null

                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Body = "ThirdCommentBody",
                    // Third Post Id
                    PostId = new Guid("52570987-e4e4-4fb4-a261-4c67d0b64c6d"),
                    // Third User Id
                    UserId = new Guid("c4ab3660-c47f-41ae-853b-b03adbbe06b9"),
                    Post = null,
                    User = null

                },
                new Comment
                {

                    Id = Guid.NewGuid(),
                    Body = "FourthCommentBody",
                    // First Post Id
                    PostId = new Guid("5c31703a-7608-4ea7-8176-d89666e2e04a"),
                    // Third User Id
                    UserId = new Guid("c4ab3660-c47f-41ae-853b-b03adbbe06b9"),
                    Post = null,
                    User = null
                },

                new Comment
                {
                    Id = Guid.NewGuid(),
                    Body = "FifthCommentBody",
                    // First Post Id
                    PostId = new Guid("5c31703a-7608-4ea7-8176-d89666e2e04a"),
                    // First User Id
                    UserId = new Guid("ca5e34ff-fc44-4c2f-9522-04ab57f76682"),
                    Post = null,
                    User = null
                },

                new Comment
                {
                    Id = Guid.NewGuid(),
                    Body = "SixthCommentBody",
                    // Second Post Id
                    PostId = new Guid("6fe717f4-bdbf-4e3d-8226-056681bb5799"),
                    // Third User Id
                    UserId = new Guid("c4ab3660-c47f-41ae-853b-b03adbbe06b9"),
                    Post = null,
                    User = null
                },

                new Comment
                {
                    Id = Guid.NewGuid(),
                    Body = "SeventhCommentBody",
                    // Fourth Post Id
                    PostId = new Guid("557b0023-d8f3-499e-b5af-03dce7628de3"),
                    // Fourth User Id
                    UserId = new Guid("00e72632-00ce-49bc-ad9f-10ba207a41fb"),
                    Post = null,
                    User = null
                },

                new Comment
                {
                    Id = Guid.NewGuid(),
                    Body = "EigthCommentBody",
                    // First Post Id
                    PostId = new Guid("5c31703a-7608-4ea7-8176-d89666e2e04a"),
                    // Third User Id
                    UserId = new Guid("c4ab3660-c47f-41ae-853b-b03adbbe06b9"),
                    Post = null,
                    User = null
                },

                new Comment
                {
                    Id = Guid.NewGuid(),
                    Body = "CommentBody9",
                    // Second Post Id
                    PostId = new Guid("6fe717f4-bdbf-4e3d-8226-056681bb5799"),
                    // Second User Id
                    UserId = new Guid("ba2687bc-b317-4279-beef-f67b051e7825"),
                    Post = null,
                    User = null
                },

                new Comment
                {
                    Id = Guid.NewGuid(),
                    Body = "CommentBody10",
                    // Second Post Id
                    PostId = new Guid("6fe717f4-bdbf-4e3d-8226-056681bb5799"),
                    // Second User Id
                    UserId = new Guid("ba2687bc-b317-4279-beef-f67b051e7825"),
                    Post = null,
                    User = null
                }

                    );
          
               
            
        }
    }
}
