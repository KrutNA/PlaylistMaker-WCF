using System;
using PlaylistMaker.Logic.Model;

namespace PlaylistMaker.Logic.Commands
{
    public class ClientCloser : ICommand
    {
        private const string Name = "exit";
        private const int ArgsCount = 0;

        public ObjectModel Execute(ObjectModel model)
        {
            Environment.Exit(0);
            return new ObjectModel();
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
