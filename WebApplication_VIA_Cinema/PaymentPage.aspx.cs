namespace WebApplication_VIA_Cinema
{
    using System;
    using System.Web.UI;
    using ViaPayServiceReference;
    using System.Threading.Tasks;
    using VIA_Cinema.Util;
    using ProjectionModel;
    using ViaCinemaServiceReference;
    using System.Collections.Generic;

    public partial class PaymentPage : Page
    {
        private IViaPayService payClient;
        private IViaCinemaService cinemaClient;

        protected void Page_Load(object sender, EventArgs e)
        {
            // instantiate payment web service client
            payClient = new ViaPayServiceClient();

            // get cinema client
            if (Session[Constants.SERVICE_CLIENT_KEY] == null)
                Session[Constants.SERVICE_CLIENT_KEY] = new ViaCinemaServiceClient();

            cinemaClient = Session[Constants.SERVICE_CLIENT_KEY] as IViaCinemaService;
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
                ShowMessageBox("Invalid input");
                return;
            }

            // calculate amount of money required: {number of seats * 30.0}. Each movie seat costs 30 DKK for all projections
            List<string> selectedSeatNumbers = Session[Constants.SELECTED_SEAT_NUMBERS_KEY] as List<string>;
            decimal totalPrice = Constants.SINGLE_MOVIE_TICKET_PRICE_DKK * selectedSeatNumbers.Count;

            // send transaction request
            Task<bool> transactionRequest = payClient.MakeTransactionAsync(creditCardNumber, pin, totalPrice);

            // wait for response
            Task.WaitAll(transactionRequest);

            // get response
            bool transactionSuccessful = transactionRequest.Result;

            // show message if transaction is unsuccessful
            if (!transactionSuccessful)
            { 
                ShowMessageBox("Transaction rejected");
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
            Projection projection = Session[Constants.PROJECTION_KEY] as Projection;
            string email = Session[Constants.USER_EMAIL_KEY] as string;
            List<string> seatNumbersList = Session[Constants.SELECTED_SEAT_NUMBERS_KEY] as List<string>;
            string seatNumberString = string.Join(", ", seatNumbersList);

            // request the booking of the seats
            Task<bool> bookRequest = cinemaClient.BookSeatAsync(projection.Id, email, seatNumberString);

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
