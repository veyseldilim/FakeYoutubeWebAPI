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
    public class CommentRepository : IItemRepository<Comment> , ICommentRepository
    {
        private readonly YoutubeContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public CommentRepository(YoutubeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<IEnumerable<Comment>> GetAsync()
        {
            return await _context.Comments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Comment> GetById(Guid id)
        {
            var comment = await _context.Comments
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.User)
                .Include(x => x.Post)
                .FirstOrDefaultAsync();

            return comment;
        }

        public Comment Add(Comment comment)
        {
            return _context.Comments.Add(comment).Entity;
        }


        public Comment Update(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostId(Guid postId)
        {
            var comments = await _context.Comments
                .AsNoTracking()
                .Where(x => x.PostId == postId)
                .ToListAsync();

            return comments;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserId(Guid userId)
        {
            var comments = await _context.Comments
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return comments;
        }
    }
}
