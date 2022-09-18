using MediatR;
using System.Text.Json.Serialization;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Command for deleting room reservation.
    /// </summary>
    public class DeleteReservationCommand : IRequest
    {
        /// <summary>
        /// Reservation ID.
        /// </summary>
        [JsonIgnore]
        public long Id { get; set; }
    }
}
