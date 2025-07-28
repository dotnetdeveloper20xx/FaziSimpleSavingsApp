using FaziSimpleSavings.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.SavingsGoals.Queries.GetGoalProgress;

public record GetGoalProgressQuery(Guid UserId) : IRequest<List<GoalProgressDto>>;
