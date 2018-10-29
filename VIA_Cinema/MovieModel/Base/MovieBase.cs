using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using DNP1.ViaCinema.Model.MovieModel.DAO;
using DNP1.ViaCinema.Model.Util;

namespace DNP1.ViaCinema.Model.MovieModel.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class MovieBase : IMovieBase
    {
        private readonly IDictionary<string, Movie> _movieCache = new Dictionary<string, Movie>();
        private readonly IMovieDao _movieDao;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movieDao"></param>
        public MovieBase(IMovieDao movieDao)
        {
            _movieDao = movieDao;
        }

        /// <inheritdoc cref="IMovieBase.AddMovie(string, int, string)"/>
        public Movie AddMovie(string movieName, int durationMinutes, string genre)
        {
            Validator.ValidateMovieDuration(durationMinutes);
            Validator.ValidateTextualInput(movieName, genre);

            
            if (MovieExists(movieName))
            {
                throw new DuplicateKeyException(movieName, $"Movie with name {movieName} already exists!");
            }

            Movie newMovie = _movieDao.Create(movieName, durationMinutes, genre);

            _movieCache[newMovie.Name] = newMovie;

            return newMovie;
        }

        /// <inheritdoc cref="IMovieBase.GetMovie(string)"/>
        public Movie GetMovie(string movieName)
        {
            Validator.ValidateTextualInput(movieName);

            if (!_movieCache.ContainsKey(movieName))
            {
                Movie movie = _movieDao.Read(movieName);

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
            ICollection<Movie> allMovies = _movieDao.ReadAll();

            foreach (Movie movie in allMovies)
            { 
                if (!_movieCache.ContainsKey(movie.Name))
                { 
                    _movieCache[movie.Name] = movie;
                }
            }

            return _movieCache.Values.ToList();
        }

        /// <inheritdoc cref="IMovieBase.MovieExists(string)"/>
        public bool MovieExists(string movieName)
        {
            bool exists = GetMovie(movieName) != null;

            return exists;
        }
    }
}