namespace WebApplication_VIA_Cinema
{
    using System;
    using System.Threading.Tasks;
    using System.Web.UI;
    using ViaCinemaServiceReference;
    using VIA_Cinema.Util;

    public partial class Login : Page
    {
        private IViaCinemaService client;

        protected void Page_Load(object sender, EventArgs e)
        {
            // get web service client
            if (Session[Constants.SERVICE_CLIENT_KEY] == null)
                Session[Constants.SERVICE_CLIENT_KEY] = new ViaCinemaServiceClient();

            client = (IViaCinemaService) Session[Constants.SERVICE_CLIENT_KEY];
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
                ShowMessageBox("Invalid input!"); // show error message to user
                return;
            }

            // send login request
            Task<bool> loginRequest = client.LoginAsync(email, password);

            // wait for response from server
            Task.WaitAll(loginRequest);

            // get response & save it in the session
            bool loginSuccessful = loginRequest.Result;
            Session[Constants.IS_LOGGED_IN_FLAG_KEY] = loginSuccessful;

            // react to response
            if (loginSuccessful)
            {
                // save the email
                Session[Constants.USER_EMAIL_KEY] = email; 

                // back to main page
                Response.Redirect("DefaultPage.aspx"); 
            }
            else
                // show error message
                ShowMessageBox("Wrong username or password");
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
