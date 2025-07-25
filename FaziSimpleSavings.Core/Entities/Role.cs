public class Role
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public Role(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Role name cannot be empty.");
        Name = name;
    }
}
