using Microsoft.AspNetCore.Identity;
using Store.Data;

namespace Store.Web.Identity
{
    public class User : IdentityUser
    {
        public ICollection<int> OrderIds { get; set; } = new List<int>();
    }
}
