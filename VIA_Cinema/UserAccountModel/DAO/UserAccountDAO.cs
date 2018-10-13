﻿using System;
using Npgsql;

namespace VIA_Cinema.UserAccountModel.DAO
{
    public class UserAccountDAO : IUserAccountDao
    {
        private static IUserAccountDao _instance;

        private readonly NpgsqlConnection _con;

        private UserAccountDAO()
        {
            _con = new NpgsqlConnection("Server=localhost;User Id=postgres;" +
                                        "Password=password;Database=via_cinema_system;");
            _con.Open();
        }

        /// <inheritdoc />
        public UserAccount Create(string userEmail, string userPassword, string firstName, string lastName,
            DateTime birthday)
        {
            using (var stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText =
                    "INSERT INTO via_cinema_schema.user_accounts (email, password, first_name," +
                    " last_name, birthday) VALUES (@email, @password, @first_name, @last_name, @birthday);";

                // set statement parameters
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EmailColumn, userEmail);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.PasswordColumn, userPassword);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.FirstNameColumn, firstName);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.LastNameColumn, lastName);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.BirthdayColumn, birthday);

                // execute statement
                stmt.ExecuteNonQuery();

                return new UserAccount(userEmail, userPassword, firstName, lastName, birthday);
            }
        }

        /// <inheritdoc />
        public UserAccount Read(string userEmail)
        {
            using (var stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText =
                    "SELECT * FROM via_cinema_schema.user_accounts WHERE via_cinema_schema.user_accounts.email = @email;";

                // set parameters
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EmailColumn, userEmail);

                // execute statement
                using (var reader = stmt.ExecuteReader())
                {
                    // the account does not exist
                    if (!reader.Read()) return null;

                    // get values
                    var password = (string) reader[UserAccountEntityConstants.PasswordColumn];
                    var firstName = (string) reader[UserAccountEntityConstants.FirstNameColumn];
                    var lastName = (string) reader[UserAccountEntityConstants.LastNameColumn];
                    var birthday = (DateTime) reader[UserAccountEntityConstants.BirthdayColumn];

                    return new UserAccount(userEmail, password, firstName, lastName, birthday);
                }
            }
        }

        /// <inheritdoc />
        public bool Update(UserAccount account)
        {
            using (var stmt = new NpgsqlCommand())
            {
                // set connection
                stmt.Connection = _con;

                // set statement
                stmt.CommandText = "UPDATE via_cinema_schema.user_accounts SET password = @password , " +
                                   "first_name = @first_name, last_name = @last_name, birthday = @birthday" +
                                   " WHERE email = @email;";

                // set the statement parameters
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.PasswordColumn, account.UserPassword);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.FirstNameColumn, account.FirstName);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.LastNameColumn, account.LastName);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.BirthdayColumn, account.Birthday);
                stmt.Parameters.AddWithValue(UserAccountEntityConstants.EmailColumn, account.Email);

                // execute statement
                return stmt.ExecuteNonQuery() != 0;
            }
        }

        /// <inheritdoc />
        public bool Delete(UserAccount account)
        {
            using (var stmt = new NpgsqlCommand())
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
        public static IUserAccountDao GetInstance()
        {
            return _instance ?? (_instance = new UserAccountDAO());
        }
    }
}