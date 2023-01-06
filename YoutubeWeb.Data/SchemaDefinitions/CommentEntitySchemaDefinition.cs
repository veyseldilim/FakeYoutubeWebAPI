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
               
            
        }
    }
}
