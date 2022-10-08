using MediatR;
using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Command for adding transaction.
    /// </summary>
    public class AddTransactionCommand : IRequest<long>
    {
        /// <summary>
        /// ID of reservation the transaction relates to.
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
    }
}
