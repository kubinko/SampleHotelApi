using MediatR;
using System.Text.Json.Serialization;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Command for updating comment in guestbook.
    /// </summary>
    public class UpdateCommentCommand : IRequest
    {
        /// <summary>
        /// ID of command.
        /// </summary>
        [JsonIgnore]
        public long Id { get; set; }

        /// <summary>
        /// Comment text.
        /// </summary>
        public string Text { get; set; } = "";
    }
}
