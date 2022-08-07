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
    /// Handler for hotel room queries.
    /// </summary>
    public class RoomQueryHandler :
        IRequestHandler<GetRoomQuery, GetRoomQuery.Room>,
        IRequestHandler<GetAllRoomsQuery, IEnumerable<GetAllRoomsQuery.RoomHeader>>
    {
        private readonly IRoomRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for hotel rooms.</param>
        public RoomQueryHandler(IRoomRepository repository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
        }

        /// <inheritdoc/>
        public Task<GetRoomQuery.Room> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            Room? room = _repository.GetRoom(request.Id);
            if (room == null)
            {
                throw new NotFoundException(string.Format(Resources.ResourceNotFound, request.Id));
            }

            return Task.FromResult(room.Adapt<GetRoomQuery.Room>());
        }

        /// <inheritdoc/>
        public Task<IEnumerable<GetAllRoomsQuery.RoomHeader>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
            => Task.FromResult(_repository.GetAllRooms().Adapt<IEnumerable<GetAllRoomsQuery.RoomHeader>>());
    }
}
