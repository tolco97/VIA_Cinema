namespace MovieModel.DAO
{
    /// <summary>
    ///     Stores constants used when manipulating the database
    /// </summary>
    public sealed class MovieEntityConstants
    {
        private MovieEntityConstants() {}

        public const string NameColumn = "name";
        public const string DurationColumn = "duration_minuites";
        public const string GenreColumn = "genre";
    }
}
