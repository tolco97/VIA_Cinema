using System;

namespace DNP1.ViaCinema.Model.Util
{
    /// <summary>
    /// 
    /// </summary>
    public static class Validator
    {
        /// <summary>
        ///     Validates text user input. Throws exception, if input violates the
        ///     constraints
        /// </summary>
        /// <param name="strings"> the textual input </param>
        public static void ValidateTextualInput(params string[] strings)
        {
            foreach (string str in strings)
            {
                if (string.IsNullOrWhiteSpace(str))
                { 
                    throw new ArgumentException("String is null or empty", nameof(str));
                }
            }
        }

        /// <summary>
        ///     Validates a movie duration user input. Throws exception, if input violates
        ///     the constraints
        /// </summary>
        /// <param name="durationMinutes"> the movie duration input </param>
        public static void ValidateMovieDuration(int durationMinutes)
        {
            if (durationMinutes < 1)
            { 
                throw new ArgumentException("Duration can not be below 1");
            }
        }

        /// <summary>
        ///     Validates a seat number user input. Throws exception, if input violates
        ///     the constraints
        /// </summary>
        /// <param name="seatNumbers"> the seat numbers </param>
        public static void ValidateSeatNumbers(params int[] seatNumbers)
        {
            foreach (int num in seatNumbers)
            { 
                if (num < 1 || num > 30)
                { 
                    throw new ArgumentException("Illegal seat number!", nameof(num));
                }
            }
        }

        /// <summary>
        ///     Validates complex type parameters. Throws exception, if an object is null
        /// </summary>
        /// <param name="objects"> the objects </param>
        public static void ValidateObjectsNotNull(params object[] objects)
        {
            foreach (object obj in objects)
            { 
                if (obj == null)
                { 
                    throw new ArgumentNullException(nameof(obj), "Null value!");
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="amounts"></param>
        public static void ValidateMoneyAmountPositive(params decimal[] amounts)
        {
            foreach (decimal dec in amounts)
            { 
                if (dec < decimal.Zero)
                { 
                    throw new ArgumentException("Can't withdraw an amount less than zero from bank account!", nameof(dec));
                }
            }
        }
    }
}