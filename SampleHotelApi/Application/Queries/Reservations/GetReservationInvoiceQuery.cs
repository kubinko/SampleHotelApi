using MediatR;

namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for obtaining reservation invoice.
    /// </summary>
    public class GetReservationInvoiceQuery : IRequest<bool>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="reservationId">Reservation ID.</param>
        public GetReservationInvoiceQuery(long reservationId)
        {
            ReservationId = reservationId;
        }

        /// <summary>
        /// Reservation ID.
        /// </summary>
        public long ReservationId { get; }
    }
}
