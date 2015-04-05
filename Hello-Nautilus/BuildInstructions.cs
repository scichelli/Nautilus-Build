namespace BuildMe
{
	public class BuildInstructions : Nautilus.Framework.BuildInstructions
	{
		private const string _pathToSolution = @".\src\HelloNautilus.sln";

		public string Default()
		{
			CompileSolution(_pathToSolution);
			RunUnitTests();
			return "Successfully executed Nautilus Build Instructions.";
		}
	}
}