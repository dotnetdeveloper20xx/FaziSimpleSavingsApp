public class GoalCategory
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public GoalCategory(Guid userId, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.");
        if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty.");

        UserId = userId;
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }
}
