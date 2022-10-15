using Kros.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SampleHotelApi.Application.Commands;
using SampleHotelApi.Application.Queries;
using System.Text;

namespace SampleHotelApi.Application.Controllers
{
    /// <summary>
    /// Room reservations controller.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    public class ReservationsController : ApiBaseController
    {
        /// <summary>
        /// Creates new room reservation.
        /// </summary>
        /// <param name="command">Payload.</param>
        /// <response code="201">Reservation created. Reservation ID in response body.</response>
        /// <response code="400">Payload is not valid.</response>
        /// <response code="404">Specified room does not exist.</response>
        /// <response code="409">Room is already occupied in specified dates or
        /// required number of guests exceeds room capacity.</response>
        [HttpPost(Name = nameof(CreateReservation))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateReservation(CreateReservationCommand command)
            => await this.SendCreateCommand(command);

        /// <summary>
        /// Updates room reservation information.
        /// </summary>
        /// <param name="id">ID of reservation to update.</param>
        /// <param name="command">Payload.</param>
        /// <response code="200">Reservation updated.</response>
        /// <response code="400">Payload is not valid.</response>
        /// <response code="404">Reservation was not found or room was not found.</response>
        /// <response code="409">Room is already occupied in specified dates or
        /// required number of guests exceeds room capacity.</response>
        [HttpPut("{id}", Name = nameof(UpdateReservationInformation))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateReservationInformation(long id, UpdateReservationCommand command)
        {
            command.Id = id;
            await this.SendRequest(command);

            return Ok();
        }

        /// <summary>
        /// Updates room reservation state.
        /// </summary>
        /// <param name="id">ID of reservation to update.</param>
        /// <param name="command">Payload.</param>
        /// <response code="200">Reservation state updated.</response>
        /// <response code="400">Payload is not valid.</response>
        /// <response code="404">Reservation was not found.</response>
        [HttpPut("{id}/state", Name = nameof(UpdateReservationState))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateReservationState(long id, UpdateReservationStateCommand command)
        {
            command.Id = id;
            await this.SendRequest(command);

            return Ok();
        }

        /// <summary>
        /// Deletes room reservation.
        /// </summary>
        /// <param name="id">ID of reservation to remove.</param>
        /// <response code="204">Reservation removed.</response>
        [HttpDelete("{id}", Name = nameof(DeleteReservation))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteReservation(long id)
        {
            await this.SendRequest(new DeleteReservationCommand() { Id = id });

            return NoContent();
        }

        /// <summary>
        /// Gets reservation information.
        /// </summary>
        /// <param name="id">Reservation ID.</param>
        /// <response code="200">Ok.</response>
        /// <response code="404">Reservation was not found.</response>
        /// <returns>Reservation.</returns>
        [HttpGet("{id}", Name = nameof(GetReservation))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetReservationQuery.Reservation))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetReservationQuery.Reservation> GetReservation(long id)
            => await this.SendRequest(new GetReservationQuery(id));

        /// <summary>
        /// Gets reservation information.
        /// </summary>
        /// <param name="code">Reservation code.</param>
        /// <response code="200">Ok.</response>
        /// <response code="404">Reservation was not found.</response>
        /// <returns>Reservation.</returns>
        [HttpGet("code/{code}", Name = nameof(GetReservationByCode))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetReservationByCodeQuery.Reservation))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetReservationByCodeQuery.Reservation> GetReservationByCode(string code)
            => await this.SendRequest(new GetReservationByCodeQuery(code));

        /// <summary>
        /// Gets all reservations' headers.
        /// </summary>
        /// <response code="200">Ok.</response>
        /// <returns>All reservations' headers.</returns>
        [HttpGet(Name = nameof(GetAllReservations))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllReservationsQuery.ReservationHeader>))]
        public async Task<IEnumerable<GetAllReservationsQuery.ReservationHeader>> GetAllReservations()
            => await this.SendRequest(new GetAllReservationsQuery());

        /// <summary>
        /// Gets reservations' headers filtered by dates and/or room.
        /// </summary>
        /// <param name="dateFrom">Start date. Can be <c>null</c>.</param>
        /// <param name="dateTo">End date. Can be <c>null</c>.</param>
        /// <param name="roomId">Room ID. Can be <c>null</c>.</param>
        /// <response code="200">Ok.</response>
        /// <returns>Filtered reservations' headers.</returns>
        [HttpGet("filtered", Name = nameof(GetFilteredReservations))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllReservationsQuery.ReservationHeader>))]
        public async Task<IEnumerable<GetAllReservationsQuery.ReservationHeader>> GetFilteredReservations(
            [FromQuery] DateTime? dateFrom,
            [FromQuery] DateTime? dateTo,
            [FromQuery] long? roomId)
        {
            if (dateFrom != null)
            {
                dateTo ??= DateTime.MaxValue;
            }
            else if (dateTo != null)
            {
                dateFrom ??= new DateTime(1900, 1, 1);
            }

            if (roomId != null)
            {
                if (dateFrom != null)
                {
                    return await this.SendRequest(
                        new GetReservationsByRoomAndDatesQuery(roomId.Value, dateFrom.Value, dateTo!.Value));
                }
                else
                {
                    return await this.SendRequest(new GetReservationsByRoomQuery(roomId.Value));
                }
            }
            else if (dateFrom != null)
            {
                return await this.SendRequest(new GetReservationsByDatesQuery(dateFrom.Value, dateTo!.Value));
            }
            else
            {
                return await this.SendRequest(new GetAllReservationsQuery());
            }
        }

        /// <summary>
        /// Gets reservations' CSV report for specified date.
        /// </summary>
        /// <param name="reportDate">Date for report.</param>
        /// <response code="200">Ok.</response>
        /// <returns>Reservations' CSV report.</returns>
        [HttpGet("report/{reportDate}", Name = nameof(GetHotelReservationsSheet))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileResult))]
        [Produces("text/csv")]
        public async Task<FileResult> GetHotelReservationsSheet(DateTime reportDate)
        {
            Response.Headers.ContentDisposition = $"inline; filename=report-{reportDate}.csv";

            IEnumerable<GetAllReservationsQuery.ReservationHeader> reservations =
                await this.SendRequest(new GetReservationsByDatesQuery(reportDate, reportDate));

            var sb = new StringBuilder();
            sb.Append($"{nameof(GetAllReservationsQuery.ReservationHeader.Id)};");
            sb.Append($"{nameof(GetAllReservationsQuery.ReservationHeader.ReservationCode)};");
            sb.Append($"{nameof(GetAllReservationsQuery.ReservationHeader.DateFrom)};");
            sb.Append($"{nameof(GetAllReservationsQuery.ReservationHeader.DateTo)};");
            sb.Append($"{nameof(GetAllReservationsQuery.ReservationHeader.RoomId)};");
            sb.Append($"{nameof(GetAllReservationsQuery.ReservationHeader.RoomNo)};");
            sb.Append($"{nameof(GetAllReservationsQuery.ReservationHeader.AccomodationPrice)};");
            sb.AppendLine(nameof(GetAllReservationsQuery.ReservationHeader.State));
            foreach (var r in reservations)
            {
                sb.AppendLine($"{r.Id};{r.ReservationCode};{r.DateFrom:d};{r.DateTo:d};{r.RoomId};{r.RoomNo};{r.AccomodationPrice};{r.State}");
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv");
        }
    }
}
