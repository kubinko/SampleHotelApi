using Kros.AspNetCore.Exceptions;
using Kros.Utils;
using Mapster;
using MediatR;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;
using SampleHotelApi.Properties;

namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Handler for <see cref="GetTransactionQuery"/>.
    /// </summary>
    public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, GetTransactionQuery.Transaction>
    {
        private readonly ITransactionRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for transactions.</param>
        public GetTransactionQueryHandler(ITransactionRepository repository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
        }

        /// <inheritdoc/>
        public Task<GetTransactionQuery.Transaction> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            Transaction? transaction = _repository.GetTransaction(request.Id);
            if (transaction == null)
            {
                throw new NotFoundException(string.Format(Resources.ResourceNotFound, request.Id));
            }

            return Task.FromResult(transaction.Adapt<GetTransactionQuery.Transaction>());
        }
    }
}
