using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.EF.Identity
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(int id);
        Task<Comment> GetByIdAsync(int id);
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int id);

        Task<Comment> GetAsync(int id);
    }
}
