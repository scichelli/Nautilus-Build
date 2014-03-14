using System;
using System.Collections.Generic;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;

namespace Nautilus.Framework
{
    public abstract class Shell
    {
        public void CompileSolution(string pathToSolution, string outputPath)
        {
            var projectCollection = new ProjectCollection();
            var globalProperty = new Dictionary<string, string>
                {
                    {"Configuration", "Debug"},
                    {"OutputPath", outputPath}
                };

            var buildParameters = new BuildParameters(projectCollection);
            var buildRequest = new BuildRequestData(pathToSolution, globalProperty, "4.0", new[] { "build" }, null);
            var buildResult = BuildManager.DefaultBuildManager.Build(buildParameters, buildRequest);
            if (buildResult.OverallResult == BuildResultCode.Success)
            {
                Console.WriteLine("Successfully compiled the solution.");
            }
            else
            {
                Console.WriteLine(buildResult.Exception);
            }
        }

        public void RunUnitTests()
        {
            Console.WriteLine("I ran tests!");
        }
    }
}
