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
    /// Handler for <see cref="AddRoomCommand"/>.
    /// </summary>
    public class AddRoomCommandHandler : IRequestHandler<AddRoomCommand, long>
    {
        private readonly IRoomRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for hotel rooms.</param>
        public AddRoomCommandHandler(IRoomRepository repository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
        }

        /// <inheritdoc/>
        public Task<long> Handle(AddRoomCommand request, CancellationToken cancellationToken)
        {
            if (_repository.GetRoomByNumber(request.RoomNo) != null)
            {
                throw new RequestConflictException(string.Format(Resources.DuplicateRoomNumber, request.RoomNo));
            }

            request.MaxNumberOfBeds ??= request.BaseNumberOfBeds;
            request.ExtraBedPrice ??= request.BaseBedPrice;
            request.SingleGuestSurcharge ??= 1;

            var room = request.Adapt<Room>();
            _repository.AddRoom(room);

            return Task.FromResult(room.Id);
        }
    }
}
