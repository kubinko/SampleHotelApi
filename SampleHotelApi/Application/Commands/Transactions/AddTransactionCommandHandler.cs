using Kros.AspNetCore.Exceptions;
using Kros.Utils;
using Mapster;
using MediatR;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;
using SampleHotelApi.Properties;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Handler for <see cref="AddTransactionCommand"/>.
    /// </summary>
    public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, long>
    {
        private readonly ITransactionRepository _repository;
        private readonly IReservationRepository _reservationRepository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for transactions.</param>
        /// <param name="reservationRepository">Repository for room reservations.</param>
        public AddTransactionCommandHandler(ITransactionRepository repository, IReservationRepository reservationRepository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _reservationRepository = Check.NotNull(reservationRepository, nameof(reservationRepository));
        }

        /// <inheritdoc/>
        public Task<long> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            Reservation? reservation = _reservationRepository.GetReservation(request.ReservationId);
            if (reservation == null)
            {
                throw new NotFoundException(string.Format(Resources.ReservationNotFound, request.ReservationId));
            }
            if (request.TransactionType == TransactionType.Credit &&
                reservation.PaidAmount + request.Amount > reservation.AccomodationPrice + reservation.AdditionalExpenses)
            {
                throw new RequestConflictException(string.Format(Resources.ReservationAlreadyPaid, request.ReservationId));
            }

            if (request.TransactionType == TransactionType.Credit)
            {
                reservation.PaidAmount += request.Amount;
            }
            else
            {
                reservation.AdditionalExpenses += request.Amount;
            }
            _reservationRepository.UpdateReservation(reservation.Id, reservation);

            var transaction = request.Adapt<Transaction>();
            _repository.AddTransaction(transaction);

            return Task.FromResult(transaction.Id);
        }
    }
}
