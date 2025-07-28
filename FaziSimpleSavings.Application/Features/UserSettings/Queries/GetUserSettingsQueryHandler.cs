
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserSettings.Queries.GetUserSettings;

public class GetUserSettingsQueryHandler : IRequestHandler<GetUserSettingsQuery, UserSettingsDto>
{
    private readonly IAppDbContext _context;

    public GetUserSettingsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<UserSettingsDto> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
    {
        var settings = await _context.UserSettings
            .FirstOrDefaultAsync(us => us.UserId == request.UserId, cancellationToken);

        if (settings == null)
        {
            // Optional: return default settings if not found
            return new UserSettingsDto
            {
                Currency = "GBP",
                ReceiveEmailNotifications = true
            };
        }

        return new UserSettingsDto
        {
            Currency = settings.Currency,
            ReceiveEmailNotifications = settings.ReceiveEmailNotifications
        };
    }
}
