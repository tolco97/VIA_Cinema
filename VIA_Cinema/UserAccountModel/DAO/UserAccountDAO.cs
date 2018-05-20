namespace UserAccountModel.DAO
{
    using System;
    using System.Collections.Generic;
    using Npgsql;

    public class UserAccountDAO : IUserAccountDAO
    {
        private static IUserAccountDAO _instance;

        private readonly NpgsqlConnection _con;

        private UserAccountDAO()
        {
            _con = new NpgsqlConnection("Server=localhost;User Id=postgres;" +
                                       "Password=password;Database=via_cinema_system;");
            _con.Open();
        }

        /// <inheritdoc/>
        public UserAccount Create(string email, string userPassword, string firstName, string lastName,
            DateTime birthday)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText =
                    "INSERT INTO via_cinema_schema.user_accounts (email, password, first_name," +
                    " last_name, birthday) VALUES (@email, @password, @first_name, @last_name, @birthday);";

                // set statement parameters
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EmailColumn, email);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.PasswordColumn, userPassword);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.FirstNameColumn, firstName);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.LastNameColumn, lastName);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.BirthdayColumn, birthday);

                // execute statement
                stmt.ExecuteNonQuery();

                return new UserAccount(email, userPassword, firstName, lastName, birthday);
            }
        }

        /// <inheritdoc/>
        public UserAccount Read(string email)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText =
                    "SELECT * FROM via_cinema_schema.user_accounts WHERE via_cinema_schema.user_accounts.email = @email;";

                // set parameters
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EmailColumn, email);

                // execute statement
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // the account does not exist
                    if (!reader.Read()) return null;

                    // get values
                    string password = (string) reader[UserAccountEntityConstants.PasswordColumn];
                    string firstName = (string) reader[UserAccountEntityConstants.FirstNameColumn];
                    string lastName = (string) reader[UserAccountEntityConstants.LastNameColumn];
                    DateTime birthday = (DateTime) reader[UserAccountEntityConstants.BirthdayColumn];

                    return new UserAccount(email, password, firstName, lastName, birthday);
                }
            }
        }

        /// <inheritdoc/>
        public ICollection<UserAccount> ReadAll()
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set the connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "SELECT * FROM via_cinema_schema.user_accounts;";

                // create a collection for the movies
                List<UserAccount> allAccounts = new List<UserAccount>(); // avoid array list resizing

                // execute statement
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // loop through the reader and collect data
                    while (reader.Read())
                    {
                        // get values
                        string email = (string) reader[UserAccountEntityConstants.EmailColumn];
                        string password = (string) reader[UserAccountEntityConstants.PasswordColumn];
                        string firstName = (string) reader[UserAccountEntityConstants.FirstNameColumn];
                        string lastName = (string) reader[UserAccountEntityConstants.LastNameColumn];
                        DateTime birthday = (DateTime) reader[UserAccountEntityConstants.BirthdayColumn];

                        allAccounts.Add(new UserAccount(email, password, firstName, lastName, birthday));
                    }
                }

                return allAccounts;
            }
        }

        /// <inheritdoc/>
        public bool Update(UserAccount updatedAct)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "UPDATE via_cinema_schema.user_accounts SET password = @password , " +
                                   "first_name = @first_name, last_name = @last_name, birthday = @birthday" +
                                   " WHERE email = @email;";

                // set the statement parameters
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.PasswordColumn, updatedAct.UserPassword);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.FirstNameColumn, updatedAct.FirstName);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.LastNameColumn, updatedAct.LastName);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.BirthdayColumn, updatedAct.Birthday);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EmailColumn, updatedAct.Email);

                // execute statement
                return stmt.ExecuteNonQuery() != 0;
            }
        }

        /// <inheritdoc/>
        public bool Delete(UserAccount account)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText =
                    "DELETE FROM via_cinema_schema.seat_reservations WHERE seat_reservations.email = @email";
                
                // set statement parameters
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EmailColumn, account.Email);

                // delete seat reservations of the user account
                stmt.ExecuteNonQuery();
                
                stmt.CommandText = "DELETE FROM via_cinema_schema.user_accounts WHERE user_accounts.email = @email;";
                
                // delete user account
                return stmt.ExecuteNonQuery() != 0;
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
        /// <returns> an instance of a user account data access object </returns>
        public static IUserAccountDAO GetInstance()
        {
            return _instance ?? (_instance = new UserAccountDAO());
        }
        
    }
}
