namespace VIA_Cinema.UserAccountModel.Base
{
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using DAO;
    using Util;

    public class UserAccountBase : IUserAccountBase
    {
        private readonly IDictionary<string, UserAccount> _userAccountCache = new Dictionary<string, UserAccount>();
        private readonly IUserAccountDAO _userAccountDao;

        public UserAccountBase(IUserAccountDAO userAccountDao)
        {
            _userAccountDao = userAccountDao;
        }

        /// <inheritdoc/>
        public UserAccount CreateAccount(string userEmail, string userPassword, string firstName, string lastName,
            DateTime birthday)
        {
            // validate input
            Validator.ValidateTextualInput(userEmail, firstName, lastName, userPassword);

            // check if user already exists
            if (UserExists(userEmail)) throw new DuplicateKeyException($"User with account {userEmail} already exists!");

            // create a new account in the database
            UserAccount newAccount = _userAccountDao.Create(userEmail, userPassword, firstName, lastName, birthday);

            // cache the new account
            _userAccountCache[newAccount.Email] = newAccount;

            return newAccount;
        }

        /// <inheritdoc/>
        public bool Login(string userEmail, string userPassword)
        {
            // validate input
            Validator.ValidateTextualInput(userEmail, userPassword);

            // check if user exists
            if (!UserExists(userEmail)) return false;

            // get the account 
            UserAccount userAccount = GetUserAccount(userEmail);

            // verify the password
            bool loginSuccessful = userPassword.Equals(userAccount.UserPassword);
            return loginSuccessful;
        }

        /// <inheritdoc/>
        public UserAccount GetUserAccount(string userEmail)
        {
            // validate input
            Validator.ValidateTextualInput(userEmail);

            // account is not cached in the memory
            if (!_userAccountCache.ContainsKey(userEmail))
            {
                // read it from the database
                UserAccount userAccount = _userAccountDao.Read(userEmail);

                // account does not exist
                if (userAccount == null) return null;

                // cache it
                _userAccountCache[userAccount.Email] = userAccount;
            }

            return _userAccountCache[userEmail];
        }

        /// <inheritdoc/>
        public IList<UserAccount> GetAllAccounts()
        {
            // read all accounts from the database
            ICollection<UserAccount> allUsers = _userAccountDao.ReadAll();

            // cache all accounts that have not been red so far
            foreach (UserAccount user in allUsers)
                if (!_userAccountCache.ContainsKey(user.Email))
                    _userAccountCache[user.Email] = user;

            return new List<UserAccount>(_userAccountCache.Values);
        }

        /// <inheritdoc/>
        public bool UserExists(string userEmail)
        {
            // user account is null, if it doesn't exist
            bool exists = GetUserAccount(userEmail) != null;

            return exists;
        }
    }
}
