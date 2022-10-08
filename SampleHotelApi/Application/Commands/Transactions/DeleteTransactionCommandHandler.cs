using Kros.AspNetCore.Exceptions;
using Kros.Utils;
using MediatR;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;
using Resources = SampleHotelApi.Properties.Resources;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Handler for <see cref="DeleteTransactionCommand"/>.
    /// </summary>
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Unit>
    {
        private readonly ITransactionRepository _repository;
        private readonly IReservationRepository _reservationRepository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for transactions.</param>
        /// <param name="reservationRepository">Repository for reservations.</param>
        public DeleteTransactionCommandHandler(ITransactionRepository repository, IReservationRepository reservationRepository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _reservationRepository = Check.NotNull(reservationRepository, nameof(reservationRepository));
        }

        /// <inheritdoc/>
        public Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            Transaction? transaction = _repository.GetTransaction(request.Id);
            if (transaction != null)
            {
                Reservation? reservation = _reservationRepository.GetReservation(transaction.ReservationId);
                if (reservation != null)
                {
                    if (reservation.State == ReservationState.Finished)
                    {
                        throw new ResourceIsForbiddenException(Resources.CannotDeleteTransactionFromFinishedReservation);
                    }

                    if (transaction.TransactionType == TransactionType.Credit)
                    {
                        reservation.PaidAmount -= transaction.Amount;
                    }
                    else
                    {
                        reservation.AdditionalExpenses -= transaction.Amount;
                    }
                    _reservationRepository.UpdateReservation(reservation.Id, reservation);
                }
            }

            _repository.DeleteTransaction(request.Id);

            return Unit.Task;
        }
    }
}
