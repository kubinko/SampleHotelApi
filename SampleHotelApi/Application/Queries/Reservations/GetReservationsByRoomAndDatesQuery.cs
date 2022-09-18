namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for obtaining room reservations by dates.
    /// </summary>
    public class GetReservationsByRoomAndDatesQuery : GetAllReservationsQuery
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="roomId">Room ID.</param>
        /// <param name="dateFrom">Start of interval.</param>
        /// <param name="dateTo">End of interval.</param>
        public GetReservationsByRoomAndDatesQuery(long roomId, DateTime dateFrom, DateTime dateTo)
        {
            RoomId = roomId;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        /// <summary>
        /// Room ID.
        /// </summary>
        public long RoomId { get; }

        /// <summary>
        /// Start of interval.
        /// </summary>
        public DateTime DateFrom { get; }

        /// <summary>
        /// End of interval.
        /// </summary>
        public DateTime DateTo { get; }
    }
}
