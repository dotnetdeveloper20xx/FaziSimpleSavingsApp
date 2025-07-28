using System;
using System.Threading.Tasks;
using Application.Notifications.Commands.SendOverdueDepositReminder;
using Application.RecurringDeposits.Queries.GetOverdueRecurringDeposits;
using FaziSimpleSavings.Application.RecurringDeposits.Commands;
using FaziSimpleSavings.Application.RecurringDeposits.Queries;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FaziSimpleSavings.AzureFunctions
{
    public class ProcessRecurringDepositsFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProcessRecurringDepositsFunction> _logger;

        public ProcessRecurringDepositsFunction(IMediator mediator, ILogger<ProcessRecurringDepositsFunction> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Function("ProcessRecurringDeposits")]
        public async Task Run([TimerTrigger("0 0 * * * *")] TimerInfo timer)
        {
            _logger.LogInformation($"Recurring deposit function started at: {DateTime.UtcNow}");

            try
            {
                // Step 1: Execute due recurring deposits (existing feature)
                var deposits = await _mediator.Send(new GetDueRecurringDepositsQuery());

                foreach (var deposit in deposits)
                {
                    try
                    {
                        var success = await _mediator.Send(new ExecuteRecurringDepositCommand(deposit.Id));
                        _logger.LogInformation(success
                            ? $"Deposit {deposit.Id} executed."
                            : $"Deposit {deposit.Id} failed.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Failed to process deposit {deposit.Id}");
                    }
                }

                // Step 2: Send overdue deposit reminders (new feature)
                var now = DateTime.UtcNow;
                var overdueDeposits = await _mediator.Send(new GetOverdueRecurringDepositsQuery(now));

                foreach (var rd in overdueDeposits)
                {
                    try
                    {
                        await _mediator.Send(new SendOverdueDepositReminderCommand(rd.UserId, rd.GoalName, rd.Amount));
                        _logger.LogInformation($"Overdue reminder sent for Goal '{rd.GoalName}' to user {rd.UserId}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Failed to send overdue reminder for deposit {rd.Id}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during recurring deposit processing.");
            }

            _logger.LogInformation("Function execution completed.");
        }
    }

}
