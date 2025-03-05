using Microsoft.AspNetCore.Http;
using System.Text.Json;



namespace Store.Web.App
{
    public static class SessionExtensions
    {
        private const string Key = "Cart";

        public static void Set(this ISession session, Cart value)
        {
            if (value == null)
                return;

            // Серіалізуємо об'єкт `Cart` в JSON
            var jsonData = JsonSerializer.Serialize(value);

            // Зберігаємо JSON в сесії
            session.SetString(Key, jsonData);
        }

        public static void RemoveCart(this ISession session)
        {
            session.Remove(Key);
        }
        public static bool TryGetCart(this ISession session, out Cart value)
        {
            // Отримуємо JSON-рядок з сесії
            var jsonData = session.GetString(Key);

            if (!string.IsNullOrEmpty(jsonData))
            {
                // Десеріалізуємо JSON назад в об'єкт `Cart`
                value = JsonSerializer.Deserialize<Cart>(jsonData);
                return true;
            }

            value = null;
            return false;
        }
    }
}
