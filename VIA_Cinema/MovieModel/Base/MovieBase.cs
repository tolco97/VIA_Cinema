using MovieModel.DAO;

namespace Model_VIA_Cinema.MovieModel.Base
{
    using System.Collections.Generic;
    using Model.MovieModel;
    
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
                if (movie == null) return null;

                movieCache[movie.Name] = movie;
            }

            return movieCache[movieName];
        }

        /// <inheritdoc/>
        public List<Movie> GetAllMovies()
        {
            // read all movies from the database
            ICollection<Movie> allMovies = movieDao.ReadAll();
            
            // cache all movies that have not been read already 
            foreach (Movie movie in allMovies)
                if (!movieCache.ContainsKey(movie.Name))
                    movieCache[movie.Name] = movie;

            return new List<Movie>(movieCache.Values);
        }
    }
}
