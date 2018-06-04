using PlaylistMaker.Logic.Model;

namespace PlaylistMaker.Logic.Commands
{
    // ReSharper disable once InconsistentNaming
    public class PMHelpDisplayer : ICommand
    {
        private const string Name = "help";

        public ObjectModel Execute(ObjectModel model)
        {
            var commands = new[]
            {
                "help","   - display this message\n",
                "ls p","    - display all playlists names\n",
                "ls c","    - display all compositions in the current playlist\n",
                "add","    - add composition to the current playlist\n",
                "search"," - search composition in the current playlist\n",
                "rm","     - remove composition from the current playlist\n",
                "exit","   - close this program\n"
            };

            return new ObjectModel()
            {
                Result = "Done",
                Results = commands
            };
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
