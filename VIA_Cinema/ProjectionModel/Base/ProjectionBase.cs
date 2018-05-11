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
        public Projection GetProjection(int projId)
        {
            // if projection is not cached
            if (!projectionCache.ContainsKey(projId))
            {
                // read projection
                Projection proj = projectionDao.ReadProjection(projId);

                // projection does not exist
                if (proj == null)
                    throw new ArgumentException($"Projection with id {projId} does not exist!");

                // cache projection object
                projectionCache[projId] = proj;
            }

            return projectionCache[projId];
        }

        /// <inheritdoc/>
        public List<Projection> GetAllProjections()
        {
            // read all projections
            ICollection<Projection> allProjections = projectionDao.ReadAllProjections();
            
            // cache all projections that are not cached already
            foreach (Projection proj in allProjections)
                if (!projectionCache.ContainsKey(proj.Id))
                    projectionCache[proj.Id] = proj;
            
            return new List<Projection>(projectionCache.Values);
        }

        /// <inheritdoc/>
        public List<Projection> GetAllProjections(Movie movie)
        {
            // read all projections for this movie
            ICollection<Projection> allProjections = projectionDao.ReadAllProjections(movie);

            // create output collection
            int size = allProjections.Count;
            List<Projection> matchingProjections = new List<Projection>(size); // avoid list resizing

            // cache all projections that have not been read already
            foreach (Projection proj in allProjections)
            {
                if (!projectionCache.ContainsKey(proj.Id))
                    projectionCache[proj.Id] = proj;
                
                matchingProjections.Add(projectionCache[proj.Id]);
            }

            return matchingProjections;
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
