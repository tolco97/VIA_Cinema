using DNP1.ViaCinema.Model.Util;
using DNP1.ViaPay.Model.DAO;

namespace DNP1.ViaPay.Model.Base
{
    public class CreditCardBase : ICreditCardBase
    {
        private readonly ICreditCardDao _creditCardDao;

        public CreditCardBase(ICreditCardDao creditCardDao)
        {
            _creditCardDao = creditCardDao;
        }

        /// <inheritdoc />
        public bool MakeTransaction(string creditCardNumber, string creditCardPin, decimal amountDkk)
        {
            Validator.ValidateTextualInput(creditCardNumber, creditCardPin);
            Validator.ValidateMoneyAmountPositive(amountDkk);

            if (!Authenticate(creditCardNumber, creditCardPin))
            {
                return false;
            }

            CreditCard creditCard = _creditCardDao.Read(creditCardNumber);

            Validator.ValidateObjectsNotNull(creditCard);

            bool isSuccessful = Pay(creditCard, amountDkk);

            return isSuccessful;
        }

        /// <summary>
        ///     Attempts to transfer money from the credit card passed as a parameter
        ///     to the VIA cinema account
        /// </summary>
        /// <param name="customerCard"> the customer credit card </param>
        /// <param name="amountDkk"> amount of money to be paid </param>
        /// <returns> true, if the transaction is successful. Otherwise, false </returns>
        private bool Pay(CreditCard customerCard, decimal amountDkk)
        {
            if (!customerCard.Withdraw(amountDkk))
            {
                return false;
            }

            _creditCardDao.UpdateBalance(customerCard);

            TransferToViaCinemaAccount(amountDkk); 

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
            if (!_creditCardDao.CreditCardExists(creditCardNumber))
            { 
                return false;
            }

            CreditCard creditCard = _creditCardDao.Read(creditCardNumber);

            return string.Equals(pin, creditCard.Pin);
        }

        /// <summary>
        ///     Transfers an amount of money to VIA Cinema's account
        /// </summary>
        /// <param name="amount"> the amount of money </param>
        private void TransferToViaCinemaAccount(decimal amount)
        {
            CreditCard viaCinemaCreditCard = _creditCardDao.Read(CreditCardEntityConstants.ViaCinemaAccountNumber);

            viaCinemaCreditCard.Deposit(amount);

            _creditCardDao.UpdateBalance(viaCinemaCreditCard);
        }
    }
}