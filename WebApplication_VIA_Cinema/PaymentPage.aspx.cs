namespace WebApplication_VIA_Cinema
{
    using System;
    using System.Web.UI;
    using ViaPayServiceReference;
    using System.Threading.Tasks;
    using VIA_Cinema.Util;
    using ViaCinemaServiceReference;
    using System.Collections.Generic;
    using VIA_Cinema.ProjectionModel;

    public partial class PaymentPage : Page
    {
        private IViaPayService _payClient;
        private IViaCinemaService _cinemaClient;

        #region PaymentPageConstants

        private const string InvalidInputMessage = "Invalid input";
        private const string TransactionRejectedMessage = "Transaction rejected";

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // instantiate payment web service client
            _payClient = new ViaPayServiceClient();

            // get cinema client
            if (Session[SessionConstants.ServiceClientKey] == null)
                Session[SessionConstants.ServiceClientKey] = new ViaCinemaServiceClient();

            _cinemaClient = (IViaCinemaService) Session[SessionConstants.ServiceClientKey];
        }

        protected void PayButtonOnClick(object sender, EventArgs e)
        {
            // get user input
            string creditCardNumber = creditCardTextBox.Text;
            string pin = pinTextBox.Text;

            // validate input
            try
            {
                Validator.ValidateTextualInput(creditCardNumber, pin);
            }
            catch (ArgumentException)
            {
                ShowMessageBox(InvalidInputMessage);
                return;
            }

            // calculate amount of money required: {number of seats * 30.0}. Each movie seat costs 30 DKK for all projections
            List<string> selectedSeatNumbers = (List<string>) Session[SessionConstants.SelectedSeatNumbersKey];
            decimal totalPrice = SessionConstants.SingleMovieTicketPriceDkk * selectedSeatNumbers.Count;

            // send transaction request
            Task<bool> transactionRequest = _payClient.MakeTransactionAsync(creditCardNumber, pin, totalPrice);

            // wait for response
            Task.WaitAll(transactionRequest);

            // get response
            bool transactionSuccessful = transactionRequest.Result;

            // show message if transaction is unsuccessful
            if (!transactionSuccessful)
            { 
                ShowMessageBox(TransactionRejectedMessage);
                return;
            }

            // if payment is successful, seats are booked
            BookSeats();
        }

        /// <summary>
        ///     Books all selected seats
        /// </summary>
        private void BookSeats()
        {
            // get needed data
            Projection projection = (Projection) Session[SessionConstants.ProjectionKey];
            string email = (string) Session[SessionConstants.UserEmailKey];
            List<string> seatNumbersList = (List<string>) Session[SessionConstants.SelectedSeatNumbersKey];
            string seatNumberString = string.Join(", ", seatNumbersList);

            // request the booking of the seats
            Task<bool> bookRequest = _cinemaClient.BookSeatAsync(projection.Id, email, seatNumberString);

            // wait for response
            Task.WaitAll(bookRequest);
            
            // back to main page
            Response.Redirect("DefaultPage.aspx");
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
