using FluentValidation;

namespace Application.UserSettings.Commands.UpdateUserSettings;

public class UpdateUserSettingsCommandValidator : AbstractValidator<UpdateUserSettingsCommand>
{
    private static readonly string[] SupportedCurrencies = { "GBP", "USD", "EUR" };

    public UpdateUserSettingsCommandValidator()
    {
        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .Must(c => SupportedCurrencies.Contains(c))
            .WithMessage("Unsupported currency. Allowed: GBP, USD, EUR.");

        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty).WithMessage("User ID is required.");
    }
}
