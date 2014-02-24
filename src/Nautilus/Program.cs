using System;
using System.CodeDom.Compiler;
using System.IO;

namespace Nautilus
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteBuildScript(new ScriptCompiler(), new BuildExecutor());
            Console.ReadLine();
        }

        private static void ExecuteBuildScript(IScriptCompiler compiler, IBuildExecutor executor)
        {
            const string buildScriptFilePath = @"C:\play\nautilus\Nautilus-Build\Hello-Nautilus\BuildScript.cs";
            var source = File.ReadAllText(buildScriptFilePath);
            var results = compiler.CompileBuildScript(source);

            if (results.Errors.HasErrors)
            {
                Console.WriteLine("Building the Build Script failed.");
                foreach (CompilerError error in results.Errors)
                {
                    Console.WriteLine(error.ErrorText);
                }
            }
            else
            {
                var output = executor.ExecuteBuildScript(results);
                Console.WriteLine(output);
            }
            Console.ReadLine();
        }
    }
}