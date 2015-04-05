using Should;

namespace Nautilus.Tests
{
    public class ScriptCompilerTests
    {
        private readonly ScriptCompiler _scriptCompiler;

        public ScriptCompilerTests()
        {
            _scriptCompiler = new ScriptCompiler();
        }

        public void BaselineCompilerTest()
        {
            var results = _scriptCompiler.CompileBuildScript(Script);
            results.Errors.HasErrors.ShouldBeFalse();
        }

        public void ReportingCompilerErrors()
        {
            var results = _scriptCompiler.CompileBuildScript(string.Format("{0} invalid code", Script));
            results.Errors.HasErrors.ShouldBeTrue();
            results.Errors.Count.ShouldEqual(1);
        }

        private const string Script = @"
using Nautilus.Framework;

namespace TestToBuild
{
	public class SampleBuildInstructions : BuildInstructions
	{
		private const string _pathToSolution = @""C:\play\nautilus\Nautilus-Build\Hello-Nautilus\src\HelloNautilus.sln"";

		public void Default()
		{
			CompileSolution(_pathToSolution);
			RunUnitTests();
		}
	}
}
";
    }
}
