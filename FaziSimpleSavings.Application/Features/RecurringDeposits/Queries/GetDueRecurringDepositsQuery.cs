using MediatR;
using System;
using System.Collections.Generic;

namespace FaziSimpleSavings.Application.RecurringDeposits.Queries
{
    public class GetDueRecurringDepositsQuery : IRequest<List<RecurringDepositDto>>
    {
    }

    public class RecurringDepositDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GoalId { get; set; }
        public decimal Amount { get; set; }
        public DateTime NextDueDate { get; set; }
    }
}
