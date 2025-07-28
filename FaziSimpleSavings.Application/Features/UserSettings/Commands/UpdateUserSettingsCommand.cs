using MediatR;
using System;

namespace Application.UserSettings.Commands.UpdateUserSettings;

//public record UpdateUserSettingsCommand(Guid UserId, string Currency, bool ReceiveEmailNotifications) : IRequest<bool>;
public record UpdateUserSettingsCommand(Guid UserId, string Currency, bool ReceiveEmailNotifications) : IRequest;

