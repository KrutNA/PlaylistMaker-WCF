using System;
using System.Linq;
using PlaylistMaker.Logic.Model;
using PlaylistMaker.Logic.Stream;

namespace PlaylistMaker.Logic.Commands
{
    public class CompositionAdder : ICommand
    {
        private const string Name = "add";

        public ObjectModel Execute(ObjectModel model)
        {
            var playlist = new Playlist(model.Result.EndsWith(".pls") ? model.Result : $"{model.Result}.pls");
            playlist.Compositions.Add(model.Compositions.First());
            playlist.Save();
            return new ObjectModel() {IsNull = true, Result = "Done"};
        }

        public ObjectModel ReadArgs()
        {
            var input = new Input();
            var output = new Output();
            var args = new[]
            {
                input.Execute("Playlist name: "),
                input.Execute("Path to composition: "),
                input.Execute("Composition author: "),
                input.Execute("Composition title: "),
            };
            if (!args.Any(string.IsNullOrWhiteSpace))
                return new ObjectModel()
                {
                    IsNull = false,
                    Result = args[0],
                    Compositions = new[]
                    {
                        new Composition(args[1], args[2], args[3])
                    }
                };
            output.Execute("One or more arguments is null or whitespace!\n", ConsoleColor.Red);
            return new ObjectModel() {IsNull = true};   
        }

        public string GetName()
        {
            return Name;
        }
    }
}
