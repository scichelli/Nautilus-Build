﻿using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;
using Nautilus.Framework;

namespace Nautilus
{
    internal interface IScriptCompiler
    {
        CompilerResults CompileBuildScript(string source);
    }

    class ScriptCompiler : IScriptCompiler
    {
        public CompilerResults CompileBuildScript(string source)
        {
            var csharpParameters = new CompilerParameters(new[] {"System.dll", typeof(Shell).Assembly.Location})
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