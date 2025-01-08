using System.Collections.Generic;
namespace Store.Web.Models
{
    public class ConfirmationViewModel
    {
        public int OrderId { get; set; }
        public string CellPhone { get; set; }
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}