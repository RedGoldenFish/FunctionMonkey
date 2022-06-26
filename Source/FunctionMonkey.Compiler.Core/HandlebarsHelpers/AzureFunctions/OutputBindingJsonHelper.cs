using FunctionMonkey.Abstractions.Builders.Model;
using FunctionMonkey.Compiler.Core.Implementation;
using HandlebarsDotNet;

namespace FunctionMonkey.Compiler.Core.HandlebarsHelpers.AzureFunctions
{
    internal static class OutputBindingJsonHelper
    {
        public static void Register()
        {
            Handlebars.RegisterHelper("outputTriggerJson", (writer, context, _) => HelperFunction(writer, context));
        }

        private static void HelperFunction(EncodedTextWriter writer, Context context)
        {
            if (context.Value is AbstractFunctionDefinition functionDefinition)
            {
                if (functionDefinition.OutputBinding == null)
                {
                    return;
                }

                WriteTemplate(writer, functionDefinition);
            }
        }

        private static void WriteTemplate(EncodedTextWriter writer, AbstractFunctionDefinition functionDefinition)
        {
            TemplateProvider templateProvider = new TemplateProvider(CompileTargetEnum.AzureFunctions);
            string templateSource = templateProvider.GetJsonOutputParameterTemplate(functionDefinition.OutputBinding);
            if (templateSource != null)
            {
                var template = Handlebars.Compile(templateSource);

                string output = template(functionDefinition.OutputBinding);
                writer.Write(",");
                writer.Write(output);
            }
        }
    }
}
