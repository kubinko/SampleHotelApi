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
    /// Handler for multiple reservation queries.
    /// </summary>
    public class GetReservationsQueryHandler :
        IRequestHandler<GetAllReservationsQuery, IEnumerable<GetAllReservationsQuery.ReservationHeader>>,
        IRequestHandler<GetReservationsByDatesQuery, IEnumerable<GetAllReservationsQuery.ReservationHeader>>,
        IRequestHandler<GetReservationsByRoomQuery, IEnumerable<GetAllReservationsQuery.ReservationHeader>>,
        IRequestHandler<GetReservationsByRoomAndDatesQuery, IEnumerable<GetAllReservationsQuery.ReservationHeader>>
    {
        private readonly IReservationRepository _repository;
        private readonly IRoomRepository _roomRepository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for room reservations.</param>
        /// <param name="roomRepository">Repository for hotel rooms.</param>
        public GetReservationsQueryHandler(IReservationRepository repository, IRoomRepository roomRepository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _roomRepository = Check.NotNull(roomRepository, nameof(roomRepository));
        }

        /// <inheritdoc/>
        public Task<IEnumerable<GetAllReservationsQuery.ReservationHeader>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var roomDictionary = _roomRepository.GetAllRooms().ToDictionary(room => room.Id, room => room.RoomNo);
            var reservations = _repository.GetAllReservations()
                .Adapt<IEnumerable<GetAllReservationsQuery.ReservationHeader>>()
                .ToArray();
            reservations.ForEach(r => r.RoomNo = roomDictionary.TryGetValue(r.RoomId, out int roomNo) ? roomNo : -1);

            return Task.FromResult(reservations.AsEnumerable());
        }

        /// <inheritdoc/>
        public Task<IEnumerable<GetAllReservationsQuery.ReservationHeader>> Handle(GetReservationsByDatesQuery request, CancellationToken cancellationToken)
        {
            var roomDictionary = _roomRepository.GetAllRooms().ToDictionary(room => room.Id, room => room.RoomNo);
            var reservations = _repository.GetAllReservations(request.DateFrom, request.DateTo)
                .Adapt<IEnumerable<GetAllReservationsQuery.ReservationHeader>>()
                .ToArray();
            reservations.ForEach(r => r.RoomNo = roomDictionary.TryGetValue(r.RoomId, out int roomNo) ? roomNo : -1);

            return Task.FromResult(reservations.AsEnumerable());
        }

        /// <inheritdoc/>
        public Task<IEnumerable<GetAllReservationsQuery.ReservationHeader>> Handle(GetReservationsByRoomQuery request, CancellationToken cancellationToken)
        {
            Room? room = _roomRepository.GetRoom(request.RoomId);
            if (room == null)
            {
                throw new NotFoundException(string.Format(Resources.RoomNotFound, request.RoomId));
            }

            var reservations = _repository.GetAllReservations(request.RoomId)
                .Adapt<IEnumerable<GetAllReservationsQuery.ReservationHeader>>()
                .ToArray();
            reservations.ForEach(r => r.RoomNo = room.RoomNo);

            return Task.FromResult(reservations.AsEnumerable());
        }

        /// <inheritdoc/>
        public Task<IEnumerable<GetAllReservationsQuery.ReservationHeader>> Handle(GetReservationsByRoomAndDatesQuery request, CancellationToken cancellationToken)
        {
            Room? room = _roomRepository.GetRoom(request.RoomId);
            if (room == null)
            {
                throw new NotFoundException(string.Format(Resources.RoomNotFound, request.RoomId));
            }

            var reservations = _repository.GetAllReservations(request.RoomId, request.DateFrom, request.DateTo)
                .Adapt<IEnumerable<GetAllReservationsQuery.ReservationHeader>>()
                .ToArray();
            reservations.ForEach(r => r.RoomNo = room.RoomNo);

            return Task.FromResult(reservations.AsEnumerable());
        }
    }
}
