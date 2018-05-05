namespace Model_VIA_Pay.Base
{
    public interface ICreditCardBase
    {
        /// <summary>
        ///     Attempts to make a credit card transcation from a customer credit card to the VIA Cinema 
        ///     account
        /// </summary>
        /// <param name="creditCardNumber"> the credit card number of the card that will  </param>
        /// <param name="creditCardPin"> the pin of the credit card from which the money is withdrawn </param>
        /// <param name="transactionAmountDkk"> the amount of money to be transacted </param>
        /// <returns> true, if the trancation is successful. Otherwise, false </returns>
        bool MakeTransaction(string creditCardNumber, string creditCardPin, decimal transactionAmountDkk);
    }
}
