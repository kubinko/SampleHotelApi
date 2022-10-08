using FluentValidation;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="AddPaymentCommand"/>.
    /// </summary>
    public class AddPaymentCommandValidator : AbstractValidator<AddPaymentCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public AddPaymentCommandValidator()
        {
            RuleFor(x => x.Amount).PaymentAmountValidation();
            RuleFor(x => x.PaymentReference).ReferenceValidation();
        }
    }
}
