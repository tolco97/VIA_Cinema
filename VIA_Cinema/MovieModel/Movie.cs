namespace VIA_Cinema.MovieModel
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Movie
    {
        public Movie() {}

        public Movie(string name, int durationMinuites, string genre)
        {
            Name = name;
            DurationMinuites = durationMinuites;
            Genre = genre;
        }

        [DataMember] public string Name { get; set; } // PK

        [DataMember] public int DurationMinuites { get; set; }

        [DataMember] public string Genre { get; set; }

        public override string ToString()
        {
            return $"Movie [Name: {Name}, DurationMinuites: {DurationMinuites}, Genre: {Genre}]";
        }

    }
}
