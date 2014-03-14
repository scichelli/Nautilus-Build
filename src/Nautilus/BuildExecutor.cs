using System;
using System.CodeDom.Compiler;
using System.Linq;
using Nautilus.Framework;

namespace Nautilus
{
    public interface IBuildExecutor
    {
        object ExecuteBuildScript(CompilerResults results, string methodToInvoke);
    }

    class BuildExecutor : IBuildExecutor
    {
        public object ExecuteBuildScript(CompilerResults results, string methodToInvoke)
        {
            var compiledAssembly = results.CompiledAssembly;
            var shell = compiledAssembly.GetTypes().Single(t => (typeof(Shell)).IsAssignableFrom(t));
            var mollusk = Activator.CreateInstance(shell);
            var startOfTheBuild = mollusk.GetType().GetMethod(methodToInvoke);
            return startOfTheBuild.Invoke(mollusk, null);
        }
    }
}