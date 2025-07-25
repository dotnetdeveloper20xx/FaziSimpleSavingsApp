﻿using Application.Interfaces;
using FaziSimpleSavings.Application.Notifications.Commands;
using FaziSimpleSavings.Application.RecurringDeposits.Commands;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FaziSimpleSavings.Application.RecurringDeposits.Handlers
{
    public class ExecuteRecurringDepositCommandHandler : IRequestHandler<ExecuteRecurringDepositCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;

        public ExecuteRecurringDepositCommandHandler(IAppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }


        public async Task<bool> Handle(ExecuteRecurringDepositCommand request, CancellationToken cancellationToken)
        {
            var recurring = await _context.RecurringDeposits
                .FirstOrDefaultAsync(x => x.Id == request.RecurringDepositId, cancellationToken);
            if (recurring == null) return false;

            var goal = await _context.SavingsGoals
                .FirstOrDefaultAsync(x => x.Id == recurring.GoalId, cancellationToken);
            if (goal == null) return false;

            goal.AddDeposit(recurring.Amount);

            var transaction = new Transaction(recurring.UserId, recurring.GoalId, recurring.Amount);
            _context.Transactions.Add(transaction);

            recurring.UpdateNextDueDate();

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Send(new CreateNotificationCommand(
                        recurring.UserId,
                        $"£{recurring.Amount} was deposited into your goal automatically."
                    ));


            return true;
        }
    }

}
