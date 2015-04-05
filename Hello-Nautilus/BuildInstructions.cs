using Nautilus.Framework;

namespace BuildMe
{
	public class BuildInstructions : TaskRunner
	{
		private const string _pathToSolution = @".\src\HelloNautilus.sln";

		public string Default()
		{
			CompileSolution(_pathToSolution);
			RunUnitTests();
			return "Successfully executed Nautilus TaskRunner.";
		}
	}
}