using System;
using System.Runtime.Serialization;

namespace DNP1.ViaCinema.Model.UserAccountModel
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class UserAccount
    {
        /// <summary>
        /// 
        /// </summary>
        public UserAccount() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userPassword"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="birthday"></param>
        public UserAccount(string email, string userPassword, string firstName, string lastName, DateTime birthday)
        {
            Email = email;
            UserPassword = userPassword;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public string Email { get; set; } // PK

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public string UserPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public DateTime Birthday { get; set; }

        /// <summary>
        /// <inheritdoc cref="object.ToString"/>
        /// </summary>
        public override string ToString()
        {
            return $"UserAccount [Email: {Email}, UserPassword: {UserPassword}," +
                   $" FirstName: {FirstName}, LastName: {LastName}, Birthday: {Birthday}]";
        }
    }
}