﻿namespace WebApplication_VIA_Cinema
{
    using System;
    using System.Threading.Tasks;
    using System.Web.UI;
    using VIA_Cinema.Util;
    using ViaCinemaServiceReference;
    using UserAccountModel;

    public partial class CreateAccountPage : Page
    {
        private IViaCinemaService client;

        protected void Page_Load(object sender, EventArgs e)
        {
            // get web service client
            if (Session[Constants.SERVICE_CLIENT_KEY] == null)
                Session[Constants.SERVICE_CLIENT_KEY] = new ViaCinemaServiceClient();

            client = (IViaCinemaService) Session[Constants.SERVICE_CLIENT_KEY];
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
                Validator.ValidateTextualInput(userEmail, userPassword, firstName, lastName);
                ValidateAccountEmail(userEmail);
            }
            catch (ArgumentException)
            {
                ShowMessageBox("Invalid input");
                return;
            }
            catch (InvalidOperationException)
            {
                ShowMessageBox($"Account with user email {userEmail} already exists!");
                return;
            }
            
            // make request
            Task<UserAccount> registerRequest = client.CreateAccountAsync(userEmail, userPassword, firstName, lastName,
                int.Parse(birthdayString[0]), int.Parse(birthdayString[1]), int.Parse(birthdayString[2]));

            // wait for response
            Task.WaitAll(registerRequest);

            // get response
            UserAccount registerResponse = registerRequest.Result;
            
            // save user email
            Session[Constants.USER_EMAIL_KEY] = registerResponse.Email;

            // set login flag to true
            Session[Constants.IS_LOGGED_IN_FLAG_KEY] = true;

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
            Task<bool> userExistsRequest = client.UserExistsAsync(email);

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
