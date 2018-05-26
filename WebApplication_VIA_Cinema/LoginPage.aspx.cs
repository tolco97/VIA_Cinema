namespace WebApplication_VIA_Cinema
{
    using System;
    using System.Threading.Tasks;
    using System.Web.UI;
    using ViaCinemaServiceReference;
    using VIA_Cinema.Util;

    public partial class Login : Page
    {
        private IViaCinemaService _client;

        #region LoginPageConstants

        private const string WrongInputMessage = "Wrong username or password!";
        private const string InvalidInputMessage = "Invalid input!";
        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // get web service client
            if (Session[SessionConstants.ServiceClientKey] == null)
                Session[SessionConstants.ServiceClientKey] = new ViaCinemaServiceClient();

            _client = (IViaCinemaService) Session[SessionConstants.ServiceClientKey];
        }

        protected void LoginButtonOnClick(object sender, EventArgs e)
        {
            // get user input
            string email = emailTextField.Text;
            string password = passwordTextField.Text;

            // validate input
            try
            {
                Validator.ValidateTextualInput(email, password);
            }
            catch (ArgumentException)
            {
                ShowMessageBox(InvalidInputMessage); 
                return;
            }

            // send login request
            Task<bool> loginRequest = _client.LoginAsync(email, password);

            // wait for response from server
            Task.WaitAll(loginRequest);

            // get response & save it in the session
            bool loginSuccessful = loginRequest.Result;
            Session[SessionConstants.IsLoggedInFlagKey] = loginSuccessful;

            // react to response
            if (loginSuccessful)
            {
                // save the email
                Session[SessionConstants.UserEmailKey] = email; 

                // back to main page
                Response.Redirect("DefaultPage.aspx"); 
            }
            else
                // show error message
                ShowMessageBox(WrongInputMessage);
        }

        /// <summary>
        ///     Shows a message box to the user
        /// </summary>
        /// <param name="message"> the message </param>
        private void ShowMessageBox(string message)
        {
            string script = "<SCRIPT language='javascript'>alert('" + message.Replace("\r\n", "\\n").Replace("'", "") +
                    "'); </SCRIPT>";
            Type cstype = GetType();
            ClientScriptManager clientScriptManager = Page.ClientScript;
            clientScriptManager.RegisterClientScriptBlock(cstype, script, script);
        }
    }
}
