using MediatR;
using SampleHotelApi.Domain.Entities;
using System.Text.Json.Serialization;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Command for updating room reservation information.
    /// </summary>
    public class UpdateReservationCommand : IRequest
    {
        /// <summary>
        /// Reservation ID.
        /// </summary>
        [JsonIgnore]
        public long Id { get; set; }

        /// <summary>
        /// Start of accomodation.
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// End of accomodation.
        /// </summary>
        public DateTime DateTo { get; set; }

        /// <summary>
        /// Number of accomodated people.
        /// </summary>
        public int NumberOfGuests { get; set; }

        /// <summary>
        /// Customer name and surname.
        /// </summary>
        public string CustomerName { get; set; } = "";

        /// <summary>
        /// Contact e-mail.
        /// </summary>
        public string ContactEmail { get; set; } = "";

        /// <summary>
        /// Contact phone number.
        /// </summary>
        public string? ContactPhoneNumber { get; set; }

        /// <summary>
        /// Base agreed-upon price for accomodation.
        /// </summary>
        public decimal AccomodationPrice { get; set; }

        /// <summary>
        /// Current reservation state.
        /// </summary>
        public ReservationState State { get; set; }
    }
}
