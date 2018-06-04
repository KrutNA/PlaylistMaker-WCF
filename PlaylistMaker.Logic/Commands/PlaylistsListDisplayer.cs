using System.Collections.Generic;
using System.IO;
using System.Linq;
using PlaylistMaker.Logic.Model;

namespace PlaylistMaker.Logic.Commands
{
    public class PlaylistsListDisplayer : ICommand
    {
        private const string Name = "ls p";
        private List<string> _fileNames;

        public ObjectModel Execute(ObjectModel model)
        {
            _fileNames = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.pls").ToList();
            _fileNames = _fileNames.Select(Path.GetFileName).ToList();
            return _fileNames.Count == 0 ?
                new ObjectModel() { Result = "Not found" } :
                new ObjectModel() { Result = "Done", Results = _fileNames.ToArray() };
        }

        public ObjectModel ReadArgs()
        {
            return new ObjectModel();
        }

        public string GetName()
        {
            return Name;
        }
    }
}
