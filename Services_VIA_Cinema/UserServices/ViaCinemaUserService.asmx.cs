using System;
using System.ComponentModel;
using System.Web.Services;
using VIA_Cinema.UserAccountModel;
using VIA_Cinema.UserAccountModel.Base;
using VIA_Cinema.UserAccountModel.DAO;

namespace Services_VIA_Cinema.UserServices
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
        private readonly IUserAccountBase _userAccountBase = new UserAccountBase(UserAccountDAO.GetInstance());

        [WebMethod]
        public bool Login(string email, string userPassword)
        {
            bool loginSuccessful = _userAccountBase.Login(email, userPassword);

            return loginSuccessful;
        }

        [WebMethod]
        public UserAccount CreateAccount(string email, string userPassword, string firstName, string lastName,
            int dayOfBirth, int monthOfBirth, int yearOfBirth)
        {
            DateTime dateOfBirth = new DateTime(yearOfBirth, monthOfBirth, dayOfBirth);

            UserAccount newUser = _userAccountBase.CreateAccount(email, userPassword, firstName, lastName, dateOfBirth);

            return newUser;
        }

        [WebMethod]
        public bool UserExists(string userEmail)
        {
            bool exists = _userAccountBase.UserExists(userEmail);

            return exists;
        }

    }
}
