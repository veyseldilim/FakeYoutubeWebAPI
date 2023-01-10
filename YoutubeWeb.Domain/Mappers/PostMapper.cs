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
    public class PostMapper : IPostMapper
    {

        private readonly IUserMapper _userMapper;
        private readonly ICommentMapper _commentMapper;

        public PostMapper(IUserMapper userMapper, ICommentMapper commentMapper)
        {
            _userMapper = userMapper;
            _commentMapper = commentMapper;
        }

   


        public Post Map(AddPostRequest postRequest)
        {
            if(postRequest == null)
            {
                return null;
            }

            var post = new Post() 
            {
                Title= postRequest.Title,
                Body= postRequest.Body,
                UserId= postRequest.UserId,
            };

            return post;
        }

        public Post Map(EditPostRequest postRequest)
        {
            if(postRequest == null) 
            {
                return null;
            }

            var post = new Post() 
            { 
                Id = postRequest.Id,
                Title= postRequest.Title,
                Body = postRequest.Body
            };


            return post;

        }

        public PostResponse Map(Post post)
        {
           if(post == null)
           {
                return null;
           }

            var postResponse = new PostResponse()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                UserId = post.UserId,
                User = _userMapper.Map(post.User),
                Comments = _commentMapper.Map(post.PostComments)
                

            };

            return postResponse;

        }

        public IEnumerable<PostResponse> Map(IEnumerable<Post> posts) 
        { 
            if(posts == null)
            {
                return null;
            }


            List<PostResponse> postResponses= new List<PostResponse>();

            foreach (var post in posts)
            {
                var postResponse = Map(post);
                postResponses.Add(postResponse);
            }

            return postResponses;
        }
    }
}
