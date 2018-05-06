using MovieModel.DAO;

namespace Model_VIA_Cinema.MovieModel.Base
{
    using System.Collections.Generic;
    using Model.MovieModel;
    using System;

    public class MovieBase : IMovieBase
    {
        private readonly Dictionary<string, Movie> movieCache = new Dictionary<string, Movie>();

        private readonly IMovieDAO movieDao;

        public MovieBase(IMovieDAO movieDao)
        {
            this.movieDao = movieDao;
        }

        /// <inheritdoc/>
        public Movie AddMovie(string name, int durationMinuites, string genre)
        {
            // create new movie in the database
            Movie newMovie = movieDao.Create(name, durationMinuites, genre);

            // cache the new movie
            movieCache[newMovie.Name] = newMovie;

            return newMovie;
        }

        /// <inheritdoc/>
        public Movie GetMovie(string movieName)
        {
            // read movie from the database and cache
            if (!movieCache.ContainsKey(movieName))
            {
                Movie movie = movieDao.Read(movieName);

                // movie does not exist
                if (movie == null)
                    throw new ArgumentException($"Movie with name {movieName} does not exist!");

                movieCache[movie.Name] = movie;
            }

            return movieCache[movieName];
        }

        /// <inheritdoc/>
        public List<Movie> GetAllMovies()
        {
            // read all movies from the database
            ICollection<Movie> allMovies = movieDao.ReadAll();

            // create a collection for the output
            int size = allMovies.Count;
            List<Movie> movieList = new List<Movie>(size); // avoid list resizing by specifying size

            // go through all movies from the database
            foreach (Movie movie in allMovies)
            {
                // cache all movies that have not been cached so far
                if (!movieCache.ContainsKey(movie.Name))
                    movieCache[movie.Name] = movie;

                // add to output list
                movieList.Add(movieCache[movie.Name]);
            }

            return movieList;
        }
    }
}
