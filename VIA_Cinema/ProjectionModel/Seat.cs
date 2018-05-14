namespace ProjectionModel
{
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
        
        public override string ToString()
        {
            return $"Seat [SeatNumer: {SeatNumber}, SeatOwner: {SeatOwner}]";
        }
    }
}
