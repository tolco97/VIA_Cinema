using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using VIA_Cinema.MovieModel;
using VIA_Cinema.MovieModel.Base;
using VIA_Cinema.MovieModel.DAO;
using VIA_Cinema.ProjectionModel;
using VIA_Cinema.ProjectionModel.Base;
using VIA_Cinema.ProjectionModel.DAO;
using VIA_Cinema.UserAccountModel;
using VIA_Cinema.UserAccountModel.Base;
using VIA_Cinema.UserAccountModel.DAO;

namespace Services_VIA_Cinema.ProjectionServices
{
    /// <summary>
    /// Summary description for ViaCinemaProjectionService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ViaCinemaProjectionService : WebService
    {
        private readonly IProjectionBase _projectionBase = new ProjectionBase(ProjectionDao.GetInstance());
        private readonly IMovieBase _movieBase = new MovieBase(MovieDao.GetInstance());
        private readonly IUserAccountBase _userAccountBase = new UserAccountBase(UserAccountDAO.GetInstance());

        [WebMethod]
        public List<Projection> GetProjections(string movieName)
        {
            Movie movie = _movieBase.GetMovie(movieName);

            // get all projections of the movie
            List<Projection> projections = _projectionBase.GetAllProjections(movie);

            return projections;
        }

        [WebMethod]
        public List<Projection> GetAllProjections()
        {
            // get all projections
            List<Projection> allProjections = _projectionBase.GetAllProjections();

            return allProjections;
        }

        [WebMethod]
        public bool BookSeat(int projectionId, string email, string seatNumbers)
        {
            // get projection
            Projection projection = _projectionBase.GetProjection(projectionId);

            // get user account
            UserAccount userAccount = _userAccountBase.GetUserAccount(email);

            // parse string into int array
            int[] seatNumberArray = Array.ConvertAll(seatNumbers.Split(new[] { ", " }, StringSplitOptions.None), int.Parse);

            // book seats
            bool isBookingSuccessful = _projectionBase.BookSeats(projection, userAccount, seatNumberArray);

            return isBookingSuccessful;
        }

    }
}
