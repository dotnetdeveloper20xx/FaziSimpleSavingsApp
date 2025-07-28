using Application.Transactions.Commands.CreateManualTransaction;
using FluentValidation;

public class CreateManualTransactionCommandValidator : AbstractValidator<CreateManualTransactionCommand>
{
    public CreateManualTransactionCommandValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.GoalId)
            .NotEqual(Guid.Empty).WithMessage("GoalId is required.");
    }
}
