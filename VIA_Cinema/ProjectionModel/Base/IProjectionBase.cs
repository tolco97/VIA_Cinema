namespace ProjectionModel.Base
{
    using System.Collections.Generic;
    using Model.MovieModel;
    using UserAccountModel;
    using System;

    public interface IProjectionBase
    {
        /// <summary>
        ///     Creates a new projection in the system.
        /// </summary>
        /// <param name="movie"> the projected movie </param>
        /// <param name="movieStartTime"> the projection start time </param>
        /// <returns> the newly created projection </returns>
        Projection AddProjection(Movie movie, DateTime movieStartTime);

        /// <summary>
        ///     Assigns seats from the projection passed as a paramter to the user account passed as a
        ///     parameter
        /// </summary>
        /// <param name="proj"> the projection </param>
        /// <param name="seatOwner"> the user account </param>
        /// <param name="seatNumbers"> the seat numbers </param>
        /// <returns> true, if the booking is successful. Otherwise, false </returns>
        bool BookSeats(Projection proj, UserAccount seatOwner, params int[] seatNumbers);

        /// <summary>
        ///     Retrieves the projection from the system that matches the projeciton id passed as a
        ///     parameter
        /// </summary>
        /// <param name="projectionId"> the projection id </param>
        /// <returns> a projection object </returns>
        Projection GetProjection(int projectionId);

        /// <summary>
        ///     Retrieves all projections in the system
        /// </summary>
        /// <returns> a list of projections </returns>
        IList<Projection> GetAllProjections();

        /// <summary>
        ///     Retrieves all projections in the system that project the movie passed as a parameter
        /// </summary>
        /// <param name="movie"> the movie </param>
        /// <returns> a list of projections </returns>
        IList<Projection> GetAllProjections(Movie movie);
    }
}
