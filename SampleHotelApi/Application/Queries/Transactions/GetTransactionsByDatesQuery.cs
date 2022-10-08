namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for obtaining transactions by dates.
    /// </summary>
    public class GetTransactionsByDatesQuery : GetAllTransactionsQuery
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dateFrom">Start of interval.</param>
        /// <param name="dateTo">End of interval.</param>
        public GetTransactionsByDatesQuery(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

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
