using WebApplication_VIA_Cinema.Util;

namespace WebApplication_VIA_Cinema
{
    using System;
    using System.Threading.Tasks;
    using System.Web.UI;
    using VIA_Cinema.Util;
    using ViaCinemaServiceReference;
    using VIA_Cinema.UserAccountModel;

    public partial class CreateAccountPage : Page
    {
        private IViaCinemaService _client;

        #region CreateAccountPageConstants
        private const string InvalidInputMessage = "Invalid input";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // get web service client
            if (Session[SessionConstants.ServiceClientKey] == null)
                Session[SessionConstants.ServiceClientKey] = new ViaCinemaServiceClient();

            _client = (IViaCinemaService) Session[SessionConstants.ServiceClientKey];
        }

        protected void CreateAccountButtonOnClick(object sender, EventArgs e)
        {
            // get input
            string userEmail = emailTextBox.Text;
            string userPassword = passwordTextBox.Text;
            string firstName = firstNameTextBox.Text;
            string lastName = lastNameTextBox.Text;
            string[] birthdayString = birthdayTextBox.Text.Trim().Split('/'); // birthdayString[0] = day value,
                                                                              // birthdayString[1] = month value, 
                                                                              // birthdayString[2] = year value
            // validate input
            try
            {
                Validator.ValidateTextualInput(userEmail, userPassword, firstName, 
                    lastName, birthdayString[0], birthdayString[1], birthdayString[2]);
                ValidateAccountEmail(userEmail);
            }
            catch (ArgumentException)
            {
                ShowMessageBox(InvalidInputMessage);
                return;
            }
            catch (InvalidOperationException)
            {
                ShowMessageBox($"Account with user email {userEmail} already exists!");
                return;
            }
            
            // make request
            Task<UserAccount> registerRequest = _client.CreateAccountAsync(userEmail, userPassword, firstName, lastName,
                int.Parse(birthdayString[0]), int.Parse(birthdayString[1]), int.Parse(birthdayString[2]));

            // wait for response
            Task.WaitAll(registerRequest);

            // get response
            UserAccount registerResponse = registerRequest.Result;
            
            // save user email
            Session[SessionConstants.UserEmailKey] = registerResponse.Email;

            // set login flag to true
            Session[SessionConstants.IsLoggedInFlagKey] = true;

            // return to main page
            Response.Redirect("DefaultPage.aspx");
        }

        /// <summary>
        ///     Validates an account email
        /// </summary>
        /// <param name="email"> the email </param>
        private void ValidateAccountEmail(string email)
        {
            // make request
            Task<bool> userExistsRequest = _client.UserExistsAsync(email);

            // wait for response
            Task.WaitAll(userExistsRequest);

            // get response 
            bool accountExists = userExistsRequest.Result;

            // validate
            if (accountExists)
                throw new InvalidOperationException($"Account with email {email} already exists!");
        }

        /// <summary>
        ///     Shows a message box to the user
        /// </summary>
        /// <param name="message"> the message </param>
        private void ShowMessageBox(string message)
        {
            string s = "<SCRIPT language='javascript'>alert('" + message.Replace("\r\n", "\\n").Replace("'", "") +
                    "'); </SCRIPT>";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s);
        }
    }
}
