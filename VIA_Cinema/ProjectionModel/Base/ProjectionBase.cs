namespace VIA_Cinema.ProjectionModel.Base
{
    using System;
    using System.Collections.Generic;
    using MovieModel;
    using DAO;
    using UserAccountModel;
    using Util;

    public class ProjectionBase : IProjectionBase
    {
        private readonly IDictionary<int, Projection> _projectionCache = new Dictionary<int, Projection>();
        private readonly IProjectionDAO _projectionDao;

        public ProjectionBase(IProjectionDAO projectionDao)
        {
            _projectionDao = projectionDao;
        }

        /// <inheritdoc/>
        public Projection AddProjection(Movie movie, DateTime movieStartTime)
        {
            // validate input
            Validator.ValidateObjectsNotNull(movie, movieStartTime);

            // craete projection in the database
            Projection newProjection = _projectionDao.CreateProjection(movie, movieStartTime);

            // cache projection
            _projectionCache[newProjection.Id] = newProjection;

            return newProjection;
        }

        /// <inheritdoc/>
        public bool BookSeats(Projection proj, UserAccount seatOwner, params int[] seatNumbers)
        {
            // validate input
            Validator.ValidateObjectsNotNull(proj, seatOwner);
            Validator.ValidateSeatNumbers(seatNumbers); 

            // check if requested seats are available
            if (!AreSeatsAvailable(proj, seatNumbers)) return false;

            // seats are available. book them
            foreach (int seatNum in seatNumbers)
                proj.Seats.Add(new Seat(seatNum, seatOwner));
            
            // update database
            _projectionDao.CreateSeatReservations(proj);

            // update cache
            _projectionCache[proj.Id] = proj;

            return true;
        }

        /// <inheritdoc/>
        public Projection GetProjection(int projectionId)
        {
            // if projection is not cached
            if (!_projectionCache.ContainsKey(projectionId))
            {
                // read projection
                Projection proj = _projectionDao.ReadProjection(projectionId);

                // projection does not exist
                if (proj == null) return null;

                // cache projection object
                _projectionCache[projectionId] = proj;
            }

            return _projectionCache[projectionId];
        }

        /// <inheritdoc/>
        public IList<Projection> GetAllProjections()
        {
            // read all projections
            ICollection<Projection> allProjections = _projectionDao.ReadAllProjections();
            
            // cache all projections that are not cached already
            foreach (Projection proj in allProjections)
                if (!_projectionCache.ContainsKey(proj.Id))
                    _projectionCache[proj.Id] = proj;

            return new List<Projection>(_projectionCache.Values);
        }

        /// <inheritdoc/>
        public IList<Projection> GetAllProjections(Movie movie)
        {
            // validate input
            Validator.ValidateObjectsNotNull(movie);

            // read all projections for this movie
            ICollection<Projection> allProjections = _projectionDao.ReadAllProjections(movie);

            // create output collection
            int size = allProjections.Count;
            List<Projection> matchingProjections = new List<Projection>(size); // avoid list resizing

            // cache all projections that have not been read already
            foreach (Projection proj in allProjections)
            {
                if (!_projectionCache.ContainsKey(proj.Id))
                    _projectionCache[proj.Id] = proj;
                
                matchingProjections.Add(_projectionCache[proj.Id]);
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
            IList<Seat> seatPattern = proj.Seats;

            foreach (int seatNum in seatNumbers)
                foreach (Seat seat in seatPattern)
                    if (seat.SeatNumber == seatNum)
                         return false;

            return true;
        }

    }
}
