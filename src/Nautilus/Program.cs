using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;

namespace Nautilus
{
    class Program
    {
        const int FatalError = -1;
        const int Success = 0;

        [STAThread]
        static int Main(string[] args)
        {
            try
            {
                var commandLineParser = new CommandLineParser(args);

                if (commandLineParser.HasErrors)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var error in commandLineParser.Errors)
                        Console.WriteLine(error);
                    return FatalError;
                }

                ExecuteBuildScript(new ScriptCompiler(), new BuildExecutor(), commandLineParser.Options);
                Console.ReadLine();
                return Success;
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fatal Error: {0}", exception);
                return FatalError;
            }
        }

        private static void ExecuteBuildScript(IScriptCompiler compiler, IBuildExecutor executor, Dictionary<string, string> options)
        {
            var buildScriptFilePath = options[CommandLineOption.PathToScript];
            var source = File.ReadAllText(buildScriptFilePath);
            var buildScript = compiler.CompileBuildScript(source);

            if (buildScript.Errors.HasErrors)
            {
                Console.WriteLine("Building the Build Script failed.");
                foreach (CompilerError error in buildScript.Errors)
                {
                    Console.WriteLine(error.ErrorText);
                }
            }
            else
            {
                var output = executor.ExecuteBuildScript(buildScript, options[CommandLineOption.MethodToInvoke]);
                Console.WriteLine(output);
            }
            Console.ReadLine();
        }
    }
}