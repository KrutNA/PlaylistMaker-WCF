using System;
using System.IO;
using System.Linq;
using PlaylistMaker.Logic.Model;
using PlaylistMaker.Logic.Stream;

namespace PlaylistMaker.Logic.Commands
{
    public class CompositionsListDisplayer : ICommand
    {
        private const string Name = "ls c";

        public ObjectModel Execute(ObjectModel model)
        {
            if (!File.Exists(model.Result) && !File.Exists($"{model.Result}.pls"))
                return new ObjectModel {Result = "Playlist not found"};
            var playlist = new Playlist(model.Result.EndsWith(".pls") ? model.Result : $"{model.Result}.pls");
            return playlist.Compositions.Any() ?
                new ObjectModel { Result = "Done", Compositions = playlist.Compositions.ToArray() } :
                new ObjectModel { Result = "Not found" };
        }

        public ObjectModel ReadArgs()
        {
            var input = new Input();
            var output = new Output();
            var args = new[]
            {
                input.Execute("Playlist name: ")
            };  

            if (!string.IsNullOrWhiteSpace(args[0])) return new ObjectModel {IsNull = false, Result = args[0]};
            output.Execute("One or more arguments is null or whitespace!\n", ConsoleColor.Red);
            return new ObjectModel {IsNull = true};

        }

        public string GetName()
        {
            return Name;
        }
    }
}
