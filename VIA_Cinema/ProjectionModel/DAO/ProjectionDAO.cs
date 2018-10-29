using System;
using System.Collections.Generic;
using DNP1.ViaCinema.Model.MovieModel;
using DNP1.ViaCinema.Model.MovieModel.DAO;
using DNP1.ViaCinema.Model.UserAccountModel;
using DNP1.ViaCinema.Model.UserAccountModel.DAO;
using Npgsql;

namespace DNP1.ViaCinema.Model.ProjectionModel.DAO
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectionDao : IProjectionDao
    {
        private static IProjectionDao _instance;

        private readonly NpgsqlConnection _con;

        // I couldn't use dependency injections, because all DAO objects are Singletons
        private readonly IMovieDao _movieDao = MovieDao.GetInstance();
        private readonly IUserAccountDao _userAccountDao = UserAccountDao.GetInstance();

        private ProjectionDao()
        {
            _con = new NpgsqlConnection("Server=localhost;User Id=postgres;Password=password;Database=via_cinema_system;");
            _con.Open();
        }

        /// <inheritdoc cref="IProjectionDao.CreateProjection(Movie, DateTime)"/>
        public Projection CreateProjection(Movie projectedMovie, DateTime movieStartTime)
        {
            using (var stmt = new NpgsqlCommand())
            {
                stmt.Connection = _con;

                stmt.CommandText =
                    "INSERT INTO via_cinema_schema.projections " +
                    "(movie_name, projection_start)" +
                    " VALUES (@movie_name, @projection_start) RETURNING id;";

                stmt.Parameters.AddWithValue(ProjectionEntityConstants.MovieNameColumn,
                    projectedMovie.Name);
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.ProjectionStartColumn,
                    movieStartTime);

                var projId = (int) stmt.ExecuteScalar();

                return new Projection(projId, projectedMovie, new List<Seat>(0), movieStartTime);
            }
        }

        /// <inheritdoc cref="IProjectionDao.ReadProjection(int)"/>
        public Projection ReadProjection(int projectionId)
        {
            using (var stmt = new NpgsqlCommand())
            {
                stmt.Connection = _con;

                stmt.CommandText = "SELECT * FROM via_cinema_schema.projections" +
                                   " WHERE projections.id = @id;";

                stmt.Parameters.AddWithValue(ProjectionEntityConstants.IdColumn, projectionId);

                List<Seat> seatAllocations = ReadSeatReservations(projectionId);

                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    var movieName = (string) reader[ProjectionEntityConstants.MovieNameColumn];
                    Movie movie = _movieDao.Read(movieName);

                    var projStartTime = (DateTime) reader[ProjectionEntityConstants.ProjectionStartColumn];

                    return new Projection(projectionId, movie, seatAllocations, projStartTime);
                }
            }
        }

        /// <inheritdoc cref="IProjectionDao.ReadAllProjections()"/>
        public ICollection<Projection> ReadAllProjections()
        {
            using (var stmt = new NpgsqlCommand())
            {
                var allProjections = new List<Projection>();

                stmt.Connection = _con;

                stmt.CommandText = "SELECT * FROM via_cinema_schema.projections;";

                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var projectionId = (int) reader[ProjectionEntityConstants.IdColumn];

                        var movieName = (string) reader[ProjectionEntityConstants.MovieNameColumn];
                        Movie movie = _movieDao.Read(movieName);

                        var projectionStart = (DateTime) reader[ProjectionEntityConstants.ProjectionStartColumn];

                        var projection = new Projection
                        {
                            Id = projectionId,
                            ProjectedMovie = movie,
                            MovieStartTime = projectionStart
                        };

                        allProjections.Add(projection);
                    }
                }

                foreach (Projection proj in allProjections)
                { 
                    proj.Seats = ReadSeatReservations(proj.Id);
                }

                return allProjections;
            }
        }

        /// <inheritdoc cref="IProjectionDao.ReadAllProjections(Movie)"/>
        public ICollection<Projection> ReadAllProjections(Movie movie)
        {
            using (var stmt = new NpgsqlCommand())
            {
                var allProjections = new List<Projection>();

                stmt.Connection = _con;

                stmt.CommandText = "SELECT * FROM via_cinema_schema.projections" +
                                   " WHERE projections.movie_name = @movie_name;";

                stmt.Parameters.AddWithValue(ProjectionEntityConstants.MovieNameColumn, movie.Name);

                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var projectionId = (int) reader[ProjectionEntityConstants.IdColumn];

                        var projectionStart = (DateTime) reader[ProjectionEntityConstants.ProjectionStartColumn];

                        var projection = new Projection
                        {
                            Id = projectionId,
                            ProjectedMovie = movie,
                            MovieStartTime = projectionStart
                        };

                        allProjections.Add(projection);
                    }
                }

                foreach (Projection proj in allProjections)
                { 
                    proj.Seats = ReadSeatReservations(proj.Id);
                }

                return allProjections;
            }
        }

        /// <inheritdoc cref="IProjectionDao.UpdateProjection(Projection)"/>
        public bool UpdateProjection(Projection updatedProj)
        {
            using (var stmt = new NpgsqlCommand())
            {
                stmt.Connection = _con;

                stmt.CommandText = "UPDATE via_cinema_schema.projections " +
                                   "SET movie_name = @movie_name," +
                                   " projection_start = @projection_start " +
                                   "WHERE id = @id;";

                stmt.Parameters.AddWithValue(ProjectionEntityConstants.MovieNameColumn,
                    updatedProj.ProjectedMovie.Name);
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.ProjectionStartColumn,
                    updatedProj.MovieStartTime);
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.IdColumn, updatedProj.Id);

                return stmt.ExecuteNonQuery() != 0;
            }
        }

        /// <inheritdoc cref="IProjectionDao.DeleteSeatReservation(int, UserAccount, int)"/>
        public bool DeleteSeatReservation(int projectionId, UserAccount user, int seatNumber)
        {
            using (var stmt = new NpgsqlCommand())
            {
                stmt.Connection = _con;

                stmt.CommandText = "DELETE FROM via_cinema_schema.seat_reservations " +
                                   "WHERE seat_reservations.projection_id = @projection_id" +
                                   " AND seat_reservations.email = @email" +
                                   " AND seat_reservations.seat_number = @seat_number;";

                stmt.Parameters.AddWithValue(ProjectionEntityConstants.ProjectionIdColumn, projectionId);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EmailColumn, user.Email);
                stmt.Parameters.AddWithValue(ProjectionEntityConstants.SeatNumberColumn, seatNumber);

                return stmt.ExecuteNonQuery() != 0;
            }
        }

        /// <inheritdoc cref="IProjectionDao.CreateSeatReservations(Projection)"/>
        public IList<Seat> CreateSeatReservations(Projection proj)
        {
            using (var stmt = new NpgsqlCommand())
            {
                stmt.Connection = _con;

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
                    stmt.Parameters.AddWithValue(ProjectionEntityConstants.ProjectionIdColumn, proj.Id);
                    stmt.Parameters.AddWithValue(UserAccountEntityConstants.EmailColumn, seat.SeatOwner.Email);
                    stmt.Parameters.AddWithValue(ProjectionEntityConstants.SeatNumberColumn, seat.SeatNumber);

                    stmt.ExecuteNonQuery();

                    stmt.Parameters.Clear();
                }

                return proj.Seats;
            }
        }

        /// <inheritdoc cref="IProjectionDao.ReadSeatReservations(int)"/>
        public List<Seat> ReadSeatReservations(int projId)
        {
            using (var stmt = new NpgsqlCommand())
            {
                var seatReservations = new List<Seat>(30);

                stmt.Connection = _con;

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

                stmt.Parameters.AddWithValue(ProjectionEntityConstants.IdColumn, projId);

                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var seatNumber = (int) reader[ProjectionEntityConstants.SeatNumberColumn];
                        var email = (string) reader[UserAccountEntityConstants.EmailColumn];
                        UserAccount seatOwner = _userAccountDao.Read(email);

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
        public static IProjectionDao GetInstance()
        {
            return _instance ?? (_instance = new ProjectionDao());
        }
    }
}