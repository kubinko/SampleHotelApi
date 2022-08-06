using MediatR;

namespace SampleHotelApi.Application.Queries
{
    /// <summary>
    /// Query for retrieving page of guest book comments.
    /// </summary>
    public class GetCommentsQuery : IRequest<IEnumerable<GetCommentsQuery.Comment>>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="page">Page.</param>
        /// <param name="entriesPerPage">Number of entries per page.</param>
        public GetCommentsQuery(int page, int entriesPerPage)
        {
            Page = page;
            EntriesPerPage = entriesPerPage;
        }

        /// <summary>
        /// Page.
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Number of entries per page.
        /// </summary>
        public int EntriesPerPage { get; }

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
