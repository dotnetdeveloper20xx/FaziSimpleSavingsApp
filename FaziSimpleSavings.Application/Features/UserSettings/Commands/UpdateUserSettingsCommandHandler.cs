using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserSettings.Commands.UpdateUserSettings;

public class UpdateUserSettingsCommandHandler : IRequestHandler<UpdateUserSettingsCommand>
{
    private readonly IAppDbContext _context;

    public UpdateUserSettingsCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
    {
        var settings = await _context.UserSettings
            .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        if (settings == null)
        {
            // Optional: you can choose to throw or create new settings as below
            settings = new FaziSimpleSavings.Core.Entities.UserSettings(request.UserId, request.Currency, request.ReceiveEmailNotifications);
            _context.UserSettings.Add(settings);
        }
        else
        {
            settings.Update(request.Currency, request.ReceiveEmailNotifications);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    Task IRequestHandler<UpdateUserSettingsCommand>.Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
