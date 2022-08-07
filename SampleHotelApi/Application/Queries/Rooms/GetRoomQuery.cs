using MediatR;
using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for retrieving hotel room.
    /// </summary>
    public class GetRoomQuery : IRequest<GetRoomQuery.Room>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="id">Room ID.</param>
        public GetRoomQuery(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Room ID.
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Hotel room.
        /// </summary>
        public class Room
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
    }
}
