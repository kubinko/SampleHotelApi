using Kros.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleHotelApi.Application.Commands;
using SampleHotelApi.Application.Queries;
using SampleHotelApi.Infrastructure;

namespace SampleHotelApi.Application.Controllers
{
    /// <summary>
    /// Hotel rooms controller.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    public class RoomsController : ApiBaseController
    {
        /// <summary>
        /// Adds new hotel room.
        /// </summary>
        /// <param name="command">Payload.</param>
        /// <response code="201">Room added. Room ID in response body.</response>
        /// <response code="400">Payload is not valid.</response>
        /// <response code="403">User does not have sufficient rights to add rooms.</response>
        /// <response code="409">Room with specified number already exists.</response>
        /// <response code="415">Payload type is not valid (i.e. if payload is empty).</response>
        [HttpPost(Name = nameof(AddRoom))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [Authorize(Policy = Policies.AdminPolicyName)]
        public async Task<IActionResult> AddRoom(AddRoomCommand command)
            => await this.SendCreateCommand(command);

        /// <summary>
        /// Updates hotel room information.
        /// </summary>
        /// <param name="id">ID of room to update.</param>
        /// <param name="command">Payload.</param>
        /// <response code="200">Room updated.</response>
        /// <response code="400">Payload is not valid.</response>
        /// <response code="403">User does not have sufficient rights to update rooms.</response>
        /// <response code="404">Room was not found.</response>
        /// <response code="415">Payload type is not valid (i.e. if payload is empty).</response>
        [HttpPut("{id}", Name = nameof(UpdateRoomInformation))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [Authorize(Policy = Policies.AdminPolicyName)]
        public async Task<IActionResult> UpdateRoomInformation(long id, UpdateRoomCommand command)
        {
            command.Id = id;
            await this.SendRequest(command);

            return Ok();
        }

        /// <summary>
        /// Removes hotel room.
        /// </summary>
        /// <param name="id">ID of room to remove.</param>
        /// <response code="204">Room removed.</response>
        /// <response code="403">User does not have sufficient rights to remove rooms.</response>
        [HttpDelete("{id}", Name = nameof(RemoveRoom))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = Policies.AdminPolicyName)]
        public async Task<IActionResult> RemoveRoom(long id)
        {
            await this.SendRequest(new RemoveRoomCommand() { Id = id });

            return NoContent();
        }

        /// <summary>
        /// Gets room information. 
        /// </summary>
        /// <param name="id">Room ID.</param>
        /// <response code="200">Ok.</response>
        /// <response code="404">Room was not found.</response>
        /// <returns>Room.</returns>
        [HttpGet("{id}", Name = nameof(GetRoom))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetRoomQuery.Room))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetRoomQuery.Room> GetRoom(long id)
            => await this.SendRequest(new GetRoomQuery(id));

        /// <summary>
        /// Gets all room headers.
        /// </summary>
        /// <response code="200">Ok.</response>
        /// <returns>All rooms' headers.</returns>
        [HttpGet(Name = nameof(GetAllRooms))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllRoomsQuery.RoomHeader>))]
        public async Task<IEnumerable<GetAllRoomsQuery.RoomHeader>> GetAllRooms()
            => await this.SendRequest(new GetAllRoomsQuery());
    }
}
