using Kros.Utils;
using SampleHotelApi.Domain;
using SampleHotelApi.Domain.Entities;
using SampleHotelApi.Infrastructure.DbMock;

namespace SampleHotelApi.Infrastructure
{
    /// <summary>
    /// Repository for guestbook comments.
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        private readonly IDatabase _db;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="db">Database.</param>
        public CommentRepository(IDatabase db)
        {
            _db = Check.NotNull(db, nameof(db));
        }

        /// <inheritdoc/>
        public long CreateComment(Comment comment)
        {
            comment.CreatedTimestamp = DateTimeProvider.DateTimeOffsetUtcNow;
            comment.LastModifiedTimestamp = comment.CreatedTimestamp;

            return _db.Comments.Add(comment);
        }

        /// <inheritdoc/>
        public void UpdateComment(long id, string newText)
        {
            Comment? existingComment = _db.Comments.Get(id);
            if (existingComment != null)
            {
                existingComment.Text = newText;
                existingComment.LastModifiedTimestamp = DateTimeProvider.DateTimeOffsetUtcNow;
            }
        }

        /// <inheritdoc/>
        public void DeleteComment(long id)
            => _db.Comments.Delete(id);

        /// <inheritdoc/>
        public Comment? GetComment(long id)
            => _db.Comments.Get(id);

        /// <inheritdoc/>
        public IEnumerable<Comment> GetAllComments(int page, int countEntriesPerPage)
            => _db.Comments.GetAll()
                .OrderBy(c => c.CreatedTimestamp)
                .Skip((page - 1) * countEntriesPerPage)
                .Take(countEntriesPerPage);
    }
}
