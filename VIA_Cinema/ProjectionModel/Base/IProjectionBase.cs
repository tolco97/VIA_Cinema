namespace ProjectionModel.Base
{
    using System.Collections.Generic;
    using Model.MovieModel;
    using UserAccountModel;

    public interface IProjectionBase
    {
        /// <summary>
        ///     Assigns seats from the projection passed as a paramter to the user account passed as a
        ///     parameter
        /// </summary>
        /// <param name="proj"> the projection </param>
        /// <param name="userAcc"> the user account </param>
        /// <param name="seatNumbers"> the seat numbers </param>
        /// <returns> true, if the booking is successful. Otherwise, false </returns>
        bool BookSeats(Projection proj, UserAccount userAcc, params int[] seatNumbers);

        /// <summary>
        ///     Retrieves the projection from the system that matches the projeciton id passed as a
        ///     parameter
        /// </summary>
        /// <param name="projId"> the projection id </param>
        /// <returns> a projection object </returns>
        Projection GetProjection(int projId);

        /// <summary>
        ///     Retrieves all projections in the system
        /// </summary>
        /// <returns> a list of projections </returns>
        List<Projection> GetAllProjections();

        /// <summary>
        ///     Retrieves all projections in the system that project the movie passed as a parameter
        /// </summary>
        /// <param name="movie"> the movie </param>
        /// <returns> a list of projections </returns>
        List<Projection> GetAllProjections(Movie movie);
    }
}
