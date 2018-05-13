namespace Model_VIA_Pay.DAO
{
    using System;
    using Npgsql;

    public class CreditCardDAO : ICreditCardDAO
    {
        private readonly NpgsqlConnection con;
        private static ICreditCardDAO instance = null;

        private CreditCardDAO()
        {
            con = new NpgsqlConnection("Server=localhost;User Id=postgres;" +
                                       "Password=password;Database=via_pay_system;");
            con.Open();
        }

        /// <summary>
        ///     Singleton implementation
        /// </summary>
        /// <returns> an instance of a credit updatedCard data access object </returns>
        public static ICreditCardDAO GetInstance()
        {
            return instance ?? (instance = new CreditCardDAO());
        }

        /// <inheritdoc/>
        public int Create(string cardNumber, string pin, decimal balanceDkk)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText =
                    "INSERT INTO via_pay_schema.credit_cards" +
                    " (number, pin, balance_dkk) VALUES (@number, @pin, @balance);";

                // set statement parameters
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.CARD_NUMBER_COLUMN, cardNumber);
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.PIN_COLUMN, pin);
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.BALANCE_COLUMN, balanceDkk);

                // execute statement
                return stmt.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        public int UpdateBalance(CreditCard updatedCard)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText = "UPDATE via_pay_schema.credit_cards " +
                                   "SET balance_dkk = @balance_dkk WHERE number = @number AND pin = @pin;";

                // set the statement parameters
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.BALANCE_COLUMN, updatedCard.BalanceDkk);
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.CARD_NUMBER_COLUMN, updatedCard.CardNumber);
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.PIN_COLUMN, updatedCard.Pin);

                // execute statement
                return stmt.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        public bool CreditCardExists(string creditCardNumber)
        {
            return Read(creditCardNumber) != null;
        }

        /// <inheritdoc/>
        public CreditCard Read(string creditCardNumber)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText =
                    "SELECT * FROM via_pay_schema.credit_cards" +
                    " WHERE via_pay_schema." +
                    "credit_cards.number = @number;";

                // set parameters
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.CARD_NUMBER_COLUMN, creditCardNumber);

                // execute statement
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // the credit updatedCard does not exist
                    if (!reader.Read()) return null;

                    // collect data
                    var pin = reader[CreditCardEntityConstants.PIN_COLUMN] as string;

                    // 2 decimal symbol format for the balance
                    // Example: 450.50
                    decimal rawBalance = (decimal) reader[CreditCardEntityConstants.BALANCE_COLUMN];
                    decimal balance = Math.Round(rawBalance, 2);
                    
                    return new CreditCard(creditCardNumber, pin, balance);
                }
            }
        }

        /// <inheritdoc/>
        public void CloseConnection()
        {
            con?.Close();
        }
    }
}
