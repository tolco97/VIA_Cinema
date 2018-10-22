using System;

namespace DNP1.ViaPay.Model.DAO
{
    public interface ICreditCardDao : IDisposable
    {
        /// <summary>
        ///     Creates a new credit updatedCard entry in the credit cards database entity
        /// </summary>
        /// <param name="cardNumber"> the number of the credit card </param>
        /// <param name="pin"> the pin of the credit card </param>
        /// <param name="balanceDkk"> the balance of the credit card </param>
        /// <returns> a credit card object </returns>
        CreditCard Create(string cardNumber, string pin, decimal balanceDkk);

        /// <summary>
        ///     Updates the balance of a credit updatedCard in the database with the credit updatedCard
        ///     number of the credit updatedCard passed as a parameter
        /// </summary>
        /// <param name="updatedCard"> the credit updatedCard </param>
        /// <returns> true, if the update operation has affected at least 1 database row. Otherwise, false</returns>
        bool UpdateBalance(CreditCard updatedCard);

        /// <summary>
        ///     Checks if a credit card exists in the database
        /// </summary>
        /// <param name="creditCardNumber"> the credit card number </param>
        /// <returns> true, if the credit card exists. Otherwise, false </returns>
        bool CreditCardExists(string creditCardNumber);

        /// <summary>
        ///     Reads a credit card entry from the credit cards database entity
        ///     that matches the credit card number as parameter
        /// </summary>
        /// <param name="creditCardNumber"> the credit card number </param>
        /// <returns> a credit card object </returns>
        CreditCard Read(string creditCardNumber);
    }
}