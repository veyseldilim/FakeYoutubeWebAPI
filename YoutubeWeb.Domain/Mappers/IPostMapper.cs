using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Request.Post;
using YoutubeWeb.Domain.Response;

namespace YoutubeWeb.Domain.Mappers
{
    public interface IPostMapper
    {
        Post Map(AddPostRequest postRequest);

        Post Map(EditPostRequest postRequest);

        PostResponse Map(Post post);


    }
}
