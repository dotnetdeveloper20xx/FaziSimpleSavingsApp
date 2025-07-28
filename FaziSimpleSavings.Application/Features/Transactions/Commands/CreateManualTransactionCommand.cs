using MediatR;

namespace Application.Transactions.Commands.CreateManualTransaction;

public record CreateManualTransactionCommand(Guid UserId, Guid GoalId, decimal Amount) : IRequest<bool>;
