using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using FaziSimpleSavings.Application.RecurringDeposits.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FaziSimpleSavings.Application.RecurringDeposits.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks; 
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    namespace FaziSimpleSavings.Application.RecurringDeposits.Handlers
    {
        public class GetDueRecurringDepositsQueryHandler : IRequestHandler<GetDueRecurringDepositsQuery, List<RecurringDepositDto>>
        {
            private readonly IAppDbContext _context;

            public GetDueRecurringDepositsQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<RecurringDepositDto>> Handle(GetDueRecurringDepositsQuery request, CancellationToken cancellationToken)
            {
                var now = DateTime.UtcNow;

                return await _context.RecurringDeposits
                    .Where(r => r.NextDueDate <= now)
                    .Select(r => new RecurringDepositDto
                    {
                        Id = r.Id,
                        UserId = r.UserId,
                        GoalId = r.GoalId,
                        Amount = r.Amount,
                        NextDueDate = r.NextDueDate
                    })
                    .ToListAsync(cancellationToken);
            }
        }
    }

}
