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
    public interface ICommentMapper
    {
        Comment Map(AddCommentRequest commentRequest);

        Comment Map(EditCommentRequest commentRequest);

        CommentResponse Map(Comment comment);




    }
}
