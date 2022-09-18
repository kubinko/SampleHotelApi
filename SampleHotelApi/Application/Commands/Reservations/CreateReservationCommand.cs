using MediatR;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Command for creating room reservation.
    /// </summary>
    public class CreateReservationCommand : IRequest<long>
    {
        /// <summary>
        /// Start of accomodation.
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// End of accomodation.
        /// </summary>
        public DateTime DateTo { get; set; }

        /// <summary>
        /// ID of room for accomodation.
        /// </summary>
        public long RoomId { get; set; }

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
        public decimal? AccomodationPrice { get; set; }
    }
}
