using MediatR;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Command for adding new comment to guestbook.
    /// </summary>
    public class NewCommentCommand : IRequest<long>
    {
        /// <summary>
        /// Comment text.
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// Comment author.
        /// </summary>
        public string Author { get; set; } = "";
    }
}
