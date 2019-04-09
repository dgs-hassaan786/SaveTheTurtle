using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turtle.Domain.Data.Builder;
using Turtle.Domain.Models.Entities;
using Turtle.Domain.Models.Enums;
using Turtle.Foundation.SharedContext.FileHandler;
using Turtle.Foundation.SharedContext.Writer;

namespace Turtle.Domain.Data
{
    /// <summary>
    /// IExecutor Interface exposed to client 
    /// </summary>
    public interface IExecutor
    {
        void Run();
    }
    public class Executor : IExecutor
    {
        private Configurations _gameConfig;
        private Moves[] _movements;
        private TurtlePointer _turtle;
        Dictionary<int, BoardCell[]> _board;

        public Executor(string configFile, string movesFile)
        {
            _gameConfig = JsonFileReader.IRead<Configurations>(configFile);
            _movements = JsonFileReader.IRead<Moves[]>(movesFile);
            _turtle = new TurtlePointer(_gameConfig.EntryDirection, _gameConfig.EntryPoint.XVal, _gameConfig.EntryPoint.YVal);
            InitializeBoard();

        }

        public void Run()
        {
            Writer.Write("-------- Welcome to the Save the Turtle -------------", ConsoleColor.DarkYellow);
            Writer.Write();
            Writer.Write();

            PrintBoard();

            var loopBreak = false;

            Writer.Write("----------- Turtle Position -------------", ConsoleColor.White);
            Writer.Write($"Starting point [{_turtle.XVal},{_turtle.YVal}]", ConsoleColor.White);
            Writer.Write($"Starting direction: {_turtle.Direction}", ConsoleColor.White);
            Writer.Write();
            Writer.Write();
            Writer.Write("----------- Movements------------------", ConsoleColor.DarkCyan);

            for (int x = 0; x < _movements.Length; x++)
            {
                Console.WriteLine($"{x + 1}. {_movements[x]}");
                switch (_movements[x])
                {
                    case Moves.Move:
                        switch (_turtle.Direction)
                        {
                            case Directions.West:
                                loopBreak = MoveWest();
                                break;
                            case Directions.East:
                                loopBreak = MoveEast();
                                break;
                            case Directions.South:
                                loopBreak = MoveSouth();
                                break;
                            case Directions.North:
                                loopBreak = MoveNorth(loopBreak);
                                break;
                            default:
                                break;
                        }
                        break;
                    case Moves.Rotate:                        
                        RotateTurtle(_turtle);                        
                        break;
                    default:
                        break;
                }

                Writer.Write();

                if (loopBreak)
                    break;
            }

            if (!loopBreak)
            {
                Writer.Write("Turtle couldn't find the way and lost forever", ConsoleColor.Red);
            }
        }

        private bool MoveNorth(bool loopBreak)
        {
            if (_turtle.XVal - 1 < 0)
            {
                Writer.Write($"Cannot move turtle as beyond position: [{_turtle.XVal},{_turtle.YVal}]", ConsoleColor.Yellow);
            }
            else
            {
                if (_board[_turtle.XVal - 1][_turtle.YVal].HasMine)
                {
                    Writer.Write($"Danger Turtle can hit the mine at [{_turtle.XVal - 1},{_turtle.YVal}]", ConsoleColor.Red);
                }
                Writer.Write($"Turtle moved from position: [{_turtle.XVal},{_turtle.YVal}] to [{_turtle.XVal - 1},{_turtle.YVal}]", ConsoleColor.DarkGreen);
                _turtle.XVal -= 1;
                loopBreak = CellHasMineOrExit(_turtle, _board);
            }

            return loopBreak;
        }

        private bool MoveSouth()
        {
            bool loopBreak = false;
            if (_turtle.XVal + 1 == _gameConfig.MatrixX)
            {
                Writer.Write($"Cannot move turtle as beyond position: [{_turtle.XVal},{_turtle.YVal}]", ConsoleColor.Yellow);
            }
            else
            {
                if (_board[_turtle.XVal + 1][_turtle.YVal].HasMine)
                {                    
                    Writer.Write($"Danger Turtle can hit the mine at [{_turtle.XVal + 1},{_turtle.YVal}]", ConsoleColor.Red);
                }
                Writer.Write($"Turtle moved from position: [{_turtle.XVal},{_turtle.YVal}] to [{_turtle.XVal + 1},{_turtle.YVal}]", ConsoleColor.DarkGreen);
                _turtle.XVal += 1;
                loopBreak = CellHasMineOrExit(_turtle, _board);
            }

            return loopBreak;
        }

