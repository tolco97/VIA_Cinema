namespace UserAccountModel.Base
{
    using System;
    using System.Collections.Generic;
    using DAO;
    using VIA_Cinema.Util;

    public class UserAccountBase : IUserAccountBase
    {
        private readonly IDictionary<string, UserAccount> _userAccountCache = new Dictionary<string, UserAccount>();
        private readonly IUserAccountDAO _userAccountDao;

        public UserAccountBase(IUserAccountDAO userAccountDAO)
        {
            this._userAccountDao = userAccountDAO;
        }

        /// <inheritdoc/>
        public UserAccount CreateAccount(string email, string userPassword, string firstName, string lastName,
            DateTime birthday)
        {
            // validate input
            Validator.ValidateTextualInput(email, firstName, lastName, userPassword);

            // create a new account in the database
            UserAccount newAccount = _userAccountDao.Create(email, userPassword, firstName, lastName, birthday);

            // cache the new account
            _userAccountCache[newAccount.Email] = newAccount;

            return newAccount;
        }

        /// <inheritdoc/>
        public bool Login(string email, string userPassword)
        {
            // validate input
            Validator.ValidateTextualInput(email, userPassword);

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
            // validate input
            Validator.ValidateTextualInput(email);

            // account is not cached in the memory
            if (!_userAccountCache.ContainsKey(email))
            {
                // read it from the database
                UserAccount userAccount = _userAccountDao.Read(email);

                // account does not exist
                if (userAccount == null) return null;

                // cache it
                _userAccountCache[userAccount.Email] = userAccount;
            }

            return _userAccountCache[email];
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
        public bool UserExists(string email)
        {
            // user account is null, if it doesn't exist
            bool exists = GetUserAccount(email) != null;

            return exists;
        }
    }
}
