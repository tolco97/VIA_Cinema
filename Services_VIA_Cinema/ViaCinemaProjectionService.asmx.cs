using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using DNP1.ViaCinema.Model.MovieModel;
using DNP1.ViaCinema.Model.MovieModel.Base;
using DNP1.ViaCinema.Model.MovieModel.DAO;
using DNP1.ViaCinema.Model.ProjectionModel;
using DNP1.ViaCinema.Model.ProjectionModel.Base;
using DNP1.ViaCinema.Model.ProjectionModel.DAO;
using DNP1.ViaCinema.Model.UserAccountModel;
using DNP1.ViaCinema.Model.UserAccountModel.Base;
using DNP1.ViaCinema.Model.UserAccountModel.DAO;

namespace DNP1.ViaCinema.Services
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
        private readonly IUserAccountBase _userAccountBase = new UserAccountBase(UserAccountDao.GetInstance());

        /// <remarks/>
        [WebMethod]
        public List<Projection> GetProjections(string movieName)
        {
            Movie movie = _movieBase.GetMovie(movieName);

            // get all projections of the movie
            List<Projection> projections = _projectionBase.GetAllProjections(movie);

            return projections;
        }

        /// <inheritdoc cref="IProjectionBase.GetAllProjections()"/>
        [WebMethod]
        public List<Projection> GetAllProjections()
        {
            // get all projections
            List<Projection> allProjections = _projectionBase.GetAllProjections();

            return allProjections;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectionId"></param>
        /// <param name="email"></param>
        /// <param name="seatNumbers"></param>
        /// <returns></returns>
        [WebMethod]
        public bool BookSeat(int projectionId, string email, string seatNumbers)
        {
            // get projection
            Projection projection = _projectionBase.GetProjection(projectionId);

            // get user account
            UserAccount userAccount = _userAccountBase.GetUserAccount(email);

            // parse string into int array
            int[] seatNumberArray = Array.ConvertAll(seatNumbers.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries), int.Parse);

            // book seats
            bool isBookingSuccessful = _projectionBase.BookSeats(projection, userAccount, seatNumberArray);

            return isBookingSuccessful;
        }

    }
}
