using FluentValidation;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="UpdateRoomCommand"/>.
    /// </summary>
    public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public UpdateRoomCommandValidator()
        {
            RuleFor(x => x.BaseNumberOfBeds).BaseBedCountValidation();
            RuleFor(x => x.MaxNumberOfBeds).MaximumBedCountValidation();
            When(
                x => x.MaxNumberOfBeds != null,
                () => RuleFor(x => x.MaxNumberOfBeds).Must((command, beds) => beds >= command.BaseNumberOfBeds));
            RuleFor(x => x.BaseBedPrice).BaseBedPriceValidation();
            When(x => x.ExtraBedPrice != null, () => RuleFor(x => x.ExtraBedPrice).ExtraBedPriceValidation());
            When(x => x.SingleGuestSurcharge != null, () => RuleFor(x => x.SingleGuestSurcharge).SingleGuestSurchargeValidation());
            RuleFor(x => x.Area).AreaValidation();
            RuleFor(x => x.RoomType).RoomTypeValidation();
            When(x => x.RoomDescription != null, () => RuleFor(x => x.RoomDescription).RoomDescriptionValidation());
        }
    }
}
