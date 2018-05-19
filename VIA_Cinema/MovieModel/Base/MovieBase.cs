using MovieModel.DAO;

namespace Model_VIA_Cinema.MovieModel.Base
{
    using System.Collections.Generic;
    using Model.MovieModel;
    using VIA_Cinema.Util;

    public class MovieBase : IMovieBase
    {
        private readonly IDictionary<string, Movie> _movieCache = new Dictionary<string, Movie>();
        private readonly IMovieDAO _movieDao;

        public MovieBase(IMovieDAO movieDao)
        {
            this._movieDao = movieDao;
        }

        /// <inheritdoc/>
        public Movie AddMovie(string name, int durationMinuites, string genre)
        {
            // validate input
            Validator.ValidateMovieDuration(durationMinuites);
            Validator.ValidateTextualInput(name, genre);

            // create new movie in the database
            Movie newMovie = _movieDao.Create(name, durationMinuites, genre);

            // cache the new movie
            _movieCache[newMovie.Name] = newMovie;

            return newMovie;
        }

        /// <inheritdoc/>
        public Movie GetMovie(string movieName)
        {
            // validate input
            Validator.ValidateTextualInput(movieName);

            // read movie from the database and cache
            if (!_movieCache.ContainsKey(movieName))
            {
                Movie movie = _movieDao.Read(movieName);

                // movie does not exist
                if (movie == null) return null;

                _movieCache[movie.Name] = movie;
            }

            return _movieCache[movieName];
        }

        /// <inheritdoc/>
        public IList<Movie> GetAllMovies()
        {
            // read all movies from the database
            ICollection<Movie> allMovies = _movieDao.ReadAll();
            
            // cache all movies that have not been read already 
            foreach (Movie movie in allMovies)
                if (!_movieCache.ContainsKey(movie.Name))
                    _movieCache[movie.Name] = movie;

            return new List<Movie>(_movieCache.Values);
        }
    }
}
