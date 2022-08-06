using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Domain
{
    /// <summary>
    /// Interface describing repository for comments.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Creates new guestbook comment.
        /// </summary>
        /// <param name="comment">Comment.</param>
        /// <returns>New commend ID.</returns>
        long CreateComment(Comment comment);

        /// <summary>
        /// Updates text of comment with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Comment ID.</param>
        /// <param name="newText">New comment text.</param>
        void UpdateComment(long id, string newText);

        /// <summary>
        /// Deletes comment with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Comment ID.</param>
        void DeleteComment(long id);

        /// <summary>
        /// Attempts to retrieve comment with <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Comment ID.</param>
        /// <returns>Comment; <c>null</c>, if comment with specified ID does not exist.</returns>
        Comment? GetComment(long id);

        /// <summary>
        /// Retrieves <paramref name="countEntriesPerPage"/> guestbook comments from <paramref name="page"/>
        /// sorted by creation ascending.
        /// </summary>
        /// <param name="page">Page of comments.</param>
        /// <param name="countEntriesPerPage">Number of entries per page.</param>
        /// <returns>Comments.</returns>
        IEnumerable<Comment> GetAllComments(int page, int countEntriesPerPage);
    }
}
