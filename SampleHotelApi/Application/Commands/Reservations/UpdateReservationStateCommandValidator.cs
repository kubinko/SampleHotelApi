using FluentValidation;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="UpdateReservationStateCommand"/>.
    /// </summary>
    public class UpdateReservationStateCommandValidator : AbstractValidator<UpdateReservationStateCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public UpdateReservationStateCommandValidator()
        {
            RuleFor(x => x.State).ReservationStateValidation();
        }
    }
}
