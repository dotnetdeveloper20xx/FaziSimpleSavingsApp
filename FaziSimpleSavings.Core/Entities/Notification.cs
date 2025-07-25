public class Notification
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Message { get; private set; }
    public DateTime NotificationDate { get; private set; }
    public bool IsRead { get; private set; }

    public Notification(Guid userId, string message)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty.");
        if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Message cannot be empty.");

        UserId = userId;
        Message = message;
        NotificationDate = DateTime.UtcNow;
        IsRead = false;
    }

    // Mark notification as read
    public void MarkAsRead()
    {
        IsRead = true;
    }
}
