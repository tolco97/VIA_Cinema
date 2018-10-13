using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;
using VIA_Cinema.MovieModel;
using VIA_Cinema.MovieModel.Base;
using VIA_Cinema.MovieModel.DAO;

namespace Services_VIA_Cinema.MovieServices
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

        [WebMethod]
        public List<Movie> GetAllMovies()
        {
            return _movieBase.GetAllMovies();
        }
    }
}
