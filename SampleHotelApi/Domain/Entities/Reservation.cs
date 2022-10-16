namespace SampleHotelApi.Domain.Entities
{
    /// <summary>
    /// Room reservation entity.
    /// </summary>
    public class Reservation : IEntityWithId
    {
        /// <summary>
        /// Reservation ID.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Unique reservation code.
        /// </summary>
        public string ReservationCode { get; set; } = "";

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
        public decimal AccomodationPrice { get; set; }

        /// <summary>
        /// Current reservation state.
        /// </summary>
        public ReservationState State { get; set; }

        /// <summary>
        /// Whether invoice for reservation has been generated.
        /// </summary>
        public bool InvoiceGenerated { get; set; }

        /// <summary>
        /// Timestamp of reservation creation.
        /// </summary>
        public DateTimeOffset CreatedTimestamp { get; set; }

        /// <summary>
        /// ID of employee that created the reservation.
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// Timestamp of last reservation modification.
        /// </summary>
        public DateTimeOffset LastModifiedTimestamp { get; set; }

        /// <summary>
        /// ID of employee who was the last to modify the reservation.
        /// </summary>
        public long LastModifiedBy { get; set; }
    }

    /// <summary>
    /// Enumeration of possible reservation states.
    /// </summary>
    public enum ReservationState
    {
        /// <summary>
        /// Unefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Reservation was created.
        /// </summary>
        Created = 1,

        /// <summary>
        /// Accomodation already finished, without paying.
        /// </summary>
        Finished = 2,

        /// <summary>
        /// Reservation was cancelled.
        /// </summary>
        Cancelled = 3
    }
}
