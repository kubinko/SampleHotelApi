using MediatR;
using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for retrieving all hotel rooms.
    /// </summary>
    public class GetAllRoomsQuery : IRequest<IEnumerable<GetAllRoomsQuery.RoomHeader>>
    {
        /// <summary>
        /// Hotel room.
        /// </summary>
        public class RoomHeader
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
            /// Price per base bed.
            /// </summary>
            public decimal BaseBedPrice { get; set; }

            /// <summary>
            /// Room type.
            /// </summary>
            public RoomType RoomType { get; set; }
        }
    }
}
