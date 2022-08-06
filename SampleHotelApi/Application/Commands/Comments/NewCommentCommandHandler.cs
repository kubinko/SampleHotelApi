using Kros.Utils;
using Mapster;
using MediatR;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Handler for <see cref="NewCommentCommand"/>.
    /// </summary>
    public class NewCommentCommandHandler : IRequestHandler<NewCommentCommand, long>
    {
        private readonly ICommentRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for guestbook comments.</param>
        public NewCommentCommandHandler(ICommentRepository repository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
        }

        /// <inheritdoc/>
        public Task<long> Handle(NewCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = request.Adapt<Comment>();
            _repository.CreateComment(comment);

            return Task.FromResult(comment.Id);
        }
    }
}
