using PlaylistMaker.Logic.Model;

namespace PlaylistMaker.Logic.Commands
{
    public class PMHelpDisplayer : ICommand
    {
        private const string Name = "help";
        private const int ArgsCount = 0;

        public ObjectModel Execute(ObjectModel model)
        {
            var commands = new[]
            {
                "help - to display this message",
                "ls - to display all playlists names",
                "list - to display all compositions in the current playlist",
                "add - to add composition to the current playlist",
                "search - to search composition in the current playlist",
                "rm - to remove composition from the current playlist",
                "exit - to close this program"
            };

            return new ObjectModel()
            {
                Result = "Done",
                Results = commands
            };
        }

        public ObjectModel ReadArgs()
        {
            return new ObjectModel() { IsNotNull = false };
        }

        public string GetName()
        {
            return Name;
        }
    }
}
