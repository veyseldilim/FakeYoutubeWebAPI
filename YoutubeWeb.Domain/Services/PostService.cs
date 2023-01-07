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
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentMapper _commentMapper;


        public PostService(IItemRepository<Post> postRepository, 
            IPostMapper postMapper,
            ICommentRepository commentRepository, 
            ICommentMapper commentMapper)
        {
            _postRepository = postRepository;
            _postMapper = postMapper;
            _commentRepository = commentRepository;
            _commentMapper = commentMapper;
        }


        public async Task<IEnumerable<PostResponse>> GetAllPosts()
        {
            var posts = await _postRepository.GetAsync();

            return posts.Select(x => _postMapper.Map(x));
        }

        public async Task<PostResponse> GetPostById(GetPostRequest postRequest)
        {
            if(postRequest?.Id == null)
            {
                throw new ArgumentNullException();
            }

            var existingPost = await _postRepository.GetById(postRequest.Id);

            return _postMapper.Map(existingPost);

        }




        public async Task<PostResponse> AddPost(AddPostRequest postRequest)
        {
            var post = _postMapper.Map(postRequest);
            var result = _postRepository.Add(post);

            await _postRepository.UnitOfWork.SaveChangesAsync();

            return _postMapper.Map(result);
        }

        public async Task<PostResponse> EditPost(EditPostRequest postRequest)
        {
            if(postRequest?.Id == null)
            {
                throw new ArgumentNullException();
            }
            var existingRecord = await _postRepository.GetById(postRequest.Id);

            if(existingRecord == null)
            {
                throw new ArgumentException($"Entity with {postRequest.Id}  is not present");

            }

            var entity = _postMapper.Map(postRequest);
            var result = _postRepository.Update(entity);

            await _postRepository.UnitOfWork.SaveEntitiesAsync();

            return _postMapper.Map(result);
        }

       

        public async Task<IEnumerable<CommentResponse>> GetPostComments(GetPostRequest postRequest)
        {
           if(postRequest?.Id == null)
           {
                throw new ArgumentNullException();
           }

            var comments = await _commentRepository.GetCommentsByPostId(postRequest.Id);

            return comments.Select(x => _commentMapper.Map(x));

            
        }
    }
}
