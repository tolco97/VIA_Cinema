namespace UserAccountModel.Base
{
    using System;
    using System.Collections.Generic;
    using DAO;
    using VIA_Cinema.Util;
    using System.Data.Linq;

    public class UserAccountBase : IUserAccountBase
    {
        private readonly IDictionary<string, UserAccount> _userAccountCache = new Dictionary<string, UserAccount>();
        private readonly IUserAccountDAO _userAccountDao;

        public UserAccountBase(IUserAccountDAO userAccountDao)
        {
            _userAccountDao = userAccountDao;
        }

        /// <inheritdoc/>
        public UserAccount CreateAccount(string email, string userPassword, string firstName, string lastName,
            DateTime birthday)
        {
            // validate input
            Validator.ValidateTextualInput(email, firstName, lastName, userPassword);

            // check if user already exists
            if (UserExists(email)) throw new DuplicateKeyException($"User with account {email} already exists!");

            // create a new account in the database
            UserAccount newAccount = _userAccountDao.Create(email, userPassword, firstName, lastName, birthday);

            // cache the new account
            _userAccountCache[newAccount.Email] = newAccount;

            return newAccount;
        }

        /// <inheritdoc/>
        public bool Login(string userEmail, string userPassword)
        {
            // validate input
            Validator.ValidateTextualInput(userEmail, userPassword);

            // get the account 
            UserAccount userAccount = GetUserAccount(userEmail);

            // account does not exist
            if (userAccount == null) return false;

            // verify the password
            return userPassword.Equals(userAccount.UserPassword);
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
