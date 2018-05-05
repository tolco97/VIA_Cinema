namespace UserAccountModel.DAO
{
    /// <summary>
    ///     Stores constants used when manipulating the user accounts database entity
    /// </summary>
    public sealed class UserAccountEntityConstants
    {
        private UserAccountEntityConstants() { }
        
        public const string EMAIL_COLUMN = "email";
        public const string PASSWORD_COLUMN = "password";
        public const string FIRST_NAME_COLUMN = "first_name";
        public const string LAST_NAME_COLUMN = "last_name";
        public const string BIRTHDAY_COLUMN = "birthday";
    }
}
