using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;
using Nautilus.Framework;

namespace Nautilus
{
    public interface IBuildInstructionsCompiler
    {
        CompilerResults Compile(string source);
    }

    public class BuildInstructionsCompiler : IBuildInstructionsCompiler
    {
        public CompilerResults Compile(string source)
        {
            var csharpParameters = new CompilerParameters(new[] {"System.dll", typeof(BuildInstructions).Assembly.Location})
                {
                    GenerateExecutable = false,
                    GenerateInMemory = true,
                    IncludeDebugInformation = false
                };
            var options = new Dictionary<string, string> {{"CompilerVersion", "v4.0"}};
            var provider = new CSharpCodeProvider(options);
            var results = provider.CompileAssemblyFromSource(csharpParameters, source);
            return results;
        }
    }
}