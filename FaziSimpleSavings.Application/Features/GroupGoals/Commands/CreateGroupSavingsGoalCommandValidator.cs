using FluentValidation;

namespace FaziSimpleSavings.Application.Features.GroupGoals.Commands
{
    public class CreateGroupSavingsGoalCommandValidator : AbstractValidator<CreateGroupSavingsGoalCommand>
    {
        public CreateGroupSavingsGoalCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.TargetAmount).GreaterThan(0);
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
