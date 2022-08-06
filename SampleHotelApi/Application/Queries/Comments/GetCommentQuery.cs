using MediatR;

namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for retrieving guest book comment.
    /// </summary>
    public class GetCommentQuery : IRequest<GetCommentQuery.Comment>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="id">Comment ID.</param>
        public GetCommentQuery(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Comment ID.
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Guestbook comment.
        /// </summary>
        public class Comment
        {
            /// <summary>
            /// Comment ID.
            /// </summary>
            public long Id { get; set; }

            /// <summary>
            /// Comment text.
            /// </summary>
            public string Text { get; set; } = "";

            /// <summary>
            /// Comment author.
            /// </summary>
            public string Author { get; set; } = "";

            /// <summary>
            /// Timestamp of comment last modification.
            /// </summary>
            public DateTimeOffset LastModifiedTimestamp;
        }
    }
}
