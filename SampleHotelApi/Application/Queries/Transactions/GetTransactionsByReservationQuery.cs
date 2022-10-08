namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for obtaining transactions by reservation ID.
    /// </summary>
    public class GetTransactionsByReservationQuery : GetAllTransactionsQuery
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="reservationId">Reservation ID.</param>
        public GetTransactionsByReservationQuery(long reservationId)
        {
            ReservationId = reservationId;
        }

        /// <summary>
        /// Reservation ID.
        /// </summary>
        public long ReservationId { get; }
    }
}
