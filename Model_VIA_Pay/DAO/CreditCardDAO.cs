using System;
using Npgsql;

namespace DNP1.ViaPay.Model.DAO
{
    public class CreditCardDao : ICreditCardDao
    {
        private static ICreditCardDao _instance;
        private readonly NpgsqlConnection _con;

        private CreditCardDao()
        {
            _con = new NpgsqlConnection("Server=localhost;User Id=postgres;" +
                                        "Password=password;Database=via_pay_system;");
            _con.Open();
        }

        /// <inheritdoc />
        public CreditCard Create(string cardNumber, string pin, decimal balanceDkk)
        {
            using (var stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText =
                    "INSERT INTO via_pay_schema.credit_cards" +
                    " (number, pin, balance_dkk) VALUES (@number, @pin, @balance);";

                // set statement parameters
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.CardNumberColumn, cardNumber);
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.PinColumn, pin);
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.BalanceColumn, balanceDkk);

                // execute statement
                stmt.ExecuteNonQuery();

                return new CreditCard(cardNumber, pin, balanceDkk);
            }
        }

        /// <inheritdoc />
        public bool UpdateBalance(CreditCard updatedCard)
        {
            using (var stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "UPDATE via_pay_schema.credit_cards " +
                                   "SET balance_dkk = @balance_dkk WHERE number = @number AND pin = @pin;";

                // set the statement parameters
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.BalanceColumn, updatedCard.BalanceDkk);
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.CardNumberColumn, updatedCard.CardNumber);
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.PinColumn, updatedCard.Pin);

                // execute statement
                return stmt.ExecuteNonQuery() != 0;
            }
        }

        /// <inheritdoc />
        public bool CreditCardExists(string creditCardNumber)
        {
            return Read(creditCardNumber) != null;
        }

        /// <inheritdoc />
        public CreditCard Read(string creditCardNumber)
        {
            using (var stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText =
                    "SELECT * FROM via_pay_schema.credit_cards" +
                    " WHERE via_pay_schema." +
                    "credit_cards.number = @number;";

                // set parameters
                stmt.Parameters.AddWithValue(CreditCardEntityConstants.CardNumberColumn, creditCardNumber);

                // execute statement
                using (var reader = stmt.ExecuteReader())
                {
                    // the credit updatedCard does not exist
                    if (!reader.Read()) return null;

                    // collect data
                    var pin = (string) reader[CreditCardEntityConstants.PinColumn];

                    // 2 decimal symbol format for the balance
                    // Example: 450.50
                    var rawBalance = (decimal) reader[CreditCardEntityConstants.BalanceColumn];
                    var balance = Math.Round(rawBalance, 2);

                    return new CreditCard(creditCardNumber, pin, balance);
                }
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _con?.Dispose();
        }

        /// <summary>
        ///     Singleton implementation
        /// </summary>
        /// <returns> an instance of a credit updatedCard data access object </returns>
        public static ICreditCardDao GetInstance()
        {
            return _instance ?? (_instance = new CreditCardDao());
        }
    }
}