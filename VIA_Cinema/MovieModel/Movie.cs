namespace Model.MovieModel
{
    using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            if (!(obj is Movie))
                return false;

            Movie other = (Movie) obj;
            return Name.Equals(other.Name) && 
                   DurationMinuites.Equals(other.DurationMinuites) &&
                   Genre.Equals(other.Genre);
        }

        public override int GetHashCode()
        {
            int hashCode = 1430415809;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + DurationMinuites.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Genre);
            return hashCode;
        }

        public override string ToString()
        {
            return $"Movie [Name: {Name}, DurationMinuites: {DurationMinuites}, Genre: {Genre}]";
        }

    }
}
