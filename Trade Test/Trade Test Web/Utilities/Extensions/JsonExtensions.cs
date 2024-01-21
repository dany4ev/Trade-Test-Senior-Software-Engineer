using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using System.Data;
using System.Reflection;
using System.Text;


namespace Trade_Test.Utilities.Extensions
{
    public static class JsonExtensions
    {

        public static StringContent AsJson(this object o)
            => new(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");


        public static JObject AsObject(this string s)
            => JObject.Parse(s);


        public static T AsObject<T>(this string content) where T : class
            => JsonConvert.DeserializeObject<T>(content);


        public static bool AllStringPropertyValuesAreNonEmpty(this object myObject)
        {
            var allStringPropertyValues =
                from property in myObject.GetType().GetProperties()
                where property.PropertyType == typeof(string) && property.CanRead
                select (string)property.GetValue(myObject);

            return allStringPropertyValues.All(value => !string.IsNullOrEmpty(value));
        }


        public static bool IsAnyNullOrEmpty(this object myObject)
        {
            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(myObject);

                    if (string.IsNullOrEmpty(value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsAnyNullOrEmptyLinq(this object myObject)
        {
            return myObject.GetType().GetProperties()
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => (string)pi.GetValue(myObject))
                .Any(value => string.IsNullOrEmpty(value));
        }
    }
}
