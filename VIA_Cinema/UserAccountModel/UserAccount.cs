namespace UserAccountModel
{
    using System;
    using System.Collections.Generic;
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
        
        public override bool Equals(object obj)
        {
            if (!(obj is UserAccount))
                return false;

            UserAccount other = (UserAccount) obj;
            return Email.Equals(other.Email) && UserPassword.Equals(other.UserPassword) &&
                   FirstName.Equals(other.FirstName) && LastName.Equals(other.LastName) &&
                   Birthday.Equals(other.Birthday);
        }

        public override int GetHashCode()
        {
            int hashCode = -869368458;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserPassword);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + Birthday.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"UserAccount [Email: {Email}, UserPassword: {UserPassword}," +
                   $" FirstName: {FirstName}, LastName: {LastName}, Birthday: {Birthday}]";
        }
    }
}
