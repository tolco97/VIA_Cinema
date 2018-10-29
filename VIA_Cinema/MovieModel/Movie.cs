using System.Runtime.Serialization;

namespace DNP1.ViaCinema.Model.MovieModel
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Movie
    {
        /// <summary>
        /// 
        /// </summary>
        public Movie() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="durationMinutes"></param>
        /// <param name="genre"></param>
        public Movie(string name, int durationMinutes, string genre)
        {
            Name = name;
            DurationMinutes = durationMinutes;
            Genre = genre;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public string Name { get; set; } // PK

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public int DurationMinutes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public string Genre { get; set; }

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString()
        {
            return $"Movie [Name: {Name}, DurationMinutes: {DurationMinutes}, Genre: {Genre}]";
        }
    }
}