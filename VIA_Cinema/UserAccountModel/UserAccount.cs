namespace VIA_Cinema.UserAccountModel
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class UserAccount
    {
        public UserAccount() {}

        public UserAccount(string email, string userPassword, string firstName, string lastName, DateTime birthday)
        {
            Email = email;
            UserPassword = userPassword;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }

        [DataMember] public string Email { get; set; } // PK

        [DataMember] public string UserPassword { get; set; }

        [DataMember] public string FirstName { get; set; }

        [DataMember] public string LastName { get; set; }

        [DataMember] public DateTime Birthday { get; set; }
        
        public override string ToString()
        {
            return $"UserAccount [Email: {Email}, UserPassword: {UserPassword}," +
                   $" FirstName: {FirstName}, LastName: {LastName}, Birthday: {Birthday}]";
        }
    }
}
