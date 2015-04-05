using Should;

namespace Nautilus.Tests
{
    public class BuildInstructionsCompilerTests
    {
        private readonly BuildInstructionsCompiler _buildInstructionsCompiler;

        public BuildInstructionsCompilerTests()
        {
            _buildInstructionsCompiler = new BuildInstructionsCompiler();
        }

        public void BaselineCompilerTest()
        {
            var results = _buildInstructionsCompiler.Compile(Instructions);
            results.Errors.HasErrors.ShouldBeFalse();
        }

        public void ReportingCompilerErrors()
        {
            var results = _buildInstructionsCompiler.Compile(string.Format("{0} invalid code", Instructions));
            results.Errors.HasErrors.ShouldBeTrue();
            results.Errors.Count.ShouldEqual(1);
        }

        private const string Instructions = @"
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
