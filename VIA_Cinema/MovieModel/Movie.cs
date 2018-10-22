using System.Runtime.Serialization;

namespace DNP1.ViaCinema.Model.MovieModel
{
    [DataContract]
    public class Movie
    {
        public Movie() {}

        public Movie(string name, int durationMinutes, string genre)
        {
            Name = name;
            DurationMinutes = durationMinutes;
            Genre = genre;
        }

        [DataMember] public string Name { get; set; } // PK

        [DataMember] public int DurationMinutes { get; set; }

        [DataMember] public string Genre { get; set; }

        public override string ToString()
        {
            return $"Movie [Name: {Name}, DurationMinutes: {DurationMinutes}, Genre: {Genre}]";
        }
    }
}