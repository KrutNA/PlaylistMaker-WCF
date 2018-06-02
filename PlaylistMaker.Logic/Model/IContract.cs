using System.ServiceModel;

namespace PlaylistMaker.Logic.Model
{
    [ServiceContract]
    public interface IContract
    {
        [OperationContract]
        ObjectModel GetHelp();

        [OperationContract]
        ObjectModel AddComposition(ObjectModel model);

        [OperationContract]
        ObjectModel RemoveComposition(ObjectModel model);

        [OperationContract]
        ObjectModel SearchComposition(ObjectModel model);

        [OperationContract]
        ObjectModel GetCompositionsList(ObjectModel model);

        [OperationContract]
        ObjectModel GetPlaylistsList();
    }
}
