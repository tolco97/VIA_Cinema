namespace Model_VIA_Cinema.MovieModel.Base
{
    using System.Collections.Generic;
    using Model.MovieModel;

    public interface IMovieBase
    {
        /// <summary>
        ///     Adds a new movie to the system
        /// </summary>
        /// <param name="name"> the name of the movie </param>
        /// <param name="durationMinuites"> the duration of the movie </param>
        /// <param name="genre"> the movie genre </param>
        /// <returns> a movie object </returns>
        Movie AddMovie(string name, int durationMinuites, string genre);

        /// <summary>
        ///     Retrieves a movie from the system, that matches the movie name passed as a parameter
        /// </summary>
        /// <param name="movieName"> the name of the movie </param>
        /// <returns> a movie object </returns>
        Movie GetMovie(string movieName);

        /// <summary>
        ///     Retrieves all movies from the system
        /// </summary>
        /// <returns> a list of movies </returns>
        IList<Movie> GetAllMovies();
    }
}
