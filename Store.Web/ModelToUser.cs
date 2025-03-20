using Store.Web.Identity;
using Store.Web.Models;
namespace Store.Web
{
    public static class UserTransformer
    {
        public static User ToUser(this LoginViewModel model)
        {
            return new User
            {
                UserName = model.Name,
            };
        }

        public static User ToUser(this RegisterViewModel model)
        {
            return new User
            {
                UserName = model.Name,
            };
        }
    }
}
