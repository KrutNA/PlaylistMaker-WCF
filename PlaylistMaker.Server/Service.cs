using System;
using System.Linq;
using System.IO;
using PlaylistMaker.Logic.Commands;
using PlaylistMaker.Logic.Model;
using PlaylistMaker.Logic.Stream;
namespace PlaylistMaker.Server
{
    class Service : IContract
    {
        private Output output;
        private string s = Environment.NewLine;
        private string path = Environment.CurrentDirectory + "logs.log";

        public ObjectModel GetHelp()
        {
            output = new Output();
            var request =
                $"[{DateTime.Now}]{s}New sequence:{s}\tGetting help{s}{s}";
            output.Execute(request);
            File.AppendAllText(path, request);
            var helpDisplayer = new PMHelpDisplayer();
            return helpDisplayer.Execute(new ObjectModel());
        }

        public ObjectModel AddComposition(ObjectModel model)
        {
            output = new Output();
            var composition = model.Compositions.First();
            var request =
                $"[{DateTime.Now}]{s}New sequence:{s}\tAdding composition:{s}\t\t{model.Result}{s}\t\t{composition.Path}{s}\t\t{composition.Author}{s}\t\t{composition.Title}{s}{s}";
            output.Execute(request);
            File.AppendAllText(path, request);
            var adder = new CompositionAdder();
            return adder.Execute(model);
        }

        public ObjectModel RemoveComposition(ObjectModel model)
        {
            output = new Output();
            var composition = model.Compositions.First();
            var request =
                $"[{DateTime.Now}]{s}New sequence:{s}\tRemoving composition:{s}\t\t{model.Result}{s}\t\t{composition.Path}{s}\t\t{composition.Author}{s}\t\t{composition.Title}{s}{s}";
            output.Execute(request);
            File.AppendAllText(path, request);
            var remover = new CompositionRemover();
            return remover.Execute(model);
        }

        public ObjectModel SearchComposition(ObjectModel model)
        {
            output = new Output();
            var composition = model.Compositions.First();
            var request =
                $"[{DateTime.Now}]{s}New sequence:{s}\tSearching composition:{s}\t\t{model.Result}{s}\t\t{composition.Path}{s}\t\t{composition.Author}{s}\t\t{composition.Title}{s}{s}";
            output.Execute(request);
            File.AppendAllText(path, request);
            var searcher = new CompositionSearchEngine();
            return searcher.Execute(model);
        }

        public ObjectModel GetCompositionsList(ObjectModel model)
        {
            output = new Output();
            var request =
                $"[{DateTime.Now}]{s}New sequence:{s}\tGetting compositions list:{s}\t\t{model.Result}{s}{s}";
            output.Execute(request);
            File.AppendAllText(path, request);
            var compositionsDosplayer = new CompositionsListDisplayer();
            return compositionsDosplayer.Execute(model);
        }

        public ObjectModel GetPlaylistsList()
        {
            output = new Output();
            var request =
                $"[{DateTime.Now}]{s}New sequence:{s}\tGetting playlists list:{s}{s}";
            output.Execute(request);
            File.AppendAllText(path, request);
            var playlistsDisplyaer = new PlaylistsListDisplayer();
            return playlistsDisplyaer.Execute(new ObjectModel());
        }
    }
}
