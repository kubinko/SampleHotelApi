using FluentValidation;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="UpdateCommentCommand"/>.
    /// </summary>
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.Text).CommentTextValidation();
        }
    }
}
