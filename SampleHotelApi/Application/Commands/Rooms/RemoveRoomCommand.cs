using MediatR;
using System.Text.Json.Serialization;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Command for removing hotel room.
    /// </summary>
    public class RemoveRoomCommand : IRequest
    {
        /// <summary>
        /// Room ID.
        /// </summary>
        [JsonIgnore]
        public long Id { get; set; }
    }
}
