using FluentValidation;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="CreateReservationCommand"/>.
    /// </summary>
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.DateFrom).DateValidation();
            RuleFor(x => x.DateTo)
                .DateValidation()
                .Must((command, dateTo) => dateTo > command.DateFrom);
            RuleFor(x => x.RoomId).RoomIdValidation();
            RuleFor(x => x.NumberOfGuests).NumberOfGuestsValidation();
            RuleFor(x => x.CustomerName).CustomerNameValidation();
            RuleFor(x => x.ContactEmail).CustomerEmailValidation();
            When(x => x.ContactPhoneNumber != null, () => RuleFor(x => x.ContactPhoneNumber).CustomerPhoneValidation());
            When(x => x.AccomodationPrice != null, () => RuleFor(x => x.AccomodationPrice).AccomodationPriceValidation());
        }
    }
}
