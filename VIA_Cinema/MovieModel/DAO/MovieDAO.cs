using System;
using System.Collections.Generic;
using Npgsql;

namespace DNP1.ViaCinema.Model.MovieModel.DAO
{
    public class MovieDao : IMovieDao
    {
        private static IMovieDao _instance;
        private readonly NpgsqlConnection _con;

        private MovieDao()
        {
            _con = new NpgsqlConnection(ConnectionStringHelper.GetConnectionString());
            _con.Open();
        }

        /// <inheritdoc cref="IMovieDao.Create(string, int, string)"/>
        public Movie Create(string movieName, int durationMinutes, string genre)
        {
            using (var stmt = new NpgsqlCommand())
            {
                stmt.Connection = _con;

                stmt.CommandText =
                    "INSERT INTO via_cinema_schema.movies" +
                    " (name, duration_minuites," +
                    " genre) VALUES (@name, @duration_minuites, @genre);";

                stmt.Parameters.AddWithValue(MovieEntityConstants.NameColumn, movieName);
                stmt.Parameters.AddWithValue(MovieEntityConstants.DurationColumn, durationMinutes);
                stmt.Parameters.AddWithValue(MovieEntityConstants.GenreColumn, genre);

                stmt.ExecuteNonQuery();

                return new Movie(movieName, durationMinutes, genre);
            }
        }

        /// <inheritdoc cref="IMovieDao.Read(string)"/>
        public Movie Read(string movieName)
        {
            using (var stmt = new NpgsqlCommand())
            {
                stmt.Connection = _con;

                stmt.CommandText =
                    "SELECT * FROM via_cinema_schema.movies" +
                    " WHERE via_cinema_schema." +
                    "movies.name = @name;";

                stmt.Parameters.AddWithValue(MovieEntityConstants.NameColumn, movieName);

                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    var duration = (int) reader[MovieEntityConstants.DurationColumn];
                    var genre = (string) reader[MovieEntityConstants.GenreColumn];

                    return new Movie(movieName, duration, genre);
                }
            }
        }

        /// <inheritdoc cref="IMovieDao.ReadAll"/>
        public ICollection<Movie> ReadAll()
        {
            using (var stmt = new NpgsqlCommand())
            {
                stmt.Connection = _con;

                stmt.CommandText = "SELECT * FROM via_cinema_schema.movies;";

                var allMovies = new List<Movie>();

                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var movieName = (string) reader[MovieEntityConstants.NameColumn];
                        var duration = (int) reader[MovieEntityConstants.DurationColumn];
                        var genre = (string) reader[MovieEntityConstants.GenreColumn];

                        allMovies.Add(new Movie(movieName, duration, genre));
                    }
                }

                return allMovies;
            }
        }

        /// <inheritdoc cref="IMovieDao.Update(Movie)"/>
        public bool Update(Movie updatedMovie)
        {
            using (var stmt = new NpgsqlCommand())
            {
                stmt.Connection = _con;

                stmt.CommandText = "UPDATE via_cinema_schema.movies " +
                                   "SET duration_minuites = @duration_minuites , " +
                                   "genre = @genre WHERE name = @name;";

                stmt.Parameters.AddWithValue(MovieEntityConstants.DurationColumn, updatedMovie.DurationMinutes);
                stmt.Parameters.AddWithValue(MovieEntityConstants.GenreColumn, updatedMovie.Genre);
                stmt.Parameters.AddWithValue(MovieEntityConstants.NameColumn, updatedMovie.Name);

                return stmt.ExecuteNonQuery() != 0;
            }
        }

        /// <inheritdoc cref="IMovieDao.Delete(Movie)"/>
        public bool Delete(Movie movie)
        {
            using (var stmt = new NpgsqlCommand())
            {
                stmt.Connection = _con;

                stmt.CommandText = "DELETE FROM via_cinema_schema.movies " +
                                   "WHERE movies.name = @name;";

                stmt.Parameters.AddWithValue(MovieEntityConstants.NameColumn, movie.Name);

                return stmt.ExecuteNonQuery() != 0;
            }
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            _con?.Dispose();
        }

        /// <summary>
        ///     Singleton implementation
        /// </summary>
        /// <returns> an instance of a movie data access object </returns>
        public static IMovieDao GetInstance()
        {
            return _instance ?? (_instance = new MovieDao());
        }
    }
}