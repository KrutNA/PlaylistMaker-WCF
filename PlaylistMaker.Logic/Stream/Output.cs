using System;

namespace PlaylistMaker.Logic.Stream
{
    public class Output
    {
        public void Execute(string text)
        {
            Console.Write(text);
        }

        public void Execute(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
