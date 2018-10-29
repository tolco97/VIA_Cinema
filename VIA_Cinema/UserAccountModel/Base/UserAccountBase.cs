using System;
using System.Collections.Generic;
using System.Data.Linq;
using DNP1.ViaCinema.Model.UserAccountModel.DAO;
using DNP1.ViaCinema.Model.Util;

namespace DNP1.ViaCinema.Model.UserAccountModel.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class UserAccountBase : IUserAccountBase
    {
        private readonly IDictionary<string, UserAccount> _userAccountCache = new Dictionary<string, UserAccount>();
        private readonly IUserAccountDao _userAccountDao;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccountDao"></param>
        public UserAccountBase(IUserAccountDao userAccountDao)
        {
            _userAccountDao = userAccountDao ?? throw new ArgumentNullException(nameof(userAccountDao), "userAccountDao does not exist");
        }

        /// <inheritdoc cref="IUserAccountBase.CreateAccount(string, string, string, string, DateTime)"/>
        public UserAccount CreateAccount(string userEmail, string userPassword, string firstName, string lastName,
            DateTime birthday)
        {
            Validator.ValidateTextualInput(userEmail, firstName, lastName, userPassword);

            if (UserExists(userEmail))
            { 
                throw new DuplicateKeyException(userEmail, $"User with account {userEmail} already exists!");
            }

            UserAccount newAccount = _userAccountDao.Create(userEmail, userPassword, firstName, lastName, birthday);

            _userAccountCache[newAccount.Email] = newAccount;

            return newAccount;
        }

        /// <inheritdoc cref="IUserAccountBase.Login(string, string)"/>
        public bool Login(string userEmail, string userPassword)
        {
            Validator.ValidateTextualInput(userEmail, userPassword);

            if (!UserExists(userEmail))
            {
                return false;
            }

            UserAccount userAccount = GetUserAccount(userEmail);

            if (userAccount == null)
            {
                return false;
            }
            
            bool loginSuccessful = string.Equals(userPassword, userAccount.UserPassword);

            return loginSuccessful;
        }

        /// <inheritdoc cref="IUserAccountBase.GetUserAccount(string)"/>
        public UserAccount GetUserAccount(string userEmail)
        {
            // validate input
            Validator.ValidateTextualInput(userEmail);

            if (!_userAccountCache.ContainsKey(userEmail))
            {
                UserAccount userAccount = _userAccountDao.Read(userEmail);

                if (userAccount == null)
                {
                    return null;
                }
                
                _userAccountCache[userAccount.Email] = userAccount;
            }

            return _userAccountCache[userEmail];
        }

        /// <inheritdoc cref="IUserAccountBase.UserExists(string)"/>
        public bool UserExists(string userEmail)
        {
            bool exists = GetUserAccount(userEmail) != null;

            return exists;
        }
    }
}