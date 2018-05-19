namespace UserAccountModel.DAO
{
    /// <summary>
    ///     Stores constants used when manipulating the user accounts database entity
    /// </summary>
    public sealed class UserAccountEntityConstants
    {
        private UserAccountEntityConstants() {}
        
        public const string EmailColumn = "email";
        public const string PasswordColumn = "password";
        public const string FirstNameColumn = "first_name";
        public const string LastNameColumn = "last_name";
        public const string BirthdayColumn = "birthday";
    }
}
