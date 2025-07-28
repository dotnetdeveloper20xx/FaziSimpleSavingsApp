using MediatR;
using System;

namespace Application.UserSettings.Queries.GetUserSettings;

public record GetUserSettingsQuery(Guid UserId) : IRequest<UserSettingsDto>;
