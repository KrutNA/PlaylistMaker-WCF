using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading;
using PlaylistMaker.Logic.Stream;

namespace PlaylistMaker.Server
{
    internal class Server
    {
        private static ServiceHost _host;
        private static Output _output;
        private static Input _input;

        private static void CheckInput()
        {
            _output.Execute("Input \"stop\" to close app\n");
            while (_input.Execute().ToLower() != "stop") { }
            _host.Close();
            Environment.Exit(0);
        }

        private static void Main(string[] args)
        {
            Console.Title = "Server";
            if (args.Length != 2)
                args = new[] {"localhost", "10666"};
            _output = new Output();
            _input = new Input();
            var check = new Thread(CheckInput);
            var address = $"http://{args[0]}:{args[1]}/IContract";
            _host = new WebServiceHost(new Service(), new Uri(address));
            check.Start();
            try
            {
              _host.Open();
            }
            catch (Exception e)
            {
                _output.Execute($"{e}");
                _input.ReadKey();
            }
        }
    }
}
