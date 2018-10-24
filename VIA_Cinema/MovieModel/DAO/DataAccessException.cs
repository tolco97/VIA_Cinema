using System;

namespace DNP1.ViaCinema.Model.MovieModel.DAO
{
    public class DataAccessException : Exception
    {
        public DataAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DataAccessException(string message) : base(message)
        {
        }
    }
}