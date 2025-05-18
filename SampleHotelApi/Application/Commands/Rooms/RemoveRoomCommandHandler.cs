using Kros.Utils;
using MediatR;
using SampleHotelApi.Domain;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Handler for <see cref="RemoveRoomCommand"/>.
    /// </summary>
    public class RemoveRoomCommandHandler : IRequestHandler<RemoveRoomCommand>, IRequestHandler<RemoveAllRoomsCommand>
    {
        private readonly IRoomRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for hotel rooms.</param>
        public RemoveRoomCommandHandler(IRoomRepository repository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
        }

        /// <inheritdoc/>
        public Task Handle(RemoveRoomCommand request, CancellationToken cancellationToken)
        {
            _repository.RemoveRoom(request.Id);

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task Handle(RemoveAllRoomsCommand request, CancellationToken cancellationToken)
        {
            _repository.RemoveAllRooms();

            return Task.CompletedTask;
        }
    }
}
