namespace WebApplication_VIA_Cinema
{
    using System;
    using System.Drawing;
    using System.Web.UI;

    public partial class Default : Page
    {
        private bool _isLoggedIn;

        protected void Page_Load(object sender, EventArgs e)
        {
            // get Login status
            if (Session[Constants.IsLoggedInFlagKey] == null)
                Session[Constants.IsLoggedInFlagKey] = false;

            _isLoggedIn = (bool) Session[Constants.IsLoggedInFlagKey];

            // restrict access to buttons depending on login status
            InitializeButtons();

            // set login label
            InitializeLoginStatusLabel();
        }

        protected void LoginButtonOnClick(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }

        protected void LogoutOnClick(object sender, EventArgs e)
        {
            // set login flag to false
            Session[Constants.IsLoggedInFlagKey] = false;

            // refresh page
            Response.Redirect(Request.RawUrl);
        }

        protected void RegisterAccountOnClick(object sender, EventArgs e)
        {
            Response.Redirect("CreateAccountPage.aspx");
        }

        protected void AllMoviesButtonOnClick(object sender, EventArgs e)
        {
            Response.Redirect("AllMoviesPage.aspx");
        }

        /// <summary>
        ///     Initializes all buttons
        /// </summary>
        private void InitializeButtons()
        {
            if (_isLoggedIn)
            {
                // when logged in, you can't use login and create account buttons
                loginButton.Enabled = false;
                createAccountButton.Enabled = false;
                allMoviesButton.Enabled = true;
                logoutButton.Enabled = true;
            }
            else
            {
                // when logged out, you can't use logout and all movies buttons
                loginButton.Enabled = true;
                createAccountButton.Enabled = true;
                allMoviesButton.Enabled = false;
                logoutButton.Enabled = false;
            }
        }

        /// <summary>
        ///     Initializes the login status label
        /// </summary>
        private void InitializeLoginStatusLabel()
        {
            if (_isLoggedIn)
            {
                string userEmail = (string) Session[Constants.UserEmailKey];
                isLoggedInLabel.Text = $"Hello, {userEmail}!";
                isLoggedInLabel.ForeColor = Color.Green;
            }
            else
            {
                isLoggedInLabel.Text = "You are not logged in!";
                isLoggedInLabel.ForeColor = Color.Red;
            }
        }
    }
}
