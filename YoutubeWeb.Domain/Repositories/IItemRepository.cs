using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeWeb.Domain.Repositories
{
    public interface IItemRepository<T> : IRepository where T : class
    {
        Task<IEnumerable<T>> GetAsync();

        Task<T> GetById(Guid id);

        T Add(T t);

        T Update(T t);
    }
}
