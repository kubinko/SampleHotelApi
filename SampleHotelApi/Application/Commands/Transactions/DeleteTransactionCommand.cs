using MediatR;
using System.Text.Json.Serialization;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Command for deleting transactions.
    /// </summary>
    public class DeleteTransactionCommand : IRequest
    {
        /// <summary>
        /// Transaction ID.
        /// </summary>
        [JsonIgnore]
        public long Id { get; set; }
    }
}
