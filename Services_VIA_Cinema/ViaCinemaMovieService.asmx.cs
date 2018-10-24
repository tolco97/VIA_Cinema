using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using DNP1.ViaCinema.Model.MovieModel;
using DNP1.ViaCinema.Model.MovieModel.Base;
using DNP1.ViaCinema.Model.MovieModel.DAO;

namespace DNP1.ViaCinema.Services
{
    /// <summary>
    /// Summary description for ViaCinemaMovieService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ViaCinemaMovieService : WebService
    {
        private readonly IMovieBase _movieBase = new MovieBase(MovieDao.GetInstance());

        /// <inheritdoc cref="IMovieBase.GetAllMovies"/>
        [WebMethod]
        public List<Movie> GetAllMovies()
        {
            return _movieBase.GetAllMovies();
        }
    }
}
