using MediatR;

namespace SampleHotelApi.Application.Notifications
{
    /// <summary>
    /// Notification about payment acceptance.
    /// </summary>
    public class PaymentAcceptedNotification : INotification
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
