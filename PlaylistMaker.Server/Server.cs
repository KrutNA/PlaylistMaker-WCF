using System;
using System.ServiceModel;
using System.Threading;
using PlaylistMaker.Logic.Stream;
using PlaylistMaker.Logic.Model;

namespace PlaylistMaker.Server
{
    public class Server
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
            _output = new Output();
            _input = new Input();
            var binding = new BasicHttpBinding();
            var contract = typeof(IContract);
            var check = new Thread(CheckInput);
            const string address = "http://localhost:10666/IContract";

            _host = new ServiceHost(typeof(Service));
            _host.AddServiceEndpoint(contract, binding, address);
            check.Start();
            _host.Open();
            /*try
            {
            }
            catch (Exception e)
            {
                _output.Execute($"{e}");
                _input.ReadKey();
                return;
            }*/
        }
    }
}
