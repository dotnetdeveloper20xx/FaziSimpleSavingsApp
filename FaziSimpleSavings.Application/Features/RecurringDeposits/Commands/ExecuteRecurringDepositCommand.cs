using MediatR;
using System;

namespace FaziSimpleSavings.Application.RecurringDeposits.Commands
{
    public class ExecuteRecurringDepositCommand : IRequest<bool>
    {
        public Guid RecurringDepositId { get; set; }

        public ExecuteRecurringDepositCommand(Guid recurringDepositId)
        {
            RecurringDepositId = recurringDepositId;
        }
    }
}
