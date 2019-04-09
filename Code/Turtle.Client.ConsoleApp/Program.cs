using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turtle.Domain.Data;
using Turtle.Domain.Data.Builder;
using Turtle.Domain.Models.Entities;
using Turtle.Domain.Models.Enums;

namespace Turtle.Client.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var configFile = "game-settings.json";
            var movesFile = "moves.json";            

            if(args.Length > 0 && args.Length == 2)
            {
                configFile = args[0];
                movesFile = args[1];                
            }

            IExecutor executor = new Executor( configFile, movesFile);
            executor.Run();
         
        }


    }

}
