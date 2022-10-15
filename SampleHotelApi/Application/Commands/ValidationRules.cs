using FluentValidation;
using SampleHotelApi.Domain.Entities;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Base validation rules.
    /// </summary>
    public static class ValidationRules
    {
        #region Guestbook Comment

        /// <summary>
        /// Validation rule for comment author.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> CommentAuthorValidation<T>(this IRuleBuilder<T, string> rule)
            => rule
                .NotEmpty()
                .MaximumLength(50);

        /// <summary>
        /// Validation rule for comment text.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> CommentTextValidation<T>(this IRuleBuilder<T, string> rule)
            => rule
                .NotEmpty()
                .MaximumLength(255);

        #endregion

        #region Hotel Room

        /// <summary>
        /// Validation rule for room number.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, int> RoomNumberValidation<T>(this IRuleBuilder<T, int> rule)
            => rule
                .NotEmpty()
                .GreaterThan(0);

        /// <summary>
        /// Validation rule for base bed count.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, int> BaseBedCountValidation<T>(this IRuleBuilder<T, int> rule)
            => rule
                .NotEmpty()
                .GreaterThan(0);

        /// <summary>
        /// Validation rule for maximum bed count.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, int?> MaximumBedCountValidation<T>(this IRuleBuilder<T, int?> rule)
            => rule.GreaterThan(0);

        /// <summary>
        /// Validation rule for base bed price.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, decimal> BaseBedPriceValidation<T>(this IRuleBuilder<T, decimal> rule)
            => rule
                .NotNull()
                .GreaterThanOrEqualTo(0);

        /// <summary>
        /// Validation rule for extra bed price.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, decimal?> ExtraBedPriceValidation<T>(this IRuleBuilder<T, decimal?> rule)
            => rule.GreaterThanOrEqualTo(0);

        /// <summary>
        /// Validation rule for single guest surcharge.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, decimal?> SingleGuestSurchargeValidation<T>(this IRuleBuilder<T, decimal?> rule)
            => rule.GreaterThanOrEqualTo(0);

        /// <summary>
        /// Validation rule for area.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, float> AreaValidation<T>(this IRuleBuilder<T, float> rule)
            => rule
                .NotNull()
                .GreaterThanOrEqualTo(0);

        /// <summary>
        /// Validation rule for room type.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, RoomType> RoomTypeValidation<T>(this IRuleBuilder<T, RoomType> rule)
            => rule
                .NotEmpty()
                .IsInEnum();

        /// <summary>
        /// Validation rule for room description.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string?> RoomDescriptionValidation<T>(this IRuleBuilder<T, string?> rule)
            => rule.MaximumLength(500);

        #endregion

        #region Reservation

        /// <summary>
        /// Validation rule for room ID.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, long> RoomIdValidation<T>(this IRuleBuilder<T, long> rule)
            => rule
                .NotEmpty()
                .GreaterThan(0);

        /// <summary>
        /// Validation rule for date.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, DateTime> DateValidation<T>(this IRuleBuilder<T, DateTime> rule)
            => rule
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.Now);

        /// <summary>
        /// Validation rule for number of guests.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, int> NumberOfGuestsValidation<T>(this IRuleBuilder<T, int> rule)
            => rule
                .NotEmpty()
                .GreaterThan(0);

        /// <summary>
        /// Validation rule for customer name.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> CustomerNameValidation<T>(this IRuleBuilder<T, string> rule)
            => rule
                .NotEmpty()
                .MaximumLength(255);

        /// <summary>
        /// Validation rule for customer e-mail.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> CustomerEmailValidation<T>(this IRuleBuilder<T, string> rule)
            => rule
                .NotEmpty()
                .MaximumLength(255)
                .EmailAddress();

        /// <summary>
        /// Validation rule for customer phone number.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string?> CustomerPhoneValidation<T>(this IRuleBuilder<T, string?> rule)
            => rule
                .MaximumLength(20);

        /// <summary>
        /// Validation rule for accomodation price.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, decimal> AccomodationPriceValidation<T>(this IRuleBuilder<T, decimal> rule)
            => rule.GreaterThanOrEqualTo(0);

        /// <summary>
        /// Validation rule for accomodation price.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, decimal?> AccomodationPriceValidation<T>(this IRuleBuilder<T, decimal?> rule)
            => rule.GreaterThanOrEqualTo(0);

        /// <summary>
        /// Validation rule for reservation state.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, ReservationState> ReservationStateValidation<T>(
            this IRuleBuilder<T, ReservationState> rule)
            => rule
                .NotEmpty()
                .IsInEnum();

        #endregion

        #region Transaction

        /// <summary>
        /// Validation rule for transaction amount.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, decimal> TransactionAmountValidation<T>(this IRuleBuilder<T, decimal> rule)
            => rule.GreaterThan(0);

        /// <summary>
        /// Validation rule for transaction type state.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, TransactionType> TransactionTypeValidation<T>(
            this IRuleBuilder<T, TransactionType> rule)
            => rule
                .NotEmpty()
                .IsInEnum();

        #endregion

        #region Payment

        /// <summary>
        /// Validation rule for payment amount.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, decimal> PaymentAmountValidation<T>(this IRuleBuilder<T, decimal> rule)
            => rule.GreaterThan(0);

        /// <summary>
        /// Validation rule for payment reference.
        /// </summary>
        /// <typeparam name="T">Command type.</typeparam>
        /// <param name="rule">Rule</param>
        /// <returns>Validation rule</returns>
        public static IRuleBuilderOptions<T, string> ReferenceValidation<T>(
            this IRuleBuilder<T, string> rule)
            => rule
                .NotEmpty()
                .MaximumLength(10);

        #endregion
    }
}
