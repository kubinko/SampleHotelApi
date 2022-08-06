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
    /// Handler for comment queries.
    /// </summary>
    public class CommentQueryHandler :
        IRequestHandler<GetCommentQuery, GetCommentQuery.Comment>,
        IRequestHandler<GetCommentsQuery, IEnumerable<GetCommentsQuery.Comment>>
    {
        private readonly ICommentRepository _repository;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="repository">Repository for guestbook comments.</param>
        public CommentQueryHandler(ICommentRepository repository)
        {
            _repository = Check.NotNull(repository, nameof(repository));
        }

        /// <inheritdoc/>
        public Task<GetCommentQuery.Comment> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {
            Comment? comment = _repository.GetComment(request.Id);
            if (comment == null)
            {
                throw new NotFoundException(string.Format(Resources.ResourceNotFound, request.Id));
            }

            return Task.FromResult(comment.Adapt<GetCommentQuery.Comment>());
        }

        /// <inheritdoc/>
        public Task<IEnumerable<GetCommentsQuery.Comment>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
            => Task.FromResult(_repository.GetAllComments(request.Page, request.EntriesPerPage)
                .Adapt<IEnumerable<GetCommentsQuery.Comment>>());
    }
}
