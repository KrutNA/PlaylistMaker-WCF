using System;
using System.IO;
using System.Linq;
using PlaylistMaker.Logic.Model;
using PlaylistMaker.Logic.Stream;

namespace PlaylistMaker.Logic.Commands
{
    public class CompositionRemover : ICommand
    {
        private const string Name = "rm";
        private const int ArgsCount = 4;

        public ObjectModel Execute(ObjectModel model)
        {
            if (!File.Exists(model.Result) && !File.Exists($"{model.Result}.pls"))
                return new ObjectModel() { Result = "Playlist not found"};
            var composotion = model.Compositions.First();
            var newModel = new ObjectModel();
            var playlist = new Playlist(model.Result.EndsWith(".pls") ? model.Result : $"{model.Result}.pls");
            if (playlist.Compositions.Any(x =>
                String.Equals(x.Path, composotion.Path, StringComparison.CurrentCultureIgnoreCase) &&
                String.Equals(x.Author, composotion.Author, StringComparison.CurrentCultureIgnoreCase) &&
                String.Equals(x.Title, composotion.Title, StringComparison.CurrentCultureIgnoreCase)))
            {
                playlist.Compositions.Remove(playlist.Compositions.Find(x =>
                    x.Path == composotion.Path && x.Author == composotion.Author && x.Title == composotion.Title));
                playlist.Save();
                newModel.Result = "Done";
            }
            else
                newModel.Result = "Not found";
            return newModel;
        }

        public ObjectModel ReadArgs()
        {
            var input = new Input();
            var output = new Output();
            var _args = new[]
            {
                input.Execute("Playlist name: "),
                input.Execute("Path to composition: "),
                input.Execute("Composition author: "),
                input.Execute("Composition title: "),
            };
            if (_args.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                output.Execute("One or more arguments is null or whitespace!\n");
                return new ObjectModel() { IsNotNull = false };
            }
            return new ObjectModel()
            {
                IsNotNull = true,
                Result = _args[0],
                Compositions = new[]
                {
                    new Composition(_args[1], _args[2], _args[3])
                }
            };
        }

        public string GetName()
        {
            return Name;
        }
    }
}
