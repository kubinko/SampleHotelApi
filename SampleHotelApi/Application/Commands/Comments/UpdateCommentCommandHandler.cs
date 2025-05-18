using Kros.AspNetCore.Exceptions;
using Kros.Utils;
using MediatR;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;
using SampleHotelApi.Properties;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Handler for <see cref="UpdateCommentCommand"/>.
    /// </summary>
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
    {
        private readonly ICommentRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for guestbook comments.</param>
        public UpdateCommentCommandHandler(ICommentRepository repository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
        }

        /// <inheritdoc/>
        public Task Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            Comment? comment = _repository.GetComment(request.Id)
                ?? throw new NotFoundException(string.Format(Resources.ResourceNotFound, request.Id));
            _repository.UpdateComment(request.Id, request.Text);

            return Task.CompletedTask;
        }
    }
}
