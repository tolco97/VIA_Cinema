namespace Model_VIA_Pay
{
    using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            if (!(obj is CreditCard))
                return false;

            CreditCard other = (CreditCard) obj;
            return CardNumber.Equals(other.CardNumber) && Pin.Equals(other.Pin) && BalanceDkk.Equals(other.BalanceDkk);
        }

        public override string ToString()
        {
            return $"CreditCard {BalanceDkk} DKK";
        }

        public override int GetHashCode()
        {
            var hashCode = 1404725597;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CardNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Pin);
            hashCode = hashCode * -1521134295 + BalanceDkk.GetHashCode();
            return hashCode;
        }
    }
}
