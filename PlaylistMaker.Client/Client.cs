using System;
using System.ServiceModel.Web;
using PlaylistMaker.Logic.Model;
using PlaylistMaker.Logic.Stream;

// ReSharper disable once CheckNamespace
namespace PlaylistMaker.Client
{
    internal class Client
    {
        private static Output _output;
        private static Input _input;

        private static void Main(string[] args)
        {
            Console.Title = "Client";
            _output = new Output();
            _input = new Input();

            if (args.Length == 2)
            {
                var address = $"http://{args[0]}:{args[1]}/IContract";
                var factory = new WebChannelFactory<IContract>(new Uri(address));
                while (true)
                {
                    var engine = new RequesEngine();
                    if (!engine.Create())
                    {
                        _output.Execute("Unknown command\n", ConsoleColor.Red);
                        continue;
                    }

                    if (engine.IsNotCreated)
                        continue;
                    try
                    {
                        var channel = factory.CreateChannel();
                        engine.Execute(ref channel);
                    }
                    catch
                    {
                        _output.Execute("Can't connect to server\n",
                            ConsoleColor.Red);
                    }
                }
            }
            _output.Execute(
                "Program need only 2 arguments:\n\t1) IP addres of Server\n\t2) Port for connection\nPress any key to exit",
                ConsoleColor.Red);
            _input.ReadKey();
        }
    }
}
