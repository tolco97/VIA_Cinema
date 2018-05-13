namespace UserAccountModel.Base
{
    using System;
    using System.Collections.Generic;
    using DAO;

    public class UserAccountBase : IUserAccountBase
    {
        private readonly IDictionary<string, UserAccount> userAccountCache = new Dictionary<string, UserAccount>();
        private readonly IUserAccountDAO userAccountDAO;

        public UserAccountBase(IUserAccountDAO userAccountDAO)
        {
            this.userAccountDAO = userAccountDAO;
        }

        /// <inheritdoc/>
        public UserAccount CreateAccount(string email, string userPassword, string firstName, string lastName,
            DateTime birthday)
        {
            // create a new account in the database
            UserAccount newAccount = userAccountDAO.Create(email, userPassword, firstName, lastName, birthday);

            // cache the new account
            userAccountCache[newAccount.Email] = newAccount;

            return newAccount;
        }

        /// <inheritdoc/>
        public bool Login(string email, string userPassword)
        {
            // get the account 
            UserAccount userAccount = GetUserAccount(email);

            // account does not exist
            if (userAccount == null) return false;

            // verify the password
            return userPassword.Equals(userAccount.UserPassword);
        }

        /// <inheritdoc/>
        public UserAccount GetUserAccount(string email)
        {
            // account is not cached in the memory
            if (!userAccountCache.ContainsKey(email))
            {
                // read it from the database
                UserAccount userAccount = userAccountDAO.Read(email);

                // account does not exist
                if (userAccount == null) return null;

                // cache it
                userAccountCache[userAccount.Email] = userAccount;
            }

            return userAccountCache[email];
        }

        /// <inheritdoc/>
        public IList<UserAccount> GetAllAccounts()
        {
            // read all accounts from the database
            ICollection<UserAccount> allUsers = userAccountDAO.ReadAll();

            // cache all accounts that have not been red so far
            foreach (UserAccount user in allUsers)
                if (!userAccountCache.ContainsKey(user.Email))
                    userAccountCache[user.Email] = user;

            return new List<UserAccount>(userAccountCache.Values);
        }

        /// <inheritdoc/>
        public bool UserExists(string email)
        {
            // user account is null, if it doesn't exist
            bool exists = GetUserAccount(email) != null;

            return exists;
        }
    }
}
