namespace ProjectionModel.DAO
{
    using System;
    using System.Collections.Generic;
    using Model.MovieModel;
    using MovieModel.DAO;
    using Npgsql;
    using UserAccountModel;
    using UserAccountModel.DAO;

    public class ProjectionDAO : IProjectionDAO
    {
        private static IProjectionDAO instance = null;

        private readonly NpgsqlConnection con;

        // I couldn't use dependency injections, because all DAO objects are Singletons
        private readonly IMovieDAO movieDao = MovieDAO.GetIntance();
        private readonly IUserAccountDAO userAccountDao = UserAccountDAO.GetInstance();

        private ProjectionDAO()
        {
            con = new NpgsqlConnection("Server=localhost;User Id=postgres;" +
                                       "Password=password;Database=via_cinema_system;");
            con.Open();
        }

        /// <inheritdoc/>
        public Projection CreateProjection(Movie projectedMovie, DateTime movieStartTime)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText =
                    "INSERT INTO via_cinema_schema.projections " +
                    "(movie_name, projection_start)" +
                    " VALUES (@movie_name, @projection_start) RETURNING id;";

                // set statement parameters
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.MOVIE_NAME_COLUMN,
                    projectedMovie.Name);
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.PROJECTION_START_COLUMN,
                    movieStartTime);

                // execute statement and return generated ID
                int projectionId = (int) stmt.ExecuteScalar();

                // create new projection object
                return new Projection(projectionId, projectedMovie, new List<Seat>(), movieStartTime);
            }
        }

        /// <inheritdoc/>
        public Projection Read(int projId)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText = "SELECT * FROM via_cinema_schema.projections" +
                                   " WHERE projections.id = @id;";

                // set statement parameters
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.ID_COLUMN, projId);

                // get seats for this projection
                List<Seat> seatAllocations = ReadSeatReservations(projId);

                // execute statement and collect values
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // proj does not exist
                    if (!reader.Read()) return null;

                    // get the movie object
                    string movieName = reader[ProjectionEntityConstants.MOVIE_NAME_COLUMN] as string;
                    Movie movie = movieDao.Read(movieName);

                    // get proj start
                    DateTime projectionStart = (DateTime) reader[ProjectionEntityConstants.PROJECTION_START_COLUMN];

                    return new Projection(projId, movie, seatAllocations, projectionStart);
                }
            }
        }

        /// <inheritdoc/>
        public ICollection<Projection> ReadAll()
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // output collection
                LinkedList<Projection> allProjections = new LinkedList<Projection>();

                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText = "SELECT * FROM via_cinema_schema.projections;";

                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // collect values
                    while (reader.Read())
                    {
                        // get projection projectionId
                        int projectionId = (int) reader[ProjectionEntityConstants.ID_COLUMN];

                        // get the movie name & movie object
                        string movieName = reader[ProjectionEntityConstants.MOVIE_NAME_COLUMN] as string;
                        Movie movie = movieDao.Read(movieName);

                        // get projection start
                        DateTime projectionStart = (DateTime) reader[ProjectionEntityConstants.PROJECTION_START_COLUMN];

                        Projection projection = new Projection
                        {
                            Id = projectionId,
                            ProjectedMovie = movie,
                            MovieStartTime = projectionStart
                        };
                        allProjections.AddLast(projection);
                    }
                }

                // read all projection seat resevations
                foreach (Projection proj in allProjections)
                    proj.Seats = ReadSeatReservations(proj.Id);

                return allProjections;
            }
        }

        /// <inheritdoc/>
        public ICollection<Projection> Read(Movie movie)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // output collection
                LinkedList<Projection> allProjections = new LinkedList<Projection>();

                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText = "SELECT * FROM via_cinema_schema.projections" +
                                   " WHERE projections.movie_name = @movie_name;";

                // set parameters
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.MOVIE_NAME_COLUMN, movie.Name);

                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // collect values
                    while (reader.Read())
                    {
                        // get projection projectionId
                        int projectionId = (int) reader[ProjectionEntityConstants.ID_COLUMN];

                        // get projection start
                        DateTime projectionStart = (DateTime) reader[ProjectionEntityConstants.PROJECTION_START_COLUMN];
                        
                        Projection projection = new Projection
                        {
                            Id = projectionId,
                            ProjectedMovie = movie,
                            MovieStartTime = projectionStart
                        };

                        allProjections.AddLast(projection);
                    }
                }

                // read all projection seat reservations
                foreach (Projection proj in allProjections)
                    proj.Seats = ReadSeatReservations(proj.Id);

                return allProjections;
            }
        }

        /// <inheritdoc/>
        public int UpdateProjection(Projection updatedProj)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText = "UPDATE via_cinema_schema.projections " +
                                   "SET projections.movie_name = @movie_name," +
                                   " projections.projection_start = @projection_start " +
                                   "WHERE projections.id = @id;";

                // set parameters for proj update
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.MOVIE_NAME_COLUMN,
                    updatedProj.ProjectedMovie.Name);
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.PROJECTION_START_COLUMN,
                    updatedProj.MovieStartTime);
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.ID_COLUMN, updatedProj.Id);

                // execute statement
                return stmt.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        public int DeleteSeatReservation(int projectionId, UserAccount user, int seatNumber)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText = "DELETE FROM via_cinema_schema.seat_reservations " +
                                   "WHERE seat_reservations.projection_id = @projection_id" +
                                   " AND seat_reservations.email = @email" +
                                   " AND seat_reservations.seat_number = @seat_number;";

                // set parameters for proj update
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.PROJECTION_ID_COLUMN, projectionId);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EMAIL_COLUMN, user.Email);
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.SEAT_NUMBER_COLUMN, seatNumber);

                // execute statement
                return stmt.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        public void CloseConnection()
        {
            con?.Close();
        }

        /// <inheritdoc/>
        public int CreateSeatReservations(Projection proj)
        {
            int rowsAffected = 0;

            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText =
                    "INSERT INTO via_cinema_schema.seat_reservations " +
                    "(projection_id, email, seat_number)" +
                    " SELECT @projection_id, @email, @seat_number WHERE " +
                    "NOT EXISTS(SELECT * FROM " +
                    "via_cinema_schema.seat_reservations" +
                    " WHERE seat_reservations.projection_id = @projection_id " +
                    "AND seat_reservations.email = @email AND " +
                    " seat_number = @seat_number); ";

                foreach (Seat seat in proj.Seats)
                {
                    // set parameters
                    stmt.Parameters.AddWithValue(ProjectionEntityConstants.PROJECTION_ID_COLUMN, proj.Id);
                    stmt.Parameters.AddWithValue(UserAccountEntityConstants.EMAIL_COLUMN, seat.SeatOwner.Email);
                    stmt.Parameters.AddWithValue(ProjectionEntityConstants.SEAT_NUMBER_COLUMN, seat.SeatNumber);

                    // execute statement
                    rowsAffected += stmt.ExecuteNonQuery();

                    // clear parameters for next interation
                    stmt.Parameters.Clear();
                }

                return rowsAffected;
            }
        }

        /// <inheritdoc/>
        public List<Seat> ReadSeatReservations(int projId)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                List<Seat> seatReservations = new List<Seat>(30);

                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText =
                    "SELECT"
                    + " seat_reservations.seat_number,"
                    + " user_accounts.email"
                    + " FROM via_cinema_schema.user_accounts, " +
                    "via_cinema_schema.seat_reservations"
                    + " WHERE user_accounts.email = via_cinema_schema" +
                    ".seat_reservations.email AND " +
                    "seat_reservations.projection_id = " +
                    "@id ORDER BY seat_reservations" +
                    ".seat_number ASC;";

                // set parameters
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.ID_COLUMN, projId);

                // execute quary
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // collect values
                    while (reader.Read())
                    {
                        int seatNumber = (int) reader[ProjectionEntityConstants.SEAT_NUMBER_COLUMN];
                        string email = reader[UserAccountEntityConstants.EMAIL_COLUMN] as string;

                        seatReservations.Add(new Seat(seatNumber, userAccountDao.Read(email)));
                    }

                    return seatReservations;
                }
            }
        }

        /// <summary>
        ///     Singleton implementation
        /// </summary>
        /// <returns> an instance of a proj data access object </returns>
        public static IProjectionDAO GetInstance()
        {
            return instance ?? (instance = new ProjectionDAO());
        }
    }
}
