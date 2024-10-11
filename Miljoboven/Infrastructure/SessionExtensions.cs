using Microsoft.AspNetCore.Http; // For session management
using Newtonsoft.Json; // For JSON serialization/deserialization

namespace Miljoboven.Infrastructure
{
    // Adds extension methods to ISession for handling JSON data
    public static class SessionExtensions
    {
        // Stores an object as JSON in the session using the provided key
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value)); // Convert object to JSON and store it
        }

        // Retrieves and deserializes a JSON object from the session
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key); // Get JSON string by key
            return sessionData == null ? default : JsonConvert.DeserializeObject<T>(sessionData); // Return deserialized object or default
        }
    }
}