using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Northwind.Store.UI.Web.Internet.Settings
{
    public static class SessionExtensions
    {
        /// <summary>
        /// Extension methods enable you to "add" methods to existing types without creating a new derived type, recompiling, 
        /// or otherwise modifying the original type.Extension methods are static methods, 
        /// but they're called as if they were instance methods on the extended type.
        /// Extension methods are defined as static methods but are called by using instance method syntax. 
        /// Their first parameter specifies which type the method operates on.
        /// see https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
        /// </summary>
        public static void SetObject<T>(this ISession session, string key, T value) where T : class
        {

            string json = JsonSerializer.Serialize(value);
            byte[] serializedResult = System.Text.Encoding.UTF8.GetBytes(json);

            session?.Set(key, serializedResult);
        }

        public static T GetObject<T>(this ISession session, string key) where T : class
        {
            T result = default(T);

            byte[] requestEntriesBytes = session.Get(key);

            if (requestEntriesBytes != null && requestEntriesBytes.Length > 0)
            {
                string json = System.Text.Encoding.UTF8.GetString(requestEntriesBytes);
                result = JsonSerializer.Deserialize<T>(json);
            }

            return result;
        }
    }
}
