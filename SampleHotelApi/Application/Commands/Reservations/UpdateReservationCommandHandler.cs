using Kros.AspNetCore.Exceptions;
using Kros.Utils;
using Mapster;
using MediatR;
using SampleHotelApi.Application.Notifications;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;
using SampleHotelApi.Properties;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Handler for <see cref="UpdateReservationCommand"/>.
    /// </summary>
    public class UpdateReservationCommandHandler :
        IRequestHandler<UpdateReservationCommand>,
        IRequestHandler<UpdateReservationStateCommand>
    {
        private readonly IReservationRepository _repository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for room reservations.</param>
        /// <param name="roomRepository">Repository for hotel rooms.</param>
        /// <param name="mediator">MediatR.</param>
        public UpdateReservationCommandHandler(
            IReservationRepository repository,
            IRoomRepository roomRepository,
            IMediator mediator)
        {
            _repository = Check.NotNull(repository, nameof(repository));
            _roomRepository = Check.NotNull(roomRepository, nameof(roomRepository));
            _mediator = Check.NotNull(mediator, nameof(mediator));
        }

        /// <inheritdoc/>
        public async Task Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
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

            ReservationState originalState = reservation.State;
            _repository.UpdateReservation(request.Id, request.Adapt<Reservation>());

            if (originalState != ReservationState.Finished && request.State == ReservationState.Finished)
            {
                await _mediator.Publish(
                    new ReservationFinishedNotification() { ReservationId = request.Id },
                    cancellationToken);
            }
        }

        /// <inheritdoc/>
        public async Task Handle(UpdateReservationStateCommand request, CancellationToken cancellationToken)
        {
            Reservation? reservation = _repository.GetReservation(request.Id);
            if (reservation == null)
            {
                throw new NotFoundException(string.Format(Resources.ResourceNotFound, request.Id));
            }

            ReservationState originalState = reservation.State;
            _repository.UpdateReservationState(request.Id, request.State);

            if (originalState != ReservationState.Finished && request.State == ReservationState.Finished)
            {
                await _mediator.Publish(
                    new ReservationFinishedNotification() { ReservationId = request.Id },
                    cancellationToken);
            }
        }

        private bool IsRoomOccupied(DateTime dateFrom, DateTime dateTo, long roomId, long reservationId)
        {
            var reservations = _repository.GetAllReservations(roomId, dateFrom, dateTo);
            return reservations.Any(r => r.Id != reservationId && (r.DateTo != dateFrom || r.DateFrom != dateTo));
        }
    }
}
