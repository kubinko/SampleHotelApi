using Kros.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SampleHotelApi.Application.Commands;
using SampleHotelApi.Application.Queries;

namespace SampleHotelApi.Application.Controllers
{
    /// <summary>
    /// Transactions controller.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    public class TransactionsController : ApiBaseController
    {
        /// <summary>
        /// Adds new transaction.
        /// </summary>
        /// <param name="command">Payload.</param>
        /// <response code="201">Transaction added. Transaction ID in response body.</response>
        /// <response code="400">Payload is not valid.</response>
        /// <response code="404">Specified reservation does not exist.</response>
        /// <response code="409">Reservation is already fully paid.</response>
        [HttpPost(Name = nameof(AddTransaction))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddTransaction(AddTransactionCommand command)
            => await this.SendCreateCommand(command);

        /// <summary>
        /// Deletes transaction.
        /// </summary>
        /// <param name="id">ID of transaction to remove.</param>
        /// <response code="204">Transaction removed.</response>
        [HttpDelete("{id}", Name = nameof(DeleteTransaction))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTransaction(long id)
        {
            await this.SendRequest(new DeleteTransactionCommand() { Id = id });

            return NoContent();
        }

        /// <summary>
        /// Gets transaction information.
        /// </summary>
        /// <param name="id">Transaction ID.</param>
        /// <response code="200">Ok.</response>
        /// <response code="404">Transaction was not found.</response>
        /// <returns>Transaction.</returns>
        [HttpGet("{id}", Name = nameof(GetTransaction))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTransactionQuery.Transaction))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetTransactionQuery.Transaction> GetTransaction(long id)
            => await this.SendRequest(new GetTransactionQuery(id));

        /// <summary>
        /// Gets all transactions.
        /// </summary>
        /// <response code="200">Ok.</response>
        /// <returns>All transactions.</returns>
        [HttpGet(Name = nameof(GetAllTransactions))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllTransactionsQuery.Transaction>))]
        public async Task<IEnumerable<GetAllTransactionsQuery.Transaction>> GetAllTransactions()
            => await this.SendRequest(new GetAllTransactionsQuery());

        /// <summary>
        /// Gets transactions filtered by dates and/or reservation.
        /// </summary>
        /// <param name="dateFrom">Start date. Can be <c>null</c>.</param>
        /// <param name="dateTo">End date. Can be <c>null</c>.</param>
        /// <param name="reservationId">Reservation ID. Can be <c>null</c>.</param>
        /// <response code="200">Ok.</response>
        /// <returns>Filtered reservations' headers.</returns>
        [HttpGet("filtered", Name = nameof(GetFilteredTransactions))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllTransactionsQuery.Transaction>))]
        public async Task<IEnumerable<GetAllTransactionsQuery.Transaction>> GetFilteredTransactions(
            [FromQuery] DateTimeOffset? dateFrom,
            [FromQuery] DateTimeOffset? dateTo,
            [FromQuery] long? reservationId)
        {
            if (dateFrom != null)
            {
                dateTo ??= DateTime.MaxValue;
            }
            else if (dateTo != null)
            {
                dateFrom ??= new DateTime(1900, 1, 1);
            }

            if (reservationId != null)
            {
                if (dateFrom != null)
                {
                    return await this.SendRequest(
                        new GetTransactionsByReservationAndDatesQuery(reservationId.Value, dateFrom.Value, dateTo!.Value));
                }
                else
                {
                    return await this.SendRequest(new GetTransactionsByReservationQuery(reservationId.Value));
                }
            }
            else if (dateFrom != null)
            {
                return await this.SendRequest(new GetTransactionsByDatesQuery(dateFrom.Value, dateTo!.Value));
            }
            else
            {
                return await this.SendRequest(new GetAllTransactionsQuery());
            }
        }

        /// <summary>
        /// Adds dummy payment to hotel.
        /// </summary>
        /// <param name="command">Payload.</param>
        /// <response code="201">Payment added.</response>
        /// <response code="400">Payload is not valid.</response>
        [HttpPost("payment", Name = nameof(AddDummyPayment))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDummyPayment(AddPaymentCommand command)
        {
            await this.SendRequest(command);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
