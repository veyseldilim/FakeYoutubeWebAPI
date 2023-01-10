using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;

namespace YoutubeWeb.Domain.Repositories
{
    public interface IPostRepository : IRepository
    {
        Task<IEnumerable<Post>> GetAsync();

        Task<Post> GetById(Guid id);

        Post Add(Post t);

        Post Update(Post t);

        Task<IEnumerable<Post>> GetPostsByUserId(Guid userId);
    }
}
