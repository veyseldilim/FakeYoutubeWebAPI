using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Request.Post;
using YoutubeWeb.Domain.Response;

namespace YoutubeWeb.Domain.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostResponse>> GetAllPosts();

        Task<PostResponse> GetPostById(GetPostRequest postRequest);

        Task<PostResponse> AddPost(AddPostRequest postRequest);

        Task<PostResponse> EditPost(EditPostRequest postRequest);

        Task<IEnumerable<CommentResponse>> GetPostComments(GetPostRequest postRequest);



    }
}
