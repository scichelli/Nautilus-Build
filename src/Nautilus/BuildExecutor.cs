using System;
using System.CodeDom.Compiler;
using System.Linq;
using Nautilus.Framework;
using System.Reflection;

namespace Nautilus
{
    public interface IBuildExecutor
    {
        object ExecuteBuildInstructions(CompilerResults results, string methodToInvoke);
    }

    class BuildExecutor : IBuildExecutor
    {
        public object ExecuteBuildInstructions(CompilerResults results, string methodToInvoke)
        {
            var compiledAssembly = results.CompiledAssembly;
            var buildInstructionsType = compiledAssembly.GetTypes().Single(t => (typeof(BuildInstructions)).IsAssignableFrom(t));
            var buildInstructions = Activator.CreateInstance(buildInstructionsType);
            var startOfTheBuild = buildInstructions.GetType()
                .GetMethod(methodToInvoke,
                    BindingFlags.Public | BindingFlags.Instance | 
                    BindingFlags.Static | BindingFlags.IgnoreCase |
                    BindingFlags.DeclaredOnly);
            return startOfTheBuild.Invoke(buildInstructions, null);
        }
    }
}