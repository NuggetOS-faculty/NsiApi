using FluentValidation;
using NSIProject.Application.Common.Dto.Post;

namespace NSIProject.Application.Common.Validators.Post;

public class PostCreateValidator : AbstractValidator<CreatePostDto>
{
    public PostCreateValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Title is required");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required");
    }
}