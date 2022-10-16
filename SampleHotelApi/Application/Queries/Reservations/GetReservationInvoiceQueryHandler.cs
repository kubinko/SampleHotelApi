using Kros.Utils;
using MediatR;
using SampleHotelApi.Domain;

namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Handler for <see cref="GetReservationInvoiceQuery"/>.
    /// </summary>
    public class GetReservationInvoiceQueryHandler : IRequestHandler<GetReservationInvoiceQuery, bool>
    {
        private readonly IReservationRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for room reservations.</param>
        public GetReservationInvoiceQueryHandler(IReservationRepository repository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
        }

        /// <inheritdoc/>
        public Task<bool> Handle(GetReservationInvoiceQuery request, CancellationToken cancellationToken)
        {
            var reservation = _repository.GetReservation(request.ReservationId);
            return Task.FromResult(reservation?.InvoiceGenerated == true);
        }
    }
}
