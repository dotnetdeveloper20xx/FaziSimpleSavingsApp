namespace FaziSimpleSavings.Application.GroupGoals.Queries.GetAvailableUsersForGroupGoal
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
