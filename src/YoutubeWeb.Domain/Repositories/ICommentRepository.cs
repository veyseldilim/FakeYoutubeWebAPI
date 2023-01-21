using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;

namespace YoutubeWeb.Domain.Repositories
{
    public interface ICommentRepository : IRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByPostId(Guid postId);

        Task<IEnumerable<Comment>> GetCommentsByUserId(Guid userId);

        Task<IEnumerable<Comment>> GetAsync();

        Task<Comment> GetById(Guid id);

        Comment Add(Comment t);

        Comment Update(Comment t);
    }
}
