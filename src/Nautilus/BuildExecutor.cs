using System.CodeDom.Compiler;

namespace Nautilus
{
    public interface IBuildExecutor
    {
        object ExecuteBuildScript(CompilerResults results);
    }

    class BuildExecutor : IBuildExecutor
    {
        public object ExecuteBuildScript(CompilerResults results)
        {
            var compiledAssembly = results.CompiledAssembly;
            var programClass = compiledAssembly.GetType("BuildMe.Program");
            var mainMethod = programClass.GetMethod("Main");
            var output = mainMethod.Invoke(null, null);
            return output;
        }
    }
}