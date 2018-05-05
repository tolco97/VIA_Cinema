namespace WebService_VIA_Cinema
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using Model.MovieModel;
    using ProjectionModel;
    using UserAccountModel;

    [ServiceContract]
    public interface IViaCinemaService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetAllMovies", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Movie> GetAllMovies();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Login?email={email}&userPassword={userPassword}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool Login(string email, string userPassword);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetAllProjections", RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Projection> GetAllProjections();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/CreateAccount?email={email}&userPassword={userPassword}&" +
                                                  "firstName={firstName}&lastName={lastName}&dayOfBirth={dayOfBirth}" +
                                                  "&monthOfBirth={monthOfBirth}&yearOfBirth={yearOfBirth}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        UserAccount CreateAccount(string email, string userPassword, string firstName, string lastName,
            int dayOfBirth, int monthOfBirth, int yearOfBirth);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetProjections?movieName={movieName}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Projection> GetProjections(string movieName);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/BookSeat?projectionId={projectionId}&email={email}&seatNumbers={seatNumbers}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool BookSeat(int projectionId, string email, string seatNumbers);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "/UserExists?email={email}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool UserExists(string email);
    }
}
