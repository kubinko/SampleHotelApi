namespace SampleHotelApi.Domain.Entities
{
    /// <summary>
    /// Transaction entity.
    /// </summary>
    public class Transaction : IEntityWithId
    {
        /// <inheritdoc/>
        public long Id { get; set; }

        /// <summary>
        /// Reservation the transaction is related to.
        /// </summary>
        public long ReservationId { get; set; }

        /// <summary>
        /// Transaction amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Transaction type.
        /// </summary>
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Timestamp of transaction.
        /// </summary>
        public DateTimeOffset CreatedTimestamp { get; set; }

        /// <summary>
        /// ID of employee that logged the transaction.
        /// </summary>
        public long CreatedBy { get; set; }
    }

    /// <summary>
    /// Type of transaction.
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Undefined transaction type.
        /// </summary>
        None = 0,

        /// <summary>
        /// Transaction to hotel.
        /// </summary>
        Credit = 1,

        /// <summary>
        /// Transaction from hotel.
        /// </summary>
        Debit = 2
    }
}
