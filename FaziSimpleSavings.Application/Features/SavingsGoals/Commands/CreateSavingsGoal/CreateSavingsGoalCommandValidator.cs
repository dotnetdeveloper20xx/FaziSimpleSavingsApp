using FluentValidation;

namespace Application.Features.SavingsGoals.Commands.CreateSavingsGoal;

public class CreateSavingsGoalCommandValidator : AbstractValidator<CreateSavingsGoalCommand>
{
    public CreateSavingsGoalCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.TargetAmount).GreaterThan(0);
    }
}
