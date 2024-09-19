using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests.Integration
{
    internal static class Extensions
    {
        public static string Ignore(this string jsonPayload, params string[]? ignoredPaths)
        {
            var json = JToken.Parse(jsonPayload);

            if (ignoredPaths != null)
            {
                foreach (var ignoredPath in ignoredPaths)
                {
                    foreach (var token in json.SelectTokens(ignoredPath))
                    {
                        switch (token)
                        {
                            case JValue jValue:
                                jValue.Value = "__IGNORED_VALUE__";
                                break;

                            case JArray jArray:
                                jArray.Clear();
                                jArray.Add("__IGNORED_VALUE__");
                                break;
                        }
                    }
                }
            }

            return json.ToString(Formatting.None);
        }
    }
}
