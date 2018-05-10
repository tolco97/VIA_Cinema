namespace Model_VIA_Pay.Base
{
    using DAO;

    public class CreditCardBase : ICreditCardBase
    {
        private readonly ICreditCardDAO creditCardDao;

        public CreditCardBase(ICreditCardDAO creditCardDao)
        {
            this.creditCardDao = creditCardDao;
        }
 
        /// <inheritdoc/>
        public bool MakeTransaction(string creditCardNumber, string creditCardPin, decimal amountDkk)
        {
            if (!Authenticate(creditCardNumber, creditCardPin))
                return false;

            CreditCard creditCard = creditCardDao.Read(creditCardNumber);
            return Pay(creditCard, amountDkk);
        }

        /// <summary>
        ///     Attempts to transfer money from the credit card passed as a parameter
        ///     to the VIA cinema account
        /// </summary>
        /// <param name="customerCard"></param>
        /// <param name="amountDkk"></param>
        /// <returns> true, if the transaction is successful. Otherwise, false </returns>
        private bool Pay(CreditCard customerCard, decimal amountDkk)
        {
            // attempt to withdraw the required amountDkk
            if (!customerCard.Withdraw(amountDkk))
                return false; // card does not have sufficient funds

            creditCardDao.UpdateBalance(customerCard); // card has sufficient funds
            TransferToViaCinemaAccount(amountDkk); // transfer funds to VIA cinema credit card

            return true;
        }

        /// <summary>
        ///     Checks if there is a credit card entry with the credit card number and pin passed
        ///     as parameters
        /// </summary>
        /// <param name="creditCardNumber"> the credit card number </param>
        /// <param name="pin"> the credit card pin </param>
        /// <returns> true, if authentication is successful. Otherwise, false </returns>
        private bool Authenticate(string creditCardNumber, string pin)
        {
            // check if credit card exists
            if (!creditCardDao.CreditCardExists(creditCardNumber))
                return false;

            // read credit card from the database
            CreditCard creditCard = creditCardDao.Read(creditCardNumber);

            // check if pins match
            return pin.Equals(creditCard.Pin);
        }

        /// <summary>
        ///     Transfers an amount of money to VIA Cinema's account
        /// </summary>
        /// <param name="amount"> the amount of money </param>
        private void TransferToViaCinemaAccount(decimal amount)
        {
            // get VIA Cinema's bank accoumt
            CreditCard viaCinemaCreditCard = creditCardDao.Read(CreditCardEntityConstants.VIA_CINEMA_ACCOUNT_NUMBER);

            // deposit to account
            viaCinemaCreditCard.Deposit(amount);

            // update credit card account
            creditCardDao.UpdateBalance(viaCinemaCreditCard);
        }

    }
}
