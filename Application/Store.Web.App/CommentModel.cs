using System.ComponentModel.DataAnnotations;
using System;

namespace Store.Web.App
{
    public class CommentModel
    {
        public int Id { get; set; }
        [StringLength(305, ErrorMessage = "Довжина до 305 символів")]
        public string Content { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
