using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Mappers;
using YoutubeWeb.Domain.Repositories;
using YoutubeWeb.Domain.Request.Post;
using YoutubeWeb.Domain.Response;

namespace YoutubeWeb.Domain.Services
{
    public class PostService : IPostService
    {

        private readonly IItemRepository<Post> _postRepository;
        private readonly IPostMapper _postMapper;


        public PostService(IItemRepository<Post> postRepository, IPostMapper postMapper)
        {
            _postRepository = postRepository;
            _postMapper = postMapper;
        }




        public Task<PostResponse> AddPost(AddPostRequest postRequest)
        {
            throw new NotImplementedException();
        }

        public Task<PostResponse> EditPost(EditPostRequest postRequest)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostResponse>> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public Task<PostResponse> GetPostById(GetPostRequest postRequest)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CommentResponse>> GetPostComments(GetPostRequest postRequest)
        {
            throw new NotImplementedException();
        }
    }
}
