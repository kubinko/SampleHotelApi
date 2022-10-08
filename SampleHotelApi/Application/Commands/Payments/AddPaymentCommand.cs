using MediatR;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Command to add dummy payment.
    /// </summary>
    public class AddPaymentCommand : IRequest
    {
        /// <summary>
        /// Payment amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Payment reference.
        /// </summary>
        /// <remarks>Should be equal to reservation ID.</remarks>
        public string PaymentReference { get; set; } = "";
    }
}
