namespace VIA_Cinema.Util
{
    using System;

    public sealed class Validator
    {
        private Validator() {}

        /// <summary>
        ///     Validates text user input. Throws exception, if input violates the 
        ///     constraints
        /// </summary>
        /// <param name="strings"> the textual input </param>
        public static void ValidateTextualInput(params string[] strings)
        {
            foreach (string s in strings)
                if (string.IsNullOrEmpty(s))
                    throw new ArgumentException("String is null or empty");
        }

        /// <summary>
        ///     Validates a movie duration user input. Throws exception, if input violates
        ///     the constraints 
        /// </summary>
        /// <param name="durationMinuites"> the movie duration input </param>
        public static void ValidateMovieDuration(int durationMinuites)
        {
            if (durationMinuites < 1)
                throw new ArgumentException("Duration can not be below 1");
        }

        /// <summary>
        ///     Validates a seat number user input. Throws exception, if input violates
        ///     the constraints
        /// </summary>
        /// <param name="seatNumbers"> the seat numbers </param>
        public static void ValidateSeatNumbers(params int[] seatNumbers)
        {
            foreach (int num in seatNumbers)
                if (num < 1 || num > 30)
                    throw new ArgumentException("Illegal seat number!");
        }

        /// <summary>
        ///     Validates complex type parameters. Throws exception, if an object is null
        /// </summary>
        /// <param name="objects"> the objects </param>
        public static void ValidateObjectsNotNull(params object[] objects)
        {
            foreach (object obj in objects)
                if (obj == null)
                    throw new ArgumentNullException("null value!");
        }

    }
}
