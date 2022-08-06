using MediatR;
using System.Text.Json.Serialization;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Command for deleting comment from guestbook.
    /// </summary>
    public class DeleteCommentCommand : IRequest
    {
        /// <summary>
        /// ID of command.
        /// </summary>
        [JsonIgnore]
        public long Id { get; set; }
    }
}
