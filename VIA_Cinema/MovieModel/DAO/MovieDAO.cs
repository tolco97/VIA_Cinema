namespace MovieModel.DAO
{
    using System.Collections.Generic;
    using Model.MovieModel;
    using Npgsql;

    public class MovieDAO : IMovieDAO
    {
        private static IMovieDAO movieDao = null;
        private readonly NpgsqlConnection con;

        private MovieDAO()
        {
            con = new NpgsqlConnection("Server=localhost;User Id=postgres;" +
                                       "Password=password;Database=via_cinema_system;");
            con.Open();
        }

        /// <inheritdoc/>
        public Movie Create(string movieName, int durationMinuites, string genre)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText =
                    "INSERT INTO via_cinema_schema.movies" +
                    " (name, duration_minuites," +
                    " genre) VALUES (@name, @duration_minuites, @genre);";

                // set statement parameters
                stmt.Parameters.AddWithValue(MovieEntityConstants.NAME_COLUMN, movieName);
                stmt.Parameters.AddWithValue(MovieEntityConstants.DURATION_COLUMN, durationMinuites);
                stmt.Parameters.AddWithValue(MovieEntityConstants.GENRE_COLUMN, genre);

                // execute statement
                return new Movie(movieName, durationMinuites, genre);
            }
        }

        /// <inheritdoc/>
        public Movie Read(string movieName)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText =
                    "SELECT * FROM via_cinema_schema.movies" +
                    " WHERE via_cinema_schema." +
                    "movies.name = @name;";

                // set parameters
                stmt.Parameters.AddWithValue(MovieEntityConstants.NAME_COLUMN, movieName);

                // execute statement
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // the movie does not exist
                    if (!reader.Read()) return null;

                    // collect data
                    int duration = (int) reader[MovieEntityConstants.DURATION_COLUMN];
                    string genre = reader[MovieEntityConstants.GENRE_COLUMN] as string;

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
                stmt.Connection = con;

                // set statement
                stmt.CommandText = "SELECT * FROM via_cinema_schema.movies;";

                // create a collection for the movies. LinkedList is used, because
                // it is not known how many objects are stored in the database & it is 
                // preffered to avoid array list resizing
                LinkedList<Movie> allMovies = new LinkedList<Movie>();

                // execute statement
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // loop through the reader and collect data
                    while (reader.Read())
                    {
                        string movieName = reader[MovieEntityConstants.NAME_COLUMN] as string;
                        int duration = (int) reader[MovieEntityConstants.DURATION_COLUMN];
                        string genre = reader[MovieEntityConstants.GENRE_COLUMN] as string;

                        allMovies.AddLast(new Movie(movieName, duration, genre));
                    }
                }

                return allMovies;
            }
        }

        /// <inheritdoc/>
        public int Update(Movie updatedMovie)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText = "UPDATE via_cinema_schema.movies " +
                                   "SET duration_minuites = @duration , " +
                                   "genre = @genre WHERE name = @name;";

                // set the statement parameters
                stmt.Parameters.AddWithValue(MovieEntityConstants.DURATION_COLUMN, updatedMovie.DurationMinuites);
                stmt.Parameters.AddWithValue(MovieEntityConstants.GENRE_COLUMN, updatedMovie.Genre);
                stmt.Parameters.AddWithValue(MovieEntityConstants.NAME_COLUMN, updatedMovie.Name);

                // execute statement
                return stmt.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        public int Delete(Movie movie)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText = "DELETE FROM via_cinema_schema.movies " +
                                   "WHERE movies.name = @name;";

                // set statement parameters
                stmt.Parameters.AddWithValue(MovieEntityConstants.NAME_COLUMN, movie.Name);

                // execute statement
                return stmt.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        public void CloseConnection()
        {
            con?.Close();
        }
        
        /// <summary>
        ///     Singleton implementation
        /// </summary>
        /// <returns> an instance of a movie data access object </returns>
        public static IMovieDAO GetIntance()
        {
            // Return movieDao if it is not null. Otherwise create create new MovieDAO object and
            // assign it to movieDao & return.
            return movieDao ?? (movieDao = new MovieDAO());
        }
    }
}
