using FluentValidation;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="AddRoomCommand"/>.
    /// </summary>
    public class AddRoomCommandValidator : AbstractValidator<AddRoomCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public AddRoomCommandValidator()
        {
            RuleFor(x => x.RoomNo).RoomNumberValidation();
            RuleFor(x => x.BaseNumberOfBeds).BaseBedCountValidation();
            When(x => x.MaxNumberOfBeds != null, () => RuleFor(x => x.MaxNumberOfBeds).MaximumBedCountValidation());
            RuleFor(x => x.BaseBedPrice).BaseBedPriceValidation();
            When(x => x.ExtraBedPrice != null, () => RuleFor(x => x.ExtraBedPrice).ExtraBedPriceValidation());
            When(x => x.SingleGuestSurcharge != null, () => RuleFor(x => x.SingleGuestSurcharge).SingleGuestSurchargeValidation());
            RuleFor(x => x.Area).AreaValidation();
            RuleFor(x => x.RoomType).RoomTypeValidation();
            When(x => x.RoomDescription != null, () => RuleFor(x => x.RoomDescription).RoomDescriptionValidation());
        }
    }
}
