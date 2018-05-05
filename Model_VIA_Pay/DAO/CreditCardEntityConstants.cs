namespace Model_VIA_Pay.DAO
{
    /// <summary>
    ///     Stores constants used when manipulating the credit cards database
    /// </summary>
    public sealed class CreditCardEntityConstants
    {
        private CreditCardEntityConstants() {}

        public const string CARD_NUMBER_COLUMN = "number";
        public const string PIN_COLUMN = "pin";
        public const string BALANCE_COLUMN = "balance_dkk";


        // All money from the tickets is transfered to VIA Cinema's bank account with these details:
        public const string VIA_CINEMA_ACCOUNT_NUMBER = "000000000000";
        public const string VIA_CINEMA_ACCOUNT_PIN = "0000";

    }
}
