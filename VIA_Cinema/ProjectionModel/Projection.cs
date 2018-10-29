using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DNP1.ViaCinema.Model.MovieModel;

namespace DNP1.ViaCinema.Model.ProjectionModel
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Projection
    {
        /// <summary>
        /// 
        /// </summary>
        public Projection() {}

        // constructor for reading from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectedMovie"></param>
        /// <param name="seats"></param>
        /// <param name="movieStartTime"></param>
        public Projection(int id, Movie projectedMovie, List<Seat> seats,
            DateTime movieStartTime)
        {
            Id = id;
            ProjectedMovie = projectedMovie;
            Seats = seats;
            MovieStartTime = movieStartTime;
        }

        // constructor for saving to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectedMovie"></param>
        /// <param name="seats"></param>
        /// <param name="movieStartTime"></param>
        public Projection(Movie projectedMovie, List<Seat> seats,
            DateTime movieStartTime)
        {
            ProjectedMovie = projectedMovie;
            Seats = seats;
            MovieStartTime = movieStartTime;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public int Id { get; set; } // PK

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public Movie ProjectedMovie { get; set; } // FK one to one relationship

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public List<Seat> Seats { get; set; } // FK one to many relationship

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public DateTime MovieStartTime { get; set; }

        /// <inheritdoc cref="object.ToString"/> 
        public override string ToString()
        {
            return $"Projection [id: {Id}, Movie: {ProjectedMovie}, " +
                   $"\nAudience:\n{string.Join("\n", Seats)}, MovieStartTime: {MovieStartTime};";
        }
    }
}