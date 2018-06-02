using System.IO;
using System.Linq;
using PlaylistMaker.Logic.Model;
using PlaylistMaker.Logic.Stream;

namespace PlaylistMaker.Logic.Commands
{
    public class CompositionsListDisplayer : ICommand
    {
        private const string Name = "list";
        private const int ArgsCount = 1;

        public ObjectModel Execute(ObjectModel model)
        {
            if (!File.Exists(model.Result) && !File.Exists($"{model.Result}.pls"))
                return new ObjectModel() {Result = "Playlist not found"};
            var playlist = new Playlist(model.Result.EndsWith(".pls") ? model.Result : $"{model.Result}.pls");
            if (playlist.Compositions.Any())
                return new ObjectModel()
                {
                    Result = "Done",
                    Compositions = playlist.Compositions.ToArray()
                };
            else
                return new ObjectModel()
                {
                    Result = "Not found"
                };
        }

        public ObjectModel ReadArgs()
        {
            var input = new Input();
            var output = new Output();
            var _args = new[]
            {
                input.Execute("Playlist name: ")
            };

            if (string.IsNullOrWhiteSpace(_args[0]))
            {
                output.Execute("One or more arguments is null or whitespace!\n");
                return new ObjectModel() {IsNotNull = false};
            }

            return new ObjectModel()
            {
                IsNotNull = true,
                Result = _args[0]
            };
        }

        public string GetName()
        {
            return Name;
        }
    }
}
