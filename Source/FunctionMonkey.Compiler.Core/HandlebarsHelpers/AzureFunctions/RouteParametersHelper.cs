using FunctionMonkey.Compiler.Core.Extensions;
using FunctionMonkey.Model;
using HandlebarsDotNet;

namespace FunctionMonkey.Compiler.Core.HandlebarsHelpers.AzureFunctions
{
    internal static class RouteParametersHelper
    {
        public static void Register()
        {
            Handlebars.RegisterHelper("routeParameters", (writer, context, _) => HelperFunction(writer, context));
        }

        private static void HelperFunction(EncodedTextWriter writer, Context context)
        {
            if (context.Value is HttpFunctionDefinition httpFunctionDefinition)
            {
                bool first = true;
                foreach (HttpParameter parameter in httpFunctionDefinition.RouteParameters)
                {
                    writer.Write(first ? "?" : "&");
                    writer.Write(parameter.Name.ToCamelCase());
                    writer.Write("={");
                    writer.Write(parameter.Name.ToCamelCase());
                    writer.Write("}");
                    first = false;
                }
            }
            else
            {
                throw new CompilerException(
                    "azureAuthenticationType helper can only be used with a HttpFunctionDefinition");
            }
        }
    }
}
