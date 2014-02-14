using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.CSharp;

namespace Nautilus
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildFromACSharpFile();
        }

        private static void BuildFromACSharpFile()
        {
            const string buildScriptFilePath = @"C:\play\nautilus\Hello-Nautilus\BuildScript.cs";
            var source = File.ReadAllText(buildScriptFilePath);
            var csharpParameters = new CompilerParameters(new []{"System.dll"})
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                IncludeDebugInformation = false
            };
            var options = new Dictionary<string, string> { { "CompilerVersion", "v4.0" } };
            var provider = new CSharpCodeProvider(options);
            var results = provider.CompileAssemblyFromSource(csharpParameters, source);

            if (results.Errors.HasErrors)
            {
                foreach (CompilerError error in results.Errors)
                {
                    Console.WriteLine(error.ErrorText);
                }
            }
            else
            {
                var compiledAssembly = results.CompiledAssembly;
                var programClass = compiledAssembly.GetType("BuildMe.Program");
                var mainMethod = programClass.GetMethod("Main");
                var output = mainMethod.Invoke(null, null);
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