namespace PlaylistMaker.Logic.Model
{
    public interface ICommand
    {
        string GetName();

        ObjectModel Execute(ObjectModel model);

        ObjectModel ReadArgs();
    }
}
