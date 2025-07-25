#nullable enable

namespace FaziSimpleSavings.Core.Entities;

public class GoalCategory
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public GoalCategory(Guid userId, string name, string description)
    {
        UserId = userId != Guid.Empty ? userId : throw new ArgumentException("User ID cannot be empty.");
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name cannot be empty.");
        Description = description ?? string.Empty;
    }
}
