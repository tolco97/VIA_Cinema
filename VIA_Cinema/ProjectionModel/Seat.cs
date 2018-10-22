using System.Runtime.Serialization;
using DNP1.ViaCinema.Model.UserAccountModel;

namespace DNP1.ViaCinema.Model.ProjectionModel
{
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
            return $"Seat [SeatNumber: {SeatNumber}, SeatOwner: {SeatOwner}]";
        }
    }
}