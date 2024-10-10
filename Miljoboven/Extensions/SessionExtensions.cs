// using Microsoft.AspNetCore.Http;
// using Newtonsoft.Json;

// namespace Miljoboven.Extensions
// {
//     public static class SessionExtensions
//     {
//         // Stores an object as JSON in the session using the provided key
//         public static void SetJson(this ISession session, string key, object value)
//         {
//             session.SetString(key, JsonConvert.SerializeObject(value));
//         }

//         // Retrieves and deserializes a JSON object from the session
//         public static T GetJson<T>(this ISession session, string key)
//         {
//             var sessionData = session.GetString(key);
//             return value == null ? default : JsonConvert.DeserializeObject<T>(sessionData);
//         }
//     }
// }