namespace VIA_Cinema.Util
{
    using System;
    using System.Linq;

    public sealed class Validator
    {
        private Validator() {}

        /// <summary>
        ///     Validates text user input. Throws exception, if input violates the 
        ///     constraints
        /// </summary>
        /// <param name="args"> the textual input </param>
        public static void ValidateTextualInput(params string[] args)
        {
            foreach (string s in args)
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
                throw new ArgumentException("Duration can not be below 0");
        }

        /// <summary>
        ///     Validates a seat number user input
        /// </summary>
        /// <param name="seatNumbers"> the seat numbers </param>
        /// <returns> true, if the input is valid, Otherwise, false </returns>
        public static bool ValidateSeatNumber(params int[] seatNumbers)
        {
            return seatNumbers.All(num => num >= 1 && num <= 30);
        }
    }
}