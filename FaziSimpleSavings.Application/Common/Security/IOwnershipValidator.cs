using System;
using System.Threading.Tasks;

namespace Application.Common.Security;

public interface IOwnershipValidator
{
    Task<bool> UserOwnsGoal(Guid userId, Guid goalId);
    Task<bool> UserOwnsNotification(Guid userId, Guid notificationId);
}
