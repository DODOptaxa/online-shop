using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Store.Data;

namespace Store.Data.EF.Identity
{
    public class User : IdentityUser
    {
        public ICollection<int> OrderIds { get; set; } = new List<int>();

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
