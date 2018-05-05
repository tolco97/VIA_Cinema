namespace Model_VIA_Pay.DAO
{
    public interface ICreditCardDAO
    {
        /// <summary>
        ///     Creates a new credit updatedCard entry in the credit cards database entity
        /// </summary>
        /// <param name="newCreditCard"> the new credit updatedCard objcet </param>
        /// <returns> the number of database rows affected </returns>
        int Create(CreditCard newCreditCard);

        /// <summary>
        ///     Updates the balance of a credit updatedCard in the database with the credit updatedCard
        ///     number of the credit updatedCard passed as a parameter
        /// </summary>
        /// <param name="updatedCard"> the credit updatedCard </param>
        /// <returns> the number of database rows affected </returns>
        int UpdateBalance(CreditCard updatedCard);

        /// <summary>
        ///     Checks if a credit card exists in the database
        /// </summary>
        /// <param name="creditCardNumber"> the credit card number </param>
        /// <returns> true, if the credit card exists. Otherwise, false </returns>
        bool CreditCardExists(string creditCardNumber);

        /// <summary>
        ///     Reads a creidt card entry from the credit cards database entity
        ///     that matches the credit card number as parameter
        /// </summary>
        /// <param name="creditCardNumber"> the credit card number </param>
        /// <returns> a credit card object </returns>
        CreditCard Read(string creditCardNumber);
    }
}
