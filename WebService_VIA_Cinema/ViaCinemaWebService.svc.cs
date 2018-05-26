namespace WebService_VIA_Cinema
{
    using System;
    using System.Collections.Generic;
    using Model.MovieModel;
    using Model_VIA_Cinema.MovieModel.Base;
    using MovieModel.DAO;
    using ProjectionModel;
    using ProjectionModel.Base;
    using ProjectionModel.DAO;
    using UserAccountModel;
    using UserAccountModel.Base;
    using UserAccountModel.DAO;
    
    public class ViaCinemaService : IViaCinemaService
    {
        private readonly IMovieBase _movieBase = new MovieBase(MovieDAO.GetInstance());
        private readonly IProjectionBase _projectionBase = new ProjectionBase(ProjectionDAO.GetInstance());
        private readonly IUserAccountBase _userAccountBase = new UserAccountBase(UserAccountDAO.GetInstance());

        public IList<Movie> GetAllMovies()
        {
            // get all movies
            IList<Movie> allMovies = _movieBase.GetAllMovies();

            return allMovies;
        }

        public bool Login(string email, string userPassword)
        {
            // try to login
            bool loginSuccessful = _userAccountBase.Login(email, userPassword);

            return loginSuccessful;
        }

        public IList<Projection> GetAllProjections()
        {
            // get all projections
            IList<Projection> allProjections = _projectionBase.GetAllProjections();

            return allProjections;
        }

        public UserAccount CreateAccount(string email, string userPassword, string firstName, string lastName,
            int dayOfBirth, int monthOfBirth, int yearOfBirth)
        {
            // create date time object
            DateTime dateOfBirth = new DateTime(yearOfBirth, monthOfBirth, dayOfBirth);

            // create new account
            UserAccount newUser = _userAccountBase.CreateAccount(email, userPassword, firstName, lastName, dateOfBirth);

            return newUser;
        }

        public IList<Projection> GetProjections(string movieName)
        {
            // get movie
            Movie movie = _movieBase.GetMovie(movieName);

            // get all projections of the movie
            IList<Projection> projections = _projectionBase.GetAllProjections(movie);

            return projections;
        }

        public bool BookSeat(int projectionId, string email, string seatNumbers)
        {
            // get projection
            Projection projection = _projectionBase.GetProjection(projectionId);

            // get user account
            UserAccount userAccount = _userAccountBase.GetUserAccount(email);

            // parse string into int array
            int[] seatNumberArray = Array.ConvertAll(seatNumbers.Split(new[] {", "}, StringSplitOptions.None), int.Parse);

            // book seats
            bool isBookingSuccessful = _projectionBase.BookSeats(projection, userAccount, seatNumberArray);

            return isBookingSuccessful;
        }

        public bool UserExists(string email)
        {
            bool exists = _userAccountBase.UserExists(email);

            return exists;
        }

    }

}
