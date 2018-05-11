namespace ProjectionModel.Base
{
    using System.Collections.Generic;
    using Model.MovieModel;
    using DAO;
    using UserAccountModel;
    using VIA_Cinema.Util;
    using System;

    public class ProjectionBase : IProjectionBase
    {
        private readonly Dictionary<int, Projection> projectionCache = new Dictionary<int, Projection>();
        private readonly IProjectionDAO projectionDao;

        public ProjectionBase(IProjectionDAO projectionDao)
        {
            this.projectionDao = projectionDao;
        }

        /// <inheritdoc/>
        public bool BookSeats(Projection proj, UserAccount userAcc, params int[] seatNumbers)
        {
            // illegal seat number
            if (!Validator.ValidateSeatNumber(seatNumbers)) return false;

            // check if requested seats are available
            if (!AreSeatsAvailable(proj, seatNumbers)) return false;

            // seats are available. book them
            foreach (int seatNum in seatNumbers)
                proj.Seats.Add(new Seat(seatNum, userAcc));
            
            // update database
            projectionDao.CreateSeatReservations(proj);

            // update cache
            projectionCache[proj.Id] = proj;

            return true;
        }

        /// <inheritdoc/>
        public Projection GetProjection(int projectionId)
        {
            // if projection is not cached
            if (!projectionCache.ContainsKey(projectionId))
            {
                // read projection
                Projection proj = projectionDao.ReadProjection(projectionId);

                // projection does not exist
                if (proj == null)
                    throw new ArgumentException($"Projection with id {projectionId} does not exist!");

                // cache projection object
                projectionCache[projectionId] = proj;
            }

            return projectionCache[projectionId];
        }

        /// <inheritdoc/>
        public List<Projection> GetAllProjections()
        {
            // read all projection skeletons
            ICollection<Projection> allProjections = projectionDao.ReadAllProjections();
            
            // cache all projections that are not cached already
            foreach (Projection proj in allProjections)
            {
                // cache all projections
                if (!projectionCache.ContainsKey(proj.Id))
                    projectionCache[proj.Id] = proj;
            }

            return new List<Projection>(projectionCache.Values);
        }

        /// <inheritdoc/>
        public List<Projection> GetAllProjections(Movie movie)
        {
            // read all projection skeletons for this movie
            ICollection<Projection> allProjections = projectionDao.ReadAllProjections(movie);

            // create output collection
            int size = allProjections.Count;
            List<Projection> allMatchingProjections = new List<Projection>(size);

            foreach (Projection proj in allProjections)
            {
                // get the projection Id
                int projectionId = proj.Id;

                // check if projection is already cached
                if (!projectionCache.ContainsKey(projectionId))
                    projectionCache[projectionId] = proj;
                
                // add to output list
                allMatchingProjections.Add(projectionCache[projectionId]);
            }

            return allMatchingProjections;
        }
        
        /// <summary>
        ///     Checks if the projection passed as a parameter has free seats at the seat numbers that 
        ///     correspond to the seat numbers passed as a parameter
        /// </summary>
        /// <param name="proj"> the projection </param>
        /// <param name="seatNumbers"> the seat numbers </param>
        /// <returns> true, if all the seats are available. Otherwise, false </returns>
        private bool AreSeatsAvailable(Projection proj, params int[] seatNumbers)
        {
            List<Seat> seatPattern = proj.Seats;

            foreach (int seatNum in seatNumbers)
                foreach (Seat seat in seatPattern)
                    if (seat.SeatNumber == seatNum)
                         return false;

            return true;
        }

    }
}
