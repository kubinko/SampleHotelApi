using Kros.Utils;
using MediatR;
using SampleHotelApi.Domain;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Handler for <see cref="DeleteCommentCommand"/>.
    /// </summary>
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly ICommentRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for guestbook comments.</param>
        public DeleteCommentCommandHandler(ICommentRepository repository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
        }

        /// <inheritdoc/>
        public Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            _repository.DeleteComment(request.Id);

            return Task.CompletedTask;
        }
    }
}
