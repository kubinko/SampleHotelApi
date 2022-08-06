namespace SampleHotelApi.Domain.Entities
{
    /// <summary>
    /// Guestbook comment entity.
    /// </summary>
    public class Comment : IEntityWithId
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
        /// Timestamp of comment creation.
        /// </summary>
        public DateTimeOffset CreatedTimestamp { get; set; }

        /// <summary>
        /// Timestamp of last comment modification.
        /// </summary>
        public DateTimeOffset LastModifiedTimestamp { get; set; }
    }
}
