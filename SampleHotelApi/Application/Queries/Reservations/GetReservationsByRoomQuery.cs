namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for obtaining room reservations by room ID.
    /// </summary>
    public class GetReservationsByRoomQuery : GetAllReservationsQuery
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="roomId">Room ID.</param>
        public GetReservationsByRoomQuery(long roomId)
        {
            RoomId = roomId;
        }

        /// <summary>
        /// Room ID.
        /// </summary>
        public long RoomId { get; }
    }
}
