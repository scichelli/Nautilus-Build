using System.CodeDom.Compiler;

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
            var programClass = compiledAssembly.GetType("BuildMe.Program");
            var startingMethod = programClass.GetMethod(methodToInvoke);
            var output = startingMethod.Invoke(null, null);
            return output;
        }
    }
}