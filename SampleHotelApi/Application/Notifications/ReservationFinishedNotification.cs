using MediatR;

namespace SampleHotelApi.Application.Notifications
{
    /// <summary>
    /// Notification about reservation finish.
    /// </summary>
    public class ReservationFinishedNotification : INotification
    {
        /// <summary>
        /// ID for finished reservation.
        /// </summary>
        public long ReservationId { get; set; }
    }
}
