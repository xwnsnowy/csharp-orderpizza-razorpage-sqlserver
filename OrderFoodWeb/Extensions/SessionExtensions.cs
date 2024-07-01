using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace OrderFoodWeb.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }

        public static void SetDouble(this ISession session, string key, double value)
        {
            session.SetString(key, value.ToString());
        }

        public static double GetDouble(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? 0.0 : double.Parse(value);
        }
    }
}
