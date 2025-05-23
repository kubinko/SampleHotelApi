﻿using MediatR;
using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for obtaining room reservation by code.
    /// </summary>
    public class GetReservationByCodeQuery : IRequest<GetReservationByCodeQuery.Reservation>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="code">Reservation code.</param>
        public GetReservationByCodeQuery(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Reservation ID.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Room reservation.
        /// </summary>
        public class Reservation
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
}
