using System;

namespace Turtle.Foundation.SharedContext.Writer
{
    public static class Writer
    {
        public static void Write(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static void Write(string msg = "")
        {
            Console.WriteLine(msg);
        }
    }
}
