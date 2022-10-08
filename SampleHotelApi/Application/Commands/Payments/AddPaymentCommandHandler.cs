using Kros.Utils;
using Mapster;
using MediatR;
using SampleHotelApi.Application.Notifications;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Handler for <see cref="AddPaymentCommand"/>.
    /// </summary>
    public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, Unit>
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="mediator">MediatR.</param>
        public AddPaymentCommandHandler(IMediator mediator)
        {
            _mediator = Check.NotNull(mediator, nameof(mediator));
        }

        /// <inheritdoc/>
        public async Task<Unit> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Publish(request.Adapt<PaymentAcceptedNotification>(), cancellationToken);

            return Unit.Value;
        }
    }
}
