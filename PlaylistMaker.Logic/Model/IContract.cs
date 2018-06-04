using System.ServiceModel;
using System.ServiceModel.Web;

namespace PlaylistMaker.Logic.Model
{
    [ServiceContract]
    public interface IContract
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Help")]
        ObjectModel GetHelp();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Add")]
        ObjectModel AddComposition(ObjectModel model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Remove")]
        ObjectModel RemoveComposition(ObjectModel model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Search")]
        ObjectModel SearchComposition(ObjectModel model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCopositions")]
        ObjectModel GetCompositionsList(ObjectModel model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPlaylists")]
        ObjectModel GetPlaylistsList();
    }
}
