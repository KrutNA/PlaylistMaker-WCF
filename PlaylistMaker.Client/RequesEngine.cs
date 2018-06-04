using System;
using System.Linq;
using System.Text.RegularExpressions;
using PlaylistMaker.Logic.Model;
using PlaylistMaker.Logic.Commands;
using PlaylistMaker.Logic.Stream;

// ReSharper disable once CheckNamespace
namespace PlaylistMaker.Client
{
    internal class RequesEngine
    {
        public bool IsNotCreated;
        private Output _output;
        private Input _input;
        private readonly ICommand[] _commands;
        private ObjectModel _model;
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
            _output.Execute("Input command: ", ConsoleColor.Green);
            var commandName = Regex.Replace(_input.Execute(ConsoleColor.Cyan), @"\s+", " ");
            var retrn = _commands.Any(x => x.GetName() == commandName);
            if (retrn)
            {
                _command = _commands.FirstOrDefault(x => x.GetName() == commandName);
                if (commandName == "exit")
                    _command.Execute(_model = new ObjectModel());
                else
                {
                    _model = _command.ReadArgs();
                }
            }
            IsNotCreated = _model.IsNull;
            return retrn;
        }

        public void OutputResult(string result)
        {
            _output.Execute(_model.Result + "\n", result == "Done" ? ConsoleColor.Green : ConsoleColor.Red);
        }

        public void Execute(ref IContract channel)
        {
            switch (_command.GetName())
            {
                case "help":
                    _model = channel.GetHelp();
                    OutputResult(_model.Result);
                    for (var i = 0; i < 14; i++)
                        if (i % 2 == 0)
                            _output.Execute(_model.Results[i], ConsoleColor.Gray);
                        else
                            _output.Execute(_model.Results[i]);
                    return;
                case "add":
                    _model = channel.AddComposition(_model);
                    break;
                case "rm":
                    _model = channel.RemoveComposition(_model);
                    break;
                case "search":
                    _model = channel.SearchComposition(_model);
                    break;
                case "ls c":
                    _model = channel.GetCompositionsList(_model);
                    break;
                case "ls p":
                    _model = channel.GetPlaylistsList();
                    break;
            }
            
            OutputResult(_model.Result);
            _output.Execute(_model.ToString());
        }
    }
}
