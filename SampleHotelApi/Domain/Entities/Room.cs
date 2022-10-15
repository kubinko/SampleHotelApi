namespace SampleHotelApi.Domain.Entities
{
    /// <summary>
    /// Room entity.
    /// </summary>
    public class Room : IEntityWithId
    {
        /// <summary>
        /// Room ID.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Room number.
        /// </summary>
        public int RoomNo { get; set; }

        /// <summary>
        /// Base number of beds in room.
        /// </summary>
        public int BaseNumberOfBeds { get; set; }

        /// <summary>
        /// Maximum possible number of beds in room.
        /// </summary>
        public int MaxNumberOfBeds { get; set; }

        /// <summary>
        /// Price per base bed.
        /// </summary>
        public decimal BaseBedPrice { get; set; }

        /// <summary>
        /// Price for extra bed.
        /// </summary>
        public decimal ExtraBedPrice { get; set; }

        /// <summary>
        /// Surcharge if guest is single.
        /// </summary>
        public decimal SingleGuestSurcharge { get; set; }

        /// <summary>
        /// Room area.
        /// </summary>
        public float Area { get; set; }

        /// <summary>
        /// Room type.
        /// </summary>
        public RoomType RoomType { get; set; }

        /// <summary>
        /// Room description.
        /// </summary>
        public string? RoomDescription { get; set; }
    }

    /// <summary>
    /// Enumeration of room types.
    /// </summary>
    public enum RoomType
    {
        /// <summary>
        /// Classic room.
        /// </summary>
        Classic = 1,

        /// <summary>
        /// Studio.
        /// </summary>
        Studio = 2,

        /// <summary>
        /// Suite.
        /// </summary>
        Suite = 3,

        /// <summary>
        /// Apartment.
        /// </summary>
        Apartment = 4,

        /// <summary>
        /// Deluxe room.
        /// </summary>
        Deluxe = 5
    }
}
