using System.Collections.Generic;

namespace DNP1.ViaCinema.Model.MovieModel.Base
{
    public interface IMovieBase
    {
        /// <summary>
        ///     Adds a new movie to the system
        /// </summary>
        /// <param name="movieName"> the name of the movie </param>
        /// <param name="durationMinutes"> the duration of the movie </param>
        /// <param name="genre"> the movie genre </param>
        /// <returns> a movie object </returns>
        Movie AddMovie(string movieName, int durationMinutes, string genre);

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
        List<Movie> GetAllMovies();

        /// <summary>
        ///     Checks if a movie exists in system.
        /// </summary>
        /// <param name="movieName"> the name of the movie </param>
        /// <returns> true, if the movie already exists. Otherwise, false </returns>
        bool MovieExists(string movieName);
    }
}