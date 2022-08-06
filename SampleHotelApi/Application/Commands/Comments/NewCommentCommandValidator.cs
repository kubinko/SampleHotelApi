using FluentValidation;

namespace SampleHotelApi.Application.Commands
{
    /// <summary>
    /// Validator for <see cref="NewCommentCommand"/>.
    /// </summary>
    public class NewCommentCommandValidator : AbstractValidator<NewCommentCommand>
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public NewCommentCommandValidator()
        {
            RuleFor(x => x.Text).CommentTextValidation();
            RuleFor(x => x.Author).CommentAuthorValidation();
        }
    }
}
