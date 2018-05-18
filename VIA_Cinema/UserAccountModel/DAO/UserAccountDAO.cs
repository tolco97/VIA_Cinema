namespace UserAccountModel.DAO
{
    using System;
    using System.Collections.Generic;
    using Npgsql;

    public class UserAccountDAO : IUserAccountDAO
    {
        private static IUserAccountDAO instance;

        private readonly NpgsqlConnection con;

        private UserAccountDAO()
        {
            con = new NpgsqlConnection("Server=localhost;User Id=postgres;" +
                                       "Password=password;Database=via_cinema_system;");
            con.Open();
        }

        /// <inheritdoc/>
        public UserAccount Create(string email, string userPassword, string firstName, string lastName,
            DateTime birthday)
        {
            using (NpgsqlCommand stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = con;

                // set statement
                stmt.CommandText =
                    "INSERT INTO via_cinema_schema.user_accounts (email, password, first_name," +
                    " last_name, birthday) VALUES (@email, @password, @first_name, @last_name, @birthday);";

                // set statement parameters
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EMAIL_COLUMN, email);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.PASSWORD_COLUMN, userPassword);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.FIRST_NAME_COLUMN, firstName);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.LAST_NAME_COLUMN, lastName);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.BIRTHDAY_COLUMN, birthday);

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
                stmt.Connection = con;

                // set statement
                stmt.CommandText =
                    "SELECT * FROM via_cinema_schema.user_accounts WHERE via_cinema_schema.user_accounts.email = @email;";

                // set parameters
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EMAIL_COLUMN, email);

                // execute statement
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // the account does not exist
                    if (!reader.Read()) return null;

                    // get values
                    string password = (string) reader[UserAccountEntityConstants.PASSWORD_COLUMN];
                    string firstName = (string) reader[UserAccountEntityConstants.FIRST_NAME_COLUMN];
                    string lastName = (string) reader[UserAccountEntityConstants.LAST_NAME_COLUMN];
                    DateTime birthday = (DateTime) reader[UserAccountEntityConstants.BIRTHDAY_COLUMN];

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
                stmt.Connection = con;

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
                        string email = (string) reader[UserAccountEntityConstants.EMAIL_COLUMN];
                        string password = (string) reader[UserAccountEntityConstants.PASSWORD_COLUMN];
                        string firstName = (string) reader[UserAccountEntityConstants.FIRST_NAME_COLUMN];
                        string lastName = (string) reader[UserAccountEntityConstants.LAST_NAME_COLUMN];
                        DateTime birthday = (DateTime) reader[UserAccountEntityConstants.BIRTHDAY_COLUMN];

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
                stmt.Connection = con;

                // set statement
                stmt.CommandText = "UPDATE via_cinema_schema.user_accounts SET password = @password , " +
                                   "first_name = @first_name, last_name = @last_name, birthday = @birthday" +
                                   " WHERE name = @name;";

                // set the statement parameters
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.PASSWORD_COLUMN, updatedAct.UserPassword);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.FIRST_NAME_COLUMN, updatedAct.FirstName);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.LAST_NAME_COLUMN, updatedAct.LastName);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.BIRTHDAY_COLUMN, updatedAct.Birthday);

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
                stmt.Connection = con;

                // set statement
                stmt.CommandText = "DELETE FROM via_cinema_schema.user_accounts WHERE user_accounts.email = @email;";

                // set statement parameters
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EMAIL_COLUMN, account.Email);

                // execute statement
                return stmt.ExecuteNonQuery() != 0;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            con?.Dispose();
        }

        /// <summary>
        ///     Singleton implementation
        /// </summary>
        /// <returns> an instance of a user account data access object </returns>
        public static IUserAccountDAO GetInstance()
        {
            return instance ?? (instance = new UserAccountDAO());
        }
        
    }
}
