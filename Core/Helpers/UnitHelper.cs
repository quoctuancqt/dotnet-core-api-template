using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class UnitHelper
    {
        public static int GetTotalPage(int totalRecord, int take)
        {
            if (take > 0)
            {
                return (int)Math.Ceiling((double)((double)totalRecord / (double)take));
            }
            throw new Exception("The take parameter require greater than zero.");
        }

        public static int GetTotalPage(this long totalRecord, int take)
        {
            if (take > 0)
            {
                return (int)Math.Ceiling((double)((double)totalRecord / (double)take));
            }
            throw new Exception("The take parameter require greater than zero.");
        }

        public static TModel JsonToObj<TModel>(this string json)
        {
            return JsonConvert.DeserializeObject<TModel>(json);
        }

        public static object JsonToObj(this string json)
        {
            return JsonConvert.DeserializeObject(json);
        }

        public static string ObjToJson<TModel>(this TModel model)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(model, jsonSettings);
        }

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public static string FirstCharToLower(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToLower() + input.Substring(1);
            }
        }

        public static async Task<JObject> ResponseToJObjectAsync(this HttpResponseMessage response)
        {
            string json = await response.Content.ReadAsStringAsync();

            return JObject.Parse(json);
        }

        public static async Task<T> ResponseToObjAsync<T>(this HttpResponseMessage response)
        {
            string json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(json)) return default(T);

            return json.JsonToObj<T>();
        }

        public static async Task<string> ResponseToStrAsync(this HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        public static T ParseEnum<T>(this string value)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                return default(T);
            }

        }

        public static bool IsValidEmailAddress(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static string ReplaceCharacters(this string tag)
        {
            tag = tag.ToUpper()
            .Replace("'", "`")
            .Replace('"', '`')
            .Replace("&", "and")
            .Replace(",", ":")
            .Replace(@"\", "/"); //Do not allow escaped characters from user
            tag = Regex.Replace(tag, @"\s+", " "); //multiple spaces with single spaces
            return tag;
        }

        public static string GetTokenFromHeader(HttpContext context)
        {
            try
            {
                var headers = context.Request.Headers;
                if (!string.IsNullOrEmpty(headers["Authorization"]))
                {
                    var array = headers["Authorization"].ToString().Split(' ');
                    return array[1];
                }
            }
            catch { }

            return string.Empty;
        }
    }
}
