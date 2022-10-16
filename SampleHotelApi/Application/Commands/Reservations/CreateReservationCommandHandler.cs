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
    /// Handler for <see cref="CreateReservationCommand"/>.
    /// </summary>
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, long>
    {
        private readonly IReservationRepository _repository;
        private readonly IRoomRepository _roomRepository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for room reservations.</param>
        /// <param name="roomRepository">Repository for hotel rooms.</param>
        public CreateReservationCommandHandler(IReservationRepository repository, IRoomRepository roomRepository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _roomRepository = Check.NotNull(roomRepository, nameof(roomRepository));
        }

        /// <inheritdoc/>
        public Task<long> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            Room? room = _roomRepository.GetRoomByNumber(request.RoomNo);
            if (room == null)
            {
                throw new NotFoundException(string.Format(Resources.RoomNotFound, request.RoomNo));
            }
            if (IsRoomOccupied(request.DateFrom, request.DateTo, room.Id))
            {
                throw new RequestConflictException(
                    string.Format(Resources.RoomOccupied, request.RoomNo, request.DateFrom, request.DateTo));
            }
            if (request.NumberOfGuests > room.MaxNumberOfBeds)
            {
                throw new RequestConflictException(
                    string.Format(Resources.TooManyGuests, request.RoomNo, room.MaxNumberOfBeds));
            }

            int numberOfNights = (request.DateTo.Date - request.DateFrom.Date).Days;
            request.AccomodationPrice ??= CalculateBaseRoomPrice(request.NumberOfGuests, room) * numberOfNights;

            var reservation = request.Adapt<Reservation>();
            reservation.RoomId = room.Id;
            _repository.CreateReservation(reservation);

            return Task.FromResult(reservation.Id);
        }

        private bool IsRoomOccupied(DateTime dateFrom, DateTime dateTo, long roomId)
        {
            var reservations = _repository.GetAllReservations(roomId, dateFrom, dateTo);
            return reservations.Any(r => r.DateTo != dateFrom || r.DateFrom != dateTo);
        }

        private decimal CalculateBaseRoomPrice(int numberOfGuests, Room room)
        {
            if (numberOfGuests == 1)
            {
                return (1 + room.SingleGuestSurcharge) * room.BaseBedPrice;
            }
            else if (numberOfGuests <= room.BaseNumberOfBeds)
            {
                return numberOfGuests * room.BaseBedPrice;
            }
            else
            {
                return room.BaseNumberOfBeds * room.BaseBedPrice + (numberOfGuests - room.BaseNumberOfBeds) * room.ExtraBedPrice;
            }
        }
    }
}
