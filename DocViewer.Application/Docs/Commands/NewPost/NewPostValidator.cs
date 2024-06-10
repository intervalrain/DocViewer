using FluentValidation;

namespace DocViewer.Application.Docs.Commands.NewPost;

public class NewPostValidator : AbstractValidator<NewPostCommand>
{
    public NewPostValidator()
    {
        RuleFor(x => x.Title).MinimumLength(3).MaximumLength(32);
        RuleFor(x => x.Category).MinimumLength(3).MaximumLength(32);
        RuleFor(x => x.Author).MinimumLength(1).MaximumLength(32);
    }
}

