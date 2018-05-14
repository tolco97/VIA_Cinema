namespace Model_VIA_Pay
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CreditCard
    {
        public CreditCard(string cardNumber, string pin, decimal balanceDkk)
        {
            CardNumber = cardNumber;
            Pin = pin;
            BalanceDkk = balanceDkk;
        }

        [DataMember] public string CardNumber { get; }

        [DataMember] public string Pin { get; }

        [DataMember] public decimal BalanceDkk { get; private set; }

        public bool Withdraw(decimal amountDkk)
        {
            // check if account has enough money
            if (amountDkk >= BalanceDkk)
                return false;

            // update balanceDkk
            BalanceDkk -= amountDkk;

            return true;
        }

        public bool Deposit(decimal amountDkk)
        {
            // update amountDkk
            BalanceDkk += amountDkk;

            return true;
        }

        public override string ToString()
        {
            return $"CreditCard {BalanceDkk} DKK";
        }
    }
}
