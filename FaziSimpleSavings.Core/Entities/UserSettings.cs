
namespace FaziSimpleSavings.Core.Entities;

public class UserSettings
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Currency { get; private set; }
    public bool ReceiveEmailNotifications { get; private set; }

    public UserSettings(Guid userId, string currency, bool receiveEmailNotifications)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty.");
        if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency cannot be empty.");

        UserId = userId;
        Currency = currency;
        ReceiveEmailNotifications = receiveEmailNotifications;
    }

    public void Update(string currency, bool receiveEmailNotifications)
    {
        if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency cannot be empty.");

        Currency = currency;
        ReceiveEmailNotifications = receiveEmailNotifications;
    }
}
