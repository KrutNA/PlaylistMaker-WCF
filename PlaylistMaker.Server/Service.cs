using System;
using System.Linq;
using System.IO;
using System.ServiceModel;
using PlaylistMaker.Logic.Commands;
using PlaylistMaker.Logic.Model;
using PlaylistMaker.Logic.Stream;
namespace PlaylistMaker.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    internal class Service : IContract
    {
        private Output _output;
        private readonly string _newLine = Environment.NewLine;
        private readonly string _path = Environment.CurrentDirectory + "logs.log";
        
        public ObjectModel GetHelp()
        {
            _output = new Output();
            var request =
                $"[{DateTime.Now}]{_newLine}New request:{_newLine}\tGetting help{_newLine}{_newLine}";
            _output.Execute(request);
            File.AppendAllText(_path, request);
            var helpDisplayer = new PMHelpDisplayer();
            return helpDisplayer.Execute(new ObjectModel());
        }

        public ObjectModel AddComposition(ObjectModel model)
        {
            _output = new Output();
            var composition = model.Compositions.First();
            var request =
                $"[{DateTime.Now}]{_newLine}New request:{_newLine}\tAdding composition:{_newLine}\t\t{model.Result}{_newLine}\t\t{composition.Path}{_newLine}\t\t{composition.Author}{_newLine}\t\t{composition.Title}{_newLine}{_newLine}";
            _output.Execute(request);
            File.AppendAllText(_path, request);
            var adder = new CompositionAdder();
            return adder.Execute(model);
        }

        public ObjectModel RemoveComposition(ObjectModel model)
        {
            _output = new Output();
            var composition = model.Compositions.First();
            var request =
                $"[{DateTime.Now}]{_newLine}New request:{_newLine}\tRemoving composition:{_newLine}\t\t{model.Result}{_newLine}\t\t{composition.Path}{_newLine}\t\t{composition.Author}{_newLine}\t\t{composition.Title}{_newLine}{_newLine}";
            _output.Execute(request);
            File.AppendAllText(_path, request);
            var remover = new CompositionRemover();
            return remover.Execute(model);
        }

        public ObjectModel SearchComposition(ObjectModel model)
        {
            _output = new Output();
            var composition = model.Compositions.First();
            var request =
                $"[{DateTime.Now}]{_newLine}New request:{_newLine}\tSearching composition:{_newLine}\t\t{model.Result}{_newLine}\t\t{composition.Path}{_newLine}\t\t{composition.Author}{_newLine}\t\t{composition.Title}{_newLine}{_newLine}";
            _output.Execute(request);
            File.AppendAllText(_path, request);
            var searcher = new CompositionSearchEngine();
            return searcher.Execute(model);
        }

        public ObjectModel GetCompositionsList(ObjectModel model)
        {
            _output = new Output();
            var request =
                $"[{DateTime.Now}]{_newLine}New request:{_newLine}\tGetting compositions list:{_newLine}\t\t{model.Result}{_newLine}{_newLine}";
            _output.Execute(request);
            File.AppendAllText(_path, request);
            var compositionsDosplayer = new CompositionsListDisplayer();
            return compositionsDosplayer.Execute(model);
        }

        public ObjectModel GetPlaylistsList()
        {
            _output = new Output();
            var request =
                $"[{DateTime.Now}]{_newLine}New request:{_newLine}\tGetting playlists list:{_newLine}{_newLine}";
            _output.Execute(request);
            File.AppendAllText(_path, request);
            var playlistsDisplyaer = new PlaylistsListDisplayer();
            return playlistsDisplyaer.Execute(new ObjectModel());
        }
    }
}
