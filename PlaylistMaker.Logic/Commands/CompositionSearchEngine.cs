using System.IO;
using System.Linq;
using PlaylistMaker.Logic.Model;
using PlaylistMaker.Logic.Stream;

namespace PlaylistMaker.Logic.Commands
{
    public class CompositionSearchEngine : ICommand
    {
        private const string Name = "search";
        private const int ArgsCount = 4;

        public ObjectModel Execute(ObjectModel model)
        {
            if (!File.Exists(model.Result) && !File.Exists($"{model.Result}.pls"))
                return new ObjectModel() { Result = "Playlist not found" };
            var composotion = model.Compositions.First();
            var newModel = new ObjectModel();
            composotion = new Composition(
                string.IsNullOrWhiteSpace(composotion.Path) ? "" : composotion.Path,
                string.IsNullOrWhiteSpace(composotion.Author) ? "" : composotion.Author,
                string.IsNullOrWhiteSpace(composotion.Title) ? "" : composotion.Title);
            var playlist = new Playlist(model.Result.EndsWith(".pls") ? model.Result : $"{model.Result}.pls");
            var compositions = playlist.Compositions.FindAll(x =>
                x.Path.Contains(composotion.Path) &&
                x.Author.Contains(composotion.Author) &&
                x.Title.Contains(composotion.Title));
            if (compositions.Any())
            {
                newModel.Compositions = compositions.ToArray();
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
            if (string.IsNullOrWhiteSpace(_args[0]) ||
                (string.IsNullOrWhiteSpace(_args[1]) &&
                 string.IsNullOrWhiteSpace(_args[2]) &&
                 string.IsNullOrWhiteSpace(_args[3])))
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
