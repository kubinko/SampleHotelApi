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
    /// Handler for <see cref="UpdateRoomCommand"/>.
    /// </summary>
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand>
    {
        private readonly IRoomRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for hotel rooms.</param>
        public UpdateRoomCommandHandler(IRoomRepository repository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
        }

        /// <inheritdoc/>
        public Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            Room? existingRoom = _repository.GetRoom(request.Id);
            if (existingRoom == null)
            {
                throw new NotFoundException(string.Format(Resources.ResourceNotFound, request.Id));
            }

            if (request.MaxNumberOfBeds == null)
            {
                request.MaxNumberOfBeds = request.BaseNumberOfBeds;
            }
            if (request.ExtraBedPrice == null)
            {
                request.ExtraBedPrice = request.BaseBedPrice;
            }
            if (request.SingleGuestSurcharge == null)
            {
                request.SingleGuestSurcharge = 1;
            }

            _repository.UpdateRoom(request.Id, request.Adapt<Room>());

            return Task.CompletedTask;
        }
    }
}
