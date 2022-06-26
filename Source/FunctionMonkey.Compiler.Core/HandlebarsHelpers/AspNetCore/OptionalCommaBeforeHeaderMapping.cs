using FunctionMonkey.Model;
using HandlebarsDotNet;
using System;
using System.Linq;

namespace FunctionMonkey.Compiler.Core.HandlebarsHelpers.AspNetCore
{
    internal static class OptionalCommaBeforeHeaderMapping
    {
        public static void Register()
        {
            Handlebars.RegisterHelper("optionalCommaBeforeHeaderMapping", (writer, context, _) => HelperFunction(writer, context));
        }

        private static void HelperFunction(EncodedTextWriter writer, Context context)
        {
            if (context.Value is HttpFunctionDefinition httpFunctionDefinition)
            {
                if ((httpFunctionDefinition.QueryParametersWithoutHeaderMapping.Any() ||
                    httpFunctionDefinition.RouteParameters.Any() ||
                    (httpFunctionDefinition.CommandRequiresBody && httpFunctionDefinition.IsBodyBased)) && httpFunctionDefinition.QueryParametersWithHeaderMapping.Any())
                {
                    writer.Write(", ");
                }
            }
            else
            {
                throw new NotSupportedException("Verbs only apply to HTTP functions");
            }
        }
    }
}
