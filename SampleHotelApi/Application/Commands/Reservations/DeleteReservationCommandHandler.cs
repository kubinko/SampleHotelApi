using Kros.AspNetCore.Exceptions;
using Kros.Utils;
using MediatR;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;
using SampleHotelApi.Properties;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Handler for <see cref="DeleteReservationCommand"/>.
    /// </summary>
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, Unit>
    {
        private readonly IReservationRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for room reservations.</param>
        public DeleteReservationCommandHandler(IReservationRepository repository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
        }

        /// <inheritdoc/>
        public Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            Reservation? reservation = _repository.GetReservation(request.Id);
            if (reservation != null && reservation.State == ReservationState.Finished)
            {
                throw new ResourceIsForbiddenException(Resources.ReservationFinished);
            }

            _repository.RemoveReservation(request.Id);

            return Unit.Task;
        }
    }
}
