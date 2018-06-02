using System.Linq;
using PlaylistMaker.Logic.Model;
using PlaylistMaker.Logic.Stream;

namespace PlaylistMaker.Logic.Commands
{
    public class CompositionAdder : ICommand
    {
        private const string Name = "add";
        private const int ArgsCount = 4;

        public ObjectModel Execute(ObjectModel model)
        {
            var playlist = new Playlist(model.Result.EndsWith(".pls") ? model.Result : $"{model.Result}.pls");
            playlist.Compositions.Add(model.Compositions.First());
            playlist.Save();
            return new ObjectModel() {IsNotNull = true, Result = "Done"};
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
                return new ObjectModel() {IsNotNull = false};
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
