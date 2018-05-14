namespace MovieModel.DAO
{
    using System.Collections.Generic;
    using Model.MovieModel;

    public interface IMovieDAO
    {
        /// <summary>
        ///     Creates a new movie entry in the movies entity
        /// </summary>
        /// <param name="movieName"> the name of hte movie </param>
        /// <param name="durationMinuites"> the duration of the movie </param>
        /// <param name="genre"> the genre of the movie </param>
        /// <returns> a movie object </returns>
        Movie Create(string movieName, int durationMinuites, string genre);

        /// <summary>
        ///     Reads a movie from the movies entity, that has the movie name passed as a parameter
        /// </summary>
        /// <param name="movieName"> the movie name </param>
        /// <returns> a movie object </returns>
        Movie Read(string movieName);

        /// <summary>
        ///     Reads all movie entries from the movie entity and returns a collection of them
        /// </summary>
        /// <returns> a collection of all movie objects</returns>
        ICollection<Movie> ReadAll();

        /// <summary>
        ///     Updates a movie entry in the database, with the data from the movie passed as a parameter
        /// </summary>
        /// <param name="updatedMovie"> the updated movie object </param>
        /// <returns> true, if the update operation has affected at least 1 database row. Otherwise, false </returns>
        bool Update(Movie updatedMovie);

        /// <summary>
        ///     Deletes a movie entry from the database that matches the properties of the movie object passed as a parameter
        /// </summary>
        /// <param name="movie"> the movie to be deleted </param>
        /// <returns> true, if the delete operation has affected at least 1 database row. Otherwise, false </returns>
        bool Delete(Movie movie);

        /// <summary>
        ///     Closes the connection to the database
        /// </summary>
        void CloseConnection();
    }
}
