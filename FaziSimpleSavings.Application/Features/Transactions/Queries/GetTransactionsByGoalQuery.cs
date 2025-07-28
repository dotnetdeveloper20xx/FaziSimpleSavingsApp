using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Transactions.Queries.GetTransactionsByGoal;

public record GetTransactionsByGoalQuery(Guid UserId, Guid GoalId) : IRequest<List<TransactionDto>>;
