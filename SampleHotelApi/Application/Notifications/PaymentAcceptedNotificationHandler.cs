using Kros.Utils;
using MediatR;
using SampleHotelApi.Application.Commands;

namespace SampleHotelApi.Application.Notifications
{
    /// <summary>
    /// Handler for <see cref="PaymentAcceptedNotification"/>.
    /// </summary>
    public class PaymentAcceptedNotificationHandler : INotificationHandler<PaymentAcceptedNotification>
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="mediator">MediatR.</param>
        public PaymentAcceptedNotificationHandler(IMediator mediator)
        {
            _mediator = Check.NotNull(mediator, nameof(mediator));
        }

        /// <inheritdoc/>
        public async Task Handle(PaymentAcceptedNotification notification, CancellationToken cancellationToken)
        {
            Thread.Sleep(3000);

            if (long.TryParse(notification.PaymentReference, out long reservationId))
            {
                await _mediator.Send(new AddTransactionCommand()
                {
                    ReservationId = reservationId,
                    Amount = notification.Amount,
                    TransactionType = Domain.Entities.TransactionType.Credit
                });
            }
        }
    }
}
