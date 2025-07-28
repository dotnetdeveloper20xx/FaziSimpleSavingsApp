using FaziSimpleSavings.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.RecurringDeposits.Queries.GetOverdueRecurringDeposits;

public record GetOverdueRecurringDepositsQuery(DateTime NowUtc) : IRequest<List<OverdueDepositDto>>;
