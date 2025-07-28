using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserSettings.Commands.UpdateUserSettings;

public class UpdateUserSettingsCommandHandler : IRequestHandler<UpdateUserSettingsCommand, bool>
{
    private readonly IAppDbContext _context;

    public UpdateUserSettingsCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
    {
        var settings = await _context.UserSettings
            .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        if (settings == null)
        {
            settings = new FaziSimpleSavings.Core.Entities.UserSettings(request.UserId, request.Currency, request.ReceiveEmailNotifications);
            _context.UserSettings.Add(settings);
        }
        else
        {
            settings.Update(request.Currency, request.ReceiveEmailNotifications);
        }

        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
