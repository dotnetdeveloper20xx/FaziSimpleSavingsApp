using Application.Common.Security;
using Application.Interfaces;
using Application.Transactions.Commands.CreateManualTransaction;
using FaziSimpleSavings.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Transactions.Commands;
public class CreateManualTransactionCommandHandler : IRequestHandler<CreateManualTransactionCommand, Unit>
{
    private readonly IAppDbContext _context;
    private readonly IOwnershipValidator _ownershipValidator;

    public CreateManualTransactionCommandHandler(IAppDbContext context, IOwnershipValidator ownershipValidator)
    {
        _context = context;
        _ownershipValidator = ownershipValidator;
    }

    public async Task<Unit> Handle(CreateManualTransactionCommand request, CancellationToken cancellationToken)
    {
        var ownsGoal = await _ownershipValidator.UserOwnsGoal(request.UserId, request.GoalId);
        if (!ownsGoal)
            throw new NotFoundException("SavingsGoal", request.GoalId);

        var goal = await _context.SavingsGoals
            .FirstOrDefaultAsync(g => g.Id == request.GoalId, cancellationToken);

        if (goal == null)
            throw new NotFoundException("SavingsGoal", request.GoalId);

        goal.AddDeposit(request.Amount);

        var transaction = new Transaction(request.UserId, request.GoalId, request.Amount);
        _context.Transactions.Add(transaction);

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

