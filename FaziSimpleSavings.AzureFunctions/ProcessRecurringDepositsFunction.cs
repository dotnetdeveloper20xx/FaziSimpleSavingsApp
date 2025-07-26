using System;
using System.Threading.Tasks;
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during recurring deposit processing.");
            }

            _logger.LogInformation("Function execution completed.");
        }
    }
}
