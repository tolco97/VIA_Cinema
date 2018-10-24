using System.ComponentModel;
using System.Web.Services;
using DNP1.ViaPay.Model.Base;
using DNP1.ViaPay.Model.DAO;

namespace DNP1.ViaCinema.Services
{
    /// <summary>
    /// Summary description for ViaCinemaPaymentService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ViaCinemaPaymentService : WebService
    {
        private readonly ICreditCardBase _creditCardBase = new CreditCardBase(CreditCardDao.GetInstance());

        /// <inheritdoc cref="ICreditCardBase.MakeTransaction(string, string, decimal)"/>
        [WebMethod]
        public bool MakeTransaction(string creditCardNumber, string pin, decimal amount)
        {
            bool isSuccessful = _creditCardBase.MakeTransaction(creditCardNumber, pin, amount);

            return isSuccessful;
        }
    }
}
