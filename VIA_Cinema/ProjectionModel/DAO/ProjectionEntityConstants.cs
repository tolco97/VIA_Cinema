namespace ProjectionModel.DAO
{
    /// <summary>
    ///     Stores some constants used when maniupulating the database
    /// </summary>
    public sealed class ProjectionEntityConstants
    {
        private ProjectionEntityConstants() {}

        public const string IdColumn = "id";
        public const string ProjectionIdColumn = "projection_id";
        public const string MovieNameColumn = "movie_name";
        public const string ProjectionStartColumn = "projection_start";
        public const string SeatNumberColumn = "seat_number";
    }
}
