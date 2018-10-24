﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using DNP1.ViaCinema.Model.UserAccountModel.DAO;
using DNP1.ViaCinema.Model.Util;

namespace DNP1.ViaCinema.Model.UserAccountModel.Base
{
    public class UserAccountBase : IUserAccountBase
    {
        private readonly IDictionary<string, UserAccount> _userAccountCache = new Dictionary<string, UserAccount>();
        private readonly IUserAccountDao _userAccountDao;

        public UserAccountBase(IUserAccountDao userAccountDao)
        {
            _userAccountDao = userAccountDao ?? throw new ArgumentNullException(nameof(userAccountDao), "userAccountDao does not exist");
        }

        /// <inheritdoc cref="IUserAccountBase.CreateAccount(string, string, string, string, DateTime)"/>
        public UserAccount CreateAccount(string userEmail, string userPassword, string firstName, string lastName,
            DateTime birthday)
        {
            // validate input
            Validator.ValidateTextualInput(userEmail, firstName, lastName, userPassword);

            // check if user already exists
            if (UserExists(userEmail))
            { 
                throw new DuplicateKeyException(userEmail, $"User with account {userEmail} already exists!");
            }

            // create a new account in the database
            UserAccount newAccount = _userAccountDao.Create(userEmail, userPassword, firstName, lastName, birthday);

            // cache the new account
            _userAccountCache[newAccount.Email] = newAccount;

            return newAccount;
        }

        /// <inheritdoc cref="IUserAccountBase.Login(string, string)"/>
        public bool Login(string userEmail, string userPassword)
        {
            // validate input
            Validator.ValidateTextualInput(userEmail, userPassword);

            // check if user exists
            if (!UserExists(userEmail))
            {
                return false;
            }

            // get the account 
            UserAccount userAccount = GetUserAccount(userEmail);

            if (userAccount == null)
            {
                return false;
            }
            
            // verify the password
            bool loginSuccessful = string.Equals(userPassword, userAccount.UserPassword);

            return loginSuccessful;
        }

        /// <inheritdoc cref="IUserAccountBase.GetUserAccount(string)"/>
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
                if (userAccount == null)
                {
                    return null;
                }

                // cache it
                _userAccountCache[userAccount.Email] = userAccount;
            }

            return _userAccountCache[userEmail];
        }

        /// <inheritdoc cref="IUserAccountBase.UserExists(string)"/>
        public bool UserExists(string userEmail)
        {
            // user account is null, if it doesn't exist
            bool exists = GetUserAccount(userEmail) != null;

            return exists;
        }
    }
}