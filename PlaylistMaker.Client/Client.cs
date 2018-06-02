using System;
using System.Text;
using System.Net.Http;
using System.ServiceModel;
using PlaylistMaker.Logic.Stream;
using PlaylistMaker.Logic.Model;

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
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(address);
                var factory = new ChannelFactory<IContract>(binding, endpoint);
                while (true)
                {
                    var engine = new RequesEngine();
                    if (!engine.Create())
                    {
                        _output.Execute("Unknown command\n");
                        continue;
                    }

                    var channel = factory.CreateChannel();
                    engine.Execute(ref channel);
                }
            }
            else
            {
                _output.Execute(
                    "Program need only 2 arguments:\n\t1) IP addres of Server\n\t2) Port for connection\nPress any key to exit");
                _input.ReadKey();
            }
        }
    }
}
