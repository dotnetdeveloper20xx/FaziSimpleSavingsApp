#nullable enable

namespace FaziSimpleSavings.Core.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    public User(string firstName, string lastName, string email)
    {
        FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : throw new ArgumentException("First name cannot be empty.");
        LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : throw new ArgumentException("Last name cannot be empty.");
        Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentException("Email cannot be empty.");
    }

    public void UpdateDetails(string firstName, string lastName, string email)
    {
        FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : throw new ArgumentException("First name cannot be empty.");
        LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : throw new ArgumentException("Last name cannot be empty.");
        Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentException("Email cannot be empty.");
        UpdatedAt = DateTime.UtcNow;
    }
}
