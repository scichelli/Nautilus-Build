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

                ExecuteBuildInstructions(new BuildInstructionsCompiler(), new BuildExecutor(), commandLineParser.Options);
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

        private static void ExecuteBuildInstructions(IBuildInstructionsCompiler compiler, IBuildExecutor executor, Dictionary<string, string> options)
        {
            var instructionsFilePath = options[CommandLineOption.PathToBuildInstructions];
            var source = File.ReadAllText(instructionsFilePath);
            var instructions = compiler.Compile(source);

            if (instructions.Errors.HasErrors)
            {
                //TODO: Move all error reporting into one place, not here and in Program.Main.
                Console.WriteLine("Building the Build Instructions failed.");
                foreach (CompilerError error in instructions.Errors)
                {
                    Console.WriteLine(error.ErrorText);
                }
            }
            else
            {
                var output = executor.ExecuteBuildInstructions(instructions, options[CommandLineOption.MethodToInvoke]);
                Console.WriteLine(output);
            }
        }
    }
}