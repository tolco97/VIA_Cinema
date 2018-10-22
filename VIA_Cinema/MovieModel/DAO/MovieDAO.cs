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
            _con = new NpgsqlConnection("Server=localhost;User Id=postgres;" +
                                        "Password=password;Database=via_cinema_system;");
            _con.Open();
        }

        /// <inheritdoc cref="IMovieDao.Create(string, int, string)"/>
        public Movie Create(string movieName, int durationMinutes, string genre)
        {
            using (var stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText =
                    "INSERT INTO via_cinema_schema.movies" +
                    " (name, duration_minuites," +
                    " genre) VALUES (@name, @duration_minuites, @genre);";

                // set statement parameters
                stmt.Parameters.AddWithValue(MovieEntityConstants.NameColumn, movieName);
                stmt.Parameters.AddWithValue(MovieEntityConstants.DurationColumn, durationMinutes);
                stmt.Parameters.AddWithValue(MovieEntityConstants.GenreColumn, genre);

                // execute statement
                stmt.ExecuteNonQuery();

                return new Movie(movieName, durationMinutes, genre);
            }
        }

        /// <inheritdoc cref="IMovieDao.Read(string)"/>
        public Movie Read(string movieName)
        {
            using (var stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText =
                    "SELECT * FROM via_cinema_schema.movies" +
                    " WHERE via_cinema_schema." +
                    "movies.name = @name;";

                // set parameters
                stmt.Parameters.AddWithValue(MovieEntityConstants.NameColumn, movieName);

                // execute statement
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // the movie does not exist
                    if (!reader.Read()) return null;

                    // collect data
                    int duration = (int) reader[MovieEntityConstants.DurationColumn];
                    string genre = (string) reader[MovieEntityConstants.GenreColumn];

                    return new Movie(movieName, duration, genre);
                }
            }
        }

        /// <inheritdoc cref="IMovieDao.ReadAll"/>
        public ICollection<Movie> ReadAll()
        {
            using (var stmt = new NpgsqlCommand())
            {
                // set the connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "SELECT * FROM via_cinema_schema.movies;";

                // create output collection
                var allMovies = new List<Movie>();

                // execute statement
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // loop through the reader and collect data
                    while (reader.Read())
                    {
                        string movieName = (string) reader[MovieEntityConstants.NameColumn];
                        int duration = (int) reader[MovieEntityConstants.DurationColumn];
                        string genre = (string) reader[MovieEntityConstants.GenreColumn];

                        allMovies.Add(new Movie(movieName, duration, genre));
                    }
                }

                return allMovies;
            }
        }

        /// <inheritdoc cref="IMovieDao.Update(Movie)"/>
        public bool Update(Movie updtMovie)
        {
            using (var stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "UPDATE via_cinema_schema.movies " +
                                   "SET duration_minuites = @duration_minuites , " +
                                   "genre = @genre WHERE name = @name;";

                // set the statement parameters
                stmt.Parameters.AddWithValue(MovieEntityConstants.DurationColumn, updtMovie.DurationMinutes);
                stmt.Parameters.AddWithValue(MovieEntityConstants.GenreColumn, updtMovie.Genre);
                stmt.Parameters.AddWithValue(MovieEntityConstants.NameColumn, updtMovie.Name);

                // execute statement
                return stmt.ExecuteNonQuery() != 0;
            }
        }

        /// <inheritdoc cref="IMovieDao.Delete(Movie)"/>
        public bool Delete(Movie movie)
        {
            using (var stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "DELETE FROM via_cinema_schema.movies " +
                                   "WHERE movies.name = @name;";

                // set statement parameters
                stmt.Parameters.AddWithValue(MovieEntityConstants.NameColumn, movie.Name);

                // execute statement
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
            // Return movieDao if it is not null. Otherwise create create new MovieDAO object and
            // assign it to movieDao & return.
            return _instance ?? (_instance = new MovieDao());
        }
    }
}