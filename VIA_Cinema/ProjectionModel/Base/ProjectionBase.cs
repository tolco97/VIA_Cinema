using System;
using System.Collections.Generic;
using System.Linq;
using DNP1.ViaCinema.Model.MovieModel;
using DNP1.ViaCinema.Model.ProjectionModel.DAO;
using DNP1.ViaCinema.Model.UserAccountModel;
using DNP1.ViaCinema.Model.Util;

namespace DNP1.ViaCinema.Model.ProjectionModel.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectionBase : IProjectionBase
    {
        private readonly IDictionary<int, Projection> _projectionCache = new Dictionary<int, Projection>();
        private readonly IProjectionDao _projectionDao;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectionDao"></param>
        public ProjectionBase(IProjectionDao projectionDao)
        {
            _projectionDao = projectionDao;
        }

        /// <inheritdoc cref="IProjectionBase.AddProjection(Movie, DateTime)"/>
        public Projection AddProjection(Movie movie, DateTime movieStartTime)
        {
            Validator.ValidateObjectsNotNull(movie, movieStartTime);

            Projection newProjection = _projectionDao.CreateProjection(movie, movieStartTime);

            _projectionCache[newProjection.Id] = newProjection;

            return newProjection;
        }

        /// <inheritdoc cref="IProjectionBase.BookSeats(Projection, UserAccount, int[])"/>
        public bool BookSeats(Projection proj, UserAccount seatOwner, params int[] seatNumbers)
        {
            Validator.ValidateObjectsNotNull(proj, seatOwner);
            Validator.ValidateSeatNumbers(seatNumbers);

            if (!AreSeatsAvailable(proj, seatNumbers))
            {
                return false;
            }

            foreach (int seatNum in seatNumbers)
            { 
                proj.Seats.Add(new Seat(seatNum, seatOwner));
            }

            _projectionDao.CreateSeatReservations(proj);

            _projectionCache[proj.Id] = proj;

            return true;
        }
        
        /// <inheritdoc cref="IProjectionBase.GetProjection(int)"/>
        public Projection GetProjection(int projectionId)
        {
            if (!_projectionCache.ContainsKey(projectionId))
            {
                Projection proj = _projectionDao.ReadProjection(projectionId);

                if (proj == null)
                {
                    return null;
                }

                _projectionCache[projectionId] = proj;
            }

            return _projectionCache[projectionId];
        }

        /// <inheritdoc cref="IProjectionBase.GetAllProjections()"/>
        public List<Projection> GetAllProjections()
        {
            ICollection<Projection> allProjections = _projectionDao.ReadAllProjections();

            foreach (Projection proj in allProjections)
            { 
                if (!_projectionCache.ContainsKey(proj.Id))
                { 
                    _projectionCache[proj.Id] = proj;
                }
            }

            return _projectionCache.Values.ToList();
        }

        /// <inheritdoc cref="IProjectionBase.GetAllProjections(Movie)"/>
        public List<Projection> GetAllProjections(Movie movie)
        {
            Validator.ValidateObjectsNotNull(movie);

            ICollection<Projection> allProjections = _projectionDao.ReadAllProjections(movie);

            int size = allProjections.Count;
            var matchingProjections = new List<Projection>(size); // avoid list resizing

            foreach (Projection proj in allProjections)
            {
                if (!_projectionCache.ContainsKey(proj.Id))
                { 
                    _projectionCache[proj.Id] = proj;
                }

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
            { 
                foreach (Seat seat in seatPattern)
                { 
                    if (seat.SeatNumber == seatNum)
                    { 
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
