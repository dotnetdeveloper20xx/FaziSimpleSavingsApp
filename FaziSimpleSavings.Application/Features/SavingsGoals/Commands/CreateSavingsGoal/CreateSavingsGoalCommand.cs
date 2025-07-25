using MediatR;

namespace Application.Features.SavingsGoals.Commands.CreateSavingsGoal;

public class CreateSavingsGoalCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public decimal TargetAmount { get; set; }
}
