using FunctionMonkey.Model;
using HandlebarsDotNet;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace FunctionMonkey.Compiler.Core.HandlebarsHelpers.AzureFunctions
{
    internal static class HttpVerbsHelper
    {
        public static void Register()
        {
            Handlebars.RegisterHelper("httpVerbs", (writer, context, _) => HelperFunction(writer, context, false));
            Handlebars.RegisterHelper("lowerHttpVerbs", (writer, context, _) => HelperFunction(writer, context, true));
        }

        private static void HelperFunction(EncodedTextWriter writer, Context context, bool toLowerCase)
        {
            if (context.Value is HttpFunctionDefinition httpFunctionDefinition)
            {
                HttpMethod[] verbs = httpFunctionDefinition.Verbs.ToArray();
                if (verbs.Length == 0)
                {
                    verbs = new[] { HttpMethod.Get };
                }

                StringBuilder sb = new StringBuilder($"\"{VerbToString(verbs[0], toLowerCase)}\"");

                foreach (HttpMethod verb in verbs.Skip(1))
                {
                    sb.Append(",");
                    sb.Append($"\"{VerbToString(verb, toLowerCase)}\"");
                }

                writer.Write(sb.ToString());
            }
            else
            {
                writer.Write("\"GET\"");
            }
        }

        private static string VerbToString(HttpMethod verb, bool toLower)
        {
            return toLower ? verb.ToString().ToLower() : verb.ToString();
        }
    }
}
