using FunctionMonkey.Compiler.Core.Extensions;
using FunctionMonkey.Model;
using HandlebarsDotNet;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace FunctionMonkey.Compiler.Core.HandlebarsHelpers.AspNetCore
{
    internal static class HttpVerbsHelper
    {
        public static void Register()
        {
            Handlebars.RegisterHelper("httpVerbs", HelperFunction);
        }

        private static void HelperFunction(EncodedTextWriter writer, Context context, Arguments parameters)
        {
            if (context.Value is HttpFunctionDefinition httpFunctionDefinition)
            {
                HttpMethod[] verbs = httpFunctionDefinition.Verbs.ToArray();
                if (verbs.Length == 0)
                {
                    verbs = new[] { HttpMethod.Get };
                }

                TextInfo ti = new CultureInfo("en-US", false).TextInfo;
                StringBuilder sb = new StringBuilder();
                foreach (HttpMethod verb in verbs)
                {
                    sb.AppendLine($"[Http{verb.ToString().ToLower().ToTitleCase()}]");
                }
                writer.Write(sb.ToString());
            }
            else
            {
                throw new NotSupportedException("Verbs only apply to HTTP functions");
            }
        }
    }
}
