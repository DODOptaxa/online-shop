using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.EF.Identity
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? LastModifiedAt { get; set; }
        public int BookId { get; set; } 

        public string UserId { get; set; }

        public User User { get; set; }

    }
}
