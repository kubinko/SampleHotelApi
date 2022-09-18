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
    /// Handler for single reservation queries.
    /// </summary>
    public class GetReservationQueryHandler :
        IRequestHandler<GetReservationQuery, GetReservationQuery.Reservation>,
        IRequestHandler<GetReservationByCodeQuery, GetReservationByCodeQuery.Reservation>
    {
        private readonly IReservationRepository _repository;
        private readonly IRoomRepository _roomRepository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for room reservations.</param>
        /// <param name="roomRepository">Repository for hotel rooms.</param>
        public GetReservationQueryHandler(IReservationRepository repository, IRoomRepository roomRepository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _roomRepository = Check.NotNull(roomRepository, nameof(roomRepository));
        }

        /// <inheritdoc/>
        public Task<GetReservationQuery.Reservation> Handle(GetReservationQuery request, CancellationToken cancellationToken)
        {
            Reservation? reservation = _repository.GetReservation(request.Id);
            if (reservation == null)
            {
                throw new NotFoundException(string.Format(Resources.ResourceNotFound, request.Id));
            }

            var result = reservation.Adapt<GetReservationQuery.Reservation>();
            result.RoomNo = _roomRepository.GetRoom(result.RoomId)?.RoomNo;

            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public Task<GetReservationByCodeQuery.Reservation> Handle(GetReservationByCodeQuery request, CancellationToken cancellationToken)
        {
            Reservation? reservation = _repository.GetReservation(request.Code);
            if (reservation == null)
            {
                throw new NotFoundException(string.Format(Resources.ResourceNotFound, request.Code));
            }

            var result = reservation.Adapt<GetReservationByCodeQuery.Reservation>();
            result.RoomNo = _roomRepository.GetRoom(result.RoomId)?.RoomNo;

            return Task.FromResult(result);
        }
    }
}
