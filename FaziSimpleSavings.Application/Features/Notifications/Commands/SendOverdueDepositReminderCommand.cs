using MediatR;
using System;

namespace Application.Notifications.Commands.SendOverdueDepositReminder;

public record SendOverdueDepositReminderCommand(Guid UserId, string GoalName, decimal Amount) : IRequest<Guid>;
