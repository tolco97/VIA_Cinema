namespace ProjectionModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using UserAccountModel;

    [DataContract]
    public class Seat
    {
        public Seat() {}

        public Seat(int seatNumber, UserAccount seatOwner)
        {
            SeatNumber = seatNumber;
            SeatOwner = seatOwner;
        }

        [DataMember] public int SeatNumber { set; get; }

        [DataMember] public UserAccount SeatOwner { set; get; }

        public override bool Equals(object obj)
        {
            if (!(obj is Seat))
                return false;

            Seat other = (Seat) obj;
            return SeatNumber.Equals(other.SeatNumber) && SeatOwner.Equals(other.SeatOwner);
        }

        public override int GetHashCode()
        {
            int hashCode = 111891840;
            hashCode = hashCode * -1521134295 + SeatNumber.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<UserAccount>.Default.GetHashCode(SeatOwner);
            return hashCode;
        }

        public override string ToString()
        {
            return $"Seat [SeatNumer: {SeatNumber}, SeatOwner: {SeatOwner}]";
        }
    }
}
