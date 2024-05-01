using FluentValidation;
using NSIProject.Application.Commands.Post;

namespace NSIProject.Application.Common.Validators.Post;

public class ProductCreateCommandModelValidator : AbstractValidator<CreatePostCommand>
{
    public ProductCreateCommandModelValidator()
    {
        RuleFor(x => x.Request)
            .SetValidator(new PostCreateValidator());
    }
}