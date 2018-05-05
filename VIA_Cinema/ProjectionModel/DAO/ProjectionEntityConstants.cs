namespace ProjectionModel.DAO
{
    /// <summary>
    ///     Stores some constants used when maniupulating the database
    /// </summary>
    public sealed class ProjectionEntityConstants
    {
        private ProjectionEntityConstants() {}

        public const string ID_COLUMN = "id";
        public const string PROJECTION_ID_COLUMN = "projection_id";
        public const string MOVIE_NAME_COLUMN = "movie_name";
        public const string PROJECTION_START_COLUMN = "projection_start";
        public const string SEAT_NUMBER_COLUMN = "seat_number";
    }
}
