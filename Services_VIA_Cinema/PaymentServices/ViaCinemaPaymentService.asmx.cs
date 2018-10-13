using System.ComponentModel;
using System.Web.Services;
using Model_VIA_Pay.Base;
using Model_VIA_Pay.DAO;

namespace Services_VIA_Cinema.PaymentServices
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

        [WebMethod]
        public bool MakeTransaction(string creditCardNumber, string pin, decimal amount)
        {
            bool isSuccessful = _creditCardBase.MakeTransaction(creditCardNumber, pin, amount);

            return isSuccessful;
        }
    }
}
