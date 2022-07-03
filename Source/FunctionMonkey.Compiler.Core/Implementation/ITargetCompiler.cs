using FunctionMonkey.Abstractions;
using System.Collections.Generic;

namespace FunctionMonkey.Compiler.Core.Implementation
{
    internal interface ITargetCompiler
    {
        bool CompileAssets(IFunctionCompilerMetadata functionCompilerMetadata,
            string newAssemblyNamespace,
            IFunctionAppConfiguration configuration, IReadOnlyCollection<string> externalAssemblies,
            string outputBinaryFolder);
    }
}