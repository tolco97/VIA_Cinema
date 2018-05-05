namespace WebService_VIA_Pay
{
    using Model_VIA_Pay.Base;
    using Model_VIA_Pay.DAO;

    public class ViaPayService : IViaPayService
    {
        private readonly ICreditCardBase creditCardBase = new CreditCardBase(CreditCardDAO.GetInstance());
        
        public bool MakeTransaction(string creditCardNumber, string pin, decimal amount)
        {
            return creditCardBase.MakeTransaction(creditCardNumber, pin, amount);
        }
    }
}
