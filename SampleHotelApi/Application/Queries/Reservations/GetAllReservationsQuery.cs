using MediatR;
using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for obtaining all room reservations.
    /// </summary>
    public class GetAllReservationsQuery : IRequest<IEnumerable<GetAllReservationsQuery.ReservationHeader>>
    {
        /// <summary>
        /// Room reservation.
        /// </summary>
        public class ReservationHeader
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
            /// Room number.
            /// </summary>
            public int? RoomNo { get; set; }

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
}
