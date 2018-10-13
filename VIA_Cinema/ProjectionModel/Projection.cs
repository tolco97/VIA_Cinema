using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using VIA_Cinema.MovieModel;

namespace VIA_Cinema.ProjectionModel
{
    [DataContract]
    public class Projection
    {
        public Projection() { }

        // constructor for reading from the database
        public Projection(int id, Movie projectedMovie, List<Seat> seats,
            DateTime movieStartTime)
        {
            Id = id;
            ProjectedMovie = projectedMovie;
            Seats = seats;
            MovieStartTime = movieStartTime;
        }

        // constructor for saving to the database
        public Projection(Movie projectedMovie, List<Seat> seats,
            DateTime movieStartTime)
        {
            ProjectedMovie = projectedMovie;
            Seats = seats;
            MovieStartTime = movieStartTime;
        }

        [DataMember] public int Id { get; set; } // PK

        [DataMember] public Movie ProjectedMovie { get; set; } // FK one to one relationship

        [DataMember] public List<Seat> Seats { get; set; } // FK one to many relationship

        [DataMember] public DateTime MovieStartTime { get; set; }

        public override string ToString()
        {
            return $"Projection [id: {Id}, Movie: {ProjectedMovie}, " +
                   $"\nAudience:\n{string.Join("\n", Seats)}, MovieStartTime: {MovieStartTime};";
        }
    }
}