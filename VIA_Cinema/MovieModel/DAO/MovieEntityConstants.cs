namespace MovieModel.DAO
{
    /// <summary>
    ///     Stores constants used when manipulating the database
    /// </summary>
    public sealed class MovieEntityConstants
    {
        private MovieEntityConstants() {}

        public const string NAME_COLUMN = "name";
        public const string DURATION_COLUMN = "duration_minuites";
        public const string GENRE_COLUMN = "genre";
    }
}
