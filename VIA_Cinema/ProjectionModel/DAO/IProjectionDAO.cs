using System;
using System.Collections.Generic;
using DNP1.ViaCinema.Model.MovieModel;
using DNP1.ViaCinema.Model.UserAccountModel;

namespace DNP1.ViaCinema.Model.ProjectionModel.DAO
{
    public interface IProjectionDao : IDisposable
    {
        /// <summary>
        ///     Creates a projection database entry in the projections entity
        /// </summary>
        /// <param name="projectedMovie"> the movie to be projected in the projection </param>
        /// <param name="movieStartTime"> the time at which the projection starts </param>
        /// <returns> a projection object </returns>
        Projection CreateProjection(Movie projectedMovie, DateTime movieStartTime);

        /// <summary>
        ///     Creates a seat reservation entries in the seat reservations entity with
        ///     the data from the proj passed as a parameter
        /// </summary>
        /// <param name="proj"> the projection </param>
        /// <returns> the projection's seat pattern </returns>
        IList<Seat> CreateSeatReservations(Projection proj);

        /// <summary>
        ///     Reads a projection entry from the database that matches the projection
        ///     id passed as a parameter
        /// </summary>
        /// <param name="projectionId"> the projection id </param>
        /// <returns> a projection object </returns>
        Projection ReadProjection(int projectionId);

        /// <summary>
        ///     Reads all projection database entries from the projections entity
        /// </summary>
        /// <returns> a collection of projection objects </returns>
        ICollection<Projection> ReadAllProjections();


        /// <summary>
        ///     Reads all projection database entries that project the movie passed as a parameter
        /// </summary>
        /// <param name="movie"> the movie </param>
        /// <returns> a collection of projection objects </returns>
        ICollection<Projection> ReadAllProjections(Movie movie);

        /// <summary>
        ///     Reads all seat reservation entries from the seat reservations entity that match
        ///     the projection id passed as a parameter
        /// </summary>
        /// <param name="projId"> the projection id </param>
        /// <returns> a list of seats </returns>
        List<Seat> ReadSeatReservations(int projId);

        /// <summary>
        ///     Updates a projection database entry with the data from the projection passed
        ///     as a parameter
        /// </summary>
        /// <param name="updatedProj"> the updated projection object </param>
        /// <returns> true, if the update operation has affected at least 1 database row. Otherwise, false </returns>
        bool UpdateProjection(Projection updatedProj);

        /// <summary>
        ///     Deletes the seat reservation database entries that match the projection id,
        ///     user email and seat number passed as parameters
        /// </summary>
        /// <param name="projectionId"> the projection id </param>
        /// <param name="user"> the user account </param>
        /// <param name="seatNumber"> the seat number </param>
        /// <returns> true, if the delete operation has affected at least 1 database row. Otherwise, false </returns>
        bool DeleteSeatReservation(int projectionId, UserAccount user, int seatNumber);
    }
}