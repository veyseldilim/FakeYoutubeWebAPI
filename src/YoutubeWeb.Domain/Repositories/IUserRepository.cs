using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;

namespace YoutubeWeb.Domain.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<IEnumerable<User>> GetAsync();

        Task<User> GetById(Guid id);

        User Add(User t);

        User Update(User t);
    }
}
