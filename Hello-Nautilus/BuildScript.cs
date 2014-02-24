using Nautilus.Framework;

namespace BuildMe
{
    public class Program
    {
        public static string Main()
        {
			string pathToSolution = @"C:\play\nautilus\Nautilus-Build\Hello-Nautilus\src\HelloNautilus.sln";
			string outputPath = @"C:\play\nautilus\Nautilus-Build\Hello-Nautilus\Output";
			var nautilus = new Worker();
			nautilus.CompileSolution(pathToSolution, outputPath);
			nautilus.RunUnitTests();
            return "Successfully executed build script.";
        }
    }
}