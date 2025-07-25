#nullable enable

namespace FaziSimpleSavings.Core.Entities;

public class Notification
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; }
    public string Message { get; private set; }
    public DateTime NotificationDate { get; private set; } = DateTime.UtcNow;
    public bool IsRead { get; private set; } = false;

    public Notification(Guid userId, string message)
    {
        UserId = userId != Guid.Empty ? userId : throw new ArgumentException("User ID cannot be empty.");
        Message = !string.IsNullOrWhiteSpace(message) ? message : throw new ArgumentException("Message cannot be empty.");
    }

    public void MarkAsRead() => IsRead = true;
}
