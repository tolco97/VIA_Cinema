﻿namespace DNP1.ViaPay.Model.DAO
{
    /// <summary>
    ///     Stores constants used when manipulating the credit cards database
    /// </summary>
    public static class CreditCardEntityConstants
    {
        public const string CardNumberColumn = "number";
        public const string PinColumn = "pin";
        public const string BalanceColumn = "balance_dkk";


        // All money from the tickets is transfered to VIA Cinema's bank account with these details:
        public const string ViaCinemaAccountNumber = "000000000000";
        public const string ViaCinemaAccountPin = "0000";
    }
}