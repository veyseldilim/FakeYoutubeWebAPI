using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Request.Comment;
using YoutubeWeb.Domain.Request.Post;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Domain.Response;

namespace YoutubeWeb.Domain.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponse>> GetAllComments();

        Task<CommentResponse> GetCommentById(GetCommentRequest commentRequest);

        Task<CommentResponse> AddComment(AddCommentRequest commentRequest);

        Task<CommentResponse> EditComment(EditCommentRequest commentRequest);

        




    }
}
