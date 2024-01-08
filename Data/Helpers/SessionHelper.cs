using Newtonsoft.Json;

namespace JamesRecipes.Data.Helper;

public static class SessionHelper
{
    public static T GetJson<T>(this ISession session, string key)
    {
        var data = session.GetString(key);
        return data == null ? default(T) : JsonConvert.DeserializeObject<T>(data);
    }

    public static void SetJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }
}