using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Domain
{
    /// <summary>
    /// Interface describing repository for transactions.
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Adds new transaction.
        /// </summary>
        /// <param name="transaction">Transaction.</param>
        /// <returns>New transaction ID.</returns>
        long AddTransaction(Transaction transaction);

        /// <summary>
        /// Deletes transaction with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Transaction ID.</param>
        void DeleteTransaction(long id);

        /// <summary>
        /// Attempts to retrieve transaction with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Transaction ID.</param>
        /// <returns>Transaction; <c>null</c>, if transaction with specified ID does not exist.</returns>
        Transaction? GetTransaction(long id);

        /// <summary>
        /// Retrieves all transactions.
        /// </summary>
        /// <returns>Transactions.</returns>
        IEnumerable<Transaction> GetAllTransactions();

        /// <summary>
        /// Retrieves all transaction between <paramref name="dateFrom"/> and <paramref name="dateTo"/> inclusive.
        /// </summary>
        /// <param name="dateFrom">Start of search interval.</param>
        /// <param name="dateTo">End of search interval.</param>
        /// <returns>Transactions.</returns>
        IEnumerable<Transaction> GetAllTransactions(DateTimeOffset dateFrom, DateTimeOffset dateTo);

        /// <summary>
        /// Retrieves all transactions for reservation with <paramref name="reservationId"/>.
        /// </summary>
        /// <param name="reservationId">Reservation ID.</param>
        /// <returns>Transactions.</returns>
        IEnumerable<Transaction> GetAllTransactions(long reservationId);

        /// <summary>
        /// Retrieves all transactions for reservation with <paramref name="reservationId"/>
        /// between <paramref name="dateFrom"/> and <paramref name="dateTo"/> inclusive.
        /// </summary>
        /// <param name="reservationId">Reservation ID.</param>
        /// <param name="dateFrom">Start of search interval.</param>
        /// <param name="dateTo">End of search interval.</param>
        /// <returns>Transactions.</returns>
        IEnumerable<Transaction> GetAllTransactions(long reservationId, DateTimeOffset dateFrom, DateTimeOffset dateTo);
    }
}
