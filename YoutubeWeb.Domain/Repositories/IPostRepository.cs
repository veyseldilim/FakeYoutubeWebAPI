using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;

namespace YoutubeWeb.Domain.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPostsByUserId(Guid userId);
    }
}
