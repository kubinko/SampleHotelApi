namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for obtaining transactions by reservation and dates.
    /// </summary>
    public class GetTransactionsByReservationAndDatesQuery : GetAllTransactionsQuery
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="reservationId">Reservation ID.</param>
        /// <param name="dateFrom">Start of interval.</param>
        /// <param name="dateTo">End of interval.</param>
        public GetTransactionsByReservationAndDatesQuery(long reservationId, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            ReservationId = reservationId;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        /// <summary>
        /// Reservation ID.
        /// </summary>
        public long ReservationId { get; }

        /// <summary>
        /// Start of interval.
        /// </summary>
        public DateTimeOffset DateFrom { get; }

        /// <summary>
        /// End of interval.
        /// </summary>
        public DateTimeOffset DateTo { get; }
    }
}
