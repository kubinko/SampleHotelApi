using MediatR;
using SampleHotelApi.Domain.Entities;
using System.Text.Json.Serialization;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Command for updating room reservation state.
    /// </summary>
    public class UpdateReservationStateCommand : IRequest
    {
        /// <summary>
        /// Reservation ID.
        /// </summary>
        [JsonIgnore]
        public long Id { get; set; }

        /// <summary>
        /// New reservation state.
        /// </summary>
        public ReservationState State { get; set; }
    }
}
