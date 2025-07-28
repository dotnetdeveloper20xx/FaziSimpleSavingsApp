
using Application.Common.Security;
using Application.Interfaces;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Transactions.Commands.CreateManualTransaction;

public class CreateManualTransactionCommandHandler : IRequestHandler<CreateManualTransactionCommand, bool>
{
    private readonly IAppDbContext _context;
    private readonly IOwnershipValidator _ownershipValidator;

    public CreateManualTransactionCommandHandler(IAppDbContext context, IOwnershipValidator ownershipValidator)
    {
        _context = context;
        _ownershipValidator = ownershipValidator;
    }

    public async Task<bool> Handle(CreateManualTransactionCommand request, CancellationToken cancellationToken)
    {
        // Ownership check
        if (!await _ownershipValidator.UserOwnsGoal(request.UserId, request.GoalId))
            return false;

        var goal = await _context.SavingsGoals
            .FirstOrDefaultAsync(g => g.Id == request.GoalId, cancellationToken);

        if (goal == null)
            return false;

        goal.AddDeposit(request.Amount);

        var transaction = new Transaction(request.UserId, request.GoalId, request.Amount);
        _context.Transactions.Add(transaction);

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
