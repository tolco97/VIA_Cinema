﻿namespace UserAccountModel.DAO
{
    using System.Collections.Generic;
    using System;

    public interface IUserAccountDAO : IDisposable
    {
        /// <summary>
        ///     Creates a new user account database entry from the user account parameter
        /// </summary>
        /// <param name="email"> the email of the user </param>
        /// <param name="userPassword"> the password of the user </param>
        /// <param name="firstName"> the first name of the user </param>
        /// <param name="lastName"> the last name of the user </param>
        /// <param name="birthday"> the birthday of the user </param>
        /// <returns> a user account object </returns>
        UserAccount Create(string email, string userPassword, string firstName, string lastName,
            DateTime birthday);

        /// <summary>
        ///     Reads a user account database entry from the user accounts entry that matches
        ///     the email passed as a parameter
        /// </summary>
        /// <param name="email"> the email </param>
        /// <returns> a user account object </returns>
        UserAccount Read(string email);

        /// <summary>
        ///     Reads all user account entries from the database
        /// </summary>
        /// <returns> a collection of all user accounts </returns>
        ICollection<UserAccount> ReadAll();

        /// <summary>
        ///     Updates a user account database entry that matches the account email
        ///     of the account passed as a parameter
        /// </summary>
        /// <param name="updatedAct"> the updated user account </param>
        /// <returns> true, if the update operation has affected at least 1 database row. Otherwise, false </returns>
        bool Update(UserAccount updatedAct);

        /// <summary>
        ///     Deletes a user account database entry that matches the account email
        ///     of the account passed as a parameter
        /// </summary>
        /// <param name="account"> the account </param>
        /// <returns> true, if the delete operation has affected at least 1 database row. Otherwise, false </returns>
        bool Delete(UserAccount account);

    }
}
