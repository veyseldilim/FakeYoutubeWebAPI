using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Repositories;

namespace YoutubeWeb.Data.Repositories
{
    public class UserRepository : IItemRepository<User>
    {
        private readonly YoutubeContext _context;

        public UserRepository(YoutubeContext context)
        {
            _context = context ??  throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.Posts)
                .Include(x => x.UserComments)
                .FirstOrDefaultAsync();

            return user;
                
        }



        public User Add(User user)
        {
            return _context.Users
           .Add(user).Entity;
        }


        public User Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return user;
        }
    }
}
