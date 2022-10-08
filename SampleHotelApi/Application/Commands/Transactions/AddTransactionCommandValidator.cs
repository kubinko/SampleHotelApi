using FluentValidation;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="AddTransactionCommand"/>.
    /// </summary>
    public class AddTransactionCommandValidator : AbstractValidator<AddTransactionCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public AddTransactionCommandValidator()
        {
            RuleFor(x => x.Amount).TransactionAmountValidation();
            RuleFor(x => x.TransactionType).TransactionTypeValidation();
        }
    }
}
