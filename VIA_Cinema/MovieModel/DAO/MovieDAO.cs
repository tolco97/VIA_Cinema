namespace MovieModel.DAO
{
    using System.Collections.Generic;
    using Model.MovieModel;
    using Npgsql;

    public class MovieDAO : IMovieDAO
    {
        private static IMovieDAO _instance;
        private readonly NpgsqlConnection _con;

        private MovieDAO()
        {
            _con = new NpgsqlConnection("Server=localhost;User Id=postgres;" +
                                       "Password=password;Database=via_cinema_system;");
            _con.Open();
        }

        /// <inheritdoc/>
        public Movie Create(string movieName, int durationMinuites, string genre)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
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
                stmt.Parameters.AddWithValue(MovieEntityConstants.DurationColumn, durationMinuites);
                stmt.Parameters.AddWithValue(MovieEntityConstants.GenreColumn, genre);

                // execute statement
                stmt.ExecuteNonQuery();

                return new Movie(movieName, durationMinuites, genre);
            }
        }

        /// <inheritdoc/>
        public Movie Read(string movieName)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
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

        /// <inheritdoc/>
        public ICollection<Movie> ReadAll()
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set the connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "SELECT * FROM via_cinema_schema.movies;";

                // create output collection
                List<Movie> allMovies = new List<Movie>();

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

        /// <inheritdoc/>
        public bool Update(Movie updatedMovie)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "UPDATE via_cinema_schema.movies " +
                                   "SET duration_minuites = @duration_minuites , " +
                                   "genre = @genre WHERE name = @name;";

                // set the statement parameters
                stmt.Parameters.AddWithValue(MovieEntityConstants.DurationColumn, updatedMovie.DurationMinuites);
                stmt.Parameters.AddWithValue(MovieEntityConstants.GenreColumn, updatedMovie.Genre);
                stmt.Parameters.AddWithValue(MovieEntityConstants.NameColumn, updatedMovie.Name);

                // execute statement
                return stmt.ExecuteNonQuery() != 0;
            }
        }

        /// <inheritdoc/>
        public bool Delete(Movie movie)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
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
        
        /// < inheritdoc />
        public void Dispose()
        {
            _con?.Dispose();
        }

        /// <summary>
        ///     Singleton implementation
        /// </summary>
        /// <returns> an instance of a movie data access object </returns>
        public static IMovieDAO GetInstance()
        {
            // Return movieDao if it is not null. Otherwise create create new MovieDAO object and
            // assign it to movieDao & return.
            return _instance ?? (_instance = new MovieDAO());
        }
    }
}
