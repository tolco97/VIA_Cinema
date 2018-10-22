using System.Collections.Generic;
using System.Data.Linq;
using DNP1.ViaCinema.Model.MovieModel.DAO;
using DNP1.ViaCinema.Model.Util;

namespace DNP1.ViaCinema.Model.MovieModel.Base
{
    public class MovieBase : IMovieBase
    {
        private readonly IDictionary<string, Movie> _movieCache = new Dictionary<string, Movie>();
        private readonly IMovieDao _movieDao;

        public MovieBase(IMovieDao movieDao)
        {
            _movieDao = movieDao;
        }

        /// <inheritdoc cref="IMovieBase.AddMovie(string, int, string)"/>
        public Movie AddMovie(string movieName, int durationMinutes, string genre)
        {
            // validate input
            Validator.ValidateMovieDuration(durationMinutes);
            Validator.ValidateTextualInput(movieName, genre);

            // movie already exists
            if (MovieExists(movieName))
            {
                throw new DuplicateKeyException(movieName, $"Movie with name {movieName} already exists!");
            }

            // create new movie in the database
            Movie newMovie = _movieDao.Create(movieName, durationMinutes, genre);

            // cache the new movie
            _movieCache[newMovie.Name] = newMovie;

            return newMovie;
        }

        /// <inheritdoc cref="IMovieBase.GetMovie(string)"/>
        public Movie GetMovie(string movieName)
        {
            // validate input
            Validator.ValidateTextualInput(movieName);

            // read movie from the database and cache
            if (!_movieCache.ContainsKey(movieName))
            {
                Movie movie = _movieDao.Read(movieName);

                // movie does not exist
                if (movie == null)
                {
                    return null;
                }

                _movieCache[movie.Name] = movie;
            }

            return _movieCache[movieName];
        }

        /// <inheritdoc cref="IMovieBase.GetAllMovies"/>
        public List<Movie> GetAllMovies()
        {
            // read all movies from the database
            ICollection<Movie> allMovies = _movieDao.ReadAll();

            // cache all movies that have not been read already 
            foreach (Movie movie in allMovies)
            { 
                if (!_movieCache.ContainsKey(movie.Name))
                { 
                    _movieCache[movie.Name] = movie;
                }
            }
            return new List<Movie>(_movieCache.Values);
        }

        /// <inheritdoc cref="IMovieBase.MovieExists(string)"/>
        public bool MovieExists(string movieName)
        {
            // movie is null if it doesn't exist
            bool exists = GetMovie(movieName) != null;

            return exists;
        }
    }
}