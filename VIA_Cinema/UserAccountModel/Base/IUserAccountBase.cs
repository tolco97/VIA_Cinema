using System;

namespace DNP1.ViaCinema.Model.UserAccountModel.Base
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserAccountBase
    {
        /// <summary>
        ///     Creates a new account in the system
        /// </summary>
        /// <param name="email"> the email </param>
        /// <param name="userPassword"> the password </param>
        /// <param name="firstName"> the first name of the customer </param>
        /// <param name="lastName"> the last name of the customer </param>
        /// <param name="birthday"> the date of birth of the customer </param>
        /// <returns> a user account object </returns>
        UserAccount CreateAccount(string email, string userPassword, string firstName, string lastName,
            DateTime birthday);

        /// <summary>
        ///     Checks if there is a user account in the system with the email
        ///     and password passed as parameters
        /// </summary>
        /// <param name="userEmail"> the email </param>
        /// <param name="userPassword"> the password </param>
        /// <returns>
        ///     true, if there is a user account with the
        ///     email and password parameters. Otherwise, false
        /// </returns>
        bool Login(string userEmail, string userPassword);

        /// <summary>
        ///     Retrieves the user account in the system that matches the email
        ///     passed as a parameter
        /// </summary>
        /// <param name="userEmail"> the email </param>
        /// <returns> a user account object </returns>
        UserAccount GetUserAccount(string userEmail);

        /// <summary>
        ///     Checks if a user with the email passed as a parameter exists
        ///     in the system
        /// </summary>
        /// <param name="userEmail"> the email </param>
        /// <returns>
        ///     true, if a user account with the email exists
        ///     in the system. Otherwise, false
        /// </returns>
        bool UserExists(string userEmail);
    }
}