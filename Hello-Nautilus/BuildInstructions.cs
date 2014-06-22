using Nautilus.Framework;

namespace BuildMe
{
	public class BuildInstructions : TaskRunner
	{
		private const string _pathToSolution = @"C:\play\nautilus\Nautilus-Build\Hello-Nautilus\src\HelloNautilus.sln";
		private const string _outputPath = @"C:\play\nautilus\Nautilus-Build\Hello-Nautilus\Output";

		public string Default()
		{
			CompileSolution(_pathToSolution, _outputPath);
			RunUnitTests();
			return "Successfully executed Nautilus TaskRunner.";
		}
	}
}