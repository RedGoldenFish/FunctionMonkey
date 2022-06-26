using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using FunctionMonkey.Compiler.Core;
using FunctionMonkey.Tests.Integration.Common;

namespace FunctionMonkey.Tests.Integration.AspNetCore
{
    public class FunctionAppHost : IFunctionAppHost
    {
        public void Build(IFunctionAppHostBuilder builder)
        {
            builder
                .CompilerOptions(options => options
                    .HttpTarget(CompileTargetEnum.AspNetCore)
                    .OutputSourceTo(@"C:/Repos/cgoconseils/FunctionMonkey/code/authoredSource")
                )
                .UseFunctionAppConfiguration<HttpFunctionAppConfiguration>();
        }
    }
}