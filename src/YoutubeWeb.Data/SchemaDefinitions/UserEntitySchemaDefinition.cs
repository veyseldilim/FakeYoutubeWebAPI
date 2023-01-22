using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;

namespace YoutubeWeb.Data.SchemaDefinitions
{
    public class UserEntitySchemaDefinition : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", YoutubeContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);

            builder.HasData
                (
                     new User
                     {
                         // First User Id
                         Id = new Guid("ca5e34ff-fc44-4c2f-9522-04ab57f76682"),
                         Name = "User1",
                         Posts = null,
                         UserComments = null
                     },

                     new User
                     {
                         Id = new Guid("ba2687bc-b317-4279-beef-f67b051e7825"),
                         Name = "User2",
                         Posts = null,
                         UserComments = null
                     },

                     new User
                     {
                         Id = new Guid("c4ab3660-c47f-41ae-853b-b03adbbe06b9"),
                         Name = "User3",
                         Posts = null,
                         UserComments = null
                     },

                     new User
                     {
                         Id = new Guid("00e72632-00ce-49bc-ad9f-10ba207a41fb"),
                         Name = "User4",
                         Posts = null,
                         UserComments = null
                     },

                     new User
                     {
                         Id = Guid.NewGuid(),
                         Name = "User5",
                         Posts = null,
                         UserComments = null
                     }

                  );

        }
    }
}
