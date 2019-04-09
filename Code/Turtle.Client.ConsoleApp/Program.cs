using Turtle.Domain.Data;

namespace Turtle.Client.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var configFile = "game-settings.json";
            var movesFile = "moves.json";

            //parsing the file names to our variables
            if (args.Length > 0 && args.Length == 2)
            {
                configFile = args[0];
                movesFile = args[1];
            }

            //calling the executor interface
            IExecutor executor = new Executor(configFile, movesFile);
            executor.Run();

        }


    }

}
