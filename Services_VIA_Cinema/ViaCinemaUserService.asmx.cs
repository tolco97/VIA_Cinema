using System;
using System.ComponentModel;
using System.Web.Services;
using DNP1.ViaCinema.Model.UserAccountModel;
using DNP1.ViaCinema.Model.UserAccountModel.Base;
using DNP1.ViaCinema.Model.UserAccountModel.DAO;

namespace DNP1.ViaCinema.Services
{
    /// <summary>
    /// Summary description for ViaCinemaUserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ViaCinemaUserService : WebService
    {
        private readonly IUserAccountBase _userAccountBase = new UserAccountBase(UserAccountDao.GetInstance());

        /// <see cref="IUserAccountBase.Login(string, string)"/>
        [WebMethod]
        public bool Login(string email, string userPassword)
        {
            bool loginSuccessful = _userAccountBase.Login(email, userPassword);

            return loginSuccessful;
        }

        /// <see cref="IUserAccountBase.CreateAccount(string, string, string, string, DateTime)"/>
        [WebMethod]
        public UserAccount CreateAccount(string email, string userPassword, string firstName, string lastName,
            int dayOfBirth, int monthOfBirth, int yearOfBirth)
        {
            DateTime dateOfBirth = new DateTime(yearOfBirth, monthOfBirth, dayOfBirth);

            UserAccount newUser = _userAccountBase.CreateAccount(email, userPassword, firstName, lastName, dateOfBirth);

            return newUser;
        }

        /// <see cref="IUserAccountBase.UserExists(string)"/>
        [WebMethod]
        public bool UserExists(string userEmail)
        {
            bool exists = _userAccountBase.UserExists(userEmail);

            return exists;
        }

    }
}
