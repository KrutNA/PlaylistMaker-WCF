using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaylistMaker.Logic.Model;
using PlaylistMaker.Logic.Commands;
using PlaylistMaker.Logic.Stream;

namespace PlaylistMaker.Client
{
    internal class RequesEngine
    {
        private Output _output;
        private Input _input;
        private readonly ICommand[] _commands;
        private ObjectModel model;
        private ICommand _command;

        public RequesEngine()
        {
            _commands = new ICommand[]
            {
                new ClientCloser(),
                new CompositionAdder(),
                new CompositionRemover(),
                new CompositionSearchEngine(),
                new CompositionsListDisplayer(), 
                new PlaylistsListDisplayer(),
                new PMHelpDisplayer(),
            };
        }

        public bool Create()
        {
            _input = new Input();
            _output = new Output();
            _output.Execute("Input command: ");
            var commandName = _input.Execute();
            var retrn = _commands.Any(x => x.GetName() == commandName);
            if (retrn)
            {
                _command = _commands.FirstOrDefault(x => x.GetName() == commandName);
                if (commandName == "exit")
                    _command.Execute(model = new ObjectModel());
                else
                {
                    model = _command.ReadArgs();
                }
            }
            return retrn;
        }

        public void Execute(ref IContract channel)
        {
            switch (_command.GetName())
            {
                case "help":
                    model = channel.GetHelp();
                    break;
                case "add":
                    model = channel.AddComposition(model);
                    break;
                case "rm":
                    model = channel.RemoveComposition(model);
                    break;
                case "search":
                    model = channel.SearchComposition(model);
                    break;
                case "list":
                    model = channel.GetCompositionsList(model);
                    break;
                case "ls":
                    model = channel.GetPlaylistsList();
                    break;
            }

            _output.Execute(model.ToString());
        }
    }
}
