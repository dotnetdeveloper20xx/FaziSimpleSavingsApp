namespace FaziSimpleSavings.Application.GroupGoals.Queries.GetUserGroupGoals
{
    public class GroupGoalDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal TargetAmount { get; set; }
        public decimal TotalSaved { get; set; }
        public decimal MyContribution { get; set; }
    }
}
