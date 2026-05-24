using Application.Features.Workspaces.DTOs;
using FluentValidation;

namespace Application.Features.Workspaces.Validators
{
    public class CreateWorkspaceRequestValidator : AbstractValidator<CreateWorkspaceRequest>
    {
        public CreateWorkspaceRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
