﻿namespace UserAccountModel.DAO
{
    using System;
    using System.Collections.Generic;
    using Npgsql;

    public class UserAccountDAO : IUserAccountDAO
    {
        private static IUserAccountDAO instance = null;

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
                    string password = reader[UserAccountEntityConstants.PASSWORD_COLUMN] as string;
                    string firstName = reader[UserAccountEntityConstants.FIRST_NAME_COLUMN] as string;
                    string lastName = reader[UserAccountEntityConstants.LAST_NAME_COLUMN] as string;
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
                LinkedList<UserAccount> allAccounts = new LinkedList<UserAccount>(); // avoid array list resizing

                // execute statement
                using (NpgsqlDataReader reader = stmt.ExecuteReader())
                {
                    // loop through the reader and collect data
                    while (reader.Read())
                    {
                        // get values
                        string email = reader[UserAccountEntityConstants.EMAIL_COLUMN] as string;
                        string password = reader[UserAccountEntityConstants.PASSWORD_COLUMN] as string;
                        string firstName = reader[UserAccountEntityConstants.FIRST_NAME_COLUMN] as string;
                        string lastName = reader[UserAccountEntityConstants.LAST_NAME_COLUMN] as string;
                        DateTime birthday = (DateTime) reader[UserAccountEntityConstants.BIRTHDAY_COLUMN];

                        allAccounts.AddLast(new UserAccount(email, password, firstName, lastName, birthday));
                    }
                }

                return allAccounts;
            }
        }

        /// <inheritdoc/>
        public int Update(UserAccount updatedAct)
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
                return stmt.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        public int Delete(UserAccount account)
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
                return stmt.ExecuteNonQuery();
            }
        }

        /// <inheritdoc/>
        public void CloseConnection()
        {
            con?.Close();
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
