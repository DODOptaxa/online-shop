using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Store.Data.EF.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.EF.Identity
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationContextFactory _contextFactory;

        public CommentRepository(ApplicationContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task AddAsync(Comment comment)
        {
            ApplicationDbContext _context = _contextFactory.Create();
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            ApplicationDbContext _context = _contextFactory.Create();
            Comment comment = await _context.Comments.FirstOrDefaultAsync(ComId => ComId.Id == id);

           if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<Comment> GetAsync(int id) 
        {
            ApplicationDbContext _context = _contextFactory.Create();
            Comment comment = await _context.Comments.FirstOrDefaultAsync(ComId => ComId.Id == id);
            if (comment == null)
                throw new Exception("Коментар не знайдений");
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync(int bookId)
        {
            ApplicationDbContext _context = _contextFactory.Create();
            List<Comment> comments = await _context.Comments.Where(ComId => ComId.BookId == bookId).Include(c => c.User).ToListAsync();
            if (comments == null)
                throw new Exception("Коментарі не знайдені");
            return comments;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            ApplicationDbContext _context = _contextFactory.Create();
            Comment comment = await _context.Comments.FirstOrDefaultAsync(ComId => ComId.Id == id);
            if (comment == null)
                throw new Exception("Коментар не знайдений");
            return comment;
        }

        public async Task UpdateAsync(Comment comment)
        {
            ApplicationDbContext _context = _contextFactory.Create();
            await _context.SaveChangesAsync();
        }
    }
}
