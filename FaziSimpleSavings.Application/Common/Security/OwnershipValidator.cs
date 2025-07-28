
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Security;

public class OwnershipValidator : IOwnershipValidator
{
    private readonly IAppDbContext _context;

    public OwnershipValidator(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> UserOwnsGoal(Guid userId, Guid goalId)
    {
        return await _context.SavingsGoals
            .AnyAsync(g => g.Id == goalId && g.UserId == userId);
    }

    public async Task<bool> UserOwnsNotification(Guid userId, Guid notificationId)
    {
        return await _context.Notifications
            .AnyAsync(n => n.Id == notificationId && n.UserId == userId);
    }
}
