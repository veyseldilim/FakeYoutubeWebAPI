using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Request.Comment;
using YoutubeWeb.Domain.Response;

namespace YoutubeWeb.Domain.Mappers
{
    public class CommentMapper : ICommentMapper
    {

        private readonly IUserMapper _userMapper;
        private readonly IPostMapper _postMapper;



        public CommentMapper(IUserMapper userMapper, IPostMapper postMapper)
        {
            _userMapper = userMapper;
            _postMapper = postMapper;
        }

        public Comment Map(AddCommentRequest commentRequest)
        {
            if (commentRequest == null)
            {
                return null;
            }

            var comment = new Comment()
            {
                Body= commentRequest.Body,
                UserId= commentRequest.UserId,
                PostId= commentRequest.PostId,
                
            };

            return comment;
        }

        public Comment Map(EditCommentRequest commentRequest)
        {
            if(commentRequest == null) 
            {
                return null;
            }

            var comment = new Comment()
            {
                Id= commentRequest.Id,
                Body= commentRequest.Body,
            };

            return comment;
        }

        public CommentResponse Map(Comment comment)
        {
           if(comment == null)
           {
                return null;
           }

            var commentResponse = new CommentResponse()
            {
                Id = comment.Id,
                Body = comment.Body,
                UserId = comment.UserId,
                PostId = comment.PostId,

            };

            return commentResponse;

        }

    public IEnumerable<CommentResponse> Map(IEnumerable<Comment> comments)
        {
            if (comments == null)
            {
                return null;
            }


            List<CommentResponse> commentResponses = new List<CommentResponse>();

            

            foreach (var comment in comments)
            {
                var commentResponse = Map(comment);
                commentResponses.Add(commentResponse);
            }

            return commentResponses;
        }
    }
}
