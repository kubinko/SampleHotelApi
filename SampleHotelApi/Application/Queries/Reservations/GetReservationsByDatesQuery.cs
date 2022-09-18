namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for obtaining room reservations by dates.
    /// </summary>
    public class GetReservationsByDatesQuery : GetAllReservationsQuery
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dateFrom">Start of interval.</param>
        /// <param name="dateTo">End of interval.</param>
        public GetReservationsByDatesQuery(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

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
