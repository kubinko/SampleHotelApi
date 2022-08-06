using FluentValidation;

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
    }
}
