using Kros.AspNetCore.Exceptions;
using Kros.Utils;
using Mapster;
using MediatR;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;
using SampleHotelApi.Properties;

namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Handler for multiple transaction queries.
    /// </summary>
    public class GetTransactionsQueryHandler :
        IRequestHandler<GetAllTransactionsQuery, IEnumerable<GetAllTransactionsQuery.Transaction>>,
        IRequestHandler<GetTransactionsByDatesQuery, IEnumerable<GetAllTransactionsQuery.Transaction>>,
        IRequestHandler<GetTransactionsByReservationQuery, IEnumerable<GetAllTransactionsQuery.Transaction>>,
        IRequestHandler<GetTransactionsByReservationAndDatesQuery, IEnumerable<GetAllTransactionsQuery.Transaction>>
    {
        private readonly ITransactionRepository _repository;
        private readonly IReservationRepository _reservationRepository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for transactions.</param>
        /// <param name="reservationRepository">Repository for room reservations.</param>
        public GetTransactionsQueryHandler(ITransactionRepository repository, IReservationRepository reservationRepository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _reservationRepository = Check.NotNull(reservationRepository, nameof(reservationRepository));
        }

        /// <inheritdoc/>
        public Task<IEnumerable<GetAllTransactionsQuery.Transaction>> Handle(
            GetAllTransactionsQuery request,
            CancellationToken cancellationToken)
            => Task.FromResult(_repository.GetAllTransactions().Adapt<IEnumerable<GetAllTransactionsQuery.Transaction>>());

        /// <inheritdoc/>
        public Task<IEnumerable<GetTransactionsByDatesQuery.Transaction>> Handle(
            GetTransactionsByDatesQuery request,
            CancellationToken cancellationToken)
            => Task.FromResult(_repository.GetAllTransactions(request.DateFrom, request.DateTo)
                .Adapt<IEnumerable<GetAllTransactionsQuery.Transaction>>());

        /// <inheritdoc/>
        public Task<IEnumerable<GetTransactionsByDatesQuery.Transaction>> Handle(GetTransactionsByReservationQuery request, CancellationToken cancellationToken)
        {
            Reservation? reservation = _reservationRepository.GetReservation(request.ReservationId);
            if (reservation == null)
            {
                throw new NotFoundException(string.Format(Resources.ReservationNotFound, request.ReservationId));
            }

            return Task.FromResult(_repository.GetAllTransactions(request.ReservationId)
                .Adapt<IEnumerable<GetAllTransactionsQuery.Transaction>>());
        }

        /// <inheritdoc/>
        public Task<IEnumerable<GetTransactionsByDatesQuery.Transaction>> Handle(GetTransactionsByReservationAndDatesQuery request, CancellationToken cancellationToken)
        {
            Reservation? reservation = _reservationRepository.GetReservation(request.ReservationId);
            if (reservation == null)
            {
                throw new NotFoundException(string.Format(Resources.ReservationNotFound, request.ReservationId));
            }

            return Task.FromResult(_repository.GetAllTransactions(request.ReservationId, request.DateFrom, request.DateTo)
                .Adapt<IEnumerable<GetAllTransactionsQuery.Transaction>>()); ;
        }
    }
}
