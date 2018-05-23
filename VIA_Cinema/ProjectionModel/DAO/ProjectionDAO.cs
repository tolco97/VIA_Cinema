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
        private static IProjectionDAO _instance;

        private readonly NpgsqlConnection _con;

        // I couldn't use dependency injections, because all DAO objects are Singletons
        private readonly IMovieDAO movieDao = MovieDAO.GetInstance();
        private readonly IUserAccountDAO userAccountDao = UserAccountDAO.GetInstance();

        private ProjectionDAO()
        {
            _con = new NpgsqlConnection("Server=localhost;User Id=postgres;" +
                                       "Password=password;Database=via_cinema_system;");
            _con.Open();
        }

        /// <inheritdoc/>
        public Projection CreateProjection(Movie projectedMovie, DateTime movieStartTime)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText =
                    "INSERT INTO via_cinema_schema.projections " +
                    "(movie_name, projection_start)" +
                    " VALUES (@movie_name, @projection_start) RETURNING id;";

                // set statement parameters
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.MovieNameColumn,
                    projectedMovie.Name);
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.ProjectionStartColumn,
                    movieStartTime);

                // execute statement and return generated ID
                int projId = (int) stmt.ExecuteScalar();

                // create new projection object
                return new Projection(projId, projectedMovie, new List<Seat>(), movieStartTime);
            }
        }

        /// <inheritdoc/>
        public Projection ReadProjection(int projectionId)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "SELECT * FROM via_cinema_schema.projections" +
                                   " WHERE projections.id = @id;";

                // set statement parameters
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.IdColumn, projectionId);

                // get seats for this projection
                IList<Seat> seatAllocations = ReadSeatReservations(projectionId);

                // execute statement and collect values
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // projection does not exist
                    if (!reader.Read()) return null;

                    // get the movie object
                    string movieName = (string) reader[ProjectionEntityConstants.MovieNameColumn];
                    Movie movie = movieDao.Read(movieName);

                    // get projection start
                    DateTime projStartTime = (DateTime) reader[ProjectionEntityConstants.ProjectionStartColumn];

                    return new Projection(projectionId, movie, seatAllocations, projStartTime);
                }
            }
        }

        /// <inheritdoc/>
        public ICollection<Projection> ReadAllProjections()
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // output collection
                List<Projection> allProjections = new List<Projection>();

                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "SELECT * FROM via_cinema_schema.projections;";

                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // collect values
                    while (reader.Read())
                    {
                        // get projection projectionId
                        int projectionId = (int) reader[ProjectionEntityConstants.IdColumn];

                        // get the movie name & movie object
                        string movieName = (string) reader[ProjectionEntityConstants.MovieNameColumn];
                        Movie movie = movieDao.Read(movieName);

                        // get projection start
                        DateTime projectionStart = (DateTime) reader[ProjectionEntityConstants.ProjectionStartColumn];

                        Projection projection = new Projection
                        {
                            Id = projectionId,
                            ProjectedMovie = movie,
                            MovieStartTime = projectionStart
                        };
                        allProjections.Add(projection);
                    }
                }

                // read all projection seat resevations
                foreach (Projection proj in allProjections)
                    proj.Seats = ReadSeatReservations(proj.Id);

                return allProjections;
            }
        }

        /// <inheritdoc/>
        public ICollection<Projection> ReadAllProjections(Movie movie)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // output collection
                List<Projection> allProjections = new List<Projection>();

                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "SELECT * FROM via_cinema_schema.projections" +
                                   " WHERE projections.movie_name = @movie_name;";

                // set parameters
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.MovieNameColumn, movie.Name);

                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // collect values
                    while (reader.Read())
                    {
                        // get projection projectionId
                        int projectionId = (int) reader[ProjectionEntityConstants.IdColumn];

                        // get projection start
                        DateTime projectionStart = (DateTime) reader[ProjectionEntityConstants.ProjectionStartColumn];
                        
                        Projection projection = new Projection
                        {
                            Id = projectionId,
                            ProjectedMovie = movie,
                            MovieStartTime = projectionStart
                        };

                        allProjections.Add(projection);
                    }
                }

                // read all projection seat reservations
                foreach (Projection proj in allProjections)
                    proj.Seats = ReadSeatReservations(proj.Id);

                return allProjections;
            }
        }

        /// <inheritdoc/>
        public bool UpdateProjection(Projection updatedProj)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "UPDATE via_cinema_schema.projections " +
                                   "SET movie_name = @movie_name," +
                                   " projection_start = @projection_start " +
                                   "WHERE id = @id;";

                // set parameters for proj update
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.MovieNameColumn,
                    updatedProj.ProjectedMovie.Name);
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.ProjectionStartColumn,
                    updatedProj.MovieStartTime);
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.IdColumn, updatedProj.Id);

                // execute statement
                return stmt.ExecuteNonQuery() != 0;
            }
        }

        /// <inheritdoc/>
        public bool DeleteSeatReservation(int projectionId, UserAccount user, int seatNumber)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "DELETE FROM via_cinema_schema.seat_reservations " +
                                   "WHERE seat_reservations.projection_id = @projection_id" +
                                   " AND seat_reservations.email = @email" +
                                   " AND seat_reservations.seat_number = @seat_number;";

                // set parameters for proj update
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.ProjectionIdColumn, projectionId);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EmailColumn, user.Email);
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.SeatNumberColumn, seatNumber);

                // execute statement
                return stmt.ExecuteNonQuery() != 0;
            }
        }

        /// <inheritdoc/>
        public IList<Seat> CreateSeatReservations(Projection proj)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

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
                    stmt.Parameters.AddWithValue(ProjectionEntityConstants.ProjectionIdColumn, proj.Id);
                    stmt.Parameters.AddWithValue(UserAccountEntityConstants.EmailColumn, seat.SeatOwner.Email);
                    stmt.Parameters.AddWithValue(ProjectionEntityConstants.SeatNumberColumn, seat.SeatNumber);

                    // execute statement
                    stmt.ExecuteNonQuery();

                    // clear parameters for next interation
                    stmt.Parameters.Clear();
                }

                return proj.Seats;
            }
        }

        /// <inheritdoc/>
        public IList<Seat> ReadSeatReservations(int projId)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                List<Seat> seatReservations = new List<Seat>(30);

                // set connection
                stmt.Connection = _con;

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
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.IdColumn, projId);

                // execute quary
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // collect values
                    while (reader.Read())
                    {
                        int seatNumber = (int) reader[ProjectionEntityConstants.SeatNumberColumn];
                        string email = (string) reader[UserAccountEntityConstants.EmailColumn];
                        UserAccount seatOwner = userAccountDao.Read(email);

                        seatReservations.Add(new Seat(seatNumber, seatOwner));
                    }

                    return seatReservations;
                }
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _con?.Dispose();
        }

        /// <summary>
        ///     Singleton implementation
        /// </summary>
        /// <returns> a reference to a projection data access object </returns>
        public static IProjectionDAO GetInstance()
        {
            return _instance ?? (_instance = new ProjectionDAO());
        }
        
    }
}
