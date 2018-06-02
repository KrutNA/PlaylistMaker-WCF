using System.Collections.Generic;
using System.IO;
using System.Linq;
using PlaylistMaker.Logic.Model;

namespace PlaylistMaker.Logic.Commands
{
    public class PlaylistsListDisplayer : ICommand
    {
        private const string Name = "ls";
        private const int ArgsCount = 0;
        private List<string> _fileNames;

        public ObjectModel Execute(ObjectModel model)
        {
            _fileNames = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.pls").ToList();
            _fileNames = _fileNames.Select(Path.GetFileName).ToList();
            if (_fileNames.Count == 0)
                return new ObjectModel()
                {
                    Result = "Not found"
                };
            return new ObjectModel()
            {
                Result = "Done",
                Results = _fileNames.ToArray()
            };
        }

        public ObjectModel ReadArgs()
        {
            return new ObjectModel() {IsNotNull = false};
        }

        public string GetName()
        {
            return Name;
        }
    }
}
