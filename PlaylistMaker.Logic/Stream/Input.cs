using System;

namespace PlaylistMaker.Logic.Stream
{
    public class Input
    {
        public bool IsRunnig { get; set; } = true;
        public string Value { get; private set; }
        
        public string Execute()
        {
            return Console.ReadLine();
        }

        public string Execute(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            var value = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            return value;
        }

        public string Execute(string outputText)
        {
            var output = new Output();
            output.Execute(outputText, ConsoleColor.Yellow);
            return this.Execute(ConsoleColor.Cyan);
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public void ConsoleReading()
        {
            do
            {
                Value = this.Execute();
            } while (this.IsRunnig);
        }
    }
}
