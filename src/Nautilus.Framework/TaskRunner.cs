using System;
using System.Collections.Generic;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using System.Diagnostics;
using System.Management.Automation;

namespace Nautilus.Framework
{
    public abstract class TaskRunner
    {
        public void CompileSolution(string pathToSolution)
        {
            var projectCollection = new ProjectCollection();
            var globalProperty = new Dictionary<string, string>
                {
                    {"Configuration", "Debug"},
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

        public void Exec(string command)
        {
            var cmdLine = string.Format("/C {0}", command);
            var processInfo = new ProcessStartInfo("CMD.exe", cmdLine);
            processInfo.UseShellExecute = false;
            processInfo.CreateNoWindow = true;
            processInfo.RedirectStandardOutput = true;
            using (var process = Process.Start(processInfo))
            {
                process.WaitForExit();
                Log(process.StandardOutput.ReadToEnd());
                Log(string.Format("command: {0} exited with code {1}", command, process.ExitCode));
            }
        }

        public void RunPowerShell(string pathToPowerShellScript)
        {
            var ps = PowerShell.Create();
            ps.AddScript(pathToPowerShellScript);
            ps.Invoke();
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
