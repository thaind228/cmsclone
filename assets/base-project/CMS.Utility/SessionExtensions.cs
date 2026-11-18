using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMS.Utility
{
    public static class SessionExtensions
    {
        // Set string
        public static void SetString(this ISession session, string key, string value)
            => session.Set(key, Encoding.UTF8.GetBytes(value));

        // Get string
        public static string? GetString(this ISession session, string key)
        {
            session.TryGetValue(key, out var data);
            return data == null ? null : Encoding.UTF8.GetString(data);
        }

        // Set object (serialize JSON)
        public static void SetObject<T>(this ISession session, string key, T value)
            => session.SetString(key, JsonSerializer.Serialize(value));

        // Get object (deserialize JSON)
        public static T? GetObject<T>(this ISession session, string key)
        {
            var json = session.GetString(key);
            return string.IsNullOrEmpty(json) ? default : JsonSerializer.Deserialize<T>(json);
        }
    }
}
