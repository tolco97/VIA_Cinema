namespace WebApplication_VIA_Cinema
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ProjectionModel;

    public partial class BookSeatsPage : Page
    {
        private bool isLoggedIn;

        protected void Page_Load(object sender, EventArgs e)
        {
            // get logged in status
            if (Session[Constants.IS_LOGGED_IN_FLAG_KEY] == null)
                Session[Constants.IS_LOGGED_IN_FLAG_KEY] = false;

            isLoggedIn = (bool) Session[Constants.IS_LOGGED_IN_FLAG_KEY];

            // get projection
            Projection projection = (Projection) Session[Constants.PROJECTION_KEY];
            
            // set text to the labels
            InitializeLabels(projection);

            // populate checkbox list
            InitializeCheckBoxList(projection);
        }

        protected void BookSeatsButtonOnClick(object sender, EventArgs e)
        {
            // check if user is logged in
            if (!isLoggedIn)
            {
                ShowMessageBox("You need to log in first!");
                return;
            }

            // get a list of all checked elements in the checkbox list
            List<string> selectedSeatNumbers = seatNumCheckBoxList.Items.Cast<ListItem>()
                .Where(listItem => listItem.Selected)
                .Select(listItem => listItem.Value)
                .ToList();

            // save seat numbers for payment page
            Session[Constants.SELECTED_SEAT_NUMBERS_KEY] = selectedSeatNumbers;

            // go to payment page
            Response.Redirect("PaymentPage.aspx");
        }

        /// <summary>
        ///     Initializes all the projected movie and movie start time labels
        /// </summary>
        /// <param name="proj"> the projection </param>
        private void InitializeLabels(Projection proj)
        {
            // set projection movie name and start time
            projectionMovieNameLbl.Text = $"Projected Movie: {proj?.ProjectedMovie.Name}";
            projectionStartTimeLbl.Text = $"Starts: {proj?.MovieStartTime:dddd  dd  MMMM HH:mm}";
        }

        /// <summary>
        ///     Initializes the check box list with the seats from the projection passed as a parameter
        /// </summary>
        /// <param name="proj"> the projection </param>
        private void InitializeCheckBoxList(Projection proj)
        {
            // avoid redundant repopulation
            if (seatNumCheckBoxList.Items.Count != 0) return;
            
            // get all numbers of the available seats
            List<int> availableSeatNumbers = GetAvailableSeatNumbers(proj);

            // populate checkbox list
            foreach (int seatNum in availableSeatNumbers)
                seatNumCheckBoxList.Items.Add(new ListItem(seatNum.ToString()));
        }

        /// <summary>
        ///     Gets a list of all the numbers of the available seats in a projection
        /// </summary>
        /// <param name="proj"> the projection </param>
        /// <returns> a list of integers </returns>
        private List<int> GetAvailableSeatNumbers(Projection proj)
        {
            // get all seat numbers of seats that are unavailable
            List<int> unavailableSeatsList = proj.Seats.Select(seat => seat.SeatNumber).ToList();

            // get all seat numbers of seats that are available. seat numbers can be between 1 and 30
            List<int> availableSeatsList = Enumerable.Range(1, Constants.MAX_PROJECTION_AUDIENCE_SIZE).Except(unavailableSeatsList).ToList();

            return availableSeatsList;
        }

        /// <summary>
        ///     Shows a message box to the user
        /// </summary>
        /// <param name="message"> the message </param>
        private void ShowMessageBox(string message)
        {
            // script
            string s = "<SCRIPT language='javascript'>alert('" + message.Replace("\r\n", "\\n").Replace("'", "") +
                    "'); </SCRIPT>";

            // get type of current instance
            Type cstype = GetType();

            // get script manager
            ClientScriptManager cs = Page.ClientScript;

            // execute script
            cs.RegisterClientScriptBlock(cstype, s, s);
        }

    }
}
