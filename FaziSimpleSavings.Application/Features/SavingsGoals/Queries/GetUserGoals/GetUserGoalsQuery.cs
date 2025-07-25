using FaziSimpleSavings.Application.Dtos;
using MediatR;

namespace Application.Features.SavingsGoals.Queries.GetUserGoals;

public class GetUserGoalsQuery : IRequest<List<SavingsGoalDto>>
{
    public Guid UserId { get; set; }

    public GetUserGoalsQuery(Guid userId)
    {
        UserId = userId;
    }
}
