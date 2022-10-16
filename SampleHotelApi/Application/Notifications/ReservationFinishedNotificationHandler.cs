using Kros.Utils;
using MediatR;
using SampleHotelApi.Domain;

namespace SampleHotelApi.Application.Notifications
{
    /// <summary>
    /// Handler for <see cref="ReservationFinishedNotification"/>.
    /// </summary>
    public class ReservationFinishedNotificationHandler : INotificationHandler<ReservationFinishedNotification>
    {
        private readonly IReservationRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for room reservations.</param>
        public ReservationFinishedNotificationHandler(IReservationRepository repository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
        }

        /// <inheritdoc/>
        public async Task Handle(ReservationFinishedNotification notification, CancellationToken cancellationToken)
        {
            var reservation = _repository.GetReservation(notification.ReservationId);
            if (reservation != null)
            {
                await Task.Delay(5000, cancellationToken);

                reservation.InvoiceGenerated = true;
                _repository.UpdateReservation(reservation.Id, reservation);
            }
        }
    }
}
