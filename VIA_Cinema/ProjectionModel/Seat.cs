using System.Runtime.Serialization;
using DNP1.ViaCinema.Model.UserAccountModel;

namespace DNP1.ViaCinema.Model.ProjectionModel
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Seat
    {
        /// <summary>
        /// 
        /// </summary>
        public Seat() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seatNumber"></param>
        /// <param name="seatOwner"></param>
        public Seat(int seatNumber, UserAccount seatOwner)
        {
            SeatNumber = seatNumber;
            SeatOwner = seatOwner;
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public int SeatNumber { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember] public UserAccount SeatOwner { set; get; }

        /// <summary>
        /// <inheritdoc cref="object.ToString"/>
        /// </summary>
        public override string ToString()
        {
            return $"Seat [SeatNumber: {SeatNumber}, SeatOwner: {SeatOwner}]";
        }
    }
}