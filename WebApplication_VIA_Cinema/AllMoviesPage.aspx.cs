namespace WebApplication_VIA_Cinema
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ViaCinemaServiceReference;
    using VIA_Cinema.MovieModel;
    using VIA_Cinema.ProjectionModel;

    public partial class AllMovies : Page
    {
        private IViaCinemaService _client;
        private IDictionary<int, Projection> _projectionsOnDisplay;
        private bool _isLoggedIn;

        #region AllMoviesPageConstants

        private const string ColorPropName = "color";
        private const string ColorPropValue = "white";

        private const string MovieNameHeader = "Movie Name";
        private const string MovieGenreHeader = "Movie Genre";
        private const string MovieDurationHeader = "Movie Duration";
        private const string ProjectionStartHeader = "Projection Start";
        private const string AvailableTicketsHeader = "Available Tickets";

        private const string StartTimeDateFormat = "dddd  dd  MMMM HH:mm";
        private const int BorderWidthValue = 1;

        private const string BookSeatsButtonName = "Book Seats";
        private const string SoldOutButtonName = "Sold Out";

        private const string LoginRequirementMessage = "You need to log in first!";

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // initialize cache
            _projectionsOnDisplay = new Dictionary<int, Projection>();

            // get web service client
            if (Session[SessionConstants.ServiceClientKey] == null)
                Session[SessionConstants.ServiceClientKey] = new ViaCinemaServiceClient();

            _client = (IViaCinemaService) Session[SessionConstants.ServiceClientKey];

            // get logged in status 
            if (Session[SessionConstants.IsLoggedInFlagKey] == null)
                Session[SessionConstants.IsLoggedInFlagKey] = false;

            _isLoggedIn = (bool) Session[SessionConstants.IsLoggedInFlagKey];

            // page loaded for the first time
            if (!Page.IsPostBack)
                InitializeDropdownMenu();

            // populate table with projections depending on selected dropdown item
            PopulateProjectionsTable(movieDropdownMenu.SelectedValue);
        }

        protected void BookButtonOnClick(object sender, EventArgs e)
        {
            // login check
            if (!_isLoggedIn)
            {
                ShowMessageBox(LoginRequirementMessage);
                return;
            }

            // get event source
            Button button = (Button) sender;

            // get id of pressed button
            int buttonId = int.Parse(button.ID);

            // save projection selected by the user in the session
            Session[SessionConstants.ProjectionKey] = _projectionsOnDisplay[buttonId];

            // redirect to booking page
            Response.Redirect("BookSeatsPage.aspx");
        }
        
        protected void MovieDropdownSelectedIndexChanged(object sender, EventArgs e) {}

        /// <summary>
        ///     Initializes the dropdown menu with all movie values
        /// </summary>
        private void InitializeDropdownMenu()
        {
            // request all movies
            Task<Movie[]> allMoviesRequest = _client.GetAllMoviesAsync();

            // wait for response
            Task.WaitAll(allMoviesRequest);

            // get response
            Movie[] allMoviesResponse = allMoviesRequest.Result;

            // add all movies to the dropdown menu
            foreach (Movie movie in allMoviesResponse)
                movieDropdownMenu.Items.Add(new ListItem(movie.Name));
        }

        /// <summary>
        ///     Adds the header row to the table with projections
        /// </summary>
        private void AddHeaderRow()
        {
            // create header row
            TableHeaderRow headerRow = new TableHeaderRow
            {
                BackColor = Color.Black
            };

            // movie name header cell
            TableCell movieNameHeaderCell = new TableCell
            {
                Text = MovieNameHeader,
                HorizontalAlign = HorizontalAlign.Center,
                Style = {[ColorPropName] = ColorPropValue}
            };

            // movie genre header cell
            TableCell movieGenreHeaderCell = new TableCell
            {
                Text = MovieGenreHeader,
                HorizontalAlign = HorizontalAlign.Center,
                Style = {[ColorPropName] = ColorPropValue}
            };

            // movie duration header cell
            TableCell movieDurationHeaderCell = new TableCell
            {
                Text = MovieDurationHeader,
                HorizontalAlign = HorizontalAlign.Center,
                Style = {[ColorPropName] = ColorPropValue}
            };

            // movie projection start header cell
            TableCell projectionStartHeaderCell = new TableCell
            {
                Text = ProjectionStartHeader,
                HorizontalAlign = HorizontalAlign.Center,
                Style = {[ColorPropName] = ColorPropValue}
            };

            // available tickets header cell
            TableCell seatsAvailableHeaderCell = new TableCell
            {
                Text = AvailableTicketsHeader,
                HorizontalAlign = HorizontalAlign.Center,
                Style = {[ColorPropName] = ColorPropValue}
            };

            // add all cells to a row
            headerRow.Cells.AddRange(new[]
            {
                movieNameHeaderCell, movieGenreHeaderCell, movieDurationHeaderCell,
                projectionStartHeaderCell, seatsAvailableHeaderCell
            });

            // add row to table
            projectionTable.Rows.Add(headerRow);
        }

        /// <summary>
        ///     Populates the projections table with movies that match the name as parameter
        /// </summary>
        /// <param name="movieName"> the name of the movie </param>
        private void PopulateProjectionsTable(string movieName)
        {
            // request all projections
            Task<Projection[]> projectionsRequest = _client.GetProjectionsAsync(movieName);

            // wait for response
            Task.WaitAll(projectionsRequest);

            // get response
            Projection[] projections = projectionsRequest.Result;

            // add the header row in the table
            AddHeaderRow();

            // populate table with movies data
            foreach (Projection proj in projections)
            {
                // save the projection for the on click button listener
                _projectionsOnDisplay[proj.Id] = proj;

                // create row
                TableRow row = new TableRow();

                // populate movie name cell
                TableCell movieNameCell = new TableCell
                {
                    Text = proj.ProjectedMovie.Name,
                    BorderColor = Color.Black,
                    BorderWidth = BorderWidthValue
                };

                // populate movie genre cell
                TableCell movieGenreCell = new TableCell
                {
                    Text = proj.ProjectedMovie.Genre,
                    BorderColor = Color.Black,
                    BorderWidth = BorderWidthValue
                };

                // populate movie duration cell
                TableCell movieDurationCell = new TableCell
                {
                    Text = $"{proj.ProjectedMovie.DurationMinuites} minuites",
                    BorderColor = Color.Black,
                    BorderWidth = BorderWidthValue
                };

                // populate movie projection start cell
                TableCell projectionStartCell =
                    new TableCell
                    {
                        Text = proj.MovieStartTime.ToString(StartTimeDateFormat),
                        BorderColor = Color.Black,
                        BorderWidth = BorderWidthValue
                    };

                // calculate number of available seats cell and populate seats cell
                int numAvailableSeats = GetNumAvailableSeats(proj);
                TableCell seatsAvailableCell = new TableCell
                {
                    Text = numAvailableSeats.ToString(),
                    BorderColor = Color.Black,
                    ForeColor = numAvailableSeats > 0 ? Color.Black : Color.Red, // button text color is red, if there are no more seats available
                    BorderWidth = BorderWidthValue
                };

                TableCell bookButtonCell = new TableCell();
                Button bookButton = new Button
                {
                    Text =  numAvailableSeats > 0 ? BookSeatsButtonName : SoldOutButtonName, // button text is "Sold out", if all seats are unavailable, otherwise "Book Seats"
                    Enabled = numAvailableSeats > 0, // button is disabled if all seats are unavailable
                    ID = proj.Id.ToString()
                };

                // add button listener
                bookButton.Click += BookButtonOnClick;

                // add control button
                bookButtonCell.Controls.Add(bookButton);

                // add all cells to the row
                row.Cells.AddRange(new[]
                {
                    movieNameCell, movieGenreCell, movieDurationCell,
                    projectionStartCell, seatsAvailableCell, bookButtonCell
                });

                // add row to the table
                projectionTable.Rows.Add(row);
            }
        }

        /// <summary>
        ///     Calculates the number of available seats that a projection has
        /// </summary>
        /// <param name="proj"> the projection </param>
        /// <returns> the number of available seats </returns>
        private int GetNumAvailableSeats(Projection proj)
        {
            // get number of seats in the projection
            int numUnavailableSeats = proj.Seats.Count;

            // calculate the amount of available seats {30 is the total number of seats in a cinema theatre}
            return SessionConstants.MaxProjectionAudienceSize - numUnavailableSeats;
        }

        /// <summary>
        ///     Shows a pop-up message to the user
        /// </summary>
        /// <param name="message"> the message text </param>
        private void ShowMessageBox(string message)
        {
            // script
            string s =
                $"<SCRIPT language=\'javascript\'>alert(\'{message.Replace("\r\n", "\\n").Replace("'", "")}\'); </SCRIPT>";

            // get type of this instance
            Type cstype = GetType();

            // get script manager
            ClientScriptManager cs = Page.ClientScript;

            // execute script
            cs.RegisterClientScriptBlock(cstype, s, s);
        }
    }
}
