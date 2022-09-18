using FluentValidation;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="UpdateReservationCommand"/>.
    /// </summary>
    public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public UpdateReservationCommandValidator()
        {
            RuleFor(x => x.DateFrom).DateValidation();
            RuleFor(x => x.DateTo)
                .DateValidation()
                .Must((command, dateTo) => dateTo > command.DateFrom);
            RuleFor(x => x.NumberOfGuests).NumberOfGuestsValidation();
            RuleFor(x => x.CustomerName).CustomerNameValidation();
            RuleFor(x => x.ContactEmail).CustomerEmailValidation();
            When(x => x.ContactPhoneNumber != null, () => RuleFor(x => x.ContactPhoneNumber).CustomerPhoneValidation());
            RuleFor(x => x.AccomodationPrice).AccomodationPriceValidation();
            RuleFor(x => x.State).ReservationStateValidation();
        }
    }
}