        private bool MoveEast()
        {
            bool loopBreak = false;
            if (_turtle.YVal + 1 == _gameConfig.MatrixY)
            {
                Writer.Write($"Cannot move turtle as beyond position: [{_turtle.XVal},{_turtle.YVal}]", ConsoleColor.Yellow);
            }
            else
            {
                if (_board[_turtle.XVal][_turtle.YVal + 1].HasMine)
                {
                    Writer.Write($"Danger Turtle can hit the mine at [{_turtle.XVal},{_turtle.YVal + 1}]", ConsoleColor.Red);
                }                
                Writer.Write($"Turtle moved from position: [{_turtle.XVal},{_turtle.YVal}] to [{_turtle.XVal},{_turtle.YVal + 1}]", ConsoleColor.DarkGreen);
                _turtle.YVal += 1;
                loopBreak = CellHasMineOrExit(_turtle, _board);
            }

            return loopBreak;
        }

        private bool MoveWest()
        {
            bool loopBreak = false;

            if (_turtle.YVal - 1 < 0)
            {
                Writer.Write($"Cannot move turtle as beyond position: [{_turtle.XVal},{_turtle.YVal}]", ConsoleColor.Yellow);
            }
            else
            {
                if (_board[_turtle.XVal][_turtle.YVal - 1].HasMine)
                {
                    Writer.Write($"Danger Turtle can hit the mine at [{_turtle.XVal},{_turtle.YVal - 1}]", ConsoleColor.Red);
                }
                Writer.Write($"Turtle moved from position: [{_turtle.XVal},{_turtle.YVal}] to [{_turtle.XVal},{_turtle.YVal - 1}]", ConsoleColor.DarkGreen);
                _turtle.YVal -= 1;
                loopBreak = CellHasMineOrExit(_turtle, _board);
            }

            return loopBreak;
        }

        private static bool CellHasMineOrExit(TurtlePointer turtle, Dictionary<int, BoardCell[]> board)
        {
            bool loopBreak = false;

            if (board[turtle.XVal][turtle.YVal].HasMine)
            {
                Writer.Write("Turtle hitted the mine and killed...",ConsoleColor.DarkRed);
                loopBreak = true;
            }
            else if (board[turtle.XVal][turtle.YVal].IsExitPoint)
            {
                Writer.Write("Game ended successfully", ConsoleColor.Green);
                loopBreak = true;
            }

            return loopBreak;
        }

        private static void RotateTurtle(TurtlePointer turtle)
        {
            var previousDirection = turtle.Direction;
            switch (turtle.Direction)
            {
                case Directions.North:
                    turtle.Direction = Directions.East;
                    break;
                case Directions.South:
                    turtle.Direction = Directions.West;
                    break;
                case Directions.East:
                    turtle.Direction = Directions.South;
                    break;
                case Directions.West:
                    turtle.Direction = Directions.North;
                    break;
                default:
                    break;
            }
            Writer.Write($"Turtle Direction changed from {previousDirection} to {turtle.Direction}", ConsoleColor.DarkYellow);
        }

        private void InitializeBoard()
        {
            _board = new Dictionary<int, BoardCell[]>();
            for (int i = 0; i < _gameConfig.MatrixX; i++)
            {
                BoardCell[] cells = new BoardCell[_gameConfig.MatrixY];
                bool hasMine = _gameConfig.Mines.Any(x => x.XVal == i);
                bool hasExist = _gameConfig.Exit.XVal == i ? true : false;


                for (int j = 0; j < _gameConfig.MatrixY; j++)
                {
                    cells[j] = new BoardCell(j, hasMine ? _gameConfig.Mines.Any(x => x.XVal == i && x.YVal == j) : false, hasExist ? (_gameConfig.Exit.YVal == j ? true : false) : false);
                }

                _board.Add(i, cells);
            }
        }

        private void PrintBoard()
        {
            for (int i = 0; i < _gameConfig.MatrixX; i++)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("|");                
                for (int j = 0; j < _gameConfig.MatrixY; j++)
                {
                    builder.Append($"{(_board[i][j].HasMine ? " Mine " : "      ")}{(_board[i][j].IsExitPoint ? " Exit " : "      ")}|");                    
                }

                Writer.Write(builder.ToString(), ConsoleColor.Blue);
                Writer.Write();
            }
        }

    }
}
