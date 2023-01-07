using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Data;
using YoutubeWeb.Data.Tests;
using YoutubeWeb.Domain.Mappers;

namespace YoutubeWeb.Fixtures
{
    public class YoutubeWebContextFactory
    {
        public readonly TestYoutubeContext ContextInstance;
        public readonly ICommentMapper CommentMapper;
        public readonly IUserMapper UserMapper;
        public readonly IPostMapper PostMapper;

        public YoutubeWebContextFactory()
        {
            var contextOptions = new DbContextOptionsBuilder<YoutubeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            EnsureCreation(contextOptions);

            ContextInstance = new TestYoutubeContext(contextOptions);
            
            UserMapper = new UserMapper(PostMapper, CommentMapper);
            PostMapper = new PostMapper(UserMapper, CommentMapper);
            CommentMapper = new CommentMapper(UserMapper, PostMapper);
        }
    }
}
