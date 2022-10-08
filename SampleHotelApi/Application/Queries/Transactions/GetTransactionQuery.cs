using MediatR;
using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for obtaining transaction by ID.
    /// </summary>
    public class GetTransactionQuery : IRequest<GetTransactionQuery.Transaction>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="id">Transaction ID.</param>
        public GetTransactionQuery(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Transaction ID.
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Transaction.
        /// </summary>
        public class Transaction
        {
            /// <summary>
            /// Transaction ID.
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// ID of reservation the transaction belongs to.
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
            /// Transaction date.
            /// </summary>
            public DateTimeOffset CreatedTimestamp { get; set; }
        }
    }
}
