﻿using Kros.AspNetCore.Exceptions;
using Kros.Utils;
using Mapster;
using MediatR;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;
using SampleHotelApi.Properties;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Handler for <see cref="UpdateReservationCommand"/>.
    /// </summary>
    public class UpdateReservationCommandHandler :
        IRequestHandler<UpdateReservationCommand, Unit>,
        IRequestHandler<UpdateReservationStateCommand, Unit>
    {
        private readonly IReservationRepository _repository;
        private readonly IRoomRepository _roomRepository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for room reservations.</param>
        /// <param name="roomRepository">Repository for hotel rooms.</param>
        public UpdateReservationCommandHandler(IReservationRepository repository, IRoomRepository roomRepository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _roomRepository = Check.NotNull(roomRepository, nameof(roomRepository));
        }

        /// <inheritdoc/>
        public Task<Unit> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            Reservation? reservation = _repository.GetReservation(request.Id);
            if (reservation == null)
            {
                throw new NotFoundException(string.Format(Resources.ResourceNotFound, request.Id));
            }

            Room? room = _roomRepository.GetRoom(reservation.RoomId);
            if (room == null)
            {
                throw new NotFoundException(string.Format(Resources.RoomNotFound, reservation.RoomId));
            }
            if (IsRoomOccupied(request.DateFrom, request.DateTo, reservation.RoomId, request.Id))
            {
                throw new RequestConflictException(
                    string.Format(Resources.RoomOccupied, reservation.RoomId, request.DateFrom, request.DateTo));
            }
            if (request.NumberOfGuests > room.MaxNumberOfBeds)
            {
                throw new RequestConflictException(
                    string.Format(Resources.TooManyGuests, reservation.RoomId, room.MaxNumberOfBeds));
            }

            _repository.UpdateReservation(request.Id, request.Adapt<Reservation>());

            return Unit.Task;
        }

        /// <inheritdoc/>
        public Task<Unit> Handle(UpdateReservationStateCommand request, CancellationToken cancellationToken)
        {
            Reservation? reservation = _repository.GetReservation(request.Id);
            if (reservation == null)
            {
                throw new NotFoundException(string.Format(Resources.ResourceNotFound, request.Id));
            }

            _repository.UpdateReservationState(request.Id, request.State);

            return Unit.Task;
        }

        private bool IsRoomOccupied(DateTime dateFrom, DateTime dateTo, long roomId, long reservationId)
        {
            var reservations = _repository.GetAllReservations(roomId, dateFrom, dateTo);
            return reservations.Any(r => r.Id != reservationId && (r.DateTo != dateFrom || r.DateFrom != dateTo));
        }
    }
}
