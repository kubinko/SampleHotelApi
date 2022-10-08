using Kros.Utils;
using SampleHotelApi.Application.Services;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;
using SampleHotelApi.Infrastructure.DbMock;

namespace SampleHotelApi.Infrastructure
{
    /// <summary>
    /// Repository for transactions.
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDatabase _db;
        private readonly IActiveUserInfoService _userInfo;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="db">Database.</param>
        /// <param name="userInfo"><see cref="IActiveUserInfoService"/>.</param>
        public TransactionRepository(IDatabase db, IActiveUserInfoService userInfo)
        {
            _db = Check.NotNull(db, nameof(db));
            _userInfo = Check.NotNull(userInfo, nameof(userInfo));
        }

        /// <inheritdoc/>
        public long AddTransaction(Transaction transaction)
        {
            transaction.CreatedBy = _userInfo.UserId;
            transaction.CreatedTimestamp = DateTimeOffset.UtcNow;

            _db.Transactions.Add(transaction);

            return transaction.Id;
        }

        /// <inheritdoc/>
        public void DeleteTransaction(long id)
            => _db.Transactions.Delete(id);

        /// <inheritdoc/>
        public Transaction? GetTransaction(long id)
            => _db.Transactions.Get(id);

        /// <inheritdoc/>
        public IEnumerable<Transaction> GetAllTransactions()
            => _db.Transactions.GetAll();

        /// <inheritdoc/>
        public IEnumerable<Transaction> GetAllTransactions(DateTimeOffset dateFrom, DateTimeOffset dateTo)
            => _db.Transactions.GetAll().Where(t => t.CreatedTimestamp <= dateTo && t.CreatedTimestamp >= dateFrom);

        /// <inheritdoc/>
        public IEnumerable<Transaction> GetAllTransactions(long reservationId)
            => _db.Transactions.GetAll().Where(t => t.ReservationId == reservationId);

        /// <inheritdoc/>
        public IEnumerable<Transaction> GetAllTransactions(long reservationId, DateTimeOffset dateFrom, DateTimeOffset dateTo)
            => _db.Transactions.GetAll()
                .Where(t => t.ReservationId == reservationId && t.CreatedTimestamp <= dateTo && t.CreatedTimestamp >= dateFrom);
    }
}
