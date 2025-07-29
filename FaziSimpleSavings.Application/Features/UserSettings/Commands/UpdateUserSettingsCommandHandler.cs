using Application.Interfaces;
using FaziSimpleSavings.Application.Notifications.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserSettings.Commands.UpdateUserSettings;

public class UpdateUserSettingsCommandHandler : IRequestHandler<UpdateUserSettingsCommand>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public UpdateUserSettingsCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
       
    public async Task<Unit> Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
    {
        var settings = await _context.UserSettings
            .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        if (settings == null)
        {
            settings = new FaziSimpleSavings.Core.Entities.UserSettings(
                request.UserId,
                request.Currency,
                request.ReceiveEmailNotifications);

            _context.UserSettings.Add(settings);
        }
        else
        {
            settings.Update(request.Currency, request.ReceiveEmailNotifications);
        }

        await _context.SaveChangesAsync(cancellationToken);

        // Send notification to confirm update
        var message = "Your user settings have been updated successfully.";
        await _mediator.Send(new CreateNotificationCommand(request.UserId, message), cancellationToken);

        return Unit.Value;
    }

    Task IRequestHandler<UpdateUserSettingsCommand>.Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

