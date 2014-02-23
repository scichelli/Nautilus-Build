using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;

namespace Nautilus
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildFromACSharpFile(new ScriptCompiler(), new BuildExecutor());
        }

        private static void BuildFromACSharpFile(IScriptCompiler compiler, IBuildExecutor executor)
        {
            const string buildScriptFilePath = @"C:\play\nautilus\Nautilus-Build\Hello-Nautilus\BuildScript.cs";
            var source = File.ReadAllText(buildScriptFilePath);
            var results = compiler.CompileBuildScript(source);

            if (results.Errors.HasErrors)
            {
                foreach (CompilerError error in results.Errors)
                {
                    Console.WriteLine(error.ErrorText);
                }
            }
            else
            {
                var output = executor.ExecuteBuildScript(results);
                Console.WriteLine(output.ToString());
            }
            Console.ReadLine();
        }

        private static void BuildFromASolutionFile()
        {
            string solutionFilePath = @"C:\play\nautilus\Hello-Nautilus\src\HelloNautilus.sln";

            var projectCollection = new ProjectCollection();
            var globalProperty = new Dictionary<string, string>
                {
                    {"Configuration", "Debug"},
                    {"OutputPath", @"C:\play\nautilus\Hello-Nautilus\Output"}
                };

            var buildParameters = new BuildParameters(projectCollection);
            var buildRequest = new BuildRequestData(solutionFilePath, globalProperty, "4.0", new[] {"build"}, null);
            var buildResult = BuildManager.DefaultBuildManager.Build(buildParameters, buildRequest);

            Console.WriteLine("The result was {0}.", buildResult.OverallResult == BuildResultCode.Success ? "good" : "sad");
            Console.ReadLine();
        }
    }
}