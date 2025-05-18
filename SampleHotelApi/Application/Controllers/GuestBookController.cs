using Kros.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleHotelApi.Application.Commands;
using SampleHotelApi.Application.Queries;

namespace SampleHotelApi.Application.Controllers
{
    /// <summary>
    /// Guest book controller.
    /// </summary>    
    [AllowAnonymous]
    [Produces("application/json")]
    public class GuestBookController : ApiBaseController
    {
        /// <summary>
        /// Adds new guestbook comment. 
        /// </summary>
        /// <param name="command">Payload.</param>
        /// <response code="201">Comment added. Comment ID in response body.</response>
        /// <response code="400">Payload is not valid.</response>
        [HttpPost(Name = nameof(CreateComment))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateComment(NewCommentCommand command)
            => await this.SendCreateCommand(command);

        /// <summary>
        /// Updates guestbook comment text. 
        /// </summary>
        /// <param name="id">ID of comment to update.</param>
        /// <param name="command">Payload.</param>
        /// <response code="200">Comment updated.</response>
        /// <response code="400">Payload is not valid.</response>
        /// <response code="404">Comment was not found.</response>
        [HttpPut("{id}", Name = nameof(UpdateCommentText))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCommentText(long id, UpdateCommentCommand command)
        {
            command.Id = id;
            await this.Mediator().Send(command);

            return Ok();
        }

        /// <summary>
        /// Removes guestbook comment. 
        /// </summary>
        /// <param name="id">ID of comment to remove.</param>
        /// <response code="204">Comment removed.</response>
        [HttpDelete("{id}", Name = nameof(RemoveComment))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveComment(long id)
        {
            await this.Mediator().Send(new DeleteCommentCommand() { Id = id });

            return NoContent();
        }

        /// <summary>
        /// Gets guestbook comment. 
        /// </summary>
        /// <param name="id">Comment ID.</param>
        /// <response code="200">Ok.</response>
        /// <response code="404">Comment was not found.</response>
        /// <returns>Comment.</returns>
        [HttpGet("{id}", Name = nameof(GetComment))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCommentQuery.Comment))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetCommentQuery.Comment> GetComment(long id)
            => await this.SendRequest(new GetCommentQuery(id));

        /// <summary>
        /// Gets page of guestbook comments.
        /// </summary>
        /// <param name="page">Page.</param>
        /// <param name="entriesPerPage">Number of entries per page.</param>
        /// <response code="200">Ok.</response>
        /// <returns>Page of comments.</returns>
        [HttpGet(Name = nameof(GetCommentPage))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetCommentsQuery.Comment>))]
        public async Task<IEnumerable<GetCommentsQuery.Comment>> GetCommentPage(int page, int entriesPerPage)
            => await this.SendRequest(new GetCommentsQuery(page, entriesPerPage));
    }
}
