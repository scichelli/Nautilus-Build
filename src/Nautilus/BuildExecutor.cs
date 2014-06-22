using System;
using System.CodeDom.Compiler;
using System.Linq;
using Nautilus.Framework;

namespace Nautilus
{
    using System.Reflection;

    public interface IBuildExecutor
    {
        object ExecuteBuildScript(CompilerResults results, string methodToInvoke);
    }

    class BuildExecutor : IBuildExecutor
    {
        public object ExecuteBuildScript(CompilerResults results, string methodToInvoke)
        {
            var compiledAssembly = results.CompiledAssembly;
            var taskRunnerType = compiledAssembly.GetTypes().Single(t => (typeof(TaskRunner)).IsAssignableFrom(t));
            var taskRunner = Activator.CreateInstance(taskRunnerType);
            var startOfTheBuild = taskRunner.GetType()
                .GetMethod(methodToInvoke,
                    BindingFlags.Public | BindingFlags.Instance | 
                    BindingFlags.Static | BindingFlags.IgnoreCase |
                    BindingFlags.DeclaredOnly);
            return startOfTheBuild.Invoke(taskRunner, null);
        }
    }
}