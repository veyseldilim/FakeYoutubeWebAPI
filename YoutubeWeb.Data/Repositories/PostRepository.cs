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
    public class PostRepository : IItemRepository<Post> , IPostRepository
    {

        private readonly YoutubeContext _context;
        

        public IUnitOfWork UnitOfWork => _context;

        public PostRepository(YoutubeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
           
        }

        public async Task<IEnumerable<Post>> GetAsync()
        {
            return await _context.Posts
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Post> GetById(Guid id)
        {
            var post = await _context.Posts
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.User)
                .Include(x => x.PostComments)
                .FirstOrDefaultAsync();
                
               

            return post;
        }

        public Post Add(Post t)
        {
            return _context.Posts.Add(t).Entity;
        }

        public Post Update(Post t)
        {
            _context.Entry(t).State= EntityState.Modified;
            return t;
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(Guid userId)
        {
            var posts = await _context.Posts
                        .AsNoTracking()
                        .Where(s => s.UserId == userId)
                        .ToListAsync();

            return posts;
        }
    }
}
